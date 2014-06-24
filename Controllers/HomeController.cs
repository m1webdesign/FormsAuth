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
            /*******create new user - have to reference MinibusMembershipProvider as adding custom attributes******/
            //MinibusMembershipProvider provider = Membership.Provider as MinibusMembershipProvider;
            //provider.CreateUser(

            
            //bool g = HttpContext.User.Identity.IsAuthenticated;
            /************get user by creating cutom membership user in cretuser(...) method in memvershipprovider**********/
            //CustomMembershipUser user = Membership.GetUser("2") as CustomMembershipUser;
            //string z = user.Email;

            //ProfileCommon Profile = new ProfileCommon();
            //AccountProfile Profile = AccountProfile.GetProfile();

            //string j = Profile.StaffName;
            //FormsAuthentication.SignOut();
            return View();

            //AccountProfile Profile = AccountProfile.GetProfile();
            //bool g = HttpContext.User.Identity.IsAuthenticated;
            //string f = HttpContext.User.Identity.Name;
            //ProfileCommon Profile = new ProfileCommon();


            //string j = Profile.StaffName; //Profile.StaffName;

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
