namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOvertimeCodeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OvertimeCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 6),
                        Label = c.String(maxLength: 65),
                        ShortLabel = c.String(maxLength: 10),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OvertimeCodes");
        }
    }
}
