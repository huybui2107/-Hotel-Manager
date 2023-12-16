



using System.ComponentModel.DataAnnotations;


namespace HotelApp.API.DTOs.HotelRoom
{
    public class HotelRoomRequestDto
    {

        [StringLength(100)]
        public string Name { get; set; } = null!;
        public int Bed_quantity { get; set; }

        public int Price { get; set; }
        public string? Description { get; set; } = null!;
        [StringLength(255)]
        public string Image { get; set; } = null!;

        public int TypeRoomId { get; set; }
        public int HotelId { get; set; }

        public int UserId { get; set; }

    }
}