namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionNullableShift : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "Shift", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Shift", c => c.Int(nullable: false));
        }
    }
}
