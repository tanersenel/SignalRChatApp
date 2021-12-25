using MongoDB.Driver;
using SignalRChatApp.Data;
using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IChatContext _context;
        public ChatRepository(IChatContext context)
        {
            _context = context;
        }
        public async Task CreateMessage(Message message)
        {
           await _context.Messages.InsertOneAsync(message);
        }

        public async Task CreateRoom(string name)
        {
            var room = new Room { Name = name };
            await _context.Rooms.InsertOneAsync(room);
        }

        public async Task<bool> DeleteRoom(string id)
        {
            var mfilter = Builders<Message>.Filter.Eq(c => c.RoomId, id);
            await _context.Messages.DeleteOneAsync(mfilter);

            var filter = Builders<Room>.Filter.Eq(c => c.id, id);
            DeleteResult deleteResult = await _context.Rooms.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Message>> GetRoomMessages(string roomId)
        {
            return await _context.Messages.Find(c => c.RoomId == roomId).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _context.Rooms.Find(c => true).ToListAsync();
        }
    }
}
