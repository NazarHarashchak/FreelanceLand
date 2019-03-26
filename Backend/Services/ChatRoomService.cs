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

        public async System.Threading.Tasks.Task<IEnumerable<ChatRoom>> GetChatRoomsAsync()
        {
            IEnumerable<ChatRoom> chatRooms = await chatRoomRepo.GetAsync();

            return chatRooms;
        }

        public async System.Threading.Tasks.Task AddChatRoomAsync(ChatRoom chatRoom)
        {
            await chatRoomRepo.CreateAsync(chatRoom);
        }

        public async System.Threading.Tasks.Task<List<ChatRoomWithUserNamesDTO>> GetChatRoomsListAsync(int id)
        {
            IEnumerable<ChatRoom> chatRooms = await chatRoomRepo.GetAsync(ch => (ch.CreatorId == id) || (ch.SecondUserId == id));
            List<ChatRoomWithUserNamesDTO> dtos = new List<ChatRoomWithUserNamesDTO>(chatRooms.Count());

            foreach (ChatRoom ch in chatRooms)
            {
                ChatRoomWithUserNamesDTO chR = new ChatRoomWithUserNamesDTO();
                chR.Id = ch.Id;
                chR.Name = ch.Name;
                chR.FirstUserName = (await userRepo.GetAsync(u => u.Id == ch.CreatorId)).FirstOrDefault().Name;
                chR.SecondUserName = (await userRepo.GetAsync(u => u.Id == ch.SecondUserId)).FirstOrDefault().Name;
                chR.FirstUserLogin = (await userRepo.GetAsync(u => u.Id == ch.CreatorId)).FirstOrDefault().Login;
                chR.SecondUserLogin = (await userRepo.GetAsync(u => u.Id == ch.SecondUserId)).FirstOrDefault().Login;
                dtos.Add(chR);
            }


            return dtos;
        }
    }
}
