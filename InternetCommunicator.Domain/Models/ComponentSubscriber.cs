using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class ComponentSubscriber
    {
        public int ComponentId { get; set; }
        public int UserId { get; set; }

        public virtual Component Component { get; set; }
        public virtual RegisterUser User { get; set; }
    }
}
