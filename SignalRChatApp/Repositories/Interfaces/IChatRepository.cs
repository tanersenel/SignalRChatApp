﻿using SignalRChatApp.Entities;
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
        Task CreateMessage(Message message);
        Task<IEnumerable<Room>> GetRooms();
        Task<RoomViewModel> GetRoomWithMessages(string roomId);
        Task<IEnumerable<Message>> GetRoomMessages(string roomId);
        
        Task<bool> DeleteRoom(string id);
    }
}
