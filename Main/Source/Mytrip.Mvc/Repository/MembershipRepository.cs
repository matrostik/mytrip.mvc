/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Mytrip.Mvc.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// 
    /// </summary>
    internal class MembershipRepository : MembershipProvider
    {
        #region Entities

        Entities _entities;
        /// <summary>
        /// 
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(CoreSetting.connectionString());
                return _entities;
            }
        }

        #endregion

        #region MembershipProvider public override
        /// <summary>
        /// 
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return UsersSetting.applicationName();
            }
            set
            { string applicationName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="newPasswordQuestion"></param>
        /// <param name="newPasswordAnswer"></param>
        /// <returns></returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool EnablePasswordReset
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return UsersSetting.maxInvalidPasswordAttempts();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override int MinRequiredPasswordLength
        {
            get
            {
                return UsersSetting.minRequiredPasswordLength();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override int PasswordAttemptWindow
        {
            get
            {
                return 10;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get
            {
                return UsersSetting.requiresUniqueEmail();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public override string ResetPassword(string username, string answer)
        {
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public override void UpdateUser(MembershipUser user)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.ProviderError;
            if (!mtAccessibleUserName(username))
                status = MembershipCreateStatus.DuplicateUserName;
            else if (String.IsNullOrEmpty(username))
                status = MembershipCreateStatus.InvalidUserName;
            else if (String.IsNullOrEmpty(password))
                status = MembershipCreateStatus.InvalidPassword;
            else if (password.Length < UsersSetting.minRequiredPasswordLength())
                status = MembershipCreateStatus.InvalidPassword;
            else if (UsersSetting.requiresUniqueEmail() && !mtAccessibleEmail(email))
                status = MembershipCreateStatus.DuplicateEmail;
            else if (String.IsNullOrEmpty(email))
                status = MembershipCreateStatus.InvalidEmail;
            else if (!Regex.IsMatch(email, "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$"))
                status = MembershipCreateStatus.InvalidEmail;
            else
            {
                string userId = mtCreateUserByUserId();
                mtCreateUser(userId, username);
                mtCreateMembership(userId, password, email, isApproved);
                status = MembershipCreateStatus.Success;
            }
            var user = mtGetUserByUserNameMember(username);
            return CreateMembershipFromInternalUser(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="deleteAllRelatedData"></param>
        /// <returns></returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            bool result = false;
            var user = mtGetUserByUserName(username);
            if (user != null)
            {
                mtRemoveRolesFromUser(username);
                mtDeleteMembership(username);
                mytrip_users x = user;
                entities.DeleteObject(x);
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailToMatch"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var users = entities.mytrip_users.Include("mytrip_usersmembership").Where(x => x.mytrip_usersmembership.Email.IndexOf(emailToMatch) != -1)
                .OrderByDescending(x => x.LastActivityDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalRecords = entities.mytrip_users.Include("mytrip_usersmembership").Where(x => x.mytrip_usersmembership.Email.IndexOf(emailToMatch) != -1)
                .OrderByDescending(x => x.LastActivityDate).Count();
            return CreateMembershipCollectionFromInternalList(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usernameToMatch"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var users = entities.mytrip_users.Include("mytrip_usersmembership").Where(x => x.UserName.IndexOf(usernameToMatch) != -1)
                .OrderByDescending(x => x.LastActivityDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalRecords = entities.mytrip_users.Include("mytrip_usersmembership").Where(x => x.UserName.IndexOf(usernameToMatch) != -1)
                .OrderByDescending(x => x.LastActivityDate).Count();
            return CreateMembershipCollectionFromInternalList(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var users = entities.mytrip_users.Include("mytrip_usersmembership").OrderByDescending(x => x.LastActivityDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalRecords = entities.mytrip_users.Include("mytrip_usersmembership").OrderByDescending(x => x.LastActivityDate).Count();
            return CreateMembershipCollectionFromInternalList(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetNumberOfUsersOnline()
        {
            var users = entities.mytrip_users.OrderByDescending(x => x.LastActivityDate);
            int result = 0;
            foreach (mytrip_users user in users)
            {
                if (user.LastActivityDate.AddDays(Membership.UserIsOnlineTimeWindow) >= DateTime.Now)
                {
                    result++;
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = mtGetUserByUserNameMember(username);
            if (user != null)
            {
                return CreateMembershipFromInternalUser(user);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerUserKey"></param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = mtGetUserByUserIdMember(providerUserKey.ToString());

            if (user != null)
            {
                return CreateMembershipFromInternalUser(user);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override string GetUserNameByEmail(string email)
        {
            string result = string.Empty;
            var user = entities.mytrip_users.Include("mytrip_usersmembership").FirstOrDefault(x => x.mytrip_usersmembership.Email == email);
            if (user != null)
                result = user.UserName;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override bool UnlockUser(string userName)
        {
            bool result = false;
            var user = entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(x => x.mytrip_users.UserName == userName);
            if (user != null)
            {
                mytrip_usersmembership x = user;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override bool ValidateUser(string username, string password)
        {
            bool result = false;
            if (mtCheckUserName(username))
            {
                var user = mtGetMembershipByUserName(username);
                if (user != null && mtHashPassword(password, user.PasswordSalt) == user.Password)
                {
                    mtUpdateUserIP(user);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            var user = mtGetMembershipByUserName(username);
            if (user != null && mtHashPassword(oldPassword, user.PasswordSalt) == user.Password)
            {
                string newGuid = Guid.NewGuid().ToString();
                mytrip_usersmembership x = user;
                x.Password = mtHashPassword(newPassword, newGuid);
                x.PasswordSalt = newGuid;
                x.LastPasswordChangedDate = DateTime.Now;
                entities.SaveChanges();
                result = true;
            }
            return result;
        }

        #endregion

        #region MembershipProvider public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal mytrip_users mtGetUserByUserNameMember(string userName)
        {
            return entities.mytrip_users.Include("mytrip_usersmembership").FirstOrDefault(x => x.UserName == userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        internal IQueryable<mytrip_users> mtGetAllUsersPaginal(int pageIndex, int pageSize, string sorting, out int total)
        {
            total = entities.mytrip_users.OrderByDescending(x => x.LastActivityDate).Count();
            IQueryable<mytrip_users> users = null;
            if (sorting == "UserName")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserName")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "Email")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.mytrip_usersmembership.Email).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_Email")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.mytrip_usersmembership.Email).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastActivityDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.LastActivityDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastActivityDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.LastActivityDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "CreationDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.mytrip_usersmembership.CreationDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_CreationDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.mytrip_usersmembership.CreationDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastLoginDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.mytrip_usersmembership.LastLoginDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastLoginDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.mytrip_usersmembership.LastLoginDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "LastPasswordChangedDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.mytrip_usersmembership.LastPasswordChangedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_LastPasswordChangedDate")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.mytrip_usersmembership.LastPasswordChangedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "UserIP")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderBy(x => x.mytrip_usersmembership.UserIP).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserIP")
                users = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").OrderByDescending(x => x.mytrip_usersmembership.UserIP).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (!String.IsNullOrEmpty(sorting))
            {
                var user = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").Where(x => x.UserName.IndexOf(sorting) != -1);
                var _user = entities.mytrip_users.Include("mytrip_usersroles").Include("mytrip_usersmembership").Where(x => x.mytrip_usersmembership.Email.IndexOf(sorting) != -1);
                total = user.Union(_user).Count();
                users = user.Union(_user).OrderBy(x => x.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return users;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal bool mtAccessibleUserName(string username)
        {
            bool result = true;
            var _users = mtGetUserByUserName(username);
            if (_users != null)
                result = false;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        internal bool mtAccessibleEmail(string email)
        {
            bool result = true;
            var _users = entities.mytrip_usersmembership.FirstOrDefault(x => x.Email == email);
            if (_users != null)
                result = false;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal bool mtCheckNullUserName(string username)
        {
            bool result = false;
            var users = mtGetUserByUserNameMember(username);
            if (users != null)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal bool mtCheckUserName(string username)
        {
            bool result = false;
            var users = mtGetUserByUserNameMember(username);
            if (users != null)
            {
                bool _users = users.mytrip_usersmembership.IsApproved;
                if (_users)
                    result = true;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saptcha"></param>
        /// <returns></returns>
        internal string mtHashCaptcha(string saptcha)
        {
            string c = FormsAuthentication
                .HashPasswordForStoringInConfigFile((UsersSetting.applicationName() + saptcha), "SHA1");
            return c;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        internal bool mtCheckOldPassword(string OldPassword)
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
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal string BlokUserEmail(string userName)
        {
            var user = entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(x => x.mytrip_users.UserName == userName);
            if (user != null)
            {
                mytrip_usersmembership x = user;
                if (user.IsApproved)
                {
                    x.IsApproved = false;
                }
                entities.SaveChanges();
            }
            return user.UserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        internal string ApprovedUserEmail(string UserId)
        {
            var user = entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(x => x.UserId == UserId);
            if (user != null)
            {
                mytrip_usersmembership x = user;
                if (!user.IsApproved)
                {
                    x.IsApproved = true;
                }
                entities.SaveChanges();
                return user.mytrip_users.UserName;
            }else
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IQueryable<mytrip_users> mtGetAllUsers()
        {
            return entities.mytrip_users
                .Include("mytrip_usersroles")
                .Include("mytrip_usersmembership")
                .OrderBy(x => x.UserName);
        }
        /// <summary>
        /// Change Email
        /// </summary>
        /// <param name="email">new e-mail</param>
        /// <returns></returns>
        internal void ChangeEmail(string email)
        {
            var user = entities.mytrip_users.Include("mytrip_usersmembership").FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                user.mytrip_usersmembership.Email = email;
                entities.SaveChanges();
            }
        }
        #endregion

        #region MembershipProvider private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private MembershipUserCollection CreateMembershipCollectionFromInternalList(IQueryable<mytrip_users> users)
        {
            MembershipUserCollection ReturnCollection = new MembershipUserCollection();
            foreach (mytrip_users user in users)
            {
                ReturnCollection.Add(CreateMembershipFromInternalUser(user));
            }
            return ReturnCollection;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private MembershipUser CreateMembershipFromInternalUser(mytrip_users user)
        {
            MembershipUser _user = new MembershipUser(base.Name, user.UserName, user.UserId,
                user.mytrip_usersmembership.Email, string.Empty, string.Empty, user.mytrip_usersmembership.IsApproved, false,
                user.mytrip_usersmembership.CreationDate, user.mytrip_usersmembership.LastLoginDate, user.LastActivityDate, user.mytrip_usersmembership.LastPasswordChangedDate,
                user.mytrip_usersmembership.LastLockoutDate);
            return _user;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByUserId(string userid)
        {
            return entities.mytrip_users.FirstOrDefault(x => x.UserId == userid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByUserIdMember(string userid)
        {
            return entities.mytrip_users.Include("mytrip_usersmembership").FirstOrDefault(x => x.UserId == userid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByUserName(string userName)
        {
            return entities.mytrip_users.FirstOrDefault(x => x.UserName == userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByUserNameRole(string userName)
        {
            return entities.mytrip_users.Include("mytrip_usersroles").FirstOrDefault(x => x.UserName == userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns></returns>
        private mytrip_usersroles mtGetRoleByRoleName(string rolename)
        {
            return entities.mytrip_usersroles.FirstOrDefault(x => x.RoleName == rolename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        private void mtCreateUser(string userId, string userName)
        {
            mytrip_users x = new mytrip_users
            {
                UserId = userId,
                UserName = userName,
                LastActivityDate = DateTime.Now
            };
            entities.mytrip_users.AddObject(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="isApproved"></param>
        private void mtCreateMembership(string userId, string password, string email, bool isApproved)
        {
            string newGuid = Guid.NewGuid().ToString();
            mytrip_usersmembership x = new mytrip_usersmembership
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
            entities.mytrip_usersmembership.AddObject(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordsalt"></param>
        /// <returns></returns>
        private string mtHashPassword(string password, string passwordsalt)
        {
            string hash = FormsAuthentication
                .HashPasswordForStoringInConfigFile((passwordsalt + password), "SHA1");
            return hash;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        private void mtRemoveRolesFromUser(string username)
        {
            var user = mtGetUserByUserNameRole(username);
            if (user.mytrip_usersroles != null)
            {
                foreach (mytrip_usersroles x in user.mytrip_usersroles.ToList())
                {
                    var role = mtGetRoleByRoleName(x.RoleName);
                    user.mytrip_usersroles.Remove(role);
                    entities.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        private void mtDeleteMembership(string username)
        {
            mytrip_usersmembership x = entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(y => y.mytrip_users.UserName == username);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private mytrip_usersmembership mtGetMembershipByUserName(string username)
        {
            return entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(x => x.mytrip_users.UserName == username);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        private void mtUpdateUserIP(mytrip_usersmembership user)
        {
            mytrip_usersmembership x = user;
            x.UserIP = HttpContext.Current.Request.UserHostAddress;
            entities.SaveChanges();
            mytrip_users y = mtGetUserByUserName(user.mytrip_users.UserName);
            y.LastActivityDate = DateTime.Now;
            entities.SaveChanges();
        }

        #endregion

        #region OpenId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="approved"></param>
        /// <param name="yes"></param>
        /// <returns></returns>
        internal string UserNameOpenId(string provider, out bool approved, out bool yes)
        {
            mytrip_users user = null;
                user = entities.mytrip_users.Include("mytrip_usersmembership")
                    .Where(x => x.mytrip_usersmembership.Password == provider)
                    .FirstOrDefault(x => x.mytrip_usersmembership.PasswordSalt == provider);            
            string result = string.Empty;
            approved = false;
            yes = false;
            if (user != null)
            {
                mtUpdateUserIP(user.mytrip_usersmembership);
                result = user.UserName;
                approved = user.mytrip_usersmembership.IsApproved;
                yes = true;

            } return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="provider"></param>
        /// <param name="isApproved"></param>
        internal void CreateUserOpenId(string userName, string email, string provider, bool isApproved)
        {
            string userId = mtCreateUserByUserId();
            mytrip_users x = new mytrip_users
            {
                UserId = userId,
                UserName = userName,
                LastActivityDate = DateTime.Now
            };
            entities.mytrip_users.AddObject(x);
            entities.SaveChanges();
            CreateMembershipOpenId(userId, provider, email, isApproved);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="provider"></param>
        /// <param name="email"></param>
        /// <param name="isApproved"></param>
        private void CreateMembershipOpenId(string userId, string provider, string email, bool isApproved)
        {

            mytrip_usersmembership x = new mytrip_usersmembership
            {
                UserId = userId,
                Password = provider,
                PasswordSalt = provider,
                Email = email,
                IsApproved = isApproved,
                CreationDate = DateTime.Now,
                LastLockoutDate = DateTime.MaxValue,
                LastLoginDate = DateTime.Now,
                LastPasswordChangedDate = DateTime.Now,
                UserIP = HttpContext.Current.Request.UserHostAddress
            };
            entities.mytrip_usersmembership.AddObject(x);
            entities.SaveChanges();
        }

        #endregion

        internal IQueryable<mytrip_usersmembership> GetMembershipForForgotPassword(string email)
        {
            return entities.mytrip_usersmembership.Include("mytrip_users").Where(x => x.Email == email)
                .OrderBy(x=>x.Email);
        }
        internal void CreateDateCangePassword(mytrip_usersmembership x)
        {
            x.LastPasswordChangedDate = DateTime.Now.AddDays(1);
            
        }
        internal void SaveChanges()
        { entities.SaveChanges(); }
        internal bool DateCangePassword(string id)
        {
            var x = entities.mytrip_usersmembership.FirstOrDefault(y => y.UserId == id); 
            bool result = (x!=null&&DateTime.Now <= x.LastPasswordChangedDate) ? true : false;
            return result;
        }
        internal string RecoveryPassword(string id,string password)
        {
            string newGuid = Guid.NewGuid().ToString();
            var x = entities.mytrip_usersmembership.Include("mytrip_users").FirstOrDefault(y => y.UserId == id); 
            x.Password = mtHashPassword(password, newGuid);
            x.PasswordSalt = newGuid;
            entities.SaveChanges();
            return x.mytrip_users.UserName;
        }
    }
}