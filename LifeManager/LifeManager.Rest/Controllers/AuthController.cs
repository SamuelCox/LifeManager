﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using LifeManager.Rest.Models;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LifeManager.Rest.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserManagerWrapper _userManager;        


        public AuthController(IConfiguration config, IUserManagerWrapper userManager)
        {
            _config = config;
            _userManager = userManager;            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/register")]        
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest();
            }
            
            User user;
            user = await _userManager.FindByNameAsync(userModel.UserName);
            if (user != null)
            {
                return BadRequest();
            }
            user  = new User
            {
                UserName = userModel.UserName
            };
            var userCreation = await _userManager.CreateAsync(user, userModel.Password);            
            if (userCreation.Succeeded)
            {
                var jwtToken = GenerateJwtToken(user);
                return Ok(new { token = jwtToken });
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost] 
        [Route("api/auth/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserModel userModel)
        {
            if (!string.IsNullOrEmpty(userModel.UserName))
            {
                var user = await _userManager.FindByNameAsync(userModel.UserName);
                if (user != null)
                {
                    var correctPassword = await _userManager.CheckPasswordAsync(user, userModel.Password);
                    if (correctPassword)
                    {
                        var jwtToken = GenerateJwtToken(user);
                        return Ok(new {token = jwtToken});
                    }
                }
            }

            return BadRequest();
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
