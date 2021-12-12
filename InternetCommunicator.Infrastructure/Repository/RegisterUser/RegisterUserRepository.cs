using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class RegisterUserRepository : BaseRepository<RegisterUser>, IRegisterUserRepository
    {
        public RegisterUserRepository(CommunicatorDbContext context)
            : base(context)
        { }
    }
}
