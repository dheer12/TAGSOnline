using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    public class CheckInVM
    {
        public int TicketNo { get; set; }

        public bool IsCheckedIn { get; set; }

        public bool TicketNotFound { get; set; }

        public bool AlreadyCheckedIn { get; set; }
    }
}