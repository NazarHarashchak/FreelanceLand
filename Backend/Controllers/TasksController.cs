using Backend.DTOs;
using Backend.Hubs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITasksService tasksService;
        private INotificationService notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public TasksController(ITasksService tasksService, INotificationService notificationService,
            IHubContext<NotificationHub> hubContext)
        {
            this.tasksService = tasksService;
            this.notificationService = notificationService;
            _hubContext = hubContext;
        }

        [HttpGet("PageNumber/{pageNumber}")]
        public async Task<IActionResult> GetPageNumber(int pageNumber)
        {
            var dtos = await tasksService.GetTasks(pageNumber);

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
            string msg = "Your task was deleted by moderator";
            var userId = await tasksService.GetCustomerAsync(task.Id);
            await notificationService.AddNotification(msg, userId);
            await _hubContext.Clients.All.SendAsync("sendMessage", userId, msg);
            await tasksService.DeleteTask(task.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(task, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
