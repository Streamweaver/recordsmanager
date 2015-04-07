namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPositionRelationsToRankAndLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "Location_ID", c => c.Int());
            AddColumn("dbo.Positions", "Rank_ID", c => c.Int());
            CreateIndex("dbo.Positions", "Location_ID");
            CreateIndex("dbo.Positions", "Rank_ID");
            AddForeignKey("dbo.Positions", "Location_ID", "dbo.Locations", "ID");
            AddForeignKey("dbo.Positions", "Rank_ID", "dbo.Ranks", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "Rank_ID", "dbo.Ranks");
            DropForeignKey("dbo.Positions", "Location_ID", "dbo.Locations");
            DropIndex("dbo.Positions", new[] { "Rank_ID" });
            DropIndex("dbo.Positions", new[] { "Location_ID" });
            DropColumn("dbo.Positions", "Rank_ID");
            DropColumn("dbo.Positions", "Location_ID");
        }
    }
}
