using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace swagger_jwt.DTO
{
    public class CreateUsuario
    {
        [ForeignKey("RolesId")]
        public int RolesId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public bool Estado { get; set; }

    }
}
