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
    }
}