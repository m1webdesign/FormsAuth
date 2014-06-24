using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models.Helpers
{
    public class ProfileCommon
    {
        public string StaffName
        {
            get { return (string)HttpContext.Current.Profile.GetPropertyValue("StaffName"); }

            set{
                HttpContext.Current.Profile.SetPropertyValue("StaffName", value); 
            }
        }
    }

}