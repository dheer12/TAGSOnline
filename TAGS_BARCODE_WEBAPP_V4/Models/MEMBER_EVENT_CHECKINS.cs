namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEMBER_EVENT_CHECKINS
    {
        [Key]
        public int EVENT_CHECKIN_ID { get; set; }

        public int EVENT_ID { get; set; }

        public int MEMBER_ID { get; set; }

        public bool? IS_CHECKEDIN { get; set; }

        public bool? IS_PAID { get; set; }

        public int? UESR_ID { get; set; }

        public virtual TAGS_EVENTS TAGS_EVENTS { get; set; }

        public virtual TAGS_LOGIN TAGS_LOGIN { get; set; }

        public virtual TAGS_MEMBER TAGS_MEMBER { get; set; }
    }
}
