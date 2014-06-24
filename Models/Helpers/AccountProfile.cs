using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace MB.Models.Helpers
{
    public class AccountProfile : ProfileBase
    {

        public string StaffName
        {
            get { return (string)GetPropertyValue("StaffName"); }
        }


        /// <summary>
        /// Get the profile of the currently logged-on user.
        /// </summary>      
        public static AccountProfile GetProfile()
        {
            return (AccountProfile)HttpContext.Current.Profile;
        }

        /// <summary>
        /// Gets the profile of a specific user.
        /// </summary>
        /// <param name="userName">The user name of the user whose profile you want to retrieve.</param>
        public static AccountProfile GetProfile(string userName)
        {
            return (AccountProfile)Create(userName);
        }



        //// add additional properties here

    }
}