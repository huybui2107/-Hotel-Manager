
using System.ComponentModel.DataAnnotations;
using HotelApp.API.Databases.Entities;


namespace HotelApp.API.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string FirstName { get; set; } = null!;
        [StringLength(255)]
        public string LastName { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;
        public DateTime Birthday { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; } = null!;

        [StringLength(500)]
        public string? Image { get; set; } = null!;

        [StringLength(20)]
        public string? PhoneNumber { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Hotel> Hotels { get; set; } = new HashSet<Hotel>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

    }
}