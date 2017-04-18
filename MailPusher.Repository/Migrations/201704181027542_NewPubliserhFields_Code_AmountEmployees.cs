namespace MailPusher.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPubliserhFields_Code_AmountEmployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publishers", "CountryCompanyCode", c => c.String());
            AddColumn("dbo.Publishers", "AmountEmployees", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publishers", "AmountEmployees");
            DropColumn("dbo.Publishers", "CountryCompanyCode");
        }
    }
}
