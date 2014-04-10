namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyNameadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Name");
        }
    }
}
