using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MB.Models.POCO
{
    public class CustomMembershipUser: MembershipUser
    {
        public StaffMember currentStaffMember { get; set; }           
        
        public CustomMembershipUser(
            string providername, 
            string username, 
            object providerUserKey, 
            string email, 
            string passwordQuestion, 
            string comment, 
            bool isApproved, 
            bool isLockedOut, 
            DateTime creationDate, 
            DateTime lastLoginDate, 
            DateTime lastActivityDate, 
            DateTime lastPasswordChangedDate, 
            DateTime lastLockedOutDate, 
            StaffMember currentstaffmember) : 
            base(providername, 
            username, 
            providerUserKey, 
            email, 
            passwordQuestion, 
            comment, 
            isApproved, 
            isLockedOut, 
            creationDate, 
            lastLoginDate, 
            lastPasswordChangedDate, 
            lastActivityDate, 
            lastLockedOutDate) 
        {
            currentStaffMember = currentstaffmember;
        } 
    }
}