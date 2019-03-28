using Backend.Common;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUsersController : ControllerBase
    {
        private readonly ITopUsersService _usersService;

        public TopUsersController(ITopUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOs.TopUserDTO>>> GetTop5Users()
        {
            var topUsers = await _usersService.GetTop5Users();
            return Ok(topUsers);
        }

    }
}
