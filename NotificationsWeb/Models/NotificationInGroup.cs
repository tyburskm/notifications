using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class NotificationInGroup
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Notifications Notification { get; set; }
    }
}
