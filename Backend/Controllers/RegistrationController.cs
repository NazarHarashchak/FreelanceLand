﻿using System;
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
        public ActionResult<UserRegistrationDTO> RegisterUser([FromBody] UserRegistrationDTO user)
        {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))

            {
                return BadRequest("Fields cann't be empty!");
            }

            if (_userService.GetUserByLogin(user.Login) != null)

            {
                return BadRequest("User with the same login already exists!");
            }

            var dto = _userService.CreateUser(user.Email, user.Login, user.Password);
            return dto;
        }
    }
}
