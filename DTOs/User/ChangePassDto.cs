using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DTOs.User
{
    public class ChangePassDto
    {
        public string NewPassword { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
    }
}