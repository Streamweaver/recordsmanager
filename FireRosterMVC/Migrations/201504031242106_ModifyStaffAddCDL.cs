namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStaffAddCDL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "CDL_ID", c => c.Int());
            CreateIndex("dbo.Staff", "CDL_ID");
            AddForeignKey("dbo.Staff", "CDL_ID", "dbo.CareerDevelopmentLevels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "CDL_ID", "dbo.CareerDevelopmentLevels");
            DropIndex("dbo.Staff", new[] { "CDL_ID" });
            DropColumn("dbo.Staff", "CDL_ID");
        }
    }
}
