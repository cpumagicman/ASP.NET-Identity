namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedaddressfieldstoCompanyModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Phone", c => c.String());
            AddColumn("dbo.Companies", "Address1", c => c.String());
            AddColumn("dbo.Companies", "Address2", c => c.String());
            AddColumn("dbo.Companies", "City", c => c.String());
            AddColumn("dbo.Companies", "State", c => c.String());
            AddColumn("dbo.Companies", "Zip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Zip");
            DropColumn("dbo.Companies", "State");
            DropColumn("dbo.Companies", "City");
            DropColumn("dbo.Companies", "Address2");
            DropColumn("dbo.Companies", "Address1");
            DropColumn("dbo.Companies", "Phone");
        }
    }
}
