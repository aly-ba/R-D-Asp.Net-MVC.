using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Indentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Indentity.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {



            var email = "foo@bar.com";
            var password = "Password";
            var user = await UserManager.FindByEmailAsync(email);
            var roles = ApplicationRoleManager.Create(HttpContext.GetOwinContext());

            if (!await roles.RoleExistsAsync(SecurityRoles.Admin))
            {
                await roles.CreateAsync(new IdentityRole {Name = SecurityRoles.Admin});
            }

            if (!await roles.RoleExistsAsync(SecurityRoles.IT))
            {
                await roles.CreateAsync(new IdentityRole {Name = SecurityRoles.IT});
            }

            if (!await roles.RoleExistsAsync(SecurityRoles.Accounting))
            {
                await roles.CreateAsync(new IdentityRole {Name = SecurityRoles.Accounting});
            }

            if (user == null)
            {
                user = new CustomUser()
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Super",
                    LastName = "Admin"

                };

                await UserManager.CreateAsync(user, password);
            }

            else
            {
                // await   UserManager.AddToRoleAsync(user.Id, SecurityRoles.Admin);
                await UserManager.AddToRoleAsync(user.Id, SecurityRoles.IT);
                await UserManager.AddToRoleAsync(user.Id, SecurityRoles.Accounting);

                //var result = await SignInManager.PasswordSignInAsync(user.UserName, password, true, false);

                //if (result == SignInStatus.Success)
                //{
                //    return Content("Hello, " + user.FirstName + "<strong>"+user.LastName+"</strong>" + user.LastName);
                //}

                //user.FirstName = "Super";
                //user.LastName = "Admin";

                //await manager.UpdateAsync(user);
                return Content("Hello Aly From Senegal");
            }
            return Content("Hello Aly From Senegal");




            /////////////////////////////////////////////////////////////////////////////
            //var context = new ApplicationDbContext();//DefaultConnection, IdentityDbContext
            //var store = new UserStore<CustomUser>(context);//dentityUser
            //var manager = new UserManager<CustomUser>(store);//
            //var signInManager = new SignInManager<CustomUser, string>(manager, HttpContext.GetOwinContext().Authentication);

            //var email = "foo@bar.com";
            //var password = "Password";

            //var user = await manager.FindByEmailAsync(email);
            //if (user == null)
            //{
            //    user = new CustomUser()
            //    {
            //        UserName = email,
            //        Email = email,
            //        FirstName = "Super",
            //        LastName = "Admin"

            //    };

            //    await manager.CreateAsync(user, password);
            //}

            //else
            //{
            //    var result = await signInManager.PasswordSignInAsync(user.UserName, password, true, false);

            //    if (result == SignInStatus.Success)
            //    {
            //        return Content("Hello, " + user.FirstName + "" + user.LastName);
            //    }

            //    //user.FirstName = "Super";
            //    //user.LastName = "Admin";

            //    //await manager.UpdateAsync(user);
            //}

            //return Content("Hello Aly From Senegal");
        }



        public async Task<ActionResult> Login()
        {
            var email = "foo@bar.com";
           // var password = "Password";
            var user = await UserManager.FindByEmailAsync(email);

           await  SignInManager.SignInAsync(user, true, true);

            return RedirectToAction("Index");
          

        }


        public async Task<ActionResult> Logout()
        {


            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Index");

        }




    }
}