using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreelanceLand.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        EFGenericRepository<Task> userRepo = new EFGenericRepository<Task>(new ApplicationContext());
        //private readonly ApplicationContext _context;

        //public TasksController(ApplicationContext context)
        //{
        //    _context = context;
        //}

        // GET: api/tasks
        [HttpGet]
        public IEnumerable<Task> GetTasks()
        {
            return  userRepo.Get();
        }

        // GET: api/Tasks/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTask([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var task = await _context.Tasks.FindAsync(id);

        //    if (task == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(task);
        //}
    }
}