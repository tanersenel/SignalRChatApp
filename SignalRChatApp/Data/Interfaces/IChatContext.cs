using MongoDB.Driver;
using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Data
{
    public interface IChatContext
    {
        IMongoCollection<Room> Rooms { get; }
    }
}
