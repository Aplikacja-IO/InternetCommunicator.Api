using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    [DataContract]
    public partial class RegisterUser
    {
        public RegisterUser()
        {
            CompanyUserOwners = new HashSet<CompanyUser>();
            ComponentSubscribers = new HashSet<ComponentSubscriber>();
            Components = new HashSet<Component>();
            GroupMemberships = new HashSet<GroupMembership>();
            Groups = new HashSet<Group>();
        }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        public byte[] UserPassword { get; set; }
        [DataMember]
        public DateTime RegisterDate { get; set; }

        public virtual CompanyUser CompanyUserUser { get; set; }
        public virtual ICollection<CompanyUser> CompanyUserOwners { get; set; }
        public virtual ICollection<ComponentSubscriber> ComponentSubscribers { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<GroupMembership> GroupMemberships { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
