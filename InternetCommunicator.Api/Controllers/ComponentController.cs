using InternetCommunicator.Api.Services;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    public class ComponentController : Controller
    {

        private readonly CommunicatorDbContext _context;

        public ComponentController(CommunicatorDbContext context)
        {
            _context = context;
        }

        // GET api/<ComponentController>/
        [HttpGet("GetComponentById/{postId}")]
        public async Task<ActionResult<Component>> GetPostById(int postId)
        {
            var componentServices = new ComponentServices(_context);
            var component = await componentServices.GetComponentByIdAsync(postId);
            if (component == null) return BadRequest("Brak postu o podanym ID");
            return new ActionResult<Component>(component);
        }

        
    }
}
