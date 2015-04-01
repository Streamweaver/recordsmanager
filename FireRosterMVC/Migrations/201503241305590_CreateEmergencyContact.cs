namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEmergencyContact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmergencyContacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Relationship = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 10),
                        Order = c.Int(nullable: false),
                        PhoneType_ID = c.Int(),
                        Staff_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhoneTypes", t => t.PhoneType_ID)
                .ForeignKey("dbo.Staff", t => t.Staff_ID)
                .Index(t => t.PhoneType_ID)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmergencyContacts", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.EmergencyContacts", "PhoneType_ID", "dbo.PhoneTypes");
            DropIndex("dbo.EmergencyContacts", new[] { "Staff_ID" });
            DropIndex("dbo.EmergencyContacts", new[] { "PhoneType_ID" });
            DropTable("dbo.EmergencyContacts");
        }
    }
}
