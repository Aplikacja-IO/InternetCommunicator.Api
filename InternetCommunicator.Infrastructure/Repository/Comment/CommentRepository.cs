using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Infrastructure.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CommunicatorDbContext context)
        : base(context)
        { }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await DbContext.Comments
                .Where(x => x.SourceId == id)
                .AsNoTracking()
                .Select(x => new Comment
                {
                    SourceId = x.SourceId,
                    Component = x.Component,
                    ComponentId = x.ComponentId,
                    PostText = x.PostText,
                    Source = x.Source
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
