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
    public class MessageController : ControllerBase
    {
        EFGenericRepository<Message> messageRoomRepo;

        private IMessageService _messageService;

        public MessageController(ApplicationContext context, IMessageService messageService)
        {
            _messageService = messageService;
            messageRoomRepo = new EFGenericRepository<Message>(context);
        }

        [HttpGet("GetMessages/{id}")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetAsync(int id)
        {
            List<MessageToUserDTO> messages = await _messageService.GetMessagesForChatRoomAsync(id);

            return Ok(messages);
        }
    }
}