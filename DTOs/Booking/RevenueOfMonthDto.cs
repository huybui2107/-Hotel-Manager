

namespace HotelApp.API.DTOs.Booking
{
    public class RevenueOfMonthDto
    {
        public int Tickets { get; set; }
        public int TotalPrice { get; set; }

        public List<RevenueOfDayDto> Days { get; set; } = null!;
        public int Paid { get; set; }
        public int UnPaid { get; set; }
    }
}