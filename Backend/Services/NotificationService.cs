using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<Notification> notiRepo;
        private EFGenericRepository<User> userRepo;

        public NotificationService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            notiRepo = new EFGenericRepository<Notification>(context);
            userRepo = new EFGenericRepository<User>(context);
        }

        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var notifications = notiRepo.Get();

            if (notifications == null)
                return null;

            var dtos = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDTO>>(notifications);
            return dtos;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsAsync(int userId)
        {
            var notifications = await notiRepo.GetAsync(x => x.UserId == userId);

            if (notifications == null)
                return null;

            var dtos = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDTO>>(notifications);
            return dtos;
        }

        public async System.Threading.Tasks.Task AddNotification(string message, int? userId)
        {
            var user = (await userRepo.GetAsync(x => x.Id == userId)).FirstOrDefault();
            if (user != null)
            {
                Notification notif = new Notification();
                notif.Message = message;
                notif.DateAndTime = DateTime.Now;
                notif.UserId = user.Id;
                await notiRepo.CreateAsync(notif);
            }
        }

        public async System.Threading.Tasks.Task DeleteNotificationsAsync(int userId)
        {
            var notifications = await notiRepo.GetAsync(x => x.UserId == userId);
            foreach (var n in notifications)
            {
                await notiRepo.RemoveAsync(n);
            }
        }

        public async Task<int> GetNotificationCountAsync(int userId)
        {
            var notifications = await notiRepo.GetAsync(x => x.UserId == userId);
            return notifications.Count();
        }

        public async System.Threading.Tasks.Task DeleteNotificationAsync(int id)
        {
            var notification = (await notiRepo.GetAsync(x => x.Id == id)).FirstOrDefault();
            await notiRepo.RemoveAsync(notification);
        }
    }
}
