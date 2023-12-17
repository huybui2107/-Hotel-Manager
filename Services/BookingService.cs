



using AutoMapper;
using HotelApp.API.Database;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Booking;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.API.Services
{
    public class BookingService : IBooking
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int countItemByMonth(int hotelId, int month)
        {
            var count = _context.Bookings
                            .Where(booking => booking.HotelId == hotelId)
                            .Where(b => b.Checkin.Month == month)
                            .Distinct()
                            .Count();

            return count;
        }

        public int countMaxItemByMonth(int hotelId, int status, int month)
        {
            var count = _context.Bookings
                            .Where(booking => booking.HotelId == hotelId)
                            .Where(b => b.Status == status)
                            .Where(b => b.Checkin.Month == month)
                            .Distinct()
                            .Count();

            return count;
        }

        public BookingResDto createBooking(BookingRequestDto bookingRequestDto)
        {
            var room = _context.HotelRooms.FirstOrDefault(u => u.Id == bookingRequestDto.HotelRoomId);
            if (room is null) throw new ArgumentException("Room not found");
            var booking = _mapper.Map<Booking>(bookingRequestDto);

            var timeDiff = (bookingRequestDto.Checkout - bookingRequestDto.Checkin).TotalMilliseconds;
            var numberOfNights = Math.Ceiling(timeDiff / (1000 * 3600 * 24));

            booking.Status = 0;
            booking.TotalPrice = (int)(numberOfNights * room.Price);
            DateTime deadline = bookingRequestDto.Checkout.AddMilliseconds(1000 * 60 * 60 * 24);
            booking.Deadline = deadline;

            _context.Add(booking);
            _context.SaveChanges();
            return _mapper.Map<BookingResDto>(booking);
        }

        public SearchBookingByHotelDto getBookingByHotel(int hotelId, BookingFilterOption option)
        {

            var total = _context.Bookings
                        .Include(booking => booking.User)
                        .Where(booking => booking.HotelId == hotelId)
                        .Where(booking => booking.Status == option.Status).Count();

            var bookings = _context.Bookings
                                    .Include(booking => booking.User)
                                    .Where(booking => booking.HotelId == hotelId)
                                    .Where(booking => booking.Status == option.Status)
                                    .OrderBy(booking => booking.Checkin)
                                    .Skip((option.CurrentPage - 1) * option.PageSize)
                                    .Take(option.PageSize)
                                    .ToList();
            var res = new SearchBookingByHotelDto
            {
                Bookings = bookings.Select(booking => _mapper.Map<BookingHotelDto>(booking)).ToList(),
                Limit = option.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / option.PageSize),
                Page = option.CurrentPage
            };
            return res;

        }

        public SearchBookingByUserDto getBookingByUserId(int id, BookingFilterOption option)
        {
            var totalBooking = _context.Bookings
                                .Include(booking => booking.HotelRoom)
                                .Include(booking => booking.Hotel)
                                .Where(booking => booking.UserId == id).Count();
            var bookings = _context.Bookings
                                .Include(booking => booking.HotelRoom)
                                .Include(booking => booking.Hotel)
                                .Where(booking => booking.UserId == id)
                                .Skip((option.CurrentPage - 1) * option.PageSize)
                                .Take(option.PageSize)
                                .ToList();
            var res = new SearchBookingByUserDto
            {
                Bookings = bookings.Select(booking => _mapper.Map<BookingUserDto>(booking)).ToList(),
                Limit = option.PageSize,
                TotalPage = (int)Math.Ceiling((double)totalBooking / option.PageSize),
                Page = option.CurrentPage
            };

            return res;
        }

        public Booking getById(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(u => u.Id == id);
            if (booking is null) throw new ArgumentException("booking not found");
            return booking;
        }

        public RevenueOfMonthDto GetRevenue(RevenueOptions options)
        {
            List<RevenueOfDayDto> Days = new List<RevenueOfDayDto>();
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            int totalPrice = 0;
            int countDay = DateTimeExtension.CountDay(options.Month, currentYear);
            for (var i = 1; i <= countDay; i++)
            {
                var dateTime = new DateTime(currentYear, options.Month, i);
                Console.WriteLine(dateTime);
                var Price = getTotalOfDay(options.HotelId, dateTime);
                totalPrice += Price;
                var day = new RevenueOfDayDto
                {
                    Day = currentYear + "-" + options.Month + "-" + i,
                    TotalPrice = Price
                };
                Days.Add(day);
            }
            var res = new RevenueOfMonthDto
            {
                Days = Days,
                TotalPrice = totalPrice,
                Paid = countMaxItemByMonth(options.HotelId, 3, options.Month),
                UnPaid = countMaxItemByMonth(options.HotelId, 4, options.Month),
                Tickets = countItemByMonth(options.HotelId, options.Month)
            };
            return res;
        }

        public int getTotalOfDay(int HotelId, DateTime day)
        {
            var total = _context.Bookings
                            .Where(booking => booking.HotelId == HotelId)
                            .Where(booking => booking.Status == 3)
                            .Where(booking => booking.Checkin == day)
                            .Sum(booking => booking.TotalPrice);
            return total ?? 0;
        }

        public BookingResDto updateStatus(int bookingId)
        {
            var booking = getById(bookingId);
            if (booking.Status < 4)
            {
                booking.Status = booking.Status + 1;
            }
            _context.Update(booking);
            _context.SaveChanges();

            return _mapper.Map<BookingResDto>(booking);
        }
    }
}