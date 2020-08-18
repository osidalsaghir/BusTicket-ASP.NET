namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Travel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Travel()
        {
            reservations = new HashSet<reservation>();
        }

        [Key]
        public int Travel_ID { get; set; }

        public int company_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string from_city { get; set; }

        [Required]
        [StringLength(50)]
        public string to_city { get; set; }

        public DateTime startsAt { get; set; }

        public DateTime endsAt { get; set; }

        public int maxPassengers { get; set; }

        public double price { get; set; }

        public virtual companyInfo companyInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservation> reservations { get; set; }
    }
}
