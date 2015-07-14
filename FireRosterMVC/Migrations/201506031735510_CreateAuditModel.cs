namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModelClass = c.String(),
                        RecordID = c.String(),
                        Action = c.Int(nullable: false),
                        Details = c.String(),
                        On = c.DateTime(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audits");
        }
    }
}
