namespace FireRosterMVC.Models
{
    using FireRosterMVC.Enums;
    using FireRosterMVC.Models.Codes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("Staff")]
    public class Staff
    {
        // Model Fields
        [Key, Display(Name = "FireRoster ID")]
        public int ID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool Deleted { get; set; }

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

        // NOTE do not use DateType.Date here as Chrome overrides the value
        // SEE http://stackoverflow.com/questions/18275009/asp-net-mvc-c-sharp-chrome-not-showing-date-in-edit-mode
        // http://updates.html5rocks.com/2012/08/Quick-FAQs-on-input-type-date-in-Google-Chrome
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Rank Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RankDate { get; set; }

        [Display(Name = "CD Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CareerDevelopmentDate { get; set; }

        [Display(Name = "Date of Hire")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDate { get; set; }

        [Display(Name = "Date of Termination")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TerminationDate { get; set; }

        [Display(Name = "Military Leave")]
        public bool MilitaryLeave { get; set; }

        [StringLength(50), Display(Name = "Roster Rank")]
        public string RosterRank { get; set; }

        [StringLength(50), Display(Name = "User ID")]
        public string HenricoUserID { get; set; }

        [StringLength(50), Display(Name = "Emplooyee #")]
        public string OracleHrID { get; set; }

        [StringLength(50), Display(Name = "Badge Number")]
        public string BadgeNumber { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        // Related Fields
        public int? Gender_ID { get; set; }
        [ForeignKey("Gender_ID")]
        public virtual Gender Gender { get; set; }

        public int? Race_ID { get; set; }
        [ForeignKey("Race_ID")]
        public virtual Race Race { get; set; }

        public int? CDL_ID { get; set; }
        [ForeignKey("CDL_ID")]
        public virtual CareerDevelopmentLevel CDL { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Phone> PhoneNumbers { get; set; }

        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

        public IEnumerable<Position> CurrentPositions
        {
            get
            {
                return Positions
                        .Where(p => p.EndDate == null || p.EndDate > DateTime.Now)
                        .OrderByDescending(p => p.StartDate);
            }
        }

        // Derived Fields
        [Display(Name = "Full Name")]
        public string DisplayName
        {
            get
            {
                if (String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(LastName) && String.IsNullOrEmpty(MiddleName))
                {
                    return "Employee " + ID;
                }
                else
                {
                    return LastName + ", " + FirstName + " " + MiddleName;
                }
            }
        }

        [Display(Name="Email")]
        public string DisplayEmail
        {
            get
            {
                if (HenricoUserID != null)
                {
                    return HenricoUserID.ToLower() + "@henrico.us";
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Assignment")]
        public string CurrentAssignment
        {
            get
            {
                return "?";
            }
        }
    }
}