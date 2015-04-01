namespace FireRosterMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ImageLink = c.String(maxLength: 100),
                        Abbreviation = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SkillStaffs",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        Staff_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.Staff_ID })
                .ForeignKey("dbo.Skills", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.Staff_ID, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.Staff_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillStaffs", "Staff_ID", "dbo.Staff");
            DropForeignKey("dbo.SkillStaffs", "Skill_ID", "dbo.Skills");
            DropIndex("dbo.SkillStaffs", new[] { "Staff_ID" });
            DropIndex("dbo.SkillStaffs", new[] { "Skill_ID" });
            DropTable("dbo.SkillStaffs");
            DropTable("dbo.Skills");
        }
    }
}
