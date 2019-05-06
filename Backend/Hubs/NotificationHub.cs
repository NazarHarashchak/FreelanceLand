using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs
{
    public class NotificationHub : Hub
    {
        private INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService; ;
        }

        [Authorize]
        public async System.Threading.Tasks.Task AddNotification(int userId, string message)
        {
            await _notificationService.AddNotification(message, userId);
        }
    }
}
