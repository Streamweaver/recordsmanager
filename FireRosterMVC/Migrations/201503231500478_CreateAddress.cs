namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(maxLength: 75),
                        State = c.String(maxLength: 2),
                        Zip = c.String(maxLength: 20),
                        Primary = c.Boolean(nullable: false),
                        Staff_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Staff_ID", "dbo.Staff");
            DropIndex("dbo.Addresses", new[] { "Staff_ID" });
            DropTable("dbo.Addresses");
        }
    }
}
