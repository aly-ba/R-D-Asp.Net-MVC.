//R&D




namespace IdentitySample
{

	public partial class Startup
	{
	
	public void ConfigureAuth(IAppBuilder app)
	{
	
		 //configure the DbContext , user manager and role manager to use a single instance per request
		app.CreatePerOwinContext(ApplicationDbContext.Create);
		app.CratePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
		app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
		app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
		
		//Enalbe the application to use coolie to store information for signed in user
		//and to use a cookie to temporarily store information about user logging in with a third party login provider
		//confiugure the sign in cookie
		
		app.UseCookieAuthentification(new CookieAuthentica tionOptions
		{
			AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			LoginPath = new PathString("/Account/Login"),
			Provider = new CookieAuthenticationProvider
			  {
			    //Enables the application to validate the security stamps the user logs in
				OnValidateIdentity = SecurityStampValidator.OnvalidateIndetity<ApplicationUserManager, ApplicationUser>(  
				validateInterval:TimeSpan.FromMinutes(30);
				regenerateIdentiy:(manager, user ) =>user.GenerateuserIdentityAsync(manager))
				
				
			  });
			  
			  app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
			  
			     // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
              app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
			  
			// Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
		
		    
			
			
			//Uncomment the following lines to enable logging in with third pary login providers
			app.UseMicorsoftAccountAuthentication(
				clientId:"",
				clientSecret:"");
				
			app.UseTwitterAuthentication(
				consumerKey:"",
				consumerSecret:"");
				
				app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
			
			
			//app.UseGoogleAuthentication(
            //    clientId: "",
            //    clientSecret: "");
           }	
		}
	
	}


}


//Extrait  code
new CookieAuthentica tionOptions {
			AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			LoginPath = new PathString("/Account/Login"),
			Provider = new CookieAuthenticationProvider
			  {
			  
			  
			  }
		
		
		}