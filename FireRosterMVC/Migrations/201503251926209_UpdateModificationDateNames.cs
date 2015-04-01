namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModificationDateNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Staff", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Staff", "MilitaryLeave", c => c.Boolean(nullable: false));
            DropColumn("dbo.Staff", "DateCreated");
            DropColumn("dbo.Staff", "LastUpdateDate");
            DropColumn("dbo.Staff", "isMilitaryLeave");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staff", "isMilitaryLeave", c => c.Boolean(nullable: false));
            AddColumn("dbo.Staff", "LastUpdateDate", c => c.DateTime());
            AddColumn("dbo.Staff", "DateCreated", c => c.DateTime());
            DropColumn("dbo.Staff", "MilitaryLeave");
            DropColumn("dbo.Staff", "UpdatedOn");
            DropColumn("dbo.Staff", "CreatedOn");
        }
    }
}
