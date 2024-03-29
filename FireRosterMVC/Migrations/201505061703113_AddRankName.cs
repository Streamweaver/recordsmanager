namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRankName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ranks", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ranks", "Name");
        }
    }
}
