using InternetCommunicator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        public tmpDb database { get; set; }
        public UserController()
        {
            database = tmpDb.GetInstance();
            tmpDb.SetNumerOfUsersTo(20);
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
    }
}
