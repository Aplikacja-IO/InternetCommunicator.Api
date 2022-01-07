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
        [HttpGet("GroupMembers/{groupId}")]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> GetGroupMembers(int groupId)
        {
            var query = from member in _context.GroupMemberships
                        join groups in _context.Groups on member.GroupId equals groups.GroupId
                        join users in _context.RegisterUsers on member.UserId equals users.UserId
                        select users;

          return await query.ToListAsync();
        }

        [HttpGet("AllUserGroups/{userId}")]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllUserGroups(int userId)
        {
            var query = from groupsMembership in _context.GroupMemberships
                        join groups in _context.Groups on groupsMembership.GroupId equals groups.GroupId
                        where groupsMembership.UserId == userId
                        select groups;

            return await query.ToListAsync();
        }
    }
}
