using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Pagination;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        EFGenericRepository<User> userRepo;
        EFGenericRepository<Image> imageRepo;

        private IUsersService _usersService;
        private IImageService _imageService;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public UsersController(IUsersService usersService, ApplicationContext context, IImageService imageService)
        {
            userRepo = new EFGenericRepository<User>(context);
            imageRepo = new EFGenericRepository<Image>(context);
            _usersService = usersService;
            _imageService = imageService;
        }

        [HttpGet("Pagination/{text}")]
        
        public async Task<IActionResult> GetPageNumber([FromQuery] TextDTO text )
        {
            // var all = await _usersService.GetAllEntities();
           
            var dtos = await _usersService.GetUsers(text);

            return Ok(dtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var dtos = await _usersService.GetUserInformation(id);

            return Ok(dtos);
        }
        
        [HttpPost("CreateImage")]
        public async Task<IActionResult> Post([FromForm]ImageDTO Image)
        {
            string response = await _imageService.CreateUserImage(Image);
            if (response == "empty")
                return BadRequest("Empty request!");
            return Ok("Success!");
        }
        [HttpGet("GetImage/{id}")]
        public async System.Threading.Tasks.Task GetImage(int id)
        {
            string imgDataURL = await _imageService.GetImageAsync(id);
            if (imgDataURL == null)
            {
                await Response.WriteAsync(JsonConvert.SerializeObject("", new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            else
            {
                await Response.WriteAsync(JsonConvert.SerializeObject(imgDataURL, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
        }

        [HttpGet("getRoles")]
        public async Task<ActionResult<IEnumerable<UserRolesDTO>>> GetRoles()
        {
            var dtos = await _usersService.GetAllRolesDtos();
            return Ok(dtos);
        }
        [HttpPut("{id}")]
        public async Task<User> UpdateUser(int id, [FromBody] UserInformation value)
        {
            var dtos = await _usersService.UpdateUser(id, value);
            return dtos;
        }

    }
}
