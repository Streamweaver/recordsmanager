namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeePosition")]
    public partial class tblEmployeePosition
    {
        [Key]
        public int EmployeePositionID { get; set; }

        public int EmployeeID { get; set; }

        [StringLength(10)]
        public string PositionCode { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [Required]
        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsTestData { get; set; }

        [StringLength(50)]
        public string EmployeePositionStatus { get; set; }

        [StringLength(50)]
        public string EmployeePositionType { get; set; }

        public bool HideInRosterFlag { get; set; }

        public int ReportedHours { get; set; }

        [StringLength(50)]
        public string EmployeePositionSchedule { get; set; }

        public DateTime? EmployeePositionStartDate { get; set; }

        public DateTime? EmployeePositionEndDate { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }
    }
}
