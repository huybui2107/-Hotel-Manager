



using System.ComponentModel.DataAnnotations;
using HotelApp.API.Database.Entities;

namespace HotelApp.API.Databases.Entities
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Hotel_name { get; set; } = null!;

        public int Room_quantity { get; set; }


        public string Hotel_description { get; set; } = null!;
        [StringLength(255)]
        public string Hotel_address { get; set; } = null!;
        [StringLength(255)]
        public string Hotel_phone { get; set; } = null!;

        [StringLength(255)]
        public string Hotel_email { get; set; } = null!;
        [StringLength(255)]
        public string? Image { get; set; } = null!;

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; } = null!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<HotelRoom> HotelRooms { get; set; } = new HashSet<HotelRoom>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}