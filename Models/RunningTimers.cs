using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Notifications.Models
{
    public class RunningTimers
    {
        public DispatcherTimer Timer { get; set; }
        public int NotificationId { get; set; }
        public DateTime LastRunDate { get; set; }
    }
}
