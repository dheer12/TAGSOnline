using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGS_BARCODE_WEBAPP_V4.ViewModels
{
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

    public class SearchMemberVM
    {
        public string email { get; set; }
    }
}