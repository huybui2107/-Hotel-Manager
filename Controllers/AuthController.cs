using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DatingApp.API.DTOs.Auth;
using DatingApp.API.DTOs.User;
using HotelApp.API.Database.Entities;
using HotelApp.API.Services;
using HotelApp.API.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Namespace;

namespace HotelApp.API.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            IUserService userService,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto registerUser)
        {
            registerUser.Username = registerUser.Username.ToLower();
            if (_userService.GetUserByUsername(registerUser.Username) is not null)
                return BadRequest("Username already registered");
            if (_userService.GetUserByEmail(registerUser.Email) is not null)
                return BadRequest("Username already registered");

            using var hashFunc = new HMACSHA256();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUser.Password);

            var newUser = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Username = registerUser.Username,
                Email = registerUser.Email,
                Birthday = registerUser.Birthday,
                PasswordHash = hashFunc.ComputeHash(passwordBytes),
                PasswordSalt = hashFunc.Key,
                RoleId = 1
            };
            _userService.CreateUser(newUser);
            var res = new AuthReponseDto
            {
                Token = _tokenService.GenerateToken(newUser),
                userDto = _mapper.Map<UserDto>(newUser)
            };
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginUser)
        {
            
            var existedUser = _userService.GetUserByEmail(loginUser.Email);
            if (existedUser is null) return Unauthorized("User not found");
            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                    return Unauthorized("Password not match");
            }
            var res = new AuthReponseDto
            {
                Token = _tokenService.GenerateToken(existedUser),
                userDto = _mapper.Map<UserDto>(existedUser)
            };
            return Ok(res);

        }
    }
}