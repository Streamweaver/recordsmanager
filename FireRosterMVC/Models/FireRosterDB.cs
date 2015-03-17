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

        public virtual DbSet<tblCertificationAgency> tblCertificationAgencies { get; set; }
        public virtual DbSet<tblCertificationState> tblCertificationStates { get; set; }
        public virtual DbSet<tblEmergencyContact> tblEmergencyContacts { get; set; }
        public virtual DbSet<tblEmployee> tblEmployees { get; set; }
        public virtual DbSet<tblEmployeeAddress> tblEmployeeAddresses { get; set; }
        public virtual DbSet<tblEmployeeAttribute> tblEmployeeAttributes { get; set; }
        public virtual DbSet<tblEmployeeAttributeKey> tblEmployeeAttributeKeys { get; set; }
        public virtual DbSet<tblEmployeePager> tblEmployeePagers { get; set; }
        public virtual DbSet<tblEmployeePhone> tblEmployeePhones { get; set; }
        public virtual DbSet<tblEmployeePosition> tblEmployeePositions { get; set; }
        public virtual DbSet<tblEmployeeRosterAssignment> tblEmployeeRosterAssignments { get; set; }
        public virtual DbSet<tblPosition> tblPositions { get; set; }
        public virtual DbSet<tblRank> tblRanks { get; set; }
        public virtual DbSet<tblShift> tblShifts { get; set; }
        public virtual DbSet<tblSkill> tblSkills { get; set; }
        public virtual DbSet<tblEmployeePhoto> tblEmployeePhotoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCertificationAgency>()
                .Property(e => e.CertificationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblCertificationAgency>()
                .Property(e => e.CertificationLevel)
                .IsUnicode(false);

            modelBuilder.Entity<tblCertificationAgency>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblCertificationState>()
                .Property(e => e.CertificationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblCertificationState>()
                .Property(e => e.CertificationLevel)
                .IsUnicode(false);

            modelBuilder.Entity<tblCertificationState>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.Relationship)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmergencyContact>()
                .Property(e => e.PhoneType)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.SSN)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.NamePrefix)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.NameSuffix)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.Race)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.CareerDevelopmentLevel)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.RosterRank)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.HenricoUserID)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.OracleHrID)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .Property(e => e.BadgeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployee>()
                .HasMany(e => e.tblEmployeePositions)
                .WithRequired(e => e.tblEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEmployee>()
                .HasMany(e => e.tblEmployeeRosterAssignments)
                .WithRequired(e => e.tblEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEmployee>()
                .HasMany(e => e.tblSkills)
                .WithMany(e => e.tblEmployees)
                .Map(m => m.ToTable("tblEmployeeSkill").MapLeftKey("EmployeeID").MapRightKey("SkillID"));

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAddress>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAttribute>()
                .Property(e => e.EmployeeAttributeValue)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAttributeKey>()
                .Property(e => e.KeyName)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePager>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePager>()
                .Property(e => e.PagerProvider)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePager>()
                .Property(e => e.PagerNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePhone>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePhone>()
                .Property(e => e.PhoneType)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePhone>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePosition>()
                .Property(e => e.PositionCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePosition>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePosition>()
                .Property(e => e.EmployeePositionStatus)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePosition>()
                .Property(e => e.EmployeePositionType)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePosition>()
                .Property(e => e.EmployeePositionSchedule)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeRosterAssignment>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblPosition>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblPosition>()
                .Property(e => e.PositionCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblPosition>()
                .Property(e => e.PositionTitle)
                .IsUnicode(false);

            modelBuilder.Entity<tblPosition>()
                .Property(e => e.PositionDescription)
                .IsUnicode(false);

            modelBuilder.Entity<tblPosition>()
                .Property(e => e.PositionFlag)
                .IsUnicode(false);

            modelBuilder.Entity<tblRank>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblRank>()
                .Property(e => e.RankCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblRank>()
                .Property(e => e.SecurityRank)
                .IsUnicode(false);

            modelBuilder.Entity<tblRank>()
                .HasMany(e => e.tblEmployeeRosterAssignments)
                .WithRequired(e => e.tblRank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblShift>()
                .Property(e => e.LastUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblShift>()
                .Property(e => e.ShiftCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblShift>()
                .HasMany(e => e.tblEmployeeRosterAssignments)
                .WithRequired(e => e.tblShift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblShift>()
                .HasMany(e => e.tblPositions)
                .WithRequired(e => e.tblShift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblSkill>()
                .Property(e => e.SkillName)
                .IsUnicode(false);

            modelBuilder.Entity<tblSkill>()
                .Property(e => e.SkillImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<tblSkill>()
                .Property(e => e.SkillAbbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeePhoto>()
                .Property(e => e.OracleHrID)
                .IsUnicode(false);
        }
    }
}
