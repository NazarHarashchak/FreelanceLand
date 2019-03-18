using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInfoController : ControllerBase
    {
        private ITaskInfoService infoTaskService;
        private ITasksService taskService;
        private IUsersService usersService;

        public TaskInfoController(ITaskInfoService infoTaskService, ITasksService taskService, IUsersService usersService)
        {
            this.infoTaskService = infoTaskService;
            this.usersService = usersService;
            this.taskService = taskService;
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDescription> Get(int id)
        {
            var dtos = infoTaskService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [HttpGet("{number},{id}")]
        public ActionResult<User> Get(int number, int id)
        {
            var dtos = infoTaskService.GetTaskCustomer(id);

            return Ok(dtos);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("deleteTask, {id}")]
        public void DeleteTask(int id)
        {
            taskService.DeleteTask(id);
        }
    }
}