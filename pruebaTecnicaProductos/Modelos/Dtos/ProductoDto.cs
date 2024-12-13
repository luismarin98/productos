using System.ComponentModel.DataAnnotations;

namespace pruebaTecnicaProductos.Modelos.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public decimal precio { get; set; }

        public string? nombre { get; set; }

        public string? descripcion { get; set; }

        public bool estado { get; set; }

        public int stock { get; set; }

        public string? imagen { get; set; }

        public string? datosAuditoria { get; set; }
    }
}
