using InternetCommunicator.Application.Dto.Post;
using InternetCommunicator.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace InternetCommunicator.Application.Feature.Post
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;

        public GetPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var result = await _postRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            return new PostDto
            {
                ComponentId = result.ComponentId,
                Text = result.PostText,
                Created = DateTime.Now
            };
        }
    }
}
