using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInfoController : ControllerBase
    {
        private ITaskInfoService tasksService;
        private IUsersService usersService;

        public TaskInfoController(ITaskInfoService tasksService, IUsersService usersService)
        {
            this.tasksService = tasksService;
            this.usersService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskPageDTO>> Get(int id)
        {
            var dtos = await tasksService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [Route("getCategories")]
        [HttpGet]
        public async Task<ActionResult<TaskCategoryDTO>> Get()
        {
            var dtos = await tasksService.GetCategories();
            return Ok(dtos);
        }

        [Route("addexcecutor")]
        [HttpPost]
        public async Task<ActionResult<ExcecutorDTO>> AddExcecutor([FromBody] ExcecutorDTO user)
        {
            var dtos = await tasksService.AddExcecutor(user);
            return Ok(dtos);
        }

        [Route("addnewtask")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> AddNewTask([FromBody] TaskPageDTO task)
        {
            var result = await tasksService.AddTask(task);
            return Ok(result);
        }

        [Route("closetask")]
        [HttpPost]
        public async Task<ActionResult<TaskPageDTO>> CloseTask([FromBody] int id)
        {
            var result = await tasksService.CloseTask(id);

            return Ok(result);
        }
    }

}