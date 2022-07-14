﻿using System.Collections.Generic;

namespace swagger_jwt.Models
{
        public class Bodega
        {
            public int BodegaId { get; set; }
            public string Nombre { get; set; }
            public bool Estado { get; set; }
            public ICollection<Producto> productos { get; set; }
            public ICollection<Usuario> usuarios { get; set; }
        }
    }
}