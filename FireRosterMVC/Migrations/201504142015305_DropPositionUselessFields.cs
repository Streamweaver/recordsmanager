namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPositionUselessFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Positions", "Flag");
            DropColumn("dbo.Positions", "ReportedHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "ReportedHours", c => c.Int());
            AddColumn("dbo.Positions", "Flag", c => c.String(maxLength: 50));
        }
    }
}
