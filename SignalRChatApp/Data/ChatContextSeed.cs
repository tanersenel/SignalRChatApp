using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack.Redis;
using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Data
{
    public class ChatContextSeed
    {
        public static void SeedData(IMongoCollection<Room> roomCollection)
        {
            bool existContact = roomCollection.Find(c => true).Any();
            if (!existContact)
            {
                roomCollection.InsertManyAsync(GetConfigureRooms());
            }
            using (IRedisClient client = new RedisClient())
            {
                foreach (var room in roomCollection.AsQueryable())
                {
                    var cachedata = client.As<Room>();
                    cachedata.SetValue("Room-" + room.id, room);
                }
                
            }
        }

        private static IEnumerable<Room> GetConfigureRooms()
        {
            return new List<Room>()
           {
               new Room
               {
                   Name="Room 1",
                   Messages=new List<Message>(){ new Message {UserName="Admin", MessageText="Welcome To Room1",Time=DateTime.Now, id = ObjectId.GenerateNewId()  } }
                 
               },
               new Room
               {
                   Name ="Room 2",
                    Messages=new List<Message>(){ new Message { UserName = "Admin", MessageText ="Welcome To Room2",Time=DateTime.Now, id = ObjectId.GenerateNewId() } }
               },
                new Room
               {
                   Name ="Room 3",
                  Messages=new List<Message>(){ new Message { UserName = "Admin", MessageText ="Welcome To Room3",Time=DateTime.Now, id = ObjectId.GenerateNewId() } }
               }
           };
        }
    }
}
