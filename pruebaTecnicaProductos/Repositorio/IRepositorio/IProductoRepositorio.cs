using pruebaTecnicaProductos.Modelos;

namespace pruebaTecnicaProductos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio
    {
        ICollection<Producto> GetProductos();

        int GetTotalProductos();


        IEnumerable<Producto> BuscarProducto(string nombre);
        Producto GetProducto(int productoId);
        bool Existeproducto(int id);

        bool ExisteProducto(string nombre);

        bool CrearProducto(Producto producto);

        bool ActualizarProducto(Producto producto);
        bool Borrarproducto(Producto producto);
        bool Guardar();
    }
}
