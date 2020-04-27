using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class Pcs
    {
        public Pcs()
        {
            PcInGroup = new HashSet<PcInGroup>();
        }

        public int Id { get; set; }
        public string PcName { get; set; }

        public virtual ICollection<PcInGroup> PcInGroup { get; set; }
    }
}
