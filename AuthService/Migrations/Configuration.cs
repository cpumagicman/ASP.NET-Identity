namespace AuthService.Migrations
{
	using AuthService.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<AuthService.Models.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthService.Models.AuthContext context)
        {
			var company = AddCompany("Alien Arc Technologies, LLC");
			var user1 = AddUser(context, "admin", "password", "Luke", "Skywalker", company);
			var user2 = AddUser(context, "user1", "password", "Han", "Solo", company);
			company.Employees.Add(user1);
			company.Employees.Add(user2);
			context.SaveChanges();			
        }

		private AppUser AddUser(AuthContext context, string username, string password, string firstName, string lastName, Company company)
		{
			IdentityResult identityResult;
			UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
			
			var user = userManager.FindByName(username);
			if (user == null)
			{
				user = new AppUser() { UserName = username, FirstName = firstName, LastName = lastName, Company = company };
				identityResult = userManager.Create(user, password);
			}

			return user;
		}
		private Company AddCompany(string name)
		{
			var company = new Company() { Id = Guid.NewGuid(), Name = name};
			return company;
		}
    }
}
