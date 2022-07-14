using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
    public class Roles
    {
        [Key]
        public int RolesId { get; set; }

        [Required]

        public string Nombre { get; set; }

        public bool Estado { get; set; } = true;


    }
}
