using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthService.Controllers
{
    [RoutePrefix("companies")]
	public class CompanyController : ApiController
    {
		public AuthContext Context { get; set; }

		public CompanyController()
		{
			Context = new AuthContext();
		}

		[Authorize]
		[Route("summary")]
		public IHttpActionResult GetCompanies()
		{
			var companies = Context.Companies.ToList();
			return Ok("Test Data");
		}
    }
}
