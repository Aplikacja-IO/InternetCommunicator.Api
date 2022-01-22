using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetCommunicator.Application.Dto.Group;
using InternetCommunicator.Application.Feature.Comment;
using InternetCommunicator.Application.Feature.Group;
using MediatR;

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]/group")]
    [ApiController]
    public class GroupController : BaseController
    {
        public GroupController(IMediator mediator) : base(mediator)
        { }


        [HttpGet]
        [Route("all", Name = "GetAll")]
        public async Task<IEnumerable<GroupDto>> GetAll()
        {
            var command = new GetGroupQuery();
            var result = await Mediator.Send(command).ConfigureAwait(false);
            return result;
        }
    }
}
