using System;
using System.Linq;
using InternetCommunicator.Application.Dto.Post;
using InternetCommunicator.Infrastructure.Repository;
using System.Threading;
using System.Threading.Tasks;


namespace InternetCommunicator.Application.Feature.Post
{
    public class GetPostQueryHandler
    {
        private IPostRepository _postRepository;

        public GetPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostDto> Handle(GetPosQuery request, CancellationToken cancellationToken)
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
