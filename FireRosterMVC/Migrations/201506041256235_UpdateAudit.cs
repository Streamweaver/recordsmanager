namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audits", "ControllerClass", c => c.String());
            DropColumn("dbo.Audits", "ModelClass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Audits", "ModelClass", c => c.String());
            DropColumn("dbo.Audits", "ControllerClass");
        }
    }
}
