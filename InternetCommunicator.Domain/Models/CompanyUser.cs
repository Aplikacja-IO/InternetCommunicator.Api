using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class CompanyUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OwnerId { get; set; }

        public virtual RegisterUser Owner { get; set; }
        public virtual RegisterUser User { get; set; }
    }
}
