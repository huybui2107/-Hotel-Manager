
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using HotelApp.API.Database;
using HotelApp.API.Database.Entities;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.User;
using Namespace;

namespace HotelApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void ChangePassWord(ChangePassDto changePassDto, int Id)
        {
            var existedUser = FindUserById(Id);
            if (existedUser is null) throw new ArgumentException($"User not found.");
            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(changePassDto.OldPassword);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                    throw new ArgumentException("Password not match");
            }
            using var hashnewFunc = new HMACSHA256();
            var newpasswordBytes = Encoding.UTF8.GetBytes(changePassDto.NewPassword);
            existedUser.PasswordHash = hashnewFunc.ComputeHash(newpasswordBytes);
            existedUser.PasswordSalt = hashnewFunc.Key;
            _context.Users.Update(existedUser);
            _context.SaveChanges();
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public ProfileDto? GetInfor(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return _mapper.Map<ProfileDto>(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserByUsername(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Username == name);
        }
        public User? FindUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(string email, ProfileDto profileDto)
        {
            var user = GetUserByEmail(email);
            if (user is null) throw new ArgumentException($"User with email '{email}' not found.");
            var newUser = _mapper.Map(profileDto, user);
            _context.Users.Update(newUser);
            _context.SaveChanges();
        }

        public ResponseHotelDto RegisterManage(RegisterHotelDto hotelDto, int userId)
        {
            var existedUser = FindUserById(userId);
            if (existedUser is null) throw new ArgumentException($"User not found.");
            hotelDto.UserId = userId;
            var hotel = _mapper.Map<Hotel>(hotelDto);
            existedUser.RoleId = 2;


            _context.Hotels.Add(hotel);
            _context.Users.Update(existedUser);


            _context.SaveChanges();


            return _mapper.Map<ResponseHotelDto>(hotel);
        }
    }
}