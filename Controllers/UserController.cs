
using AutoMapper;
using HotelApp.API.DTOs.Hotel;
using HotelApp.API.DTOs.User;
using HotelApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namespace;

namespace HotelApp.API.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService)
        {
            _userService = userService;

        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult<ProfileDto> GetInfor()
        {
            var currentUserEmail = HttpContext.Items["UserEmail"] as string;
            if (string.IsNullOrEmpty(currentUserEmail)) return Unauthorized();

            return Ok(_userService.GetInfor(currentUserEmail));
        }
        [Authorize]
        [HttpPut("me")]

        public ActionResult UpdateProfile([FromBody] ProfileDto profileDto)
        {
            var currentUserEmail = HttpContext.Items["UserEmail"] as string;
            if (string.IsNullOrEmpty(currentUserEmail)) return Unauthorized();
            _userService.UpdateUser(currentUserEmail, profileDto);
            return Ok("Update successful");
        }
        [Authorize]
        [HttpPatch("me/password")]
        public ActionResult ChangePass([FromBody] ChangePassDto changePassDto)
        {
            var Id = HttpContext.Items["UserId"];
            if (Id is null) return Unauthorized();
            _userService.ChangePassWord(changePassDto, int.Parse(Id.ToString() ?? ""));
            return Ok("Change password successful");
        }

        [Authorize]
        [HttpPost("registerMember")]
        public ActionResult<ResponseHotelDto> RegisterMember([FromBody] RegisterHotelDto hotel)
        {
            var Id = HttpContext.Items["UserId"];
            if (Id is null) return Unauthorized();
            var newhotel = _userService.RegisterManage(hotel, int.Parse(Id.ToString() ?? ""));
            return Ok(newhotel);
        }

    }
}