# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["pruebaTecnicaProductos/nuget.config", "."]
COPY ["pruebaTecnicaProductos/pruebaTecnicaProductos.csproj", "pruebaTecnicaProductos/"]
RUN dotnet restore "./pruebaTecnicaProductos/pruebaTecnicaProductos.csproj"
COPY . .
WORKDIR "/src/pruebaTecnicaProductos"
RUN dotnet build "pruebaTecnicaProductos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pruebaTecnicaProductos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
RUN apk update && apk add --no-cache icu-libs
RUN apk add --no-cache openssl jq

# Limpia la caché de apk
RUN rm -rf /var/cache/apk/*

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=es_ES.UTF-8

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pruebaTecnicaProductos.dll"]
