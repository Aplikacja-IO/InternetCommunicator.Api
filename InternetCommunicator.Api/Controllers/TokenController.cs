﻿using InternetCommunicator.Api.Helpers;
using InternetCommunicator.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TokenController : Controller
    {
        private readonly CommunicatorDBContext _context;
        public IConfiguration _configuration;

        public TokenController(CommunicatorDBContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        private async Task<RegisterUser> GetUser(int id, string username, string password)
        {
            var bytePassword = Encoding.ASCII.GetBytes(password);
            return await _context.RegisterUsers.FirstOrDefaultAsync(
                u => u.UserId == id && u.UserName == username && u.UserPassword == bytePassword);
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserInfo userData)
        {
            if (userData != null && userData.Username != null && userData.Password != null)
            {
                var user = await GetUser(userData.UserId, userData.Username, userData.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Username", user.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Nieprawidlowe poswiadczenia");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
