using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class Logs
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
