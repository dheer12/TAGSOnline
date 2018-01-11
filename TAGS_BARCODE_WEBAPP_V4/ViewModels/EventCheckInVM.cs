using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGS_BARCODE_WEBAPP_V4.ViewModels
{
    public class EventCheckInVM
    {
        public int? EVENT_CHECKIN_ID { get; set; }

        public int? EVENT_ID { get; set; }

        public int? MEMBER_ID { get; set; }

        public bool? IS_CHECKEDIN { get; set; }

        public bool? IS_PAID { get; set; }

        public int? UESR_ID { get; set; }
    }

    public class MemberVM
    {
        public int? MEMBER_ID { get; set; }

        public string FIRST_NAME { get; set; }

        public string LAST_NAME { get; set; }

        public string COMPANY_NAME { get; set; }

        public string EMAIL_ID { get; set; }

        public string CELL_PHONE { get; set; }

        public string HOME_PHONE { get; set; }

        public bool? IS_VOLUNTEER { get; set; }

        public bool MemberNotFound { get; set; }

        public bool IsNewMember { get; set; }
    }

    public class MemberCheckInVM
    {
        public EventCheckInVM eventCheckInVM { get; set; }

        public MemberVM newMember { get; set; }

        public bool IsExistingMember { get; set; }

        public bool IsRegistered { get; set; }
    }
}