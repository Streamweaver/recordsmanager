using FireRosterMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public Shift? Shift { get; set; }

        public int? Status_ID { get; set; }
        [ForeignKey("Status_ID")]
        public virtual PositionStatus Status { get; set; }

        public int? Staff_ID { get; set; }
        [ForeignKey("Staff_ID")]
        public virtual Staff Staff { get; set; }

        public int? Location_ID { get; set; }
        [ForeignKey("Location_ID")]
        public virtual Location Location { get; set; }

        public int? Rank_ID { get; set; }
        [ForeignKey("Rank_ID")]
        public virtual Rank Rank { get; set; }

        [Display(Name="HR Code")]
        public string HRCode  // Format of the position coded used at HR
        {
            get
            {
                if (Code == null)
                {
                    return null;
                }
                else
                {
                    return "G.0501.0" + Code + ".001";
                }
                
            }
        }

        public string DisplayName
        {
            get
            {
                try
                {
                    return Rank.Code + " " + Location.Name + " - " + Shift + " (" + Code + ")";
                }
                catch (NullReferenceException e)
                {
                    return ID.ToString();
                }
            }
        }
    }
}