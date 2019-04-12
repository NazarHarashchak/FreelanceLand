using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

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

        [HttpGet("all")]
        public async Task<IActionResult> GetTasks([FromQuery] int page, string searchText, int priceTo, int priceFrom, string[] categ)
        {
            var dtos = await tasksService.GetTasks(page, searchText, priceTo, priceFrom, categ);

            return Ok(dtos);
        }

        [Route("history/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetHistoryTasks(int id)
        {
            var dtos = await tasksService.GetHistoryTaskByUser(id);
            
            return Ok(dtos);
        }

        [Route("Active/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetActiveTasks(int id)
        {
            var dtos = await tasksService.GetActiveTaskByUser(id);

            return Ok(dtos);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost("DeleteTask")]
        public async Task DeleteTask([FromBody] TaskDTO task)
        {
            await tasksService.DeleteTask(task.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(task, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
