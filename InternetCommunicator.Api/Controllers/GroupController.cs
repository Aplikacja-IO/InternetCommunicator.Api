using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternetCommunicator.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly CommunicatorDbContext _context;

        public GroupController(CommunicatorDbContext context)
        {
            _context = context;
        }
        // GET: api/<GroupController>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> GetGroupMembers(int id)
        {
            var query = from member in _context.GroupMemberships
                        join user in _context.RegisterUsers
                        on member.UserId equals user.UserId
                        select user;

          return await query.ToListAsync();
        }

    }
}
