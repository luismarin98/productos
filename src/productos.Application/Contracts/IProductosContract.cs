using System;
using modelo_canonico.Types;

namespace productos.Application.Contracts;

public interface IProductosContract
{
    public Task<List<ProductoType>> GetAllProductos();
    public Task<ProductoType> GetProducto(int id);
    public Task<bool> CreateProducto(ProductoType productoType);
    public Task<bool> UpdateProducto(ProductoType productoType);
    public Task<bool> DeleteProducto(int id); 
}
