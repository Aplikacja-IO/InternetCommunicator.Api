using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetCommunicator.Domain.Models;

namespace InternetCommunicator.Infrastructure.Repository
{
    interface ICommentRepository 
    {
        public Task<Comment> GetByIdAsync(int id);
    }
}
