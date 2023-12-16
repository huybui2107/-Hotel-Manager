


using HotelApp.API.DTOs.Booking;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controller

{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking _bookingService;
        public BookingController(IBooking bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<BookingResDto> createBooking([FromBody] BookingRequestDto bookingRequestDto)
        {
            var Id = HttpContext.Items["UserId"];
            if (Id is null) return Unauthorized();
            bookingRequestDto.UserId = int.Parse(Id.ToString() ?? "");
            var booking = _bookingService.createBooking(bookingRequestDto);
            return Ok(booking);
        }
        [Authorize]
        [HttpGet("hotel/{hotelId}")]
        public ActionResult<SearchBookingByHotelDto> getBookingByHotel(int hotelId, [FromQuery] BookingFilterOption option)
        {
            var booking = _bookingService.getBookingByHotel(hotelId, option);
            return Ok(booking);
        }
        [Authorize]
        [HttpGet("user")]
        public ActionResult<SearchBookingByUserDto> getBookingByUser([FromQuery] BookingFilterOption option)
        {
            var Id = HttpContext.Items["UserId"];
            if (Id is null) return Unauthorized();
            var booking = _bookingService.getBookingByUserId(int.Parse(Id.ToString() ?? ""), option);
            return Ok(booking);
        }

        [Authorize]
        [HttpPatch("{bookingId}")]
        public ActionResult<BookingResDto> updateStatus(int bookingId)
        {

            var booking = _bookingService.updateStatus(bookingId);
            return Ok(booking);
        }


    }
}