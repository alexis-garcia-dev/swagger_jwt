using System.ComponentModel.DataAnnotations;

namespace swagger_jwt.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
