using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MB.Models.Providers
{
    public class MinibusRoleProvider: RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (Minibus.Models.Context.MinibusContext _context = new Minibus.Models.Context.MinibusContext())
            {
                MB.Models.POCO.UserInfo user = _context.UserInfos.SingleOrDefault(u => u.username == username);
                IList<string> roles = new List<string>();
                foreach(MB.Models.POCO.users_roles ur in user.users_roles)
                {
                    roles.Add(ur.Role.Name);
                }
                return roles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using(Minibus.Models.Context.MinibusContext _context = new Minibus.Models.Context.MinibusContext())
            {
                MB.Models.POCO.UserInfo user = _context.UserInfos.SingleOrDefault(u => u.username == username);
                return user.users_roles.Any(ur => ur.Role.Name == roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}