using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indentity.Controllers
{
   // [Authorize]
    public class CompanyController : Controller
    {
        // GET: Company
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult EmployeeList()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content("Private Employee List");
            }
            return Content("Employee List");
        }
    }
}