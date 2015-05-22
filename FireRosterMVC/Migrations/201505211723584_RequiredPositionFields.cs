namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredPositionFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Positions", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Positions", "Code", c => c.String(maxLength: 10));
        }
    }
}
