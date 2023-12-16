namespace HotelApp.API.DTOs.Hotel
{
    public class BookingFilterOption
    {
        public int? Status { get; set; } = 0;
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public string? Sort { get; set; } = null!;

        public string? Direction { get; set; } = "asc";
    }
}