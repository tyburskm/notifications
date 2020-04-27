using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class Groups
    {
        public Groups()
        {
            NotificationInGroup = new HashSet<NotificationInGroup>();
            PcInGroup = new HashSet<PcInGroup>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<NotificationInGroup> NotificationInGroup { get; set; }
        public virtual ICollection<PcInGroup> PcInGroup { get; set; }
    }
}
