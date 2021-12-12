using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class GroupMembershipRepository : BaseRepository<GroupMembership>, IGroupMembershipRepository
    {
        public GroupMembershipRepository(CommunicatorDbContext context)
            : base(context)
        { }
    }
}
