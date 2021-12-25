using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var rooms = _chatRepository.GetRooms();
            return View(rooms.Result);
        }
        public IActionResult Room(string id)
        {
            var rooms = _chatRepository.GetRoomWithMessages(id);
            return View(rooms.Result);
        }
        [HttpPost]
        public IActionResult CreateRoom(string name)
        {
            _chatRepository.CreateRoom(name);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
