using ServiceStack.Redis;
using SignalRChatApp.Entities;
using SignalRChatApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Repositories
{
    public class RedisService : IRedisService
    {
        public List<Room> GetAll(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                List<Room> dataList = new List<Room>();
                List<string> allKeys = client.SearchKeys(cachekey);
                foreach (string key in allKeys)
                {
                    dataList.Add(client.Get<Room>(key));
                }
                return dataList;
            }
        }

        public Room GetById(string cachekey)
        {
            using (IRedisClient client = new RedisClient())
            {
                var redisdata = client.Get<Room>(cachekey);

                return redisdata;
            }
        }
    }
}
