

using HotelApp.API.Database.Entities;

namespace HotelApp.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}