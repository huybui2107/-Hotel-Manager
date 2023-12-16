

using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Databases.Entities
{
    public class Province
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Hotel> Hotels { get; set; } = new HashSet<Hotel>();

    }
}