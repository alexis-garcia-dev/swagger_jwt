using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace swagger_jwt.Models
{
    public class Salidas
    {
        [JsonIgnore]
        [Key]
        public int SalidaId { get; set; }
        public int ProductoId { get; set; }

        public int BodegaId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaSalida { get; set; }

        public int Cantidad { get; set; }
        [JsonIgnore]
        public double VentaTotal { get; set; }
        [JsonIgnore]
        public Producto producto { get; set; }
        [JsonIgnore]
        public Bodega bodega { get; set; }
        [JsonIgnore]
        public Usuario usuario { get; set; }
        

    }
}
