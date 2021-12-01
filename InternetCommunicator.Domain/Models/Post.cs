using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class Post
    {
        public int ComponentId { get; set; }
        public string PostText { get; set; }

        public virtual Component Component { get; set; }
    }
}
