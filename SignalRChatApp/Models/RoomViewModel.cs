using SignalRChatApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Models
{
    public class RoomViewModel
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
    }
}
