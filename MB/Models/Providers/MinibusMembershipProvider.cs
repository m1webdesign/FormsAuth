using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using MB.Models.POCO;
using Minibus.Models.Context;

namespace MB.Models.Providers
{
    public class MinibusMembershipProvider: MembershipProvider
    {
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword)) 
                return false; 
            
            if (oldPassword == newPassword) 
                return false; 
            
            CustomMembershipUser user = GetUser(username); 
            
            if (user == null) 
                return false;

            MinibusContext _context = new MinibusContext();
            var RawUser = _context.UserInfos.SingleOrDefault(u => u.username == username);
            
            if (string.IsNullOrWhiteSpace(RawUser.password)) 
                return false; 
            
            RawUser.password = EncodePassword(newPassword); 
            _context.SaveChanges(); 
            
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status, int companyID, StaffMember staffmember) 
        { 
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true); 
            OnValidatingPassword(args); 
            if (args.Cancel) 
            { 
                status = MembershipCreateStatus.InvalidPassword; 
                return null; 
            } 
            
            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty))) { 
                status = MembershipCreateStatus.DuplicateEmail; 
                return null; 
            } 
            
            MembershipUser membershipUser = GetUser(username); 
            
            if (membershipUser == null) { 
                try { 
                    using (MinibusContext _context = new MinibusContext()) {
                        UserInfo user = new UserInfo();
                        user.username = username;
                        user.password = password;
                        user.email_address = email;
                        user.created_date = DateTime.Now;
                        user.disabled = false;

                        _context.UserInfos.Add(user);
                        _context.SaveChanges();
                        status = MembershipCreateStatus.Success; 
                        return GetUser(username); 
                    } 
                } 
                catch { 
                    status = MembershipCreateStatus.ProviderError; 
                } 
            } 
            else { 
                status = MembershipCreateStatus.DuplicateUserName; 
            } 
            return null; 
        } 

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);               
            OnValidatingPassword(args);               
            if (args.Cancel)             
            {                 
                status = MembershipCreateStatus.InvalidPassword;                 
                return null;             
            }               
            
            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty)))             
            {                 
                status = MembershipCreateStatus.DuplicateEmail;                 
                return null;             
            }               
            
            MembershipUser membershipUser = GetUser(username, false);               
            if (membershipUser == null)             
            {                 
                try                
                {                     
                    using (MinibusContext _context = new MinibusContext())                     
                    {
                        UserInfo user = new UserInfo();
                        user.username = username;
                        user.password = password;
                        user.email_address = email;
                        user.created_date = DateTime.Now;
                        user.disabled = false;

                        _context.UserInfos.Add(user);
                        _context.SaveChanges();
                        status = MembershipCreateStatus.Success;                        
                        return GetUser(username, false);                     
                    }                   
                }                 
                
                catch                
                {                     
                    status = MembershipCreateStatus.ProviderError;                 
                }             
            }             
            
            else            
            {                 
                status = MembershipCreateStatus.DuplicateUserName;             
            }               
            
            return null;         
        } 
        

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public CustomMembershipUser GetUser(string username) 
        { 
            CustomMembershipUser CustomMembershipUser = null; 
            using (MinibusContext _context = new MinibusContext()) 
            { 
                try 
                { 
                    UserInfo user = (from u in _context.UserInfos where u.username == username select u).FirstOrDefault(); 
                    if (user != null) 
                    { 
                        CustomMembershipUser = new CustomMembershipUser(this.Name, user.username, null, user.email_address, "", "", true, false, (DateTime)user.created_date, DateTime.Now, DateTime.Now, default(DateTime), default(DateTime), user.StaffMember); 
                    } 
                } 
                catch 
                { } 
            } 
            return CustomMembershipUser; 
        } 

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser membershipUser = null; 
            using (MinibusContext _context = new MinibusContext()) 
            { 
                try 
                { 
                    UserInfo user = (from u in _context.UserInfos where u.username == username select u).FirstOrDefault(); 
                    if (user != null) 
                    {
                        membershipUser = new MembershipUser(this.Name, user.username, null, user.email_address, "", "", true, false, (DateTime)user.created_date, DateTime.Now, DateTime.Now, default(DateTime), default(DateTime)); 
                    } 
                } 
                catch(Exception e)
                {
                    string g = e.ToString();
                } 
            } 
            return membershipUser; 
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        private MachineKeySection machineKey; //Used when determining encryption key values. 

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false; 
            using (MinibusContext _context = new MinibusContext()) 
            { 
                try 
                { 
                    UserInfo user = (from u in _context.UserInfos where u.username == username select u).FirstOrDefault(); 
                    
                    if (user != null) 
                    { 
                        string storedPassword = user.password; 
                        if(storedPassword == password)
                        {
                        //if (CheckPassword(password, storedPassword)) 
                        //{ 
                            isValid = true; 
                        } 
                    } 
                } 
                catch 
                { 
                    isValid = false; 
                } 
            } 
            return isValid; 
  
        }

        private bool CheckPassword(string password, string dbpassword) 
        { 
            string pass1 = password; 
            string pass2 = dbpassword; 
            switch (PasswordFormat) 
            { 
                case MembershipPasswordFormat.Encrypted:                     
                    pass2 = UnEncodePassword(dbpassword); 
                    break; 
                case MembershipPasswordFormat.Hashed:                     
                    pass1 = EncodePassword(password); 
                    break; 
                default:                     
                        break; 
            } 
            if (pass1 == pass2) 
            { return true; } 
            return false; 
        } 

        private string UnEncodePassword(string encodedPassword)         
        {             
            string password = encodedPassword;               
            switch (PasswordFormat)             
            {                 
                case MembershipPasswordFormat.Clear:                     
                    break;                 
                case MembershipPasswordFormat.Encrypted:                     
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));                     
                    break;                 
                case MembershipPasswordFormat.Hashed:                     
                //HMACSHA1 hash = new HMACSHA1();                     
                //hash.Key = HexToByte(machineKey.ValidationKey);                     
                //password = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));                       
                throw new ProviderException("Not implemented password format (HMACSHA1).");                 
                default:                     
                    throw new ProviderException("Unsupported password format.");             
            }               return password;         
        }           
        /// <summary>         
        /// Get config value.         
        /// </summary>         
        /// <param name="configValue"></param>         
        /// <param name="defaultValue"></param>         
        /// <returns></returns>         
        /// 
        
        private string GetConfigValue(string configValue, string defaultValue)         
        {             
            if (String.IsNullOrEmpty(configValue))             
            {                 
                return defaultValue;             
            }               
            return configValue;         
        }           
        
        /// <summary>         
        
        /// Encode password.         
        /// </summary>         
        /// <param name="password">
        /// Password.</param>         
        /// <returns>Encoded password.</returns>         
        /// 
        
        private string EncodePassword(string password)         
        {             
            string encodedPassword = password;               
            switch (PasswordFormat)             
            {                 
                case MembershipPasswordFormat.Clear:                     
                    break;                 
                case MembershipPasswordFormat.Encrypted:                     
                    byte[] encryptedPass = EncryptPassword(Encoding.Unicode.GetBytes(password));                     
                    encodedPassword = Convert.ToBase64String(encryptedPass);                     
                    break;                 
                case MembershipPasswordFormat.Hashed:                     
                    HMACSHA1 hash = new HMACSHA1();                     
                    hash.Key = HexToByte(machineKey.ValidationKey);                     
                    encodedPassword =                       
                        Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));                     
                        break;                 
                default:                     
                    throw new ProviderException("Unsupported password format.");             }               
            return encodedPassword;         
        }

        /// <summary>         
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration         
        /// </summary>         
        /// <param name="hexString"></param>         
        /// <returns></returns>         
        /// <remarks></remarks>         
        
        private byte[] HexToByte(string hexString)         
        {             
            byte[] returnBytes = new byte[hexString.Length / 2];             
            for (int i = 0; i < returnBytes.Length; i++)                 
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);             
            return returnBytes;         
        } 
        
    }
}