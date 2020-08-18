namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userContact")]
    public partial class userContact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public userContact()
        {
            userPanels = new HashSet<userPanel>();
        }

        [Key]
        public int contact_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string userContact_PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string userContact_email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userPanel> userPanels { get; set; }
    }
}
