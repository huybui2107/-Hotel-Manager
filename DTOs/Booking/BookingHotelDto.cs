

using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.DTOs.Booking
{
    public class BookingHotelDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? Username { get; set; } = null!;

        [StringLength(255)]
        public string? PhoneNumber { get; set; } = null!;
        public int? TotalPrice { get; set; }
        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }

    }
}