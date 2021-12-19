using InternetCommunicator.Application.Dto.Group;
using MediatR;
using System.Collections.Generic;

namespace InternetCommunicator.Application.Feature.Group
{
    class GetGroupQuery : IRequest<IEnumerable<GroupDto>>
    {
    }
}
