



using System.ComponentModel.DataAnnotations;
using HotelApp.API.Database.Entities;

namespace HotelApp.API.Databases.Entities
{
    public class HotelRoom
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int Bed_quantity { get; set; }

        public int Price { get; set; }

        public string Description { get; set; } = null!;

        public string Image { get; set; } = null!;

        public int TypeRoomId { get; set; }
        public int HotelId { get; set; }
        public virtual TypeRoom TypeRoom { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}