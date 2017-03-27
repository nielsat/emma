namespace MailPusher.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailRawHeaders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailID = c.Int(nullable: false),
                        HeaderName = c.String(),
                        HeaderValue = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emails", t => t.EmailID, cascadeDelete: true)
                .Index(t => t.EmailID);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublisherID = c.Int(nullable: false),
                        ReceivedGMT = c.DateTime(nullable: false),
                        SubjectLine = c.String(),
                        SenderName = c.String(),
                        SenderAddress = c.String(),
                        Copy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Publishers", t => t.PublisherID, cascadeDelete: true)
                .Index(t => t.PublisherID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Domain = c.String(),
                        Language = c.String(),
                        NACEID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        LastReceivedEmail = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                        CreatorId = c.String(maxLength: 128),
                        Updated = c.DateTime(nullable: false),
                        UpdaterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.NACE", t => t.NACEID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdaterId)
                .Index(t => t.NACEID)
                .Index(t => t.CreatorId)
                .Index(t => t.UpdaterId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NACE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        ISICRef = c.String(),
                        Includes = c.String(),
                        IncludesAlso = c.String(),
                        Rulings = c.String(),
                        Excludes = c.String(),
                        ParentCode = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmailRawData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailID = c.Int(nullable: false),
                        HTML = c.String(),
                        Text = c.String(),
                        HTMLText = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emails", t => t.EmailID, cascadeDelete: true)
                .Index(t => t.EmailID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmailRawData", "EmailID", "dbo.Emails");
            DropForeignKey("dbo.EmailRawHeaders", "EmailID", "dbo.Emails");
            DropForeignKey("dbo.Emails", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.Publishers", "UpdaterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Publishers", "NACEID", "dbo.NACE");
            DropForeignKey("dbo.Publishers", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EmailRawData", new[] { "EmailID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Publishers", new[] { "UpdaterId" });
            DropIndex("dbo.Publishers", new[] { "CreatorId" });
            DropIndex("dbo.Publishers", new[] { "NACEID" });
            DropIndex("dbo.Emails", new[] { "PublisherID" });
            DropIndex("dbo.EmailRawHeaders", new[] { "EmailID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmailRawData");
            DropTable("dbo.NACE");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Publishers");
            DropTable("dbo.Emails");
            DropTable("dbo.EmailRawHeaders");
        }
    }
}
