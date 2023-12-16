



using AutoMapper;
using HotelApp.API.Database;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Booking;
using HotelApp.API.DTOs.Hotel;
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

        public BookingResDto updateStatus(int bookingId)
        {
            var booking = getById(bookingId);
            if (booking.Status < 3)
            {
                booking.Status = booking.Status + 1;
            }
            _context.Update(booking);
            _context.SaveChanges();

            return _mapper.Map<BookingResDto>(booking);
        }
    }
}