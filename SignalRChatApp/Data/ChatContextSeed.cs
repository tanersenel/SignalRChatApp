using MongoDB.Bson;
using MongoDB.Driver;
using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Data
{
    public class ChatContextSeed
    {
        public static void SeedData(IMongoCollection<Room> contactCollection)
        {
            bool existContact = contactCollection.Find(c => true).Any();
            if (!existContact)
            {
                contactCollection.InsertManyAsync(GetConfigureRooms());
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
