using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public MessageController(EFContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("getChat/{id}")]
        public IActionResult Get(int id)
        {
            List<ChatModel> chatModels = new List<ChatModel>();
            foreach (var chat in _context.ChatUsers.Where(t=>t.IdUser==id).ToList())
            {
                ChatModel chatModel = new ChatModel()
                {
                    Id = chat.ChatOf.Id,
                    Name = chat.ChatOf.Name
                };
                chatModels.Add(chatModel);
            }
            string json = JsonConvert.SerializeObject(chatModels);

            return Content(json, "application/json");
        }

        [HttpGet("getMessage/{id}")]
        public IActionResult GetMessage(int id)
        {
            List<MessageModel> messageModels = new List<MessageModel>();

                    foreach (var message in _context.Messages.Where(t => t.IdChat == id).ToList())
                    {
                        MessageModel messageModel = new MessageModel()
                        {
                            Context = message.Context,
                            Id = message.Id,
                            IdChat = message.IdChat,
                            IdUser = message.IdUser
                        };
                        messageModels.Add(messageModel);
                    }
                    string json = JsonConvert.SerializeObject(messageModels);

                    return Content(json, "application/json");

        }

        [HttpPost("PostMessage")]
        public IActionResult PostMessage([FromBody]MessageModel model)
        {

            _context.Add(new Message
            {
                Context = model.Context,
                IdChat = model.IdChat,
                IdUser = model.IdUser

            });
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost("PostChat/{list}")]
        public IActionResult PostChat([FromBody]ChatModel model, List<int> idUsers)
        {

            _context.Add



            _context.SaveChanges();
            return Ok();

        }




    }
}