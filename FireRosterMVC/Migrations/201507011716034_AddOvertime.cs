namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOvertime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Overtimes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                        Code_ID = c.Int(nullable: false),
                        Staff_ID = c.Int(nullable: false),
                        Location_ID = c.Int(nullable: false),
                        Shift = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Start = c.DateTime(),
                        End = c.DateTime(),
                        Hours = c.Double(),
                        ReviewedBy = c.String(),
                        ReviewedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OvertimeCodes", t => t.Code_ID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.Location_ID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.Staff_ID, cascadeDelete: true)
                .Index(t => t.Code_ID)
                .Index(t => t.Staff_ID)
                .Index(t => t.Location_ID);
            
            CreateTable(
                "dbo.OvertimeAvailabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Staff_ID = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID, cascadeDelete: true)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OvertimeAvailabilities", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.Overtimes", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.Overtimes", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Overtimes", "Code_ID", "dbo.OvertimeCodes");
            DropIndex("dbo.OvertimeAvailabilities", new[] { "Staff_ID" });
            DropIndex("dbo.Overtimes", new[] { "Location_ID" });
            DropIndex("dbo.Overtimes", new[] { "Staff_ID" });
            DropIndex("dbo.Overtimes", new[] { "Code_ID" });
            DropTable("dbo.OvertimeAvailabilities");
            DropTable("dbo.Overtimes");
        }
    }
}
