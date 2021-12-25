using MongoDB.Bson;
using MongoDB.Driver;
using SignalRChatApp.Data;
using SignalRChatApp.Entities;
using SignalRChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChatApp.Extensions;

namespace SignalRChatApp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IChatContext _context;
        public ChatRepository(IChatContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateMessage(Message message,string roomId)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.id, ObjectId.Parse(roomId));

            var update = Builders<Room>.Update
                .AddToSet(x => x.Messages, message);
            var updateResult = await _context.Rooms.UpdateOneAsync(filter, update);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;


        }

        public async Task CreateRoom(string name)
        {
            var room = new Room { Name = name, Messages = new List<Message>() { new Message {UserName="Admin", MessageText = "Welcome To " + name, Time = DateTime.Now, id = ObjectId.GenerateNewId() } } };
            await _context.Rooms.InsertOneAsync(room);
        }

        public async Task<bool> DeleteRoom(string id)
        {
           
            var filter = Builders<Room>.Filter.Eq(c => c.id,ObjectId.Parse(id));
            DeleteResult deleteResult = await _context.Rooms.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
       

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _context.Rooms.Find(c => true).ToListAsync();
        }

        public async Task<Room> GetRoomWithMessages(string roomId)
        {
            var id = ObjectId.Parse(roomId);
            var room =await _context.Rooms.Find(c => c.id == id).FirstOrDefaultAsync();
            return room;

        }
    }
}
