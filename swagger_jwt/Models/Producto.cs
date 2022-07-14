using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public Categoria CategoriaId { get; set; }
        public DateTime FechaCad { get; set; }
        public DateTime FechaProd { get; set; }
        public int Precio { get; set; }

    }
}
