using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RegisterUserController : ControllerBase
    {
        private readonly CommunicatorDbContext _context;

        public RegisterUserController(CommunicatorDbContext context)
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
            var userService = new UserServices(_context);
            var user = await userService.GetUserById(id);
            if (user == null) return NotFound();
            return user;
        }
        [HttpPost]
        public async Task<ActionResult<RegisterUser>> PostRegisterUser(string login, string password)
        {
            var userService = new UserServices(_context);
            var newUser = await userService.CreateNewUser(login, password);

            return CreatedAtAction("GetAllUsers", new { id = newUser.UserId }, newUser);
        }
    }
}
