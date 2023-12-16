using HotelApp.API.DTOs.HotelRoom;

namespace HotelApp.API.DTOs.Hotel
{
    public class SearchHotelRoomResDto
    {
        public List<HotelRoomResDto> Rooms { get; set; } = null!;
        public int? Limit { get; set; }
        public int TotalPage { get; set; }
        public int Page { get; set; }
    }
}