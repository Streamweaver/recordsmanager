namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeeAddress")]
    public partial class tblEmployeeAddress
    {
        [Key]
        public int EmployeeAddressID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool? IsRemoved { get; set; }

        public bool? IsTestData { get; set; }

        [StringLength(250)]
        public string AddressLine1 { get; set; }

        [StringLength(250)]
        public string AddressLine2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        public bool? IsPrimaryAddress { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }
    }
}
