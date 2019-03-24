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
        List<MessageToUserDTO> GetMessagesForChatRoom(int chatRoomId);

        void AddMessageToRoomAsync(int roomId, Message message, int SenderId);
    }
}
