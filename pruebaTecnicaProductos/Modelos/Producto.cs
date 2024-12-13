using System.ComponentModel.DataAnnotations;

namespace pruebaTecnicaProductos.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal precio { get; set; }

        [Required]
        public string? nombre { get; set; }

        public string? descripcion { get; set; }

        public bool estado { get; set; }

        public int stock { get; set; }

        public string? imagen { get; set; }

        public string? datosAuditoria {  get; set; }
    }
}
