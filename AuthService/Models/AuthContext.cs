using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Models
{
	public class AuthContext : IdentityDbContext<AppUser>
	{
		public DbSet<Company> Companies { get; set; }

		public AuthContext() : base("AuthContext")
		{

		}
	}
}
