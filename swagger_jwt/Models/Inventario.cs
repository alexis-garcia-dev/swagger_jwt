using System.Collections.Generic;

namespace swagger_jwt.Models
{
    public class Inventario
    {
        public int InventarioId { get; set; }
        public int ProductoId { get; set; }
        public int UsuarioId { get; set; }
        public int BodegaId { get; set; }

        public int Cantidad { get; set; }
    }
}
