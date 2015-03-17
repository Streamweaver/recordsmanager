namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployee")]
    public partial class tblEmployee
    {
        public tblEmployee()
        {
            tblEmergencyContacts = new HashSet<tblEmergencyContact>();
            tblEmployeeAddresses = new HashSet<tblEmployeeAddress>();
            tblEmployeeAttributes = new HashSet<tblEmployeeAttribute>();
            tblEmployeePagers = new HashSet<tblEmployeePager>();
            tblEmployeePhones = new HashSet<tblEmployeePhone>();
            tblEmployeePositions = new HashSet<tblEmployeePosition>();
            tblEmployeeRosterAssignments = new HashSet<tblEmployeeRosterAssignment>();
            tblSkills = new HashSet<tblSkill>();
        }

        [Key, Display(Name="FireRoster ID")]
        public int EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool? isRemoved { get; set; }

        public bool? isTestData { get; set; }

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

        [Display(Name = "Military Leave")]
        public bool isMilitaryLeave { get; set; }

        [StringLength(50), Display(Name = "Roster Rank")]
        public string RosterRank { get; set; }

        [StringLength(50), Display(Name = "User ID")]
        public string HenricoUserID { get; set; }

        [StringLength(50), Display(Name = "HR ID")]
        public string OracleHrID { get; set; }

        [StringLength(50), Display(Name = "Badge Number")]
        public string BadgeNumber { get; set; }

        [Display(Name="Full Name")]
        public string DisplayName
        {
            get
            {
                if (String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(LastName) && String.IsNullOrEmpty(MiddleName))
                {
                    return "Employee " + EmployeeID;
                }
                else
                {
                    return LastName + ", " + FirstName + " " + MiddleName;
                }
            }
        }

        [Display(Name="Assignment")]
        public string CurrentAssignment
        {
            get 
            { 
                return "?"; 
            }
        }

        public virtual ICollection<tblEmergencyContact> tblEmergencyContacts { get; set; }

        public virtual ICollection<tblEmployeeAddress> tblEmployeeAddresses { get; set; }

        public virtual ICollection<tblEmployeeAttribute> tblEmployeeAttributes { get; set; }

        public virtual ICollection<tblEmployeePager> tblEmployeePagers { get; set; }

        public virtual ICollection<tblEmployeePhone> tblEmployeePhones { get; set; }

        public virtual ICollection<tblEmployeePosition> tblEmployeePositions { get; set; }

        public virtual ICollection<tblEmployeeRosterAssignment> tblEmployeeRosterAssignments { get; set; }

        public virtual ICollection<tblSkill> tblSkills { get; set; }
    }
}
