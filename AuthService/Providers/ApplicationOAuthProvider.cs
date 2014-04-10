using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using AuthService.Models;

// This file is originally from the SPA template that ships with
// Visual Studio Express 2013 for Web

namespace AuthService.Providers
{
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
	{
		private string AppClientId { get; set; }
		private Func<UserManager<AppUser>> UserManagerFactory { get; set; }

		public ApplicationOAuthProvider(string appClientId, Func<UserManager<AppUser>> userManagerFactory)
		{
			AppClientId = appClientId;
			UserManagerFactory = userManagerFactory;
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			using (var userManager = UserManagerFactory())
			{
				var user = await userManager.FindAsync(context.UserName, context.Password);

				if (user == null)
				{
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				}

				var identity = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
				//ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
				var properties = GetAppUserProperties(user);
				var ticket = new AuthenticationTicket(identity, properties);
				context.Validated(ticket);
				//context.Request.Context.Authentication.SignIn(cookiesIdentity);
			}
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (var property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			// Resource owner password credentials does not provide a Client Id.
			if (context.ClientId == null)
			{
				context.Validated();
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
		{
			if (context.ClientId == AppClientId)
			{
				Uri expectedRootUri = new Uri(context.Request.Uri, "/");

				if (expectedRootUri.AbsoluteUri == context.RedirectUri)
				{
					context.Validated();
				}
			}

			return Task.FromResult<object>(null);
		}

		private AuthenticationProperties GetAppUserProperties(AppUser user)
		{
			var dictionary = new Dictionary<string, string>()
			{ 
				{"username", user.UserName},
				{"firstName", user.FirstName},
				{"lastName", user.LastName}
			};

			return new AuthenticationProperties(dictionary);
		}
	}
}