using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var dtos = await _usersService.GetAllEntities();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var dtos = await _usersService.GetUserInformation(id);

            return Ok(dtos);
        }

        [HttpPut("{id}")]
        public async Task<User> PutUser(int id, [FromBody] UserInformation value)
        {
            var dtos = await _usersService.UpdateUser(id, value);
            return dtos;
        }

    }
}
