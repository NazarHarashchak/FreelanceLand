using AutoMapper;
using Backend.DTOs;
using Backend.Hubs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Models;
using FreelanceLand.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<Message> messageRepo;
        private EFGenericRepository<ChatRoom> chatRoomRepo;
        private EFGenericRepository<User> userRepo;
        private readonly ApplicationContext db;
        private IUsersService usersService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MessageService(IMapper mapper, ApplicationContext context, 
            INotificationService notificationService, IUsersService usersService,
            IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            db = context;
            messageRepo = new EFGenericRepository<Message>(context);
            userRepo = new EFGenericRepository<User>(context);
            chatRoomRepo = new EFGenericRepository<ChatRoom>(context);
            this.usersService = usersService;
            _hubContext = hubContext;
        }

        public async Task<List<MessageToUserDTO>> GetMessagesForChatRoomAsync(int chatRoomId)
        {
            IEnumerable<Message> messages = await messageRepo.GetAsync(m => m.ChatRoomId == chatRoomId);
            List<MessageToUserDTO> dtos = new List<MessageToUserDTO>(messages.Count());
            foreach(Message msg in messages)
            {
                MessageToUserDTO m = new MessageToUserDTO();
                m.Content = msg.Content;
                m.SenderLogin = (await userRepo.GetAsync(u => u.Id == msg.SenderUserId)).FirstOrDefault().Login;
                m.DateAndTime = msg.DateAndTime.ToLongTimeString();
                dtos.Add(m);
            }


            return dtos;
        }

        public async System.Threading.Tasks.Task AddMessageToRoomAsync(int chatRoomId, Message message, int SenderId)
        {
            message.SenderUserId = SenderId;
            message.ChatRoomId = chatRoomId;
            message.DateAndTime = DateTime.Now;

            await messageRepo.CreateAsync(message);

            var userName = (await usersService.GetUserById(SenderId)).Name;
            string msg = $"You have new message from {userName}";
            var chatRoom = (await chatRoomRepo.GetAsync(x => x.Id == message.ChatRoomId)).FirstOrDefault();
            int? userId;
            if (chatRoom.CreatorId == message.SenderUserId)
                userId = chatRoom.SecondUserId;
            else
                userId = chatRoom.CreatorId;
            await _hubContext.Clients.All.SendAsync("chatNotification", userId, msg);

        }
    }
}
