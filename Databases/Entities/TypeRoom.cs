
using System.ComponentModel.DataAnnotations;
using HotelApp.API.Databases.Entities;


namespace HotelApp.API.Database.Entities
{
    public class TypeRoom
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;
        public virtual ICollection<HotelRoom> HotelRooms { get; set; } = new HashSet<HotelRoom>();




    }
}