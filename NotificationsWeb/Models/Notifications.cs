using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotificationsWeb.Models
{
    public partial class Notifications
    {
        public Notifications()
        {
            NotificationInGroup = new HashSet<NotificationInGroup>();
            Parameters = new HashSet<Parameters>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int Repeat { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? RunAtTime { get; set; }

        public virtual ICollection<NotificationInGroup> NotificationInGroup { get; set; }
        public virtual ICollection<Parameters> Parameters { get; set; }
    }
}
