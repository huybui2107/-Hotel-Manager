namespace HotelApp.API.DTOs.Hotel
{
    public class HotelFilterOption
    {

        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }
        public int ProvinceId { get; set; }
        public int TypeRoomId { get; set; }
        public int Bed_quantity { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}