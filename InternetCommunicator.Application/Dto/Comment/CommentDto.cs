using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Dto
{
    public class CommentDto
    {
        public int ComponentId { get; set; }
        public string PostText { get; set; }
        public int SourceId { get; set; }
    }
}