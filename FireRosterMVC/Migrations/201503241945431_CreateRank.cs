namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRank : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 3),
                        Security = c.String(maxLength: 20),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Staff", "Rank_ID", c => c.Int());
            CreateIndex("dbo.Staff", "Rank_ID");
            AddForeignKey("dbo.Staff", "Rank_ID", "dbo.Ranks", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "Rank_ID", "dbo.Ranks");
            DropIndex("dbo.Staff", new[] { "Rank_ID" });
            DropColumn("dbo.Staff", "Rank_ID");
            DropTable("dbo.Ranks");
        }
    }
}
