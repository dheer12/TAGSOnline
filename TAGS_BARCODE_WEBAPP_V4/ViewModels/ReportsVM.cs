using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGS_BARCODE_WEBAPP_V4.ViewModels
{
    public class ReportsVM
    {
        public int EVENT_ID { get; set; }

        public string EVENT_NAME { get; set; }

        public string EVENT_LOCATION { get; set; }

        public DateTime EVENT_DATE { get; set; }

        public bool? IS_TICKETED_EVENT { get; set; }

        public EventReportVM EventReports { get; set; }

        public List<TicketReportVM> TicketReports { get; set; }

    }

    public class EventReportVM
    {
        public int? checkIns { get; set; }

        public int? isPaids { get; set; }

        public int? registrations { get; set; }

    }

    public class TicketReportVM
    {
        public int? checkIns { get; set; }

        public string TicketType { get; set; }
    }
}