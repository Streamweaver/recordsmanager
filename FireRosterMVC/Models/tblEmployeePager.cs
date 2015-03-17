namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeePager")]
    public partial class tblEmployeePager
    {
        [Key]
        public int EmployeePagerID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool? IsRemoved { get; set; }

        [StringLength(50)]
        public string PagerProvider { get; set; }

        [StringLength(10)]
        public string PagerNumber { get; set; }

        public bool? PagingOnUnit { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }
    }
}
