




using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.HotelRoom;

namespace HotelApp.API.Services
{
    public interface IHotelRoomService
    {


        HotelRoomResDto? createHotelRoom(int id, HotelRoomRequestDto hotelRoomRequestDto);
        HotelRoomResDto? updateHotelRoom(int id, HotelRoomRequestDto hotelRoomRequestDto);
        HotelRoomResDto? SearchHotelRoom(int id);
        HotelRoom? getById(int id);

        SearchHotelRoomResDto? getRoomByHotelId(int hotelId, int currentPage, int limit);

        void deleteRoom(int id);
    }
}