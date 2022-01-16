using InternetCommunicator.Api.Services;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private CommunicatorDbContext _context;
        public ChatHub(CommunicatorDbContext context)
        {
            _context = context;
            
        }

        public string GetConnectionId() => Context.ConnectionId; //ConnectionId - unique ID thta SignalR gives to every client
        public string GetUserName() => Context.User.Identity.Name;
        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];

            await Groups.AddToGroupAsync(GetConnectionId(), $"user_{userId}");
            await Clients.Group($"user_{userId}").SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendPost(string authorIdString, string parentGroupIdString, string postText)
        {
            int authorId = Int32.Parse(authorIdString);
            int parentGroupId = Int32.Parse(parentGroupIdString);

            var componentServices = new ComponentServices(_context);
            var newPost = await componentServices.CreatePost(authorId, parentGroupId, postText);
            if(newPost != null)
            {
                await NotifyGroup(newPost);
                await NotifyAuthorAboutSuccess(newPost);
            }
            await NotifyAuthorAboutFailure(authorId);

        }

        private async Task NotifyAuthorAboutFailure(int authorId)
        {
            await Clients.Group($"user_{authorId}").SendAsync("PostAddingFailure", "failure");
        }

        private async Task NotifyAuthorAboutSuccess(Component newPost)
        {
            var authorId = newPost.AuthorId;
            var postText = newPost.Post.PostText;
            await Clients.Group($"user_{authorId}").SendAsync("PostAddingSuccess", postText);
        }

        private async Task NotifyGroup(Component newPost)
        {
            var parentGroupId = newPost.ParentGroupId;
            var groupServices = new GroupServices(_context);
            var groupMembers = await groupServices.GetAllGroupMembers(parentGroupId);

            foreach(var user in groupMembers)
            {
                await NotifyUserAboutMessage(user, newPost);
            }
        }

        private async Task NotifyUserAboutMessage(RegisterUser user, Component newPost)
        {
            var userId = user.UserId;
            var userName = user.UserName;
            var postText = newPost.Post.PostText;
            await Clients.Group($"user_{userId}").SendAsync("NewMessageNotify", userName, postText);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendPrivateMessage(string toUser, string fromUserId, string message)
        {
            await Clients.Group($"user_{fromUserId}").SendAsync("notify", message);
        }
    }
}
