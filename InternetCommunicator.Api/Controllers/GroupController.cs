using InternetCommunicator.Api.Services;
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

        [HttpPost("NewGroup")]
        public async Task<ActionResult<Group>> PostNewGroup(string groupName, int? parentGroupId, int authorId)
        {
            var groupServices = new GroupServices(_context);
            var newGroup = await groupServices.CreateGroup(groupName, parentGroupId, authorId);
            if (newGroup == null) return BadRequest("Nie udalo sie utworzyc grupy");
            return new ActionResult<Group>(newGroup);
        }
        [HttpGet("GetGroupById/{groupId}")]
        public async Task<ActionResult<Group>> GetGroupById(int groupId)
        {
            var groupServices = new GroupServices(_context);
            var group = await groupServices.GetGroupById(groupId);
            return group;
        }
        [HttpPost("newGroupMember")]
        public async Task<ActionResult<GroupMembership>> AddNewUserToGroup(int groupId, int userId)
        {
            var groupServices = new GroupServices(_context);
            var newGroupMembership = await groupServices.AddUserToGroup(userId, groupId);
            if (newGroupMembership == null) return BadRequest("Nie udalo sie dodac uzytkownika do grupy");
            return new ActionResult<GroupMembership>(newGroupMembership);
        }

    }
}
