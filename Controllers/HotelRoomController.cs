


using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.HotelRoom;
using HotelApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controller

{
    [Route("api/room")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomService _hotelRoomService;
        public HotelRoomController(IHotelRoomService hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
        }

        [Authorize]
        [HttpPost("hotel/{id}")]
        public ActionResult<HotelRoomResDto> createRoom(int id, [FromBody] HotelRoomRequestDto hotelRoomRequestDto)
        {

            var newRoom = _hotelRoomService.createHotelRoom(id, hotelRoomRequestDto);
            return Ok(newRoom);
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<HotelRoomResDto> updateRoom(int id, [FromBody] HotelRoomRequestDto hotelRoomRequestDto)
        {

            var Room = _hotelRoomService.updateHotelRoom(id, hotelRoomRequestDto);
            return Ok(Room);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult deleteRoom(int id)
        {
            _hotelRoomService.deleteRoom(id);
            return Ok("delete room successful");
        }

        [HttpGet("hotel/{id}")]
        public ActionResult<SearchHotelRoomResDto> getRoomByHotelId(int id, [FromQuery] HotelRoomFilterOption filterOption)
        {
            Console.WriteLine($"1 :page {filterOption.CurrentPage} limit {filterOption.PageSize}");
            var res = _hotelRoomService.getRoomByHotelId(id, filterOption.CurrentPage, filterOption.PageSize);
            if (res is null) return BadRequest();
            return res;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<HotelRoomResDto> getRoomById(int id)
        {
            var Room = _hotelRoomService.SearchHotelRoom(id);
            if (Room is null) return NotFound("Hotel room not found");
            return Room;
        }

    }
}