using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TimeTrackerApi.Models;
using TimeTrackerBackend.Repositories;

namespace TimeTrackerApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AccountController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok(new { Message = "Up and running!" });
        }
    }
}
