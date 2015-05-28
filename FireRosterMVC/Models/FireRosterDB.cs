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

        public System.Data.Entity.DbSet<Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<EmergencyContact> EmergencyContacts { get; set; }

        public System.Data.Entity.DbSet<LocationGroup> LocationGroups { get; set; }

        public System.Data.Entity.DbSet<Phone> Phones { get; set; }

        public System.Data.Entity.DbSet<PhoneType> PhoneTypes { get; set; }

        public System.Data.Entity.DbSet<Position> Positions { get; set; }

        public System.Data.Entity.DbSet<PositionStatus> PositionStatus { get; set; }

        public System.Data.Entity.DbSet<CareerDevelopmentLevel> CareerDevelopmentLevels { get; set; }

        public System.Data.Entity.DbSet<Rank> Ranks { get; set; }
    }
}