using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class Account_Types
    {
        public Account_Types()
        {
            this.UserInfos = new HashSet<UserInfo>();
        }

        [Key]
        public string Type_Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}