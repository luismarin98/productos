using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using pruebaTecnicaProductos.Modelos;

namespace pruebaTecnicaProductos.Data
{
    public class AplicationDbContext:DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                    {
                        dbCreator.Create();
                    }
                    if (!dbCreator.HasTables())
                    {
                        dbCreator.CreateTables();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
