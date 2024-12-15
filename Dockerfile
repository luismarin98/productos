# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copiar los archivos .csproj de los proyectos
COPY ["src/productos.Api/productos.Api.csproj", "src/productos.Api/"]
COPY ["src/productos.Application/productos.Application.csproj", "src/productos.Application/"]
COPY ["src/productos.Domain/productos.Domain.csproj", "src/productos.Domain/"]
COPY ["src/productos.Infraestructure/productos.Infraestructure.csproj", "src/productos.Infraestructure/"]

# Restaurar las dependencias
RUN dotnet restore "src/productos.Api/productos.Api.csproj"

# Copiar todos los archivos del proyecto
COPY . .

# Construir la solución
RUN dotnet build "src/productos.Api/productos.Api.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "src/productos.Api/productos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa base (para ejecutar la aplicación)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Actualizar OpenSSL y las dependencias necesarias para soportar TLS 1.2
RUN apk add --no-cache icu-libs openssl libssl3
RUN apk update && apk upgrade busybox

# Establecer variables de entorno
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=es_ES.UTF-8

# Etapa final (donde se ejecuta la aplicación)
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=publish /app/publish .

# Definir el punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "productos.Api.dll"]
