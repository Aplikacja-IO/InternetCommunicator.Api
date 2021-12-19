using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCommunicator.Application.Dto.Group
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentGroupId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
