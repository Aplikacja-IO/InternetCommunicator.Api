using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class Group
    {
        public Group()
        {
            Components = new HashSet<Component>();
            GroupMemberships = new HashSet<GroupMembership>();
            InverseParentGroup = new HashSet<Group>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? ParentGroupId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual RegisterUser Author { get; set; }
        public virtual Group ParentGroup { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<GroupMembership> GroupMemberships { get; set; }
        public virtual ICollection<Group> InverseParentGroup { get; set; }
    }
}
