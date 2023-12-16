


using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Services.Auth
{
    public class LoginRequestDto
    {

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;
    }
}