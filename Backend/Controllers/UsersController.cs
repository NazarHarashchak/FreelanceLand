using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Backend.Services;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;

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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            var dtos = _usersService.GetAllEntities();

            return Ok(dtos);
        }
    }
}