using InternetCommunicator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]

    public class RegisterUserController : ControllerBase
    {
        public tmpDb database { get; set; }
        public RegisterUserController()
        {
            database = tmpDb.GetInstance();

            tmpDb.SetNumerOfUsersTo(10);
            tmpDb.SetPercenOfCompanyUsersTo(50);
        }

        [HttpGet("{id}")]
        public RegisterUser GetUserById(int id)
        {
            var allUsers = database.GetAllUsers();
            var user = allUsers.Where(user => user.UserId == id);

            return user.FirstOrDefault();
        }

        [HttpGet]
        public IEnumerable<RegisterUser> GetAllUsers()
        {
            return database.GetAllUsers();
        }

        [HttpDelete] 
        public bool DeleteRegisterUser(int id)
        {
            return database.RemoveUserById(id);
        }

        [HttpPost]
        public bool PostRegisterUser(string login, string password)
        {
            var bytePassword = Encoding.ASCII.GetBytes(password);
            return database.CreateNewUser(login, bytePassword);
        }
    }
}
