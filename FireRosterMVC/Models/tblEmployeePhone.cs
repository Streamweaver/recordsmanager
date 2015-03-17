namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeePhone")]
    public partial class tblEmployeePhone
    {
        [Key]
        public int EmployeePhoneID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool? IsRemoved { get; set; }

        public bool? IsTestData { get; set; }

        [StringLength(50)]
        public string PhoneType { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public bool? IsPrimaryNumber { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }
    }
}
