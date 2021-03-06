﻿using Backend.DTOs;
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

        [Route("topActive/{id}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTopActiveUserTask(int id)
        {
            var dtos = await tasksService.GetTopActiveTaskForUser(id);
            return Ok(dtos);
        }

        [Route("topHistory/{id}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTopHistoryUserTask(int id)
        {
            var dtos = await tasksService.GetTopHistoryTaskForUser(id);
            return Ok(dtos);
        }

        [Route("Active/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetActiveTasks(int id)
        {
            var dtos = await tasksService.GetActiveTaskByUserAsync(id);

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
            string msg;
            if (dropDTO.FinalStatus == "In progress")
                msg = $"Your task status was changed to To do";
            else
                msg = $"Your task status was changed to {dropDTO.FinalStatus}";
            var userId = await tasksService.GetExecutorAsync(dropDTO.TaskId);
            await notificationService.AddNotification(msg, userId);
            await _hubContext.Clients.All.SendAsync("sendMessage", userId, msg);
            var dtos = await tasksService.DragAndDropTaskByCustomer(dropDTO.TaskId, dropDTO.CustomerId,
                                        dropDTO.FinalStatus);

            return Ok(dtos);
        }

        [Route("DragAndDropExecutor")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> DragAndDropTaskByExecutor
                    ([FromBody] CustomerDragDropDTO dropDTO)
        {
            string msg = $"Your task status was changed to {dropDTO.FinalStatus}";
            var userId = await tasksService.GetCustomerAsync(dropDTO.TaskId);
            await notificationService.AddNotification(msg, userId);
            await _hubContext.Clients.All.SendAsync("sendMessage", userId, msg);
            var dtos = await tasksService.DragAndDropTaskByExecutorAsync(dropDTO.TaskId, dropDTO.CustomerId,
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
