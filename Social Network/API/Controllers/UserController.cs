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
    public class UserController : ControllerBase
    {

        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public UserController(EFContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            
            foreach (var user in _context.Users.ToList())
            {
                if (user.Id == id)
                {
                    UserModel model = new UserModel()
                    {
                        Gmail = user.Gmail,
                        Name = user.Name,
                        Password = user.Password
                    };
                    
                    string json = JsonConvert.SerializeObject(model);
                    return Content(json, "application/json");
                }
            }
                return BadRequest();
        }

        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in _context.Users.ToList())
            {

                    UserModel model = new UserModel()
                    {
                        Id = user.Id,
                        Gmail = user.Gmail,
                        Name = user.Name,
                        Password = user.Password
                    };
                    userModels.Add(model);
            }
            string json = JsonConvert.SerializeObject(userModels);
            return Content(json, "application/json");

        }

        // POST api/user
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserModel model)
        {
            foreach (var user in _context.Users.ToList())
            {
                if(user.Gmail==model.Gmail && user.Password==model.Password)
                {
                    return Ok(user.Id);
                }
            }

            return BadRequest();
        }


        [HttpPost("signup")]
        public IActionResult Signup([FromBody]UserModel model)
        {

            _context.Add(new User { 
                Gmail = model.Gmail, 
                Name = model.Name, 
                Password = model.Password });
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost]
        public void Post([FromBody] string value)
        {



        }

    }
}