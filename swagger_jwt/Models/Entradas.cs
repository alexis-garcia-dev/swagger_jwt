using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace swagger_jwt.Models
{
    public class Entradas
    {
        [JsonIgnore]
        [Key]
        public int EntradaId { get; set; }
        public int ProductoId { get; set; }

        public int BodegaId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaEntrada { get; set; }

        public int Cantidad { get; set; }
        [JsonIgnore]
        public bool estado { get; set; }
        /**
         para evitar que se envie al front se usa JsonIgnore
         */
        [JsonIgnore]
        public float EntradaTotal { get; set; }
        [JsonIgnore]
        public Producto producto { get; set; }
        [JsonIgnore]
        public Bodega bodega { get; set; }
        [JsonIgnore]
        public Usuario usuario { get; set; }
 
    }
}
