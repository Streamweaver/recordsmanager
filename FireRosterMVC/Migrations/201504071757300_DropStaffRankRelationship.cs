namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropStaffRankRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Staff", "Rank_ID", "dbo.Ranks");
            DropIndex("dbo.Staff", new[] { "Rank_ID" });
            DropColumn("dbo.Staff", "Rank_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staff", "Rank_ID", c => c.Int());
            CreateIndex("dbo.Staff", "Rank_ID");
            AddForeignKey("dbo.Staff", "Rank_ID", "dbo.Ranks", "ID");
        }
    }
}
