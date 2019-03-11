using Backend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;

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
        public async System.Threading.Tasks.Task Login([FromBody] UserAccountDTO user)
        {
            var dto = _userService.Authenticate(user.Login, user.Password);
            await Response.WriteAsync(_userTokensService.CreateToken(dto));
        }

        [HttpPost("register")]
        public async System.Threading.Tasks.Task Register([FromBody] UserAccountDTO user)
        {
            var dto = _userService.CreateUser(user.Email, user.Login, user.Password);
            await Response.WriteAsync(_userTokensService.CreateToken(dto));
        }
    }
}