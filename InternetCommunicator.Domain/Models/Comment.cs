using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class Comment
    {
        public int ComponentId { get; set; }
        public string PostText { get; set; }
        public int SourceId { get; set; }

        public virtual Component Component { get; set; }
        public virtual Component Source { get; set; }
    }
}
