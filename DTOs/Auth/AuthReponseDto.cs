

using DatingApp.API.DTOs.User;

namespace Namespace
{
    public class AuthReponseDto
    {
        public string Token { get; set; } = null!;

        public UserDto userDto { get; set; } = null!;

    }
}