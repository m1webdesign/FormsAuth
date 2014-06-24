using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class Account_Type
    {
        public Account_Type()
        {
            this.UserInfoes = new HashSet<UserInfo>();
        }

        public string Type_Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}