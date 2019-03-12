using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController:ControllerBase
    {
        private IRolesUserService rolesUserService;

        public UserRoleController(IRolesUserService rolesUserService)
        {
            this.rolesUserService = rolesUserService;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<UserRolesDTO>> Get()
        {
            var dtos = rolesUserService.GetAllRolesDtos();
            return Ok(dtos);
        }
    }
}
