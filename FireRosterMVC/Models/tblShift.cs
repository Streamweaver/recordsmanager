namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblShift")]
    public partial class tblShift
    {
        public tblShift()
        {
            tblEmployeeRosterAssignments = new HashSet<tblEmployeeRosterAssignment>();
            tblPositions = new HashSet<tblPosition>();
        }

        [Key]
        public int ShiftID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [Required]
        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool isRemoved { get; set; }

        public bool isTestData { get; set; }

        [Required]
        [StringLength(50)]
        public string ShiftCode { get; set; }

        public int ShiftOrder { get; set; }

        public virtual ICollection<tblEmployeeRosterAssignment> tblEmployeeRosterAssignments { get; set; }

        public virtual ICollection<tblPosition> tblPositions { get; set; }
    }
}
