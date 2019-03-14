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
        public ActionResult<TaskDescription> Get(int id)
        {
            var dtos = tasksService.GetTaskDescription(id);
            return Ok(dtos);
        }

        [HttpGet("{number},{id}")]
        public ActionResult<User> Get(int number, int id)
        {
            var dtos = tasksService.GetTaskCustomer(id);

            return Ok(dtos);
        }
    }
}