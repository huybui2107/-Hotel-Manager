


using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.HotelRoom;
using HotelApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controller

{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public ActionResult<SearchHotelResponseDto> searchHotel([FromQuery] HotelFilterOption hotelFilterOption)
        {

            var hotel = _hotelService.searchHotel(hotelFilterOption);
            return Ok(hotel);
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<ResponseHotelDto> updateHotel(int id, [FromBody] RegisterHotelDto registerHotelDto)
        {
            var Id = HttpContext.Items["UserId"];
            if (Id is null) return Unauthorized();
            registerHotelDto.UserId = int.Parse(Id.ToString() ?? "");
            var updateHotel = _hotelService.updateHotel(id, registerHotelDto);
            return Ok(updateHotel);
        }


    }
}