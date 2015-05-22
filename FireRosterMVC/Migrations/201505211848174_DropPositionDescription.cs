namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPositionDescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Positions", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "Description", c => c.String(maxLength: 150));
        }
    }
}
