namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPositionNullFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "ReportedHours", c => c.Int());
            AlterColumn("dbo.Positions", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Positions", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Positions", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Positions", "ReportedHours", c => c.Int(nullable: false));
        }
    }
}
