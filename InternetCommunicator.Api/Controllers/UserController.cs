using InternetCommunicator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetCommunicator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public RegisterUser GetUserById(int id)
        {

        }
        public IEnumerable<RegisterUser> GetAllUsers()
        {

        }
    }
}
