using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Dto.Post
{
    public class PostDto
    {
        public int ComponentId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

    }
}
