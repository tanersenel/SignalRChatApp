using SignalRChatApp.Entities;
using SignalRChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Repositories
{
    public interface IChatRepository
    {
        Task CreateRoom(string name);
        Task<bool> CreateMessage(Message message,string roomId);
        Task<IEnumerable<Room>> GetRooms();
        Task<Room> GetRoomWithMessages(string roomId);
        
        Task<bool> DeleteRoom(string id);
    }
}
