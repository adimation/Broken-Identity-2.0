using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using Microsoft.Owin.Security.Facebook;
using Entities.EntityModels;
using Services.Services;

namespace IdentityBase
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            // Configure the db context and user manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        //regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                        regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(manager, user),
                        getUserIdCallback:(id)=>(Int32.Parse(id.GetUserId())))
                }
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var fb = new FacebookAuthenticationOptions();
            fb.Scope.Add("email");
            //fb.Scope.Add("friends_about_me");
            fb.AppId = "324646907679107";
            fb.AppSecret = "748e0c8d1184ebcc16bac09edf67d0a1";
            fb.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = async context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                }
            };
            fb.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;

            app.UseFacebookAuthentication(fb);

            //app.UseFacebookAuthentication(
            //   appId: "324646907679107",
            //   appSecret: "748e0c8d1184ebcc16bac09edf67d0a1");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        //public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser user)
        //{
        //    return Task.FromResult(GenerateUserIdentity(manager, user));
        //}

        //public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager, ApplicationUser user)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = manager.CreateIdentity<ApplicationUser, int>(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}