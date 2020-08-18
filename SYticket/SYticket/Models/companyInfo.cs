namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("companyInfo")]
    public partial class companyInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public companyInfo()
        {
            companyAddresses = new HashSet<companyAddress>();
            companyContacts = new HashSet<companyContact>();
            Travels = new HashSet<Travel>();
            Users = new HashSet<User>();
        }

        [Key]
        public int company_ID { get; set; }

        public int admin_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string company_Name { get; set; }

        [Required]
        [StringLength(500)]
        public string company_Logo { get; set; }

        [Column(TypeName = "date")]
        public DateTime company_StartDate { get; set; }

        public virtual Admin_panel Admin_panel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<companyAddress> companyAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<companyContact> companyContacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Travel> Travels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
