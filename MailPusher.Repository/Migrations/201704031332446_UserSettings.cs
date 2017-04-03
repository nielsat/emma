namespace MailPusher.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSettings", new[] { "UserId" });
            DropTable("dbo.UserSettings");
        }
    }
}
