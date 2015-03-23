namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePhoneNumbers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Primary = c.Boolean(nullable: false),
                        Staff_ID = c.Int(),
                        Type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID)
                .ForeignKey("dbo.PhoneTypes", t => t.Type_ID)
                .Index(t => t.Staff_ID)
                .Index(t => t.Type_ID);
            
            CreateTable(
                "dbo.PhoneTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "Type_ID", "dbo.PhoneTypes");
            DropForeignKey("dbo.Phones", "Staff_ID", "dbo.Staff");
            DropIndex("dbo.Phones", new[] { "Type_ID" });
            DropIndex("dbo.Phones", new[] { "Staff_ID" });
            DropTable("dbo.PhoneTypes");
            DropTable("dbo.Phones");
        }
    }
}
