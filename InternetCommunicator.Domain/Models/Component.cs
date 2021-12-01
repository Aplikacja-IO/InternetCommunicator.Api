using System;
using System.Collections.Generic;

#nullable disable

namespace InternetCommunicator.Domain.Models
{
    public partial class Component
    {
        public Component()
        {
            CommentSources = new HashSet<Comment>();
            ComponentSubscribers = new HashSet<ComponentSubscriber>();
        }

        public int ComponentId { get; set; }
        public int ParentGroupId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual RegisterUser Author { get; set; }
        public virtual Group ParentGroup { get; set; }
        public virtual Comment CommentComponent { get; set; }
        public virtual Image Image { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Comment> CommentSources { get; set; }
        public virtual ICollection<ComponentSubscriber> ComponentSubscribers { get; set; }
    }
}
