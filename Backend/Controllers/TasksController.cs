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

        [Route("Created/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetCreatedTasks(int id)
        {
            var dtos = await tasksService.GetCreatedTaskByUser(id);

            return Ok(dtos);
        }

        [Route("DragAndDropCustomer")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> DragAndDropTaskByCustomer 
                    ([FromBody] CustomerDragDropDTO dropDTO)
        {
            var dtos = await tasksService.DragAndDropTaskByCustomer(dropDTO.TaskId, dropDTO.CustomerId,
                                        dropDTO.FinalStatus);

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
