using Backend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Newtonsoft.Json;
using NLog;
using Backend.Common;
using FreelanceLand.Models;
using NLog.Fluent;
using Microsoft.AspNetCore.Mvc.Filters;
using Task = System.Threading.Tasks.Task;

namespace Backend.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public IUsersService _userService;
        private IUserTokensService _userTokensService;
        public ActionFilterAttribute _ActionFilters;
        public ApplicationContext db;

        public LogAttribute La;

        public AccountController(ApplicationContext context, IUsersService userService,
            IUserTokensService userTokensService, ActionFilterAttribute ActionFilter)
        {
            _userService = userService;
            _userTokensService = userTokensService;
            _ActionFilters = ActionFilter;
            La = new LogAttribute(context);
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
        }
    }
}