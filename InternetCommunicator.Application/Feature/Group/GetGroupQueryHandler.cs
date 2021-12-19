using InternetCommunicator.Application.Dto.Group;
using InternetCommunicator.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Feature.Group
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, IEnumerable<GroupDto>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        }

        public async Task<IEnumerable<GroupDto>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.GetAllAsync().ConfigureAwait(false);
            return result?.Select(x => new GroupDto
            {
                Id = x.GroupId, 
                Name = x.GroupName, 
                AuthorId = x.AuthorId,
                CreationDate = x.CreationDate,
                ParentGroupId = x.ParentGroupId
            });
        }
    }
}
