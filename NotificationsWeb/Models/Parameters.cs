using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class Parameters
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string NotificationText { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundImage { get; set; }
        public string TextColor { get; set; }
        public bool? Maximized { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool? Autosize { get; set; }
        public string Gradient { get; set; }

        public virtual Notifications Notification { get; set; }
    }
}
