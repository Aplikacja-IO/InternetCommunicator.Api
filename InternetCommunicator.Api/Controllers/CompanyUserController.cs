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
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyUserController : ControllerBase
    {
        public tmpDb database { get; set; }
        public CompanyUserController()
        {
            database = tmpDb.GetInstance();

            tmpDb.SetNumerOfUsersTo(10);
            tmpDb.SetPercenOfCompanyUsersTo(50);
        }

        [HttpGet("{id}")]
        public CompanyUser GetUserById(int id)
        {
            var allUsers = database.GetAllCompanyUsers();
            var user = allUsers.Where(user => user.UserId == id);

            return user.FirstOrDefault();
        }
        [HttpGet]
        public IEnumerable<CompanyUser> GetAllCompanyUsers()
        {
            return database.GetAllCompanyUsers();
        }
        [HttpDelete]
        public bool DeleteCompanyUserById(int id)
        {
            return database.RemoveUserById(id);
        }
        [HttpPost]
        public bool PostCompanyUser(string firstName, string lastName, string login, string password, int ownerId)
        {
            var allUsers = database.GetAllUsers();
            var owner = allUsers.Where(user => user.UserId == ownerId).FirstOrDefault();
            var bytePassword = Encoding.ASCII.GetBytes(password);
            return database.CreateNewUser(firstName, lastName, login, bytePassword, owner);
        }
    }
}
