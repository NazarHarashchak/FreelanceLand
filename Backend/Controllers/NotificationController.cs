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
    public class NotificationController : ControllerBase
    {
        public INotificationService _notifService;

        public NotificationController(INotificationService notifService)
        {
            _notifService = notifService;
        }

        [HttpPost("getNotifications")]
        public async Task GetNotifications([FromBody] UserAccountDTO user)
        {
            var dto = await _notifService.GetNotificationsAsync(user.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost("getCount")]
        public async Task<int> GetCount([FromBody] UserAccountDTO user)
        {
            var count = await _notifService.GetNotificationCountAsync(user.Id);
            return count;
        }

        [HttpPost("deleteNotifications")]
        public async Task DeleteNotifications([FromBody] UserAccountDTO user)
        {
            await _notifService.DeleteNotificationsAsync(user.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(null, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost("deleteNotification")]
        public async Task DeleteNotification([FromBody] NotificationDTO notification)
        {
            await _notifService.DeleteNotificationAsync(notification.Id);
            var dto = await _notifService.GetNotificationsAsync(notification.UserId);
            await Response.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}