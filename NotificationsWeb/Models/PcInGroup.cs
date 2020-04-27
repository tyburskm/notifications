using System;
using System.Collections.Generic;

namespace NotificationsWeb.Models
{
    public partial class PcInGroup
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int Pcid { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Pcs Pc { get; set; }
    }
}
