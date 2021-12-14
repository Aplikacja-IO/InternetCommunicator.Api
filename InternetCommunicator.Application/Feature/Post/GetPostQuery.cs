using InternetCommunicator.Application.Dto.Post;
using MediatR;

namespace InternetCommunicator.Application.Feature.Post
{
    public class GetPostQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
        public GetPostQuery(int id)
        {
            Id = id;
        }
    }
}
