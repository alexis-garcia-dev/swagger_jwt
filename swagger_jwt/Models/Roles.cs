using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace swagger_jwt.Models
{
    public class Roles
    {
        [JsonIgnore]
        [Key]
        public int RolesId { get; set; }

        [Required]

        public string Nombre { get; set; }

        public bool Estado { get; set; } = true;


    }
}
