using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.users_roles = new HashSet<users_roles>();
        }

        [Key]
        public string username { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public Nullable<bool> locked { get; set; }
        public Nullable<System.DateTime> locked_date { get; set; }
        public Nullable<System.DateTime> last_login { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }

        [ForeignKey("Account_Types")] 
        public string account_type { get; set; }
        public string ip { get; set; }
        public string security_question { get; set; }
        public string security_answer { get; set; }
        public Nullable<bool> online { get; set; }
        public Nullable<bool> disabled { get; set; }

        public virtual Account_Types Account_Types { get; set; }


        public virtual StaffMember StaffMember { get; set; }
        public virtual ICollection<users_roles> users_roles { get; set; }
    }
}