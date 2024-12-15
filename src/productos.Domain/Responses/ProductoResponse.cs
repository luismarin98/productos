using System;
using modelo_canonico.Types;

namespace productos.Domain.Responses;

public class ProductoResponse
{
    public int TotalRegistros { get; set; }
    public List<ProductoType>? Producto { get; set; }
}
