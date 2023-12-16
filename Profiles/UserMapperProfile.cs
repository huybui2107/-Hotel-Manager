using AutoMapper;
using DatingApp.API.DTOs.User;
using HotelApp.API.Database.Entities;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Booking;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.HotelRoom;
using Namespace;


namespace DatingApp.API.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, ProfileDto>();
            CreateMap<ProfileDto, User>();
            CreateMap<RegisterHotelDto, Hotel>();
            CreateMap<Hotel, RegisterHotelDto>();
            CreateMap<Hotel, ResponseHotelDto>();
            CreateMap<HotelRoomRequestDto, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomResDto>();
            CreateMap<BookingRequestDto, Booking>();
            CreateMap<Booking, BookingResDto>();
            CreateMap<Booking, BookingUserDto>()
                .ForMember(u => u.Hotelname,
                options => options.MapFrom(s => s.Hotel.Hotel_name))
                .ForMember(u => u.Address,
                options => options.MapFrom(s => s.Hotel.Hotel_address))
                .ForMember(u => u.RoomName,
                options => options.MapFrom(s => s.HotelRoom.Name))
                .ForMember(u => u.Hotelname,
                options => options.MapFrom(s => s.Hotel.Hotel_name))
                .ForMember(u => u.TotalPrice,
                options => options.MapFrom(s => s.TotalPrice));
            CreateMap<Booking, BookingHotelDto>()
                .ForMember(u => u.Username,
                options => options.MapFrom(s => s.Fullname))
                .ForMember(u => u.PhoneNumber,
                options => options.MapFrom(s => s.PhoneNumber));


        }
    }
}