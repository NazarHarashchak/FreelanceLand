using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Backend.Interfaces.ServiceInterfaces;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        EFGenericRepository<User> userRepo;
        EFGenericRepository<Image> imageRepo;

        private IUsersService _usersService;

        public UsersController(IUsersService usersService, ApplicationContext context)
        {
            userRepo = new EFGenericRepository<User>(context);
            imageRepo = new EFGenericRepository<Image>(context);
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult<User> Get()
        {
            var dtos = _usersService.GetAllEntities();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var dtos = _usersService.GetUserInformation(id);

            return Ok(dtos);
        }
        
        [HttpPost("CreateImage")]
        public async Task<IActionResult> Post([FromForm]ImageDTO Image)
        {
            string response = await _usersService.CreateUserImage(Image);
            if (response == "empty")
                return BadRequest("Empty request!");
            return Ok("Success!");
        }
        [HttpGet("GetImage/{id}")]
        public async System.Threading.Tasks.Task GetImage(int id)
        {
            Image image = imageRepo.Get(im => im.UserId == id).FirstOrDefault();
            if (image == null)
            {
                await Response.WriteAsync(JsonConvert.SerializeObject("", new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            else
            {
                byte[] fileBytes = image.Picture;
                string imgBase64Data = Convert.ToBase64String(fileBytes);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imgBase64Data);
                ImageToClientDTO imgDTO = new ImageToClientDTO();
                imgDTO.Image = imgDataURL;
                imgDTO.FileName = image.FileName;
                await Response.WriteAsync(JsonConvert.SerializeObject(imgDataURL, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
        }

        [HttpPut("{id}")]
        public User PutUser(int id, [FromBody] UserInformation value)
        {
            var dtos = _usersService.UpdateUser(id, value);
            return dtos;
        }

    }
}
