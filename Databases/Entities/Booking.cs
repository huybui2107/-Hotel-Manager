

using System.ComponentModel.DataAnnotations;
using HotelApp.API.Database.Entities;

namespace HotelApp.API.Databases.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Fullname { get; set; } = null!;

        [StringLength(255)]
        public string? PhoneNumber { get; set; } = null!;

        [StringLength(255)]
        public string? Cccd { get; set; } = null!;
        public int Status { get; set; } = 0;
        public int? TotalPrice { get; set; }
        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }

        public DateTime? Deadline { get; set; }


        public int UserId { get; set; }
        public int HotelId { get; set; }

        public int HotelRoomId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual HotelRoom HotelRoom { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
    }
}