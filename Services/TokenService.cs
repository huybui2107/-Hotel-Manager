
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelApp.API.Database.Entities;
using Microsoft.IdentityModel.Tokens;



namespace HotelApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecretKey;

        public TokenService(IConfiguration configuration)
        {
            _jwtSecretKey = configuration["JwtSecretKey"] ?? string.Empty;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email),
            };
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}