using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Position
    {
        [Key]
        public int ID { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [Display(Name="Reported Hours")]
        public int? ReportedHours { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public virtual PositionStatus Status { get; set; }

        public string HRCode  // Format of the position coded used at HR
        {
            get
            {
                return "G.0501.0" + Code + ".001";
            }
        }
    }
}