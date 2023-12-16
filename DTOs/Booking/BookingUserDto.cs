

using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.DTOs.Booking
{
    public class BookingUserDto
    {

        [StringLength(100)]
        public string? Hotelname { get; set; } = null!;

        [StringLength(255)]
        public string? Address { get; set; } = null!;

        [StringLength(255)]
        public string? RoomName { get; set; } = null!;
        public int? TotalPrice { get; set; }
        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }

    }
}