using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevertPassController : ControllerBase
    {
        public IUsersService _userService;
        private readonly IEmailService _emailService;
        private Random rand;
        private CodeDTO verificationCode;

        public RevertPassController(IUsersService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
            rand = new Random();
            verificationCode = new CodeDTO();
        }

        [HttpPost("validateUser")]
        public async Task ValidateUser([FromBody] UserAccountDTO user)
        {
            var dto = await _userService.GetUserByLogin(user.Login);

            await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost("sendCode")]
        public async Task SendCode([FromBody] UserAccountDTO user)
        {
            verificationCode.Code = rand.Next(100000, 999999);
            string message = $"<h2>Your verification code: {verificationCode.Code}<h2>";
            _emailService.SendEmailAsync(user.Email, "Administration", message);
            await Response.WriteAsync(JsonConvert.SerializeObject(verificationCode, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost("changePass")]
        public async Task ChangePass([FromBody] UserAccountDTO user)
        {
            var dto = await _userService.ChangePass(user.Login, user.Password);
            await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}