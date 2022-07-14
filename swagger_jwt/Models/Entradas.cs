using System;
using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
    public class Entradas
    {

        [Key]
        public int EntradaId { get; set; }
        public int ProductoId { get; set; }

        public int BodegaId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaEntrada { get; set; }

        public float Cantidad { get; set; }

        public float EntradaTotal { get; set; }

        public Producto producto { get; set; }
        public Bodega bodega { get; set; }
        public Usuario usuario { get; set; }
 
    }
}
