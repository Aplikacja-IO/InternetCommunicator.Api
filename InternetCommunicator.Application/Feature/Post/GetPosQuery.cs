using InternetCommunicator.Application.Dto.Post;
using MediatR;

namespace InternetCommunicator.Application.Feature.Post
{
    public class GetPosQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
        public GetPosQuery(int id)
        {
            Id = id;
        }
    }
}
