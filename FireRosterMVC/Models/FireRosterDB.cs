namespace FireRosterMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FireRosterDB : DbContext
    {
        public FireRosterDB()
            : base("name=FireRosterDB")
        {
        }

        public virtual DbSet<Staff> StaffList { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.EmergencyContact> EmergencyContacts { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.LocationGroup> LocationGroups { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.Phone> Phones { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.PhoneType> PhoneTypes { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.Position> Positions { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.PositionStatus> PositionStatus { get; set; }

        public System.Data.Entity.DbSet<FireRosterMVC.Models.Codes.CareerDevelopmentLevel> CareerDevelopmentLevels { get; set; }
    }
}