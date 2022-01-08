using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Services
{
    public class GroupServices
    {
        private CommunicatorDbContext _context;
        public GroupServices(CommunicatorDbContext context)
        {
            _context = context;
        }
        public async Task<Group> CreateGroup(string _groupName, int? _parentGroupId, int _authorId)
        {
            var highestId = _context.Groups.AsQueryable().OrderByDescending(u => u.GroupId).FirstOrDefault().GroupId;
            highestId++;

            var group = new Group
            {
                GroupId = highestId,
                GroupName = _groupName,
                ParentGroupId = _parentGroupId,
                AuthorId = _authorId,
                CreationDate = DateTime.Now
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }
        public async Task<Group> GetGroupById(int _groupId)
        {
            var group = await _context.Groups.FindAsync(_groupId);
            return group;
        }
        public async Task<GroupMembership> AddUserToGroup(int _userId, int _groupId)
        {
            var groupMember = new GroupMembership
            {
                UserId = _userId,
                GroupId = _groupId
            };
            _context.GroupMemberships.Add(groupMember);
            await _context.SaveChangesAsync();
            return groupMember;
        }
    }
}
