



using System.ComponentModel.DataAnnotations;
using HotelApp.API.Database.Entities;

namespace HotelApp.API.Databases.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}