



using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.DTOs.Hotel
{
    public class ResponseHotelDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Hotel_name { get; set; } = null!;

        [StringLength(255)]
        public string? Hotel_description { get; set; } = null!;
        [StringLength(255)]
        public string Hotel_address { get; set; } = null!;
        [StringLength(255)]
        public string Hotel_phone { get; set; } = null!;

        [StringLength(255)]
        public string Hotel_email { get; set; } = null!;
        [StringLength(255)]
        public string Image { get; set; } = null!;

        public int ProvinceId { get; set; }

        public int UserId { get; set; }

    }
}