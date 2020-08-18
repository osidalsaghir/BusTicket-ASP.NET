namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("reservation")]
    public partial class reservation
    {
        [Key]
        public int reserv_ID { get; set; }

        public int Travel_ID { get; set; }

        public int userPanel_ID { get; set; }

        [Required]
        [StringLength(3)]
        public string seatNumber { get; set; }

        public DateTime data_of_reservation { get; set; }

        public virtual Travel Travel { get; set; }

        public virtual userPanel userPanel { get; set; }
    }
}
