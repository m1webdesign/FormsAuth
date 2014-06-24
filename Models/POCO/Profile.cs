using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class Profile
    {
        [Key]
        public int ID { get; set; }
        public string current_theme { get; set; }
        public string username { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}