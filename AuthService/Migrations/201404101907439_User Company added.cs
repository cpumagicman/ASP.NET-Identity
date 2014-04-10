namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCompanyadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies");
            DropIndex("dbo.AspNetUsers", new[] { "Company_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Company_Id", newName: "CompanyId");
            AlterColumn("dbo.AspNetUsers", "CompanyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CompanyId");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            AlterColumn("dbo.AspNetUsers", "CompanyId", c => c.Guid());
            RenameColumn(table: "dbo.AspNetUsers", name: "CompanyId", newName: "Company_Id");
            CreateIndex("dbo.AspNetUsers", "Company_Id");
            AddForeignKey("dbo.AspNetUsers", "Company_Id", "dbo.Companies", "Id");
        }
    }
}
