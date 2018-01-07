namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAGS_LOGIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAGS_LOGIN()
        {
            MEMBER_EVENT_CHECKINS = new HashSet<MEMBER_EVENT_CHECKINS>();
            TICKETED_CHECKINS = new HashSet<TICKETED_CHECKINS>();
        }

        [Key]
        public int USER_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FIRST_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string LAST_NAME { get; set; }

        [Required]
        [StringLength(15)]
        public string PASSWORD { get; set; }

        public int IS_LOGGED_ID { get; set; }

        [StringLength(50)]
        public string USER_ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER_EVENT_CHECKINS> MEMBER_EVENT_CHECKINS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKETED_CHECKINS> TICKETED_CHECKINS { get; set; }
    }
}
