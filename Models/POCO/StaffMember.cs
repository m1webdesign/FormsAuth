using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MB.Models.POCO
{
    public class StaffMember
    {
        public StaffMember()
        {
            this.BookingInfos = new HashSet<BookingInfo>();
        }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        [Key, ForeignKey("UserInfo")]

        public string UserID { get; set; }

        public virtual ICollection<BookingInfo> BookingInfos { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}