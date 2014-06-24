using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class Role
    {
        public Role()
        {
            this.users_roles = new HashSet<users_roles>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<users_roles> users_roles { get; set; }
    }
}