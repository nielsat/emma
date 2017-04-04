namespace MailPusher.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publisher_StatusChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publishers", "StatusChanged", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publishers", "StatusChanged");
        }
    }
}
