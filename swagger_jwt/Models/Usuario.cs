using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }




    }
}
