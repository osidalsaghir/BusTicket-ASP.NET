namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("companyAddress")]
    public partial class companyAddress
    {
        [Key]
        public int address_ID { get; set; }

        public int company_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string company_Street { get; set; }

        [Required]
        [StringLength(50)]
        public string company_neighborhood { get; set; }

        [Required]
        [StringLength(20)]
        public string company_city { get; set; }

        [Required]
        [StringLength(20)]
        public string company_zip_code { get; set; }

        public virtual companyInfo companyInfo { get; set; }
    }
}
