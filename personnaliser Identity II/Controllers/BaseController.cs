using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;  //using Microsoft.AspNet.Identity.Owin à rentrer manuellement intellisense ne le détecte pas

namespace Indentity.Controllers
{
    public abstract  class BaseController : Controller
    {


        public ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().Get<ApplicationUserManager>(); }
        }
        //// GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ApplicationSignInManager SignInManager
        {

            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                
            }
        }




    }
}