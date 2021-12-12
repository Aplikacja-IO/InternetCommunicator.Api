using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InternetCommunicator.Application.Dto.Post;
using InternetCommunicator.Domain.Models;
using MediatR;
using InternetCommunicator.Application.Feature.Post;

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]/post")]
    [ApiController]
    public class PostControler : BaseController
    {
        public PostControler(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet]
        [Route("all", Name = "GetAll")]
        public async Task<PostDto> GetAll([FromQuery] int id)
        {
            var command = new GetPosQuery(id);
            var result = await Mediator.Send(command).ConfigureAwait(false);
            return result;
        }

    }
}
