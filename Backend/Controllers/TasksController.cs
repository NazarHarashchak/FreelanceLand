using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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


        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> Get()
        {
            var dtos = tasksService.GetToDoEntities();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TaskDTO>> GetHistoryTasks(int id)
        {
            var dtos = tasksService.GetHistoryTaskByUser(id);
            
            return Ok(dtos);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("deleteTask, {id}")]
        void DeleteTask(int id)
        {
            tasksService.DeleteTask(id);
        }
    }
}
