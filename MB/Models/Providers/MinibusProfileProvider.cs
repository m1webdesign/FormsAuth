using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;
using MB.Models.POCO;

namespace MB.Models.Providers
{
    public class MinibusProfileProvider: ProfileProvider
    {
        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
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

        public override System.Configuration.SettingsPropertyValueCollection GetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyCollection collection)
        {
            string userName = context["UserName"].ToString();
            Minibus.Models.Context.MinibusContext _context = new Minibus.Models.Context.MinibusContext();
            UserInfo user = _context.UserInfos.SingleOrDefault(u => u.username == userName);

            //string name = HttpContext.Current.User.Identity.Name;
            //bool isAuthenticated = Convert.ToBoolean(context["IsAuthenticated"]);
            

            SettingsPropertyValueCollection settingsValues = new SettingsPropertyValueCollection();
            foreach (SettingsProperty property in collection)
            {
                SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(property);

                switch (property.Name)
                {
                    case "StaffName":
                        settingsPropertyValue.PropertyValue = user.StaffMember.Firstname + " " + user.StaffMember.Surname;
                        break;
                    default:
                        throw new Exception("Unsupported property.");
                }

                settingsValues.Add(settingsPropertyValue);
            }

            return settingsValues;
        }

        public override void SetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyValueCollection collection)
        {
            string userName = context["UserName"].ToString();

            Minibus.Models.Context.MinibusContext _context = new Minibus.Models.Context.MinibusContext();
            UserInfo user = _context.UserInfos.SingleOrDefault(u => u.username == userName);

            SettingsPropertyValueCollection settingsValues = new SettingsPropertyValueCollection();
            foreach (SettingsPropertyValue property in collection)
            {
                switch (property.Name)
                {
                    case "StaffName":
                        user.email_address = property.PropertyValue.ToString();
                        break;
                    default:
                        throw new Exception("Unsupported property.");
                }

            }

            _context.SaveChanges();

        }
    }
}