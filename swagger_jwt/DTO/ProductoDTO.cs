using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace swagger_jwt.DTO
{
    public class ProductoDTO
    {
        public string Nombre { get; set; }
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
 

        public DateTime FechaCad { get; set; }
        public DateTime FechaProd { get; set; }
        public float Precio { get; set; }

        public bool Estado { get; set; } = true;
    }
}
