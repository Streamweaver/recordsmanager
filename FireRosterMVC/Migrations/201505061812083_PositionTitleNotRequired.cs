namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionTitleNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "Title", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
