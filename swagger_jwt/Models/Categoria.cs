
using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; } = true;



    }
}
