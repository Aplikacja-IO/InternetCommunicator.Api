using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using InternetCommunicator.Application.Dto;
using InternetCommunicator.Application.Feature.Comment;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Repository;
using Moq;
using Xunit;

namespace InternetCommunicator.UnitTests
{
    public class GetCommentQueryHandlerTests
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private GetCommentQueryHandler _getCommentQueryHandler;
        private Comment _comment;
        private const int _id = 1;
        private const int _sourceId = 2;
        private const string _postText = "PostText";

        public GetCommentQueryHandlerTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _getCommentQueryHandler = new GetCommentQueryHandler(_mockCommentRepository.Object);
            _comment = new Comment(){ComponentId = _id, PostText = _postText, SourceId = _sourceId};
        }

        [Fact]
        public async Task Should_Return_Null()
        {
            // Arrange
            _mockCommentRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as Comment);

            // Act
            var request = new GetCommentQuery(_id);
            var response = await _getCommentQueryHandler.Handle(request, new CancellationToken());
            // Assert

            response.Should().BeNull();
       
        }

        [Fact]
        public async Task Should_Return_CommentDto()
        {
            // Arrange
            _mockCommentRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_comment);

            // Act
            var request = new GetCommentQuery(_id);
            var response = await _getCommentQueryHandler.Handle(request, new CancellationToken());
            // Assert

            response.Should().NotBeNull();
            response.PostText.Should().Be(_postText);
            response.SourceId.Should().Be(_sourceId);
        }
    }
}
