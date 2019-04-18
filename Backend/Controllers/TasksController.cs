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
        public async Task<IActionResult> GetTasks([FromQuery] int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            var dtos = await tasksService.GetTasks(page, search, priceTo, priceFrom, categ);

            return Ok(dtos);
        }

        [Route("History")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetHistoryTasks([FromQuery] int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            var dtos = await tasksService.GetHistoryTaskByUser(id, page, search, priceTo, priceFrom, categ);

            return Ok(dtos);
        }

        [Route("Active")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetActiveTasks([FromQuery] int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            var dtos = await tasksService.GetActiveTaskByUser(id,page,search,priceTo,priceFrom,categ);

            return Ok(dtos);
        }

        [Route("Created")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetCreatedTasks([FromQuery] int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            var dtos = await tasksService.GetCreatedTaskByUser(id, page, search, priceTo, priceFrom, categ);

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
