using InternetCommunicator.Application.Dto;
using InternetCommunicator.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Feature.Comment
{
    public class GetCommentQueryHandler: IRequestHandler<GetCommentQuery, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var result = await _commentRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (result == null) return null;

            return new CommentDto
            {
               SourceId = result.SourceId,
               ComponentId = result.ComponentId,
               PostText = result.PostText
            };
        }
    }
}
