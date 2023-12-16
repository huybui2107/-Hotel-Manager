



using HotelApp.API.Database.Entities;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.User;
using Namespace;

namespace HotelApp.API.Services
{
    public interface IUserService
    {
        User? GetUserByEmail(string email);
        User? GetUserByUsername(string name);
        void CreateUser(User user);
        ProfileDto? GetInfor(string email);
        void UpdateUser(string email, ProfileDto profileDto);
        void ChangePassWord(ChangePassDto changePassDto, int Id);

        ResponseHotelDto? RegisterManage(RegisterHotelDto hotel, int userId);
    }
}