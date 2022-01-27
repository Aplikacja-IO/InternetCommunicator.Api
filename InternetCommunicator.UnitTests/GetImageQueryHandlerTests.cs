using System;
using InternetCommunicator.Application.Feature.Image;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Repository;
using Moq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace InternetCommunicator.UnitTests
{
    public class GetImageQueryHandlerTests
    {
        private Mock<IFileRepository> _mockFileRepository;
        private GetImageQueryHandler _getImageQueryHandler;
        private Image _image;
        private const int _componentId = 1;
        private const string _imageUrl = "https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg";

        public GetImageQueryHandlerTests()
        {
            _mockFileRepository = new Mock<IFileRepository>();
            _getImageQueryHandler = new GetImageQueryHandler(_mockFileRepository.Object);
            _image = new Image()
            {
                ComponentId = _componentId,
                ImageUrl = _imageUrl,
                ByteArray = UploadImageToByteArray(_imageUrl)
            };
        }

        private byte[] UploadImageToByteArray(string imageUrl)
        {
            using var webClient = new WebClient();
            return webClient.DownloadData(imageUrl);
        }

        [Fact]
        public async Task Should_Return_Null()
        {
            // Arrange
            _mockFileRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as Image);

            // Act
            var request = new GetImageQuery(_componentId);
            var response = await _getImageQueryHandler.Handle(request, new CancellationToken());
            // Assert

            response.Should().BeNull();
        }

        [Fact]
        public async Task Should_Return_ImageDto()
        {
            // Arrange
            _mockFileRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_image);

            // Act
            var request = new GetImageQuery(_componentId);
            var response = await _getImageQueryHandler.Handle(request, new CancellationToken());
            // Assert

            response.Should().NotBeNull();
            response.ComponentId.Should().Be(_image.ComponentId);
            response.ImageUrl.Should().Be(_image.ImageUrl);
            response.ByteArray.Equals(_image.ByteArray);
        }

        [Fact]
        public void GetImageQueryHandler_When_IFileRepositoryIsNull_Throw_ArgumentNullExceptionException()
        {
            // Arrange           

            // Act

            // Assert
            FluentActions.Invoking(() => new GetImageQueryHandler(null)).Should().Throw<ArgumentNullException>();
        }

    }
}
