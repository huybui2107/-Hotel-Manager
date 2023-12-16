using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [MaxLength(255)]
        public string FirstName { get; set; } = null!;
        [MaxLength(255)]
        public string LastName { get; set; } = null!;


        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [MaxLength(255)]
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }

        [MaxLength(100)]
        public string Password { get; set; } = null!;
    }

}