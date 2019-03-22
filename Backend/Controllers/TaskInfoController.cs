using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInfoController : ControllerBase
    {

        private ITaskInfoService infoTaskService;
        private IUsersService usersService;

        public TaskInfoController(ITaskInfoService infoTaskService, IUsersService usersService)
        {
            this.infoTaskService = infoTaskService;
            this.usersService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDescription>> Get(int id)
        {
            var dtos = await infoTaskService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [HttpGet("{number},{id}")]
        public async Task<ActionResult<User>> Get(int number, int id)
        {
            var dtos = await infoTaskService.GetTaskCustomer(id);

            return Ok(dtos);
        }
    }
}