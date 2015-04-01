namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePosition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        ReportedHours = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status_ID = c.Int(),
                        Staff_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PositionStatus", t => t.Status_ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID)
                .Index(t => t.Status_ID)
                .Index(t => t.Staff_ID);
            
            CreateTable(
                "dbo.PositionStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.Positions", "Status_ID", "dbo.PositionStatus");
            DropIndex("dbo.Positions", new[] { "Staff_ID" });
            DropIndex("dbo.Positions", new[] { "Status_ID" });
            DropTable("dbo.PositionStatus");
            DropTable("dbo.Positions");
        }
    }
}
