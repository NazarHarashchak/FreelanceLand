using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        public IUsersService _userService;

        public LoginController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<UserLoginDTO> ValidateUser([FromBody] UserLoginDTO user)
        {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            var dto = _userService.Authenticate(user.Login, user.Password);
            return dto;
        }
    }
}
