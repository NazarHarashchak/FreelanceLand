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
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<Message> messageRepo;
        private EFGenericRepository<User> userRepo;
        private readonly ApplicationContext db;

        public MessageService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            db = context;
            messageRepo = new EFGenericRepository<Message>(context);
            userRepo = new EFGenericRepository<User>(context);
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
            
        }
    }
}
