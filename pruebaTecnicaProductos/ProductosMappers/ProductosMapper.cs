using AutoMapper;
using pruebaTecnicaProductos.Modelos;
using pruebaTecnicaProductos.Modelos.Dtos;

namespace pruebaTecnicaProductos.ProductosMappers
{
    public class ProductosMapper:Profile
    {
        public ProductosMapper()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();
            CreateMap<Producto, ActualizarProductoDto>().ReverseMap();
        }
       
       
    }
}
