using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class ComponentSubscriberRepository : BaseRepository<ComponentSubscriber>, IComponentSubscriberRepository
    {
        public ComponentSubscriberRepository(CommunicatorDbContext context)
            : base(context)
        { }
    }
}
