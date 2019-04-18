using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using Microsoft.AspNetCore.SignalR;
using ServiceBrokerListener.Domain;
using System;

namespace Backend.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly EFGenericRepository<Notification> notifRepo;
        private readonly INotificationService _notificationService;
        private readonly SqlDependencyEx _tracking;

        public NotificationHub(ApplicationContext context, INotificationService notificationService,
            SqlDependencyEx tracking, IServiceProvider serviceProvider)
        {
            _notificationService = notificationService;
            notifRepo = new EFGenericRepository<Notification>(context);
            _tracking = tracking;
            _tracking.TableChanged += (o, args) => { OnChangeAsync(serviceProvider); };
        }

        private void OnChangeAsync(IServiceProvider sp)
        {
            var notifications = _notificationService.GetNotifications();
            Clients.All.SendAsync("sendMessage", notifications);
        }

    }
}
