using FireRosterMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Overtime
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? Code_ID { get; set; }
        [ForeignKey("Code_ID"), Required]
        public virtual OvertimeCode Code { get; set; }

        public int? Staff_ID { get; set; }
        [ForeignKey("Staff_ID"), Required]
        public virtual Staff Staff { get; set; }

        public int? Location_ID { get; set; }
        [ForeignKey("Location_ID"), Required]
        public virtual Location Location { get; set; }

        [Required]
        public Shift Shift { get; set; }

        [Required]
        public OvertimeStatus Status { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00#}", ApplyFormatInEditMode = true)]
        public double? Hours { get; set; }

        [Display(Name="Reviewed By")]
        public string ReviewedBy { get; set; }

        [Display(Name="Reviewed On")]
        public DateTime? ReviewedOn { get; set; }
    }
}