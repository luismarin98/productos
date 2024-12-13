using Microsoft.EntityFrameworkCore;
using pruebaTecnicaProductos.Data;
using pruebaTecnicaProductos.Modelos;
using pruebaTecnicaProductos.Repositorio.IRepositorio;

namespace pruebaTecnicaProductos.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {

        private readonly AplicationDbContext _dbContext;

        public ProductoRepositorio(AplicationDbContext db)
        {
            _dbContext = db;

        }

        public bool ActualizarProducto(Producto pelicula)
        {
            _dbContext.Productos.Update(pelicula);
            return Guardar();
        }

        public bool Borrarproducto(Producto pelicula)
        {
            _dbContext.Productos.Remove(pelicula);
            return Guardar();
        }

        public bool CrearProducto(Producto pelicula)
        {
            _dbContext.Productos.Add(pelicula);
            return Guardar();
        }

        public bool Existeproducto(int id)
        {
            return _dbContext.Productos.Any(p => p.Id == id);
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _dbContext.Productos.Any(p => p.nombre!.ToLower().Trim() == nombre.ToLower());
            return valor;
        }

        public ICollection<Producto> GetProductos()
        {

            //añadir paginacion a la lista de peliculas
            return _dbContext.Productos.OrderBy(p => p.nombre).ToList();
        }



        public Producto GetProducto(int id)
        {
            return _dbContext.Productos.FirstOrDefault(p => p.Id == id)!;
        }


        public IEnumerable<Producto> BuscarPelicula(string nombre)
        {
            IQueryable<Producto> query = _dbContext.Productos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.nombre!.Contains(nombre) || e.descripcion!.Contains(nombre));
            }

            return query.ToList();
        }

        public bool Guardar()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public int GetTotalPeliculas()
        {
            return _dbContext.Productos.Count();
        }

        public ICollection<Producto> GetProducto(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int GetTotalProductos()
        {
            throw new NotImplementedException();
        }

        public ICollection<Producto> GetPeliculasEnCategoria(int catId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> BuscarProducto(string nombre)
        {
            throw new NotImplementedException();
        }

    }
}
