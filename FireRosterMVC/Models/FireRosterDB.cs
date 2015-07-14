namespace FireRosterMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FireRosterMVC.Models.Codes;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class FireRosterDB : IdentityDbContext<ApplicationUser>
    {
        public FireRosterDB()
            : base("name=FireRosterDB")
        {
        }

        public static FireRosterDB Create()
        {
            return new FireRosterDB();
        }

        public virtual DbSet<Staff> StaffList { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public virtual DbSet<LocationGroup> LocationGroups { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<PositionStatus> PositionStatus { get; set; }
        public virtual DbSet<CareerDevelopmentLevel> CareerDevelopmentLevels { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<OvertimeCode> OvertimeCodes { get; set; }
        public virtual DbSet<Overtime> Overtime { get; set; }
        public virtual DbSet<OvertimeAvailability> OvertimeAvailability { get; set; }
    }
}