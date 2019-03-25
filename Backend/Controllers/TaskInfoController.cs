using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
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
        public ActionResult<TaskPageDTO> Get(int id)
        {
            var dtos = tasksService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [Route("getCategories")]
        [HttpGet]
        public ActionResult<TaskPageDTO> Get()
        {
            var dtos = tasksService.GetCategories();
            return Ok(dtos);
        }

        [Route("addexcecutor")]
        [HttpPost]
        public ActionResult<ExcecutorDTO> AddExcecutor([FromBody] ExcecutorDTO user)
        {
            var dtos = tasksService.AddExcecutor(user);
            return Ok(dtos);
        }

        [Route("addnewtask")]
        [HttpPost]
        public ActionResult<TaskPageDTO> AddNewTask([FromBody] TaskPageDTO task)
        {
            var result = tasksService.AddTask(task);
            return Ok(result);
        }
    }
}