using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoriesController : ControllerBase
    {
        private ITaskCategoriesService categoriesService;

        public TaskCategoriesController(ITaskCategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TaskCategoryDTO>> Get()
        {
            var dtos = categoriesService.GetAllEntities();

            return Ok(dtos);
        }
    }
}
