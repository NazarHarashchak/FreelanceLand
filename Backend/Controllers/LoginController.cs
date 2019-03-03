using System;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;

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
        public ActionResult<UserDTO> ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var dto = _userService.Authenticate(username, password);
            return dto;
        }
    }
}
