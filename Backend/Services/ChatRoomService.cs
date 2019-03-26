using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Models;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<ChatRoom> chatRoomRepo;
        private EFGenericRepository<User> userRepo;
        private readonly ApplicationContext db;

        public ChatRoomService(IMapper mapper, ApplicationContext context)
        {

            _mapper = mapper;
            db = context;
            chatRoomRepo = new EFGenericRepository<ChatRoom>(context);
            userRepo = new EFGenericRepository<User>(context);
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

        public List<ChatRoomWithUserNamesDTO> GetChatRoomsList(int id)
        {
            IEnumerable<ChatRoom> chatRooms = chatRoomRepo.Get(ch => (ch.CreatorId == id) || (ch.SecondUserId == id));
            List<ChatRoomWithUserNamesDTO> dtos = new List<ChatRoomWithUserNamesDTO>(chatRooms.Count());

            foreach (ChatRoom ch in chatRooms)
            {
                ChatRoomWithUserNamesDTO chR = new ChatRoomWithUserNamesDTO();
                chR.Id = ch.Id;
                chR.Name = ch.Name;
                chR.FirstUserName = userRepo.Get(u => u.Id == ch.CreatorId).FirstOrDefault().Name;
                chR.SecondUserName = userRepo.Get(u => u.Id == ch.SecondUserId).FirstOrDefault().Name;
                chR.FirstUserLogin = userRepo.Get(u => u.Id == ch.CreatorId).FirstOrDefault().Login;
                chR.SecondUserLogin = userRepo.Get(u => u.Id == ch.SecondUserId).FirstOrDefault().Login;
                dtos.Add(chR);
            }


            return dtos;
        }
    }
}
