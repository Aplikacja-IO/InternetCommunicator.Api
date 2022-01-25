using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using InternetCommunicator.Application.Dto.Group;
using InternetCommunicator.Application.Feature.Group;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Repository;
using Moq;
using Xunit;

namespace InternetCommunicator.UnitTests
{
    public class GetGroupQueryHandlerTests
    {
        private Mock<IGroupRepository> _mockGroupRepository;
        private GetGroupQueryHandler _getGroupQueryHandler;
        private List<Group> _listGroup;
        private const int _groupId = 1;
        private const string _groupName = "groupName";

        public GetGroupQueryHandlerTests()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
            _getGroupQueryHandler = new GetGroupQueryHandler(_mockGroupRepository.Object);
            _listGroup = new List<Group>() { new Group() { GroupId = _groupId, GroupName = _groupName } };
        }

        [Fact]
        public async Task Should_Return_Null()
        {
            // Arrange
            _mockGroupRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(null as List<Group>);

            // Act
            var request = new GetGroupQuery();
            var response = await _getGroupQueryHandler.Handle(request, new CancellationToken());

            // Assert

            response.Should().BeNull();
        }

        [Fact]
        public async Task Should_Return_GroupDto()
        {
            // Arrange
            _mockGroupRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(_listGroup);

            // Act
            var request = new GetGroupQuery();
            var response = await _getGroupQueryHandler.Handle(request, new CancellationToken());
            // Assert

            response.Should().NotBeNull();
            response.FirstOrDefault().Id = _groupId;
            response.FirstOrDefault().Name = _groupName;
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void GetGroupQueryHandler_When_IGroupRepositoryIsNull_Throw_ArgumentNullExceptionException()
        {
            // Arrange           

            // Act

            // Assert
            FluentActions.Invoking(() => new GetGroupQueryHandler(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
