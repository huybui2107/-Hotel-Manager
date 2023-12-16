

namespace HotelApp.API.DTOs.Booking
{
    public class SearchBookingByHotelDto
    {
        public List<BookingHotelDto> Bookings { get; set; } = null!;
        public int? Limit { get; set; }
        public int TotalPage { get; set; }
        public int Page { get; set; }
    }
}