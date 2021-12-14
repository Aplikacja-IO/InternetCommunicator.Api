using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class CompanyUserRepository : BaseRepository<CompanyUser>, ICompanyUserRepository
    {
        public CompanyUserRepository(CommunicatorDbContext context)
            : base(context)
        { }


    }
}
