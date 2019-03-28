using Backend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IUsersService _userService;
        private IUserTokensService _userTokensService;

        public AccountController(IUsersService userService, IUserTokensService userTokensService)
        {
            _userService = userService;
            _userTokensService = userTokensService;
        }

        [HttpPost("login")]
        public async Task Login([FromBody] UserAccountDTO user)
        {
            var dto = await _userService.Authenticate(user.Login, user.Password);
            if(dto == null)
            {
                await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            await Response.WriteAsync(await _userTokensService.CreateToken(dto));
        }

        [HttpPost("register")]
        public async Task Register([FromBody] UserAccountDTO user)
        {
            var dto = await _userService.CreateUser(user.Email, user.Login, user.Password);
            
            await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            
        }
        [HttpGet("confirmEmail")]
        public async Task ConfirmEmail([FromQuery]string confirmCode)
        {
            await _userService.ConfirmEmail(confirmCode);
            Response.Redirect("http://localhost:3000/loginPage");
        }
    }
}