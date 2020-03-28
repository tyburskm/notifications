using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Models
{
    public class Parameters
    {
        public string Title { get; set; }
        public string NotificationText { get; set; }
        public string BgColor { get; set; }
        public string BgImage { get; set; }
        public string TextColor { get; set; }
        public bool Maximized { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Autosize { get; set; }
        public string Gradient { get; set; }
    }
}
