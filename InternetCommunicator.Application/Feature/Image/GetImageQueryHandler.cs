using InternetCommunicator.Application.Dto.Image;
using InternetCommunicator.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Feature.Image
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, ImageDto>
    {
        private readonly IFileRepository _fileRepository;

        public GetImageQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        public async Task<ImageDto> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var result = await _fileRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (result == null) return null;

            return new ImageDto
            {
                ComponentId = result.ComponentId,
                ImageUrl = result.ImageUrl,
                ByteArray = result.ByteArray
            };
        }
    }
}
