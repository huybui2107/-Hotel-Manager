



using System.ComponentModel.DataAnnotations;

namespace Namespace
{
    public class ProfileDto
    {
        [MaxLength(255)]
        public string FirstName { get; set; } = null!;
        [MaxLength(255)]
        public string LastName { get; set; } = null!;


        [MaxLength(100)]
        public string Username { get; set; } = null!;

        public DateTime Birthday { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; } = null!;

        [MaxLength(500)]
        public string? Image { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; } = null!;
    }
}