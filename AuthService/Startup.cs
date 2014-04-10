using AuthService.Models;
using AuthService.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(AuthService.Startup))]

namespace AuthService
{
	public class Startup
	{
		public OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		public Startup()
		{
			var id = "clientAppId";
			Func<UserManager<AppUser>> userManagerFactory = () => new UserManager<AppUser>(new UserStore<AppUser>(new AuthContext()));
			OAuthOptions = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/auth"),
				Provider = new ApplicationOAuthProvider(id, userManagerFactory),
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(12),
				AllowInsecureHttp = true
			};
		}

		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseOAuthBearerTokens(OAuthOptions);
			app.UseWebApi(config);
		}
	}
}
