namespace FireRosterMVC.Models
{
    using FireRosterMVC.Enums;
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

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

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

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Rank Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RankDate { get; set; }

        [StringLength(50), Display(Name = "CDL")]
        public string CareerDevelopmentLevel { get; set; }

        [Display(Name = "CD Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CareerDevelopmentDate { get; set; }

        [Display(Name = "Date of Hire")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDate { get; set; }

        [Display(Name = "Date of Termination")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        // Related Fields
        public virtual Gender Gender { get; set; }

        public virtual Race Race { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Phone> PhoneNumbers { get; set; }

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