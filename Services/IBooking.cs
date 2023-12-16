

using HotelApp.API.Databases.Entities;
using HotelApp.API.DTOs.Booking;
using HotelApp.API.DTOs.Hotel;

namespace HotelApp.API.Services
{
    public interface IBooking
    {

        BookingResDto createBooking(BookingRequestDto bookingRequestDto);
        SearchBookingByUserDto getBookingByUserId(int id, BookingFilterOption option);
        BookingResDto updateStatus(int bookingId);
        Booking getById(int id);

        SearchBookingByHotelDto getBookingByHotel(int hotelId, BookingFilterOption option);

    }
}