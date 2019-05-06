 using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Models;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        EFGenericRepository<ChatRoom> chatRoomRepo;
        EFGenericRepository<User> userRepo;
        private IChatRoomService _chatRoomService;
        private IMessageService _messageService;

        public ChatRoomController(ApplicationContext context, IChatRoomService chatRoomService, 
            IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            userRepo = new EFGenericRepository<User>(context);
            chatRoomRepo = new EFGenericRepository<ChatRoom>(context);
        }

        [HttpGet("GetChatRooms/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoomsAsync(int id)
        {
            ChatRoom chatRoom = (await chatRoomRepo.GetAsync(ch => (ch.Id == id))).FirstOrDefault();
            ChatRoomInfoDTO dto = new ChatRoomInfoDTO();
            dto.Name = chatRoom.Name;
            dto.Id = chatRoom.Id;
            dto.FirstUserLogin = (await userRepo.GetAsync(u => u.Id == chatRoom.CreatorId)).FirstOrDefault().Login;
            dto.SecondUserLogin = (await userRepo.GetAsync(u => u.Id == chatRoom.SecondUserId)).FirstOrDefault().Login;
            return Ok(dto);
        }

        [HttpPost("CreateChatRoomWithFirstMessage")]
        public async System.Threading.Tasks.Task CreateChatRoomAfterFirstMessageAsync([FromBody] CreateChatRoomAndSendFirstMessageDTO cht)
        {
           
            ChatRoom chatRoom = (await chatRoomRepo.GetAsync(ch => 
            (ch.CreatorId == cht.creatorId && ch.SecondUserId == cht.secondUserId) ||
            (ch.CreatorId == cht.secondUserId && ch.SecondUserId == cht.creatorId)
            )).FirstOrDefault();
            if(chatRoom != null)
            {
                Message msg = new Message();
                msg.Content = cht.message;
                foreach(var ch in msg.Content)
                {
                    if (char.IsWhiteSpace(ch))
                        continue;
                    await _messageService.AddMessageToRoomAsync(chatRoom.Id, msg, cht.creatorId);
                }
            }
            else
            {
                ChatRoom newChatRoom = new ChatRoom();
                newChatRoom.CreatorId = cht.creatorId;
                newChatRoom.SecondUserId = cht.secondUserId;
                await _chatRoomService.AddChatRoomAsync(newChatRoom);

                int chId = (await chatRoomRepo.GetAsync(ch => ch.CreatorId == cht.creatorId && ch.SecondUserId == cht.secondUserId)).FirstOrDefault().Id;

                Message msg = new Message();
                msg.Content = cht.message;
                foreach (var ch in msg.Content)
                {
                    if (char.IsWhiteSpace(ch))
                        continue;
                    await _messageService.AddMessageToRoomAsync(chId, msg, cht.creatorId);
                }
            }
        }

        [HttpGet("GetChatRoomsList/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<List<ChatRoomWithUserNamesDTO>>> GetAsync(int id)
        {
            List<ChatRoomWithUserNamesDTO> dtos = await _chatRoomService.GetChatRoomsListAsync(id);
            return Ok(dtos);
        }

    }
}