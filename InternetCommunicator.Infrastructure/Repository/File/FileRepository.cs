using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    class FileRepository : BaseRepository<Image>, IFileRepository
    {
        public FileRepository(CommunicatorDbContext context) 
            : base(context)
        { }
    }
}
