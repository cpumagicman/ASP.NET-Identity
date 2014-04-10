using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthService.Models
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid CompanyId { get; set; }
		public virtual Company Company { get; set; }
	}
}