using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Interface;
using Microsoft.AspNetCore.Mvc;

namespace Aqarlist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApiDbContext _db;
        public UserController(IUserService userService, ApiDbContext db) 
        {
            _userService = userService;
            _db = db;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
        [HttpPost("signIn")]
        public IActionResult Create(UserDto model)
        {
            return Ok(_userService.Create(model));
        }
        [HttpGet("login")]
        public IActionResult Login([FromQuery]string emailAddress, [FromQuery]string password)
        {
            return Ok(_userService.Login(emailAddress,password));
        }
    }
}
