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

        public ChatRoomController(ApplicationContext context, IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            userRepo = new EFGenericRepository<User>(context);
            chatRoomRepo = new EFGenericRepository<ChatRoom>(context);
        }

        [HttpGet("GetChatRooms/{id}")]
        public ActionResult<IEnumerable<ChatRoom>> GetChatRooms(int id)
        {
            ChatRoom chatRoom = chatRoomRepo.Get(ch => (ch.Id == id)).FirstOrDefault();
            ChatRoomInfoDTO dto = new ChatRoomInfoDTO();
            dto.Name = chatRoom.Name;
            dto.Id = chatRoom.Id;
            dto.FirstUserLogin = userRepo.Get(u => u.Id == chatRoom.CreatorId).FirstOrDefault().Login;
            dto.SecondUserLogin = userRepo.Get(u => u.Id == chatRoom.SecondUserId).FirstOrDefault().Login;
            return Ok(dto);
        }

        [HttpPost("CreateChatRoomWithFirstMessage")]
        public void CreateChatRoomAfterFirstMessage([FromBody] CreateChatRoomAndSendFirstMessageDTO cht)
        {
           
            ChatRoom chatRoom = chatRoomRepo.Get(ch => ch.CreatorId == cht.creatorId && ch.SecondUserId == cht.secondUserId).FirstOrDefault();
            if(chatRoom != null)
            {
                Message msg = new Message();
                msg.Content = cht.message;
                _messageService.AddMessageToRoomAsync(chatRoom.Id, msg, cht.creatorId);
            }
            else
            {
                ChatRoom newChatRoom = new ChatRoom();
                newChatRoom.CreatorId = cht.creatorId;
                newChatRoom.SecondUserId = cht.secondUserId;
                _chatRoomService.AddChatRoomAsync(newChatRoom);

                int chId = chatRoomRepo.Get(ch => ch.CreatorId == cht.creatorId && ch.SecondUserId == cht.secondUserId).FirstOrDefault().Id;

                Message msg = new Message();
                msg.Content = cht.message;
                _messageService.AddMessageToRoomAsync(chId, msg, cht.creatorId);
            }
        }

        [HttpGet("GetChatRoomsList/{id}")]
        public ActionResult<List<ChatRoomWithUserNamesDTO>> Get(int id)
        {
            List<ChatRoomWithUserNamesDTO> dtos = _chatRoomService.GetChatRoomsList(id);
            return Ok(dtos);
        }

    }
}