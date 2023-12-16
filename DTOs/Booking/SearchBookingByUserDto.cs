

namespace HotelApp.API.DTOs.Booking
{
    public class SearchBookingByUserDto
    {
        public List<BookingUserDto> Bookings { get; set; } = null!;
        public int? Limit { get; set; }
        public int TotalPage { get; set; }
        public int Page { get; set; }
    }
}