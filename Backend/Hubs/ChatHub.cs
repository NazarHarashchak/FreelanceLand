using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Hubs
{
    public class ChatHub : Hub
    {
        private EFGenericRepository<Message> messageRepo;
        private EFGenericRepository<User> userRepo;
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
            
        public ChatHub( ApplicationContext context, IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            messageRepo = new EFGenericRepository<Message>(context);
            userRepo = new EFGenericRepository<User>(context);
        }


        [Authorize]
        public async System.Threading.Tasks.Task Send(int chatRoomId, string message, int senderId, string to)
        {
            var userName = Context.User.Identity.Name;
            
            Message msg = new Message()
            {
                Content = message   
            };

            foreach (var ch in msg.Content)
            {
                if (char.IsWhiteSpace(ch))
                    continue;

                await _messageService.AddMessageToRoomAsync(chatRoomId, msg, senderId);

                if (Context.UserIdentifier != to)
                    await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);

                await Clients.User(to).SendAsync("Receive", message, userName);                
            }                       
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public override async System.Threading.Tasks.Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }
}
