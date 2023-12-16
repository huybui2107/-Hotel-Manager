





using AutoMapper;
using HotelApp.API.Database;
using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.HotelRoom;

namespace HotelApp.API.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HotelRoomService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public HotelRoomResDto? createHotelRoom(int id, HotelRoomRequestDto hotelRoomRequestDto)
        {
            hotelRoomRequestDto.HotelId = id;
            var newRoom = _mapper.Map<HotelRoom>(hotelRoomRequestDto);
            _context.Add(newRoom);
            _context.SaveChanges();

            return _mapper.Map<HotelRoomResDto>(newRoom);
        }

        public void deleteRoom(int id)
        {

            var room = getById(id);
            if (room is null) throw new ArgumentException("The requested resource was not found.");
            _context.Remove(room);
            _context.SaveChanges();
        }

        public HotelRoom? getById(int id)
        {
            return _context.HotelRooms.FirstOrDefault(u => u.Id == id);
        }

        public SearchHotelRoomResDto? getRoomByHotelId(int hotelId, int currentPage, int limit)
        {
            var TotalItem = _context.HotelRooms.Where(room => room.HotelId == hotelId).Count();
            var rooms = _context.HotelRooms
                .Where(room => room.HotelId == hotelId)
                .Skip((currentPage - 1) * limit)
                .Take(limit)
                .ToList();
            var res = new SearchHotelRoomResDto
            {
                Rooms = rooms.Select(room => _mapper.Map<HotelRoomResDto>(room)).ToList(),
                Limit = limit,
                TotalPage = (int)Math.Ceiling((double)TotalItem / limit),
                Page = currentPage
            };
            return res;

        }

        public HotelRoomResDto? SearchHotelRoom(int id)
        {
            var room = getById(id);
            return _mapper.Map<HotelRoomResDto>(room);
        }

        public HotelRoomResDto? updateHotelRoom(int id, HotelRoomRequestDto hotelRoomRequestDto)
        {
            var existRoom = getById(id);
            if (existRoom is null) throw new ArgumentException("Room not found");
            var updateRoom = _mapper.Map(hotelRoomRequestDto, existRoom);
            _context.Update(updateRoom);
            _context.SaveChanges();

            return _mapper.Map<HotelRoomResDto>(updateRoom);

        }
    }
}