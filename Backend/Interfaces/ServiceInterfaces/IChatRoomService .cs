using Backend.DTOs;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IChatRoomService
    {
        System.Threading.Tasks.Task<IEnumerable<ChatRoom>> GetChatRoomsAsync();

        System.Threading.Tasks.Task AddChatRoomAsync(ChatRoom chatRoom);

        System.Threading.Tasks.Task<List<ChatRoomWithUserNamesDTO>> GetChatRoomsListAsync(int id);
    }
}
