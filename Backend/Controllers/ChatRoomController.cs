using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Models;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<ChatRoom> chatRooms = chatRoomRepo.Get(ch => (ch.CreatorId == id)||(ch.SecondUserId == id));
            List<ChatRoomWithUserNamesDTO> dtos = new List<ChatRoomWithUserNamesDTO>(chatRooms.Count());

            foreach (ChatRoom ch in chatRooms)
            {
                ChatRoomWithUserNamesDTO chR = new ChatRoomWithUserNamesDTO();
                chR.Id = ch.Id;
                chR.Name = ch.Name;
                chR.FirstUserName = userRepo.Get( u => u.Id == ch.CreatorId).FirstOrDefault().Name;
                chR.SecondUserName = userRepo.Get(u => u.Id == ch.SecondUserId).FirstOrDefault().Name;
                chR.FirstUserLogin = userRepo.Get(u => u.Id == ch.CreatorId).FirstOrDefault().Login;
                chR.SecondUserLogin = userRepo.Get(u => u.Id == ch.SecondUserId).FirstOrDefault().Login;
                dtos.Add(chR);
            }


            return Ok(dtos);
        }

    }
}