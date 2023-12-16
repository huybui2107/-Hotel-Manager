

using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.DTOs.Booking
{
    public class BookingRequestDto
    {
        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }
        [StringLength(100)]
        public string? Fullname { get; set; } = null!;

        [StringLength(255)]
        public string? PhoneNumber { get; set; } = null!;
        [StringLength(100)]
        public string? Cccd { get; set; } = null!;
        public int HotelRoomId { get; set; }
        public int HotelId { get; set; }
        public int? UserId { get; set; }
    }
}