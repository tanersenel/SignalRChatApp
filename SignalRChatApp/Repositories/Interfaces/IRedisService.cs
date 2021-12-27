using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Repositories.Interfaces
{
    public interface IRedisService
    {
        List<Room> GetAll(string cachekey);
        Room GetById(string cachekey);
    }
}
