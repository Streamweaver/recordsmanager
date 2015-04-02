namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCreateDevelopmentLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CareerDevelopmentLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CareerDevelopmentLevels");
        }
    }
}
