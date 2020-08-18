namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userPanel")]
    public partial class userPanel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public userPanel()
        {
            reservations = new HashSet<reservation>();
            Users = new HashSet<User>();
        }

        [Key]
        public int userPanel_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string userPanel_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string userPanel_Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime birthdate { get; set; }

        [Required]
        [StringLength(15)]
        public string userPanel_national_ID { get; set; }

        public int contact_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservation> reservations { get; set; }

        public virtual userContact userContact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
