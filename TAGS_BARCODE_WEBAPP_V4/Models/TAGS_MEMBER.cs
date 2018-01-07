namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAGS_MEMBER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAGS_MEMBER()
        {
            MEMBER_EVENT_CHECKINS = new HashSet<MEMBER_EVENT_CHECKINS>();
        }

        [Key]
        public int MEMBER_ID { get; set; }

        [StringLength(100)]
        public string FIRST_NAME { get; set; }

        [StringLength(100)]
        public string LAST_NAME { get; set; }

        [StringLength(100)]
        public string COMPANY_NAME { get; set; }

        [StringLength(200)]
        public string EMAIL_ID { get; set; }

        [StringLength(50)]
        public string CELL_PHONE { get; set; }

        [StringLength(50)]
        public string HOME_PHONE { get; set; }

        public bool? IS_VOLUNTEER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER_EVENT_CHECKINS> MEMBER_EVENT_CHECKINS { get; set; }
    }
}
