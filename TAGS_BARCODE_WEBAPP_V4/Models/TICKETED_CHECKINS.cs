namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TICKETED_CHECKINS
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TICKET_NUMBER { get; set; }

        [StringLength(50)]
        public string TICKET_TYPE { get; set; }

        [StringLength(50)]
        public string SELLER { get; set; }

        public int? EVENT_ID { get; set; }

        public bool? STATION_1 { get; set; }

        public int? STATION_1_USER_ID { get; set; }

        public DateTime? STATION_1_CHECKIN_TIME { get; set; }

        public virtual TAGS_EVENTS TAGS_EVENTS { get; set; }

        public virtual TAGS_LOGIN TAGS_LOGIN { get; set; }
    }
}
