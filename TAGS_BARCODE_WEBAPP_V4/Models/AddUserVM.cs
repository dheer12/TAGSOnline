using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    public class AddUserVM
    {
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string USER_ROLE { get; set; }
    }
}