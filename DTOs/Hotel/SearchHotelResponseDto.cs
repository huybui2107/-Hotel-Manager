namespace HotelApp.API.DTOs.Hotel
{
    public class SearchHotelResponseDto
    {
        public List<ResponseHotelDto> Hotels { get; set; } = null!;
        public int? Limit { get; set; }
        public int TotalPage { get; set; }
        public int Page { get; set; }
    }
}