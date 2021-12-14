using InternetCommunicator.Application.Dto;
using MediatR;

namespace InternetCommunicator.Application.Feature.Comment
{
    public class GetCommentQuery : IRequest<CommentDto>
    {
        public int Id { get; set; }
        public GetCommentQuery(int id)
        {
            Id = id;
        }
    }
}
