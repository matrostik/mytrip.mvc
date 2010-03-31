//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Text;
using Mytrip.Core.Repository.MsSqlUsers;
using Mytrip.Core.Repository.XmlUsers;

namespace Mytrip.Core.Repository
{
    public class MembershipRepository : MembershipProvider
    {
        usersEntities _entities;
        public usersEntities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new usersEntities(UsersSetting.connectionString);
                return _entities;
            }
        }
        MsSqlMembershipRepository _mssqlUser;
        public MsSqlMembershipRepository mssqlUser
        {
            get
            {
                if (_mssqlUser == null)
                    _mssqlUser = new MsSqlMembershipRepository();
                return _mssqlUser;
            }
        }
        XmlMembershipRepository _xmlUser;
        public XmlMembershipRepository xmlUser
        {
            get
            {
                if (_xmlUser == null)
                    _xmlUser = new XmlMembershipRepository();
                return _xmlUser;
            }
        }
        public override string ApplicationName
        {
            get
            {
                return UsersSetting.applicationName;
            }
            set { UsersSetting.applicationName = value; }
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlChangePassword(username, oldPassword, newPassword);
            }
            else if (UsersSetting.membership == "XML") {
                result = xmlUser.xmlChangePassword(username, oldPassword, newPassword);
            }
            return result;
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {

            return false;
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.ProviderError;
            if (UsersSetting.membership == "MSSQL")
            {
                status = mssqlUser.mssqlCreateUser(username, password, email, isApproved);
            }
            else if (UsersSetting.membership == "XML") { 
                status = xmlUser.xmlCreateUser(username, password, email, isApproved);
            }
            return CreateMembershipFromInternalUser(username);
        }
        private MembershipUser CreateMembershipFromInternalUser(string Username)
        {
            string baseName = base.Name;
            MembershipUser user = new MembershipUser(baseName, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, false, false,
                DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
                DateTime.Now);            
            if (UsersSetting.membership == "MSSQL")
            {
                user = mssqlUser.mssqlCreateMembershipFromInternalUser(Username, baseName);
            }
            else if (UsersSetting.membership == "XML")
            {
                user = xmlUser.xmlCreateMembershipFromInternalUser(Username, baseName);
            }
            return user;
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlDeleteUser(username);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlDeleteUser(username);
            }
            return result;
        }
        public override bool EnablePasswordReset
        {
            get
            {
                return true;
            }
        }
        public override bool EnablePasswordRetrieval
        {
            get
            {
                return false;
            }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            int total;
            var a = CreateMembershipCollectionFromInternalList(emailToMatch, "", pageIndex, pageSize, out total);
            totalRecords = total;
            return a;
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            int total;
            var a = CreateMembershipCollectionFromInternalList("", usernameToMatch, pageIndex, pageSize, out total);
            totalRecords = total;
            return a;
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            int total;
            var a= CreateMembershipCollectionFromInternalList("","", pageIndex, pageSize, out total);
            totalRecords = total;
            return a;
        }
        private MembershipUserCollection CreateMembershipCollectionFromInternalList(string emailToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            string[] result=new string[0];
            int _totalRecords = 0;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlCreateMembershipCollectionFromInternalList(emailToMatch, usernameToMatch, pageIndex, pageSize, out totalRecords);
                _totalRecords = totalRecords;
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlCreateMembershipCollectionFromInternalList(emailToMatch, usernameToMatch, pageIndex, pageSize, out totalRecords);
                _totalRecords = totalRecords;
            }
            totalRecords = _totalRecords;
            MembershipUserCollection ReturnCollection = new MembershipUserCollection();
            foreach (string user in result)
            {
                ReturnCollection.Add(CreateMembershipFromInternalUser(user));
            }
            return ReturnCollection;
        }
        public override int GetNumberOfUsersOnline()
        {
            int result = 0;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetNumberOfUsersOnline();
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetNumberOfUsersOnline();
            }
            return result;
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = mtGetUserByUserName(username);
            if (user != null)
            {
                if (userIsOnline)
                {
                    mtLastActivityDate(username);
                }
                return CreateMembershipFromInternalUser(username);
            }
            else
            {
                return null;
            }
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = mtGetUserByUserId(providerUserKey.ToString());
            if (user != null)
            {
                if (userIsOnline)
                {
                    mtLastActivityDate(user.UserName);
                }
                return CreateMembershipFromInternalUser(user.UserName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get User Name By Email
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns></returns>
        public override string GetUserNameByEmail(string email)
        {
            string result = string.Empty;
            var user = entities.mytrip_Users.FirstOrDefault(x => x.mytrip_Membership.Email == email);
            if (user != null)
                result = user.UserName;
            return result;
        }
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return UsersSetting.maxInvalidPasswordAttempts;
            }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return 0;
            }
        }
        public override int MinRequiredPasswordLength
        {
            get
            {
                return UsersSetting.minRequiredPasswordLength;
            }
        }
        public override int PasswordAttemptWindow
        {
            get
            {
                return 10;
            }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        public override bool RequiresUniqueEmail
        {
            get
            {
                return UsersSetting.requiresUniqueEmail;
            }
        }
        public override string ResetPassword(string username, string answer)
        {
            return string.Empty;
        }
        public override bool UnlockUser(string userName)
        {
            bool result = false;
            var user = entities.mytrip_Membership.FirstOrDefault(x=>x.mytrip_Users.UserName==userName);
            if (user != null)
            {
                mytrip_Membership x = user;
                if (user.IsApproved)
                {
                    x.IsApproved = false;
                }
                else
                {
                    x.IsApproved = true;
                }
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        public override void UpdateUser(MembershipUser user)
        {

        }
        public override bool ValidateUser(string username, string password)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlValidateUser(username, password);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlValidateUser(username, password);
            }
            return result;
        }
        /// <summary>
        /// Get User by UserId
        /// </summary>
        /// <param name="userid">UserId</param>
        /// <returns>mytrip_Users</returns>
        private mytrip_Users mtGetUserByUserId(string userid)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserId == userid);
        }
        /// <summary>
        /// Create Unique UserId
        /// </summary>
        /// <returns>string</returns>
        private string mtCreateUserByUserId()
        {
            string result = Guid.NewGuid().ToString();
            while (mtGetUserByUserId(result) != null)
            {
                result = Guid.NewGuid().ToString();
            }
            return result;
        }
        /// <summary>
        /// Get User by User Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>mytrip_Users</returns>
        public mytrip_Users mtGetUserByUserName(string username)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserName == username);
           
        }
        /// <summary>
        /// Get User by User Name or Email
        /// </summary>
        /// <param name="userNameOrEmail">User Name or Email</param>
        /// <returns>mytrip_Users</returns>
        public mytrip_Users mtGetUserByUserNameOrEmail(string userNameOrEmail)
        {
            var user = entities.mytrip_Users.FirstOrDefault(x => x.UserName == userNameOrEmail);
            if (user == null)
                user = entities.mytrip_Users.FirstOrDefault(x => x.mytrip_Membership.Email == userNameOrEmail);
            return user;
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="userName">User Name</param>
        private void mtCreateUser(string userId, string userName)
        {
            mytrip_Users x = new mytrip_Users
            {
                UserId = userId,
                UserName = userName,
                LastActivityDate = DateTime.Now
            };
            entities.AddTomytrip_Users(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// Create Membership
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="isApproved">Is Approved</param>
        private void mtCreateMembership(string userId, string password, string email, bool isApproved)
        {
            string newGuid = Guid.NewGuid().ToString();
            mytrip_Membership x = new mytrip_Membership
            {
                UserId = userId,
                Password = mtHashPassword(password, newGuid),
                PasswordSalt = newGuid,
                Email = email,
                IsApproved = isApproved,
                CreationDate = DateTime.Now,
                LastLockoutDate = DateTime.MaxValue,
                LastLoginDate = DateTime.Now,
                LastPasswordChangedDate = DateTime.Now,
                UserIP = HttpContext.Current.Request.UserHostAddress
            };
            entities.AddTomytrip_Membership(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// Hash Password
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="passwordsalt">Password Salt</param>
        /// <returns>string</returns>
        private string mtHashPassword(string password, string passwordsalt)
        {
            string hash = FormsAuthentication
                .HashPasswordForStoringInConfigFile((passwordsalt + password), "SHA1");
            return hash;
        }
        /// <summary>
        /// Get Membership by User Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>mytrip_Membership</returns>
        private mytrip_Membership mtGetMembershipByUserName(string username)
        {
            string userId = mtGetUserIdbyUserName(username);
            return entities.mytrip_Membership.FirstOrDefault(x => x.UserId == userId);
        }
        /// <summary>
        /// Get UserId by User Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>string</returns>
        private string mtGetUserIdbyUserName(string username)
        {
            return mtGetUserByUserName(username).UserId;
        }
        /// <summary>
        /// Remove Roles ftom User
        /// </summary>
        /// <param name="username">User Name</param>
        private void mtRemoveRolesFromUser(string username)
        {
            var user = mtGetUserByUserName(username);
            if (user.mytrip_Roles != null)
            {
                foreach (mytrip_Roles x in user.mytrip_Roles.ToList())
                {
                    var role = mtGetRoleByRoleName(x.RoleName);
                    user.mytrip_Roles.Remove(role);
                    entities.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Get Role by Role Name
        /// </summary>
        /// <param name="rolename">Role Name</param>
        /// <returns>mytrip_Roles</returns>
        private mytrip_Roles mtGetRoleByRoleName(string rolename)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleName == rolename);
        }
        /// <summary>
        /// Delete Membership
        /// </summary>
        /// <param name="username">User Name</param>
        private void mtDeleteMembership(string username)
        {
            mytrip_Membership x = entities.mytrip_Membership.FirstOrDefault(y=>y.mytrip_Users.UserName==username);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// Last Activity Date
        /// </summary>
        /// <param name="username">User Name</param>
        public void mtLastActivityDate(string username)
        {
            mytrip_Users x = mtGetUserByUserName(username);
            x.LastActivityDate = DateTime.Now;
            entities.SaveChanges();
        }
        /// <summary>
        /// Update UserIP
        /// </summary>
        /// <param name="username">User Name</param>
        private void mtUpdateUserIP(string username)
        {
            mytrip_Membership x = mtGetMembershipByUserName(username);
            x.UserIP = HttpContext.Current.Request.UserHostAddress;
            entities.SaveChanges();
        }
        /// <summary>
        /// Accessible User Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>bool</returns>
        public bool mtAccessibleUserName(string username)
        {
            bool result = true;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlAccessibleUserName(username);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlAccessibleUserName(username);
            }         
            return result;
        }
        /// <summary>
        /// Accessible Email
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>bool</returns>
        public bool mtAccessibleEmail(string email)
        {
            bool result = true;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlAccessibleEmail(email);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlAccessibleEmail(email);
            }
            return result;
        }
        /// <summary>
        /// Check User Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>bool</returns>
        public bool mtCheckUserName(string username)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlCheckUserName(username);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlCheckUserName(username);
            }
            return result;
        }
        /// <summary>
        /// Check Old Password
        /// </summary>
        /// <param name="OldPassword">Old Password</param>
        /// <returns></returns>
        public bool mtCheckOldPassword(string OldPassword)
        {
            bool result = false;
            var user = mtGetMembershipByUserName(HttpContext.Current.User.Identity.Name);
            if (user != null && mtHashPassword(OldPassword, user.PasswordSalt) == user.Password)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Get All Users Paginal
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sorting">Sorting</param>
        /// <param name="total">Total</param>
        /// <returns>IQueryable(mytrip_Users)</returns>
        public IQueryable<mytrip_Users> mtGetAllUsersPaginal(int pageIndex, int pageSize, string sorting, out int total)
        {    
            total = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate).Count();
            var users = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);            
            if (sorting == "UserName")
                users = entities.mytrip_Users.OrderBy(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserName")
                users = entities.mytrip_Users.OrderByDescending(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "Email")
                users = entities.mytrip_Users.OrderBy(x => x.mytrip_Membership.Email).Skip((pageIndex - 1) * pageSize).Take(pageSize);
           else if (sorting == "_Email")
                users = entities.mytrip_Users.OrderByDescending(x => x.mytrip_Membership.Email).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastActivityDate")
                users = entities.mytrip_Users.OrderBy(x => x.LastActivityDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastActivityDate")
                users = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "CreationDate")
                users = entities.mytrip_Users.OrderBy(x => x.mytrip_Membership.CreationDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_CreationDate")
                users = entities.mytrip_Users.OrderByDescending(x => x.mytrip_Membership.CreationDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastLoginDate")
                users = entities.mytrip_Users.OrderBy(x => x.mytrip_Membership.LastLoginDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastLoginDate")
                users = entities.mytrip_Users.OrderByDescending(x => x.mytrip_Membership.LastLoginDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastPasswordChangedDate")
                users = entities.mytrip_Users.OrderBy(x => x.mytrip_Membership.LastPasswordChangedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastPasswordChangedDate")
                users = entities.mytrip_Users.OrderByDescending(x => x.mytrip_Membership.LastPasswordChangedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "UserIP")
                users = entities.mytrip_Users.OrderBy(x => x.mytrip_Membership.UserIP).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserIP")
                users = entities.mytrip_Users.OrderByDescending(x => x.mytrip_Membership.UserIP).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (!String.IsNullOrEmpty(sorting)) 
            {
                    var user = entities.mytrip_Users.Where(x => x.UserName.IndexOf(sorting) != -1);
                    var _user = entities.mytrip_Users.Where(x => x.mytrip_Membership.Email.IndexOf(sorting) != -1);
                    total = user.Union(_user).Count();
                    users = user.Union(_user).OrderBy(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return users;
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="username">User Name</param>
        public void mtDeleteUser(string username)
        {
            var user = mtGetUserByUserName(username);
            if (user != null)
            {
                mtRemoveRolesFromUser(username);
                mtDeleteMembership(username);
                mytrip_Users x = user;
                entities.DeleteObject(x);
                entities.SaveChanges();
            }
        }
        /// <summary>
        /// Hash Captcha
        /// </summary>
        /// <param name="saptcha">captcha</param>
        /// <returns>string</returns>
        public string mtHashCaptcha(string saptcha)
        {
            string c = FormsAuthentication
                .HashPasswordForStoringInConfigFile((UsersSetting.applicationName + saptcha), "SHA1");
            return c;
        }
    }
}