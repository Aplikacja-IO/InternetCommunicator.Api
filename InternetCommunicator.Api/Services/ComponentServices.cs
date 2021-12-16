using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Services
{
    public class ComponentServices
    {
        private CommunicatorDbContext _context;

        public ComponentServices(CommunicatorDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(int authorId, int parentGroupId, string postText)
        {
            int newPostId;
            var componentsList = _context.Components.AsQueryable().OrderByDescending(c => c.ComponentId).ToList();
            if (componentsList.Count < 1) newPostId = 0;
            else
            {
                newPostId = componentsList.First().ComponentId;
                newPostId++;
            }

            var newComponent = new Component
            {
                ComponentId = newPostId,
                ParentGroupId = parentGroupId,
                AuthorId = authorId,
                CreationDate = DateTime.Now,
            };
            var newPost = new Post
            {
                ComponentId = newComponent.ComponentId,
                PostText = postText,
            };
            _context.Components.Add(newComponent);
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return newPost;
        }
    }
}
