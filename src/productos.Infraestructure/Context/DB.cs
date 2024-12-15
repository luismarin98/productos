using System;
using Microsoft.EntityFrameworkCore;
using modelo_canonico.Models;

namespace productos.Infraestructure.Context;

public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options) { }
    public DbSet<ClienteModel> cliente => Set<ClienteModel>();
    public DbSet<ProductoModel> producto => Set<ProductoModel>();
}
