using InternetCommunicator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RegisterUserController : ControllerBase
    {
        private readonly CommunicatorDBContext _context;

        public RegisterUserController(CommunicatorDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> GetAllUsers()
        {
            return await _context.RegisterUsers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterUser>> GetUserById(int id)
        {
            var users = await _context.RegisterUsers.FindAsync(id);

            if (users == null) return NotFound();
            return users;
        }
        [HttpPost]
        public async Task<ActionResult<RegisterUser>> PostRegisterUser(string login, string password)
        {
            var highestId = _context.RegisterUsers.AsQueryable().OrderByDescending(u => u.UserId).FirstOrDefault().UserId;
            highestId++;

            var bytePassword = Encoding.ASCII.GetBytes(password);
            var user = new RegisterUser
            {
                UserId = highestId,
                UserName = login,
                UserPassword = bytePassword,
                RegisterDate = DateTime.Now
            };
            _context.RegisterUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllUsers", new { id = user.UserId }, user);
        }
    }
}
