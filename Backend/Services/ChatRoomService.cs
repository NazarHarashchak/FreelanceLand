using AutoMapper;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Models;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<ChatRoom> chatRoomRepo;
        private readonly ApplicationContext db;

        public ChatRoomService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            db = context;
            chatRoomRepo = new EFGenericRepository<ChatRoom>(context);
        }

        public IEnumerable<ChatRoom> GetChatRoomsAsync()
        {
            IEnumerable<ChatRoom> chatRooms = chatRoomRepo.Get();

            return chatRooms;
        }

        public void AddChatRoomAsync(ChatRoom chatRoom)
        {
            chatRoomRepo.Create(chatRoom);
        }
    }
}
