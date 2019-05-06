using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Backend.Hubs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInfoController : ControllerBase
    {
        private ITaskInfoService tasksInfoService;
        ITasksService tasksService;
        private IUsersService usersService;
        private INotificationService notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public TaskInfoController(ITaskInfoService tasksInfoService, IUsersService usersService,
            ITasksService tasksService, INotificationService notificationService, 
            IHubContext<NotificationHub> hubContext)
        {
            this.tasksService = tasksService;
            this.tasksInfoService = tasksInfoService;
            this.usersService = usersService;
            this.notificationService = notificationService;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskPageDTO>> Get(int id)
        {
            var dtos = await tasksInfoService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [Route("getCategories")]
        [HttpGet]
        public async Task<ActionResult<TaskCategoryDTO>> Get()
        {
            var dtos = await tasksInfoService.GetCategories();
            return Ok(dtos);
        }

        [Route("addexcecutor")]
        [HttpPost]
        public async Task<ActionResult<ExcecutorDTO>> AddExcecutor([FromBody] ExcecutorDTO user)
        {
            var task = await tasksInfoService.GetTaskDescription(user.TaskId);
            string msg = $"Now you can start execute task {task.Title}";
            await notificationService.AddNotification(msg, user.ExcecutorId);
            await _hubContext.Clients.All.SendAsync("sendMessage", user.ExcecutorId, msg);
            var dtos = await tasksInfoService.AddExcecutor(user);
            return Ok(dtos);
        }

        [Route("addnewtask")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> AddNewTask([FromBody] TaskPageDTO task)
        {
            var result = await tasksInfoService.AddTask(task);
            return Ok(result);
        }

        [Route("edittask")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> EditTask([FromBody] TaskPageDTO task)
        {
            var result = await tasksInfoService.EditTask(task);

            return Ok(result);
        }

        [Route("closetask/{id}")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> CloseTask(int id)
        {
            var result = await tasksInfoService.CloseTask(id);

            return Ok(result);
        }

        [Route("finishtask/{id}")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> FinishTask(int id)
        {
            var result = await tasksInfoService.FinishTask(id);

            return Ok(result);
        }
    }

}