using InternetCommunicator.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InternetCommunicator.Application.Feature.Comment;

namespace InternetCommunicator.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        public CommentController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        [Route("all", Name = "GetAll")]
        public async Task<CommentDto> GetAll([FromQuery] int id)
        {
            var command = new GetCommentQuery(id);
            var result = await Mediator.Send(command).ConfigureAwait(false);
            return result;
        }
    }
}
