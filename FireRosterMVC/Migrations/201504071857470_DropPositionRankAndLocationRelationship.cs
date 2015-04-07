namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPositionRankAndLocationRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Positions", "Rank_ID", "dbo.Ranks");
            DropIndex("dbo.Positions", new[] { "Location_ID" });
            DropIndex("dbo.Positions", new[] { "Rank_ID" });
            DropColumn("dbo.Positions", "Location_ID");
            DropColumn("dbo.Positions", "Rank_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "Rank_ID", c => c.Int());
            AddColumn("dbo.Positions", "Location_ID", c => c.Int());
            CreateIndex("dbo.Positions", "Rank_ID");
            CreateIndex("dbo.Positions", "Location_ID");
            AddForeignKey("dbo.Positions", "Rank_ID", "dbo.Ranks", "ID");
            AddForeignKey("dbo.Positions", "Location_ID", "dbo.Locations", "ID");
        }
    }
}
