namespace SYticket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int user_Login_id { get; set; }

        [Required]
        [StringLength(50)]
        public string user_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string user_Password { get; set; }

        [Required]
        [StringLength(3)]
        public string user_role { get; set; }

        public int? adminLogin_ID { get; set; }

        public int? companyLogin_ID { get; set; }

        public int? userLogin_ID { get; set; }

        public virtual Admin_panel Admin_panel { get; set; }

        public virtual companyInfo companyInfo { get; set; }

        public virtual userPanel userPanel { get; set; }
    }
}
