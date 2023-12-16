





using AutoMapper;
using HotelApp.API.Database;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.API.Services
{
    public class HotelService : IHotelService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HotelService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Hotel? getHotelById(int id)
        {
            return _context.Hotels.FirstOrDefault(hotel => hotel.Id == id);
        }

        public SearchHotelResponseDto searchHotel(HotelFilterOption hotelFilter)
        {
            var total = _context.Hotels
                .Include(hotel => hotel.HotelRooms)
                .Include(hotel => hotel.Bookings)
                .Include(hotel => hotel.Province)
                .Where(hotel => hotel.HotelRooms.Any(
                    room => room.TypeRoomId == hotelFilter.TypeRoomId && room.Bed_quantity == hotelFilter.Bed_quantity))
            .Where(hotel => hotel.ProvinceId == hotelFilter.ProvinceId)
            .Where(hotel => hotel.Bookings.Any(booking =>
             (booking.Checkin > hotelFilter.Checkin && booking.Checkin > hotelFilter.Checkout) || (booking.Checkout < hotelFilter.Checkin && booking.Checkin < hotelFilter.Checkout))).Count();
            var result = _context.Hotels
                .Include(hotel => hotel.HotelRooms)
                .Include(hotel => hotel.Bookings)
                .Include(hotel => hotel.Province)
                .Where(hotel => hotel.HotelRooms.Any(
                    room => room.TypeRoomId == hotelFilter.TypeRoomId && room.Bed_quantity == hotelFilter.Bed_quantity))
                .Where(hotel => hotel.ProvinceId == hotelFilter.ProvinceId)
                .Where(hotel => hotel.Bookings.Any(booking =>
                 (booking.Checkin > hotelFilter.Checkin && booking.Checkin > hotelFilter.Checkout) || (booking.Checkout < hotelFilter.Checkin && booking.Checkin < hotelFilter.Checkout)))
                .Skip((hotelFilter.CurrentPage - 1) * hotelFilter.PageSize)
                .Take(hotelFilter.PageSize)
                .ToList();

            var res = new SearchHotelResponseDto
            {
                Hotels = result.Select(hotel => _mapper.Map<ResponseHotelDto>(hotel)).ToList(),
                Limit = hotelFilter.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / hotelFilter.PageSize),
                Page = hotelFilter.CurrentPage
            };
            return res;
        }

        public ResponseHotelDto updateHotel(int id, RegisterHotelDto registerHotelDto)
        {
            var existHotel = getHotelById(id);
            if (existHotel is null) throw new ArgumentException("hotel not found");
            var updateHotel = _mapper.Map(registerHotelDto, existHotel);
            _context.Update(updateHotel);
            _context.SaveChanges();

            return _mapper.Map<ResponseHotelDto>(updateHotel);

        }
    }
}