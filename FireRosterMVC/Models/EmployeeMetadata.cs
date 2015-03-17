using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class EmployeeMetadata
    {
        [StringLength(9)]
        public string SSN { get; set; }

        [StringLength(50), Display(Name = "Prefix")]
        public string NamePrefix { get; set; }

        [StringLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50), Display(Name = "Middle")]
        public string MiddleName { get; set; }

        [StringLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50), Display(Name = "Suffix")]
        public string NameSuffix { get; set; }

        [StringLength(50)]
        public string Race { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Rank Date")]
        public DateTime? RankDate { get; set; }

        [StringLength(50), Display(Name = "CDL")]
        public string CareerDevelopmentLevel { get; set; }

        [Display(Name = "CD Date")]
        public DateTime? CareerDevelopmentDate { get; set; }

        [Display(Name = "Date of Hire")]
        public DateTime? EmploymentDate { get; set; }

        [Display(Name = "Date of Termination")]
        public DateTime? TerminationDate { get; set; }

        [Display(Name = "Military Leave"), UIHint("YesNo")]
        public bool isMilitaryLeave { get; set; }

        [StringLength(50), Display(Name = "Roster Rank")]
        public string RosterRank { get; set; }

        [StringLength(50), Display(Name = "User ID")]
        public string HenricoUserID { get; set; }

        [StringLength(50), Display(Name = "HR ID")]
        public string OracleHrID { get; set; }

        [StringLength(50), Display(Name = "Badge Number")]
        public string BadgeNumber { get; set; }

    }
}