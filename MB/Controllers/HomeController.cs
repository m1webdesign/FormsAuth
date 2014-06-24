using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MB.Helper;
using MB.Models.Helpers;
using MB.Models.POCO;
using MB.Models.Providers;
using Minibus.Models.Context;


namespace MB.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [SessionExpired]
        [AllowAnonymous]
        
        public ActionResult Index()
        {
            return View();

        }

        [AllowAnonymous]
        [SessionExpired]
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (Membership.ValidateUser(login.UserName, login.Password))
            {
                FormsAuthentication.SetAuthCookie(login.UserName, true);
                return Json(new { redirectTo = Url.Action("Index", "MakeBooking") });
                //return RedirectToAction("Index", "MakeBooking");
            }

            ModelState.AddModelError("", "User Credentials are not Recognised");

            return Json(new { errorMessage = "Username or Password Incorrect" });
        }



    }
}
