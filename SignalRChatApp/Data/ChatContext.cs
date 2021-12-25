using MongoDB.Driver;
using SignalRChatApp.Entities;
using SignalRChatApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Data
{
    public class ChatContext:IChatContext
    {
        public ChatContext(IChatDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Messages = database.GetCollection<Message>(nameof(Message));
            Rooms = database.GetCollection<Room>(nameof(Room));
            ChatContextSeed.SeedData(Rooms);
        }

        public IMongoCollection<Message> Messages { get; }

        public IMongoCollection<Room> Rooms { get; }
    }
}
