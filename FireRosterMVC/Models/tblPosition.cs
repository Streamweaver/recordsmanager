namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPosition")]
    public partial class tblPosition
    {
        [Key]
        public int PositionID { get; set; }

        public int LocationID { get; set; }

        public int ShiftID { get; set; }

        public int? RankID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [Required]
        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool isRemoved { get; set; }

        public bool isTestData { get; set; }

        [Required]
        [StringLength(10)]
        public string PositionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string PositionTitle { get; set; }

        [Required]
        [StringLength(150)]
        public string PositionDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string PositionFlag { get; set; }

        public DateTime PositionStartDate { get; set; }

        public DateTime? PositionEndDate { get; set; }

        public virtual tblRank tblRank { get; set; }

        public virtual tblShift tblShift { get; set; }
    }
}
