using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using LifeManager.Rest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LifeManager.Rest.Controllers
{    
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;        

        public AuthController(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/auth/register")]        
        public async Task<object> Register([FromBody] UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest();
            }
            var user = new User
            {
                UserName = userModel.UserName
            };
            var userCreation = await _userManager.CreateAsync(user, userModel.Password);
            var jwtToken = GenerateJwtToken(user);
            if (userCreation.Succeeded)
            {
                return Ok(new { token = jwtToken });
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost] 
        [Route("Authenticate")]
        public IActionResult Authenticate(string userName, string password)
        {            
            return Ok();
        }

        [Authorize]
        [Route("/auth/test")]
        public IActionResult Test()
        {
            return Ok();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
