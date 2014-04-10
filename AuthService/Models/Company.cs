using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthService.Models
{
	public class Company
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public String Phone {get; set; }
		public virtual ICollection<AppUser> Employees { get; set; }

		public Company()
		{
			Employees = new List<AppUser>();
		}
	}
}
