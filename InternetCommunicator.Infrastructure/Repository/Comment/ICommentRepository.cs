using InternetCommunicator.Domain.Models;
using System.Threading.Tasks;

namespace InternetCommunicator.Infrastructure.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        public Task<Comment> GetByIdAsync(int id);
    }
}
