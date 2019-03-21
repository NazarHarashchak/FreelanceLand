using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
>>>>>>> d8298a5860533b66e1a09f235540c78552bb2601

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
        [HttpPost("DeleteTask")]
        public async System.Threading.Tasks.Task DeleteTask([FromBody] TaskDTO task)
        {
            tasksService.DeleteTask(task.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(task, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
