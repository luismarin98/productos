using Microsoft.EntityFrameworkCore;
using pruebaTecnicaProductos.Data;
using pruebaTecnicaProductos.ProductosMappers;
using pruebaTecnicaProductos.Repositorio;
using pruebaTecnicaProductos.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//1
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AplicationDbContext>(opciones =>
opciones.UseSqlServer(connectionString));


// 2 ----------------------------------- Agregar los repositorios
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();


//3
builder.Services.AddAutoMapper(typeof(ProductosMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
