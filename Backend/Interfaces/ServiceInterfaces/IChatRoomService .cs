using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IChatRoomService
    {
        IEnumerable<ChatRoom> GetChatRoomsAsync();

        void AddChatRoomAsync(ChatRoom chatRoom);
    }
}
