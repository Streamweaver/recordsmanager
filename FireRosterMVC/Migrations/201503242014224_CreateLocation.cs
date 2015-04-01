namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 5),
                        MinimumComplement = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Street1 = c.String(maxLength: 100),
                        Street2 = c.String(maxLength: 100),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 2),
                        Zip = c.String(maxLength: 15),
                        Order = c.Int(nullable: false),
                        Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LocationGroups", t => t.Group_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.LocationGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "Group_ID", "dbo.LocationGroups");
            DropIndex("dbo.Locations", new[] { "Group_ID" });
            DropTable("dbo.LocationGroups");
            DropTable("dbo.Locations");
        }
    }
}
