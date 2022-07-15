using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace swagger_jwt.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria categoria { get; set; }
        public DateTime FechaCad { get; set; }
        public DateTime FechaProd { get; set; }
        public float Precio { get; set; }
        public bool Estado { get; set; } = true;

    }
}
