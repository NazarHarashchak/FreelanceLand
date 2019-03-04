using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITasksService tasksService;

        public TasksController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> Get()
        {
            var dtos = tasksService.GetToDoEntities();

            return Ok(dtos);
        }
    }
}
