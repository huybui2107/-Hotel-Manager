



using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;

namespace HotelApp.API.Services
{
    public interface IHotelService
    {
        SearchHotelResponseDto searchHotel(HotelFilterOption hotelFilter);

        Hotel? getHotelById(int id);
        ResponseHotelDto updateHotel(int id, RegisterHotelDto registerHotelDto);
    }
}