using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRChatApp.Entities;
using SignalRChatApp.Hubs;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;
using SignalRChatApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatRepository _chatRepository;
        

        public ChatController(ILogger<ChatController> logger,IChatRepository chatRepository)
        {
            _logger = logger;
            _chatRepository = chatRepository;
        }

        public IActionResult Index()
        {
           
            HttpContext.Session.TryGetValue("username", out byte[] value);
            string username =value!=null? Encoding.UTF8.GetString(value):"";

            ViewBag.Username = username;
            var rooms = _chatRepository.GetRooms();
            return View(rooms.Result);
        }
        public IActionResult Room(string id)
        {
            var rooms = _chatRepository.GetRoomWithMessages(id);
            RoomViewModel model = new RoomViewModel();
            model.Room = rooms.Result;
            model.Rooms = _chatRepository.GetRooms().Result.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateRoom(string name)
        {
            _chatRepository.CreateRoom(name);
            return RedirectToAction("Index");
        }
        public IActionResult SetUserName(string username)
        {
            HttpContext.Session.Set("username", Encoding.UTF8.GetBytes(username));

            return Ok();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> SendMessage(string roomId,string message, [FromServices] IHubContext<ChatHub> chat)
        {
            roomId = roomId.Trim();
            HttpContext.Session.TryGetValue("username", out byte[] value);
            string username = value != null ? Encoding.UTF8.GetString(value) : "";

            var msg = new Message { 
                MessageText = message,
                UserName = username,
                Time = DateTime.Now
            }; 
            await _chatRepository.CreateMessage(msg,roomId);

            await chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = msg.MessageText,
                    Name = msg.UserName,
                    Timestamp = msg.Time.ToString("dd/MM/yyyy hh:mm:ss")
                });

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
