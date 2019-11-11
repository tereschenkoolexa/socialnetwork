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
    public class WallController : ControllerBase
    {

        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public WallController(EFContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            foreach (var wall in _context.Walls.ToList())
            {
                if (wall.IdUser == id)
                {
                    WallModel model = new WallModel()
                    {
                        Age = wall.Age,
                        City = wall.City,
                        Country = wall.Country,
                        Status = wall.Status,
                        ImageAvatar = wall.ImageAvatar,

                    };
                    string json = JsonConvert.SerializeObject(model);
                    return Content(json, "application/json");
                }
            }
            return BadRequest();
        }

        [HttpPost("newwall")]
        public IActionResult NewWall([FromBody]WallModel model)
        {

            _context.Walls.Add(new Wall
            {
                Age = model.Age,
                Country = model.Country,
                City = model.City,
                IdUser = model.IdUser
            });
            _context.SaveChanges();
            return Ok();

        }
    }
}