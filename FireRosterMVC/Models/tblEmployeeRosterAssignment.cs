namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeeRosterAssignment")]
    public partial class tblEmployeeRosterAssignment
    {
        [Key]
        public int EmployeeRosterAssignmentID { get; set; }

        public int EmployeeID { get; set; }

        public int LocationID { get; set; }

        public int ShiftID { get; set; }

        public int RankID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [Required]
        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool isRemoved { get; set; }

        public bool isTestData { get; set; }

        public DateTime EmployeeRosterAssignmentStartDate { get; set; }

        public DateTime? EmployeeRosterAssignmentEndDate { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }

        public virtual tblRank tblRank { get; set; }

        public virtual tblShift tblShift { get; set; }
    }
}
