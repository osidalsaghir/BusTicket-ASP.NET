namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("companyContact")]
    public partial class companyContact
    {
        [Key]
        public int contact_ID { get; set; }

        public int company_ID { get; set; }

        [StringLength(20)]
        public string company_PhoneNumber { get; set; }

        [StringLength(50)]
        public string company_Email { get; set; }

        [StringLength(500)]
        public string company_Website { get; set; }

        public virtual companyInfo companyInfo { get; set; }
    }
}
