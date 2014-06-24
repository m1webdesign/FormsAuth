using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class users_roles
    {
        [Key]
        public int ID { get; set; }
        public string username { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}