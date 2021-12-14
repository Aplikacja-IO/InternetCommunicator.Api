using InternetCommunicator.Application.Dto.Post;
using InternetCommunicator.Application.Feature.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]/post")]
    [ApiController]
    public class PostControler : BaseController
    {
        public PostControler(IMediator mediator) : base(mediator)
        { }


        [HttpGet]
        [Route("all", Name = "GetAll")]
        public async Task<PostDto> GetAll([FromQuery] int id)
        {
            var command = new GetPostQuery(id);
            var result = await Mediator.Send(command).ConfigureAwait(false);
            return result;
        }

    }
}
