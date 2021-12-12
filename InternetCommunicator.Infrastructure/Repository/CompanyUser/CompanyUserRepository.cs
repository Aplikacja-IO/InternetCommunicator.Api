using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    class CompanyUserRepository : BaseRepository<CompanyUser>, ICompanyUserRepository
    {
        public CompanyUserRepository(CommunicatorDbContext context)
            : base(context)
        { }


    }
}
