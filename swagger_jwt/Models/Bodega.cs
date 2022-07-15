using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
        public class Bodega
        {
            [Key]
            public int BodegaId { get; set; }
            public string Nombre { get; set; }
            public bool Estado { get; set; }
            
        }
    }

