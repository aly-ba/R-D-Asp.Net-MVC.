using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;



namespace IdentitySample.Models
{
		public class ApplicationUser :IdenityUser
		{
			public async Task<ClaimsIdentity>  GenerateUserIdentityAsync (UserManager<ApplicationUser> manager)
			{
			
					//Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
					var userIdentity = await manager.CreateIdenityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
					//Add customer user claims here
					return userIdentity;
					
			}
		
		
		}
		
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
		{
				public ApplicationDbContex()
					:base("DefaultConnection", throwIfV1Schema:fakse)
					{
					}
	    }

		static ApplicationDbContext()
		{
		  //
		  //
		  Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
		
		}
		
		public static ApplicationDbContext Create()
		{
			return ApplicationDbContext();
		}
		
	}
}		
		

