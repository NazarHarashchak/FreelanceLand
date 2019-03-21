using FreelanceLand.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Hubs
{
    public class NotificationHub : Hub
    {
        private EFGenericRepository<Message> messageRepo;
        public NotificationHub( ApplicationContext context)
        {
            messageRepo = new EFGenericRepository<Message>(context);
        }


        [Authorize(Roles = "User")]
        public async System.Threading.Tasks.Task Send(string message, string to)
        {
            var userName = Context.User.Identity.Name;
            
            if (Context.UserIdentifier != to) 
                await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);
            await Clients.User(to).SendAsync("Receive", message, userName);
        }

        public string GetNotification()
        {
            Message msg = messageRepo.Get().FirstOrDefault();

            return msg.Content;
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Hello {Context.UserIdentifier} .");
            await base.OnConnectedAsync();
        }
        public override async System.Threading.Tasks.Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.UserIdentifier} left us.");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
