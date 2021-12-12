using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly CommunicatorDbContext _context;

        public RegistrationController(CommunicatorDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<RegisterUser>> PostRegisterUser(string login, string password)
        {
            var userService = new UserServices(_context);
            var newUser = await userService.CreateNewUser(login, password);
            if (newUser == null) return BadRequest("Blad podczas tworzenia uzytkownika");
            return newUser;
        }
    }
}
