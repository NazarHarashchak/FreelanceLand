using System;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        public IUsersService _userService;

        public RegistrationController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult RegisterUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Fields cann't be empty!");
            }

            if (_userService.GetUserByLogin(username) != null)
            {
                return BadRequest("User with the same login already exists!");
            }

            _userService.CreateUser(username, password);
            return Ok();
        }
    }
}
