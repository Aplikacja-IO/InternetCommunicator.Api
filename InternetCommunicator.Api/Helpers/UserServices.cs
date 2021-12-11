using InternetCommunicator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    public class UserServices
    {
        private CommunicatorDBContext _context;
        public UserServices(CommunicatorDBContext context)
        {
            _context = context;
        }
        public async Task<RegisterUser> CreateNewUser(string login, string password)
        {
            var highestId = _context.RegisterUsers.AsQueryable().OrderByDescending(u => u.UserId).FirstOrDefault().UserId;
            highestId++;

            var bytePassword = Encoding.ASCII.GetBytes(password);
            var user = new RegisterUser
            {
                UserId = highestId,
                UserName = login,
                UserPassword = bytePassword,
                RegisterDate = DateTime.Now
            };
            _context.RegisterUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
