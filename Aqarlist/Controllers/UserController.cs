using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Interface;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aqarlist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApiDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IUserService userService, ApiDbContext db, IHttpClientFactory httpClientFactory) 
        {
            _userService = userService;
            _db = db;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("roles/all")]
        public IActionResult GetAllRoles()
        {
            return Ok(_userService.GetAllRoles());
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
        [HttpGet("signIn-with-google")]
        public async Task<IActionResult> SignInWithGoogle(string token) 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={token}");

            if (!response.IsSuccessStatusCode) return Unauthorized("Invalid Google token");

            var payload = await response.Content.ReadFromJsonAsync<GoogleUser>();

            var user = new UserDto
            {
                EmailAddress = payload.Email,
                Name = payload.Name
            }; ;
            return Ok(_userService.Create(user));
        }
    }
}
