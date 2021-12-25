using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Models
{
    public class RoomViewModel
    {
        public Room Room { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
