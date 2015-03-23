namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        SSN = c.String(maxLength: 9),
                        NamePrefix = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        NameSuffix = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        RankDate = c.DateTime(),
                        CareerDevelopmentLevel = c.String(maxLength: 50),
                        CareerDevelopmentDate = c.DateTime(),
                        EmploymentDate = c.DateTime(),
                        TerminationDate = c.DateTime(),
                        isMilitaryLeave = c.Boolean(nullable: false),
                        RosterRank = c.String(maxLength: 50),
                        HenricoUserID = c.String(maxLength: 50),
                        OracleHrID = c.String(maxLength: 50),
                        BadgeNumber = c.String(maxLength: 50),
                        Photo = c.Binary(storeType: "image"),
                        Gender_ID = c.Int(),
                        Race_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genders", t => t.Gender_ID)
                .ForeignKey("dbo.Races", t => t.Race_ID)
                .Index(t => t.Gender_ID)
                .Index(t => t.Race_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "Race_ID", "dbo.Races");
            DropForeignKey("dbo.Staff", "Gender_ID", "dbo.Genders");
            DropIndex("dbo.Staff", new[] { "Race_ID" });
            DropIndex("dbo.Staff", new[] { "Gender_ID" });
            DropTable("dbo.Staff");
            DropTable("dbo.Races");
            DropTable("dbo.Genders");
        }
    }
}
