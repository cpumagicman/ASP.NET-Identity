namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Companyadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Company_Id", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "Company_Id");
            AddForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies");
            DropIndex("dbo.AspNetUsers", new[] { "Company_Id" });
            DropColumn("dbo.AspNetUsers", "Company_Id");
            DropTable("dbo.Companies");
        }
    }
}
