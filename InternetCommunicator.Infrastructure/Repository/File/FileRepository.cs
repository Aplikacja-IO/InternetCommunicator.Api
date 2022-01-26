using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class FileRepository : BaseRepository<Image>, IFileRepository
    {
        public FileRepository(CommunicatorDbContext context)
            : base(context)
        { }
    }
}