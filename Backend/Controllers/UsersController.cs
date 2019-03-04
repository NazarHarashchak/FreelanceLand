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

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public ActionResult<User> Get()
        {
            var dtos = _usersService.GetAllEntities();

            return Ok(dtos);
        }


        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var dtos = _usersService.GetUserInformation(id);

            return Ok(dtos);
        }
        


        [HttpPut("{id}")]
        public User PutUser(int id, [FromBody] UserInformation value)
        {
            var dtos = _usersService.UpdateUser(id, value);
            return dtos;
        }
       
    }
}
