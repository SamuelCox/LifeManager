using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LifeManager.Rest.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IConfiguration _config;
        private UserManager<User> _userManager;

        public AuthController(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        [HttpPost]        
        public IActionResult Authenticate(string userName, string password)
        {
            return Ok();
        }
        
    }
}
