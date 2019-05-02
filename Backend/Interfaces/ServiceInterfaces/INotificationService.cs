using Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface INotificationService
    {
        IEnumerable<NotificationDTO> GetNotifications();
        Task DeleteNotificationAsync(int id);
        Task DeleteNotificationsAsync(int userId);
        Task<IEnumerable<NotificationDTO>> GetNotificationsAsync(int userId);
        Task<int> GetNotificationCountAsync(int userId);
        Task AddNotification(string message, int? userId);
    }
}
