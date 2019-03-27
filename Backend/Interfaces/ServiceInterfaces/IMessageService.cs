using Backend.DTOs;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IMessageService
    {
        Task<List<MessageToUserDTO>> GetMessagesForChatRoomAsync(int chatRoomId);

        System.Threading.Tasks.Task AddMessageToRoomAsync(int roomId, Message message, int SenderId);

    }
}
