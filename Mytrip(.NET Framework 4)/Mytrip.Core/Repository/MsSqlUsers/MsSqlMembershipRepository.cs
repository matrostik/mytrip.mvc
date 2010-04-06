using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Web;

namespace Mytrip.Core.Repository.MsSqlUsers
{
    public class MsSqlMembershipRepository
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
        public string mssqlGetUserEmail(string username)
        {
            string result = string.Empty;
            var user = mssqlGetMembershipByUserName(username);
            result = user.Email;
            return result;
        }
        public bool mssqlCheckOldPassword(string OldPassword)
        {
            bool result = false;
            var user = mssqlGetMembershipByUserName(HttpContext.Current.User.Identity.Name);
            if (user != null && mssqlHashPassword(OldPassword, user.PasswordSalt) == user.Password)
            {
                result = true;
            }
            return result;
        }
        public IQueryable<mytrip_Users> mssqlGetAllUsersPaginal(int pageIndex, int pageSize, string sorting, out int total)
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
        public bool mssqlUnlockUser(string userName)
        {
            bool result = false;
            var user = entities.mytrip_Membership.FirstOrDefault(x => x.mytrip_Users.UserName == userName);
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
        public string mssqlGetUserNameByEmail(string email)
        {
            string result = string.Empty;
            var user = entities.mytrip_Users.FirstOrDefault(x => x.mytrip_Membership.Email == email);
            if (user != null)
                result = user.UserName;
            return result;
        }
        public string mssqlGetUser(object providerUserKey, bool userIsOnline)
        {
            var user = mssqlGetUserByUserId(providerUserKey.ToString());
            if (user != null)
            {
                if (userIsOnline)
                {
                    mssqlLastActivityDate(user);
                }
                return user.UserName;
            }
            else
            {
                return string.Empty;
            }
        }
        public string mssqlGetUser(string username, bool userIsOnline)
        {
            var user = mssqlGetUserByUserName(username);
            if (user != null)
            {
                if (userIsOnline)
                {
                    mssqlLastActivityDate(user);
                }
                return username;
            }
            else
            {
                return string.Empty;
            }
        }
        public void mssqlLastActivityDate(mytrip_Users username)
        {
            mytrip_Users x = username;
            x.LastActivityDate = DateTime.Now;
            entities.SaveChanges();
        }
        public int mssqlGetNumberOfUsersOnline()
        {
            var users = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate);
            int result = 0;
            foreach (mytrip_Users user in users)
            {
                if (user.LastActivityDate.AddDays(Membership.UserIsOnlineTimeWindow) >= DateTime.Now)
                {
                    result++;
                }
            }
            return result;
        }
        public string[] mssqlCreateMembershipCollectionFromInternalList(string emailToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var users = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate)
                          .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalRecords = entities.mytrip_Users.OrderByDescending(x => x.LastActivityDate).Count();
            if (emailToMatch != null)
            {
                users = entities.mytrip_Users.Where(x => x.mytrip_Membership.Email.IndexOf(emailToMatch) != -1)
                  .OrderByDescending(x => x.LastActivityDate)
                  .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalRecords = entities.mytrip_Users.Where(x => x.mytrip_Membership.Email.IndexOf(emailToMatch) != -1)
                    .OrderByDescending(x => x.LastActivityDate).Count();
            }
            else if (usernameToMatch != null)
            {
                users = entities.mytrip_Users.Where(x => x.UserName.IndexOf(usernameToMatch) != -1)
                    .OrderByDescending(x => x.LastActivityDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                totalRecords = entities.mytrip_Users.Where(x => x.UserName.IndexOf(usernameToMatch) != -1)
                    .OrderByDescending(x => x.LastActivityDate).Count();

            }
            string[] result = new string[pageSize];
            int index = 0;
            foreach (mytrip_Users user in users)
            {
                result[index] = user.UserName;
                index++;
            }
            return result;
        }
        public bool mssqlDeleteUser(string username)
        {
            bool result = false;
            var user = mssqlGetUserByUserName(username);
            if (user != null)
            {
                mssqlRemoveRolesFromUser(username);
                mssqlDeleteMembership(username);
                mytrip_Users x = user;
                entities.DeleteObject(x);
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        private void mssqlDeleteMembership(string username)
        {
            mytrip_Membership x = entities.mytrip_Membership.FirstOrDefault(y => y.mytrip_Users.UserName == username);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        private void mssqlRemoveRolesFromUser(string username)
        {
            var user = mssqlGetUserByUserName(username);
            if (user.mytrip_Roles != null)
            {
                foreach (mytrip_Roles x in user.mytrip_Roles.ToList())
                {
                    var role = mssqlGetRoleByRoleName(x.RoleName);
                    user.mytrip_Roles.Remove(role);
                    entities.SaveChanges();
                }
            }
        }
        private mytrip_Roles mssqlGetRoleByRoleName(string rolename)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleName == rolename);
        }
        public bool mssqlValidateUser(string username, string password)
        {
            bool result = false;
            if (mssqlCheckUserName(username))
            {
                var user = mssqlGetMembershipByUserName(username);
                if (user != null && mssqlHashPassword(password, user.PasswordSalt) == user.Password)
                {
                    mssqlUpdateUserIP(username);
                    result = true;
                }
            }
            return result;
        }
        public bool mssqlCheckUserName(string username)
        {
            bool result = false;
            var users = mssqlGetUserByUserName(username);
            if (users != null)
            {
                bool _users = users.mytrip_Membership.IsApproved;
                if (_users)
                    result = true;
            }
            return result;
        }
        private void mssqlUpdateUserIP(string username)
        {
            mytrip_Membership x = mssqlGetMembershipByUserName(username);
            x.UserIP = HttpContext.Current.Request.UserHostAddress;
            entities.SaveChanges();
            mytrip_Users y = mssqlGetUserByUserName(username);
            y.LastActivityDate = DateTime.Now;
            entities.SaveChanges();
        }
        public MembershipUser mssqlCreateMembershipFromInternalUser(string Username, string baseName)
        {
            var user = mssqlGetUserByUserName(Username);
            MembershipUser muser = new MembershipUser(baseName, user.UserName, user.UserId,
                user.mytrip_Membership.Email, string.Empty, string.Empty, user.mytrip_Membership.IsApproved, false,
                user.mytrip_Membership.CreationDate, user.mytrip_Membership.LastLoginDate, user.LastActivityDate, user.mytrip_Membership.LastPasswordChangedDate,
                user.mytrip_Membership.LastLockoutDate);
            return muser;
        }
        public MembershipCreateStatus mssqlCreateUser(string username, string password, string email, bool isApproved)
        {
            MembershipCreateStatus status;
            if (!mssqlAccessibleUserName(username))
                status = MembershipCreateStatus.DuplicateUserName;
            else if (String.IsNullOrEmpty(username))
                status = MembershipCreateStatus.InvalidUserName;
            else if (String.IsNullOrEmpty(password))
                status = MembershipCreateStatus.InvalidPassword;
            else if (password.Length < UsersSetting.minRequiredPasswordLength)
                status = MembershipCreateStatus.InvalidPassword;
            else if (UsersSetting.requiresUniqueEmail && !mssqlAccessibleEmail(email))
                status = MembershipCreateStatus.DuplicateEmail;
            else if (String.IsNullOrEmpty(email))
                status = MembershipCreateStatus.InvalidEmail;
            else if (!Regex.IsMatch(email, "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$"))
                status = MembershipCreateStatus.InvalidEmail;
            else
            {
                string userId = mssqlCreateUserByUserId();
                mssqlCreateUser(userId, username);
                mssqlCreateMembership(userId, password, email, isApproved);
                status = MembershipCreateStatus.Success;
            }
            return status;

        }
        private void mssqlCreateMembership(string userId, string password, string email, bool isApproved)
        {
            string newGuid = Guid.NewGuid().ToString();
            mytrip_Membership x = new mytrip_Membership
            {
                UserId = userId,
                Password = mssqlHashPassword(password, newGuid),
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
        private void mssqlCreateUser(string userId, string userName)
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
        private string mssqlCreateUserByUserId()
        {
            string result = Guid.NewGuid().ToString();
            while (mssqlGetUserByUserId(result) != null)
            {
                result = Guid.NewGuid().ToString();
            }
            return result;
        }
        private mytrip_Users mssqlGetUserByUserId(string userid)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserId == userid);
        }
        public bool mssqlAccessibleEmail(string email)
        {
            bool result = true;
            var _users = entities.mytrip_Membership.FirstOrDefault(x => x.Email == email);
            if (_users != null)
                result = false;
            return result;
        }
        public bool mssqlAccessibleUserName(string username)
        {
            bool result = true;
            var _users = mssqlGetUserByUserName(username);
            if (_users != null)
                result = false;
            return result;
        }
        public bool mssqlChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            var user = mssqlGetMembershipByUserName(username);
            if (user != null && mssqlHashPassword(oldPassword, user.PasswordSalt) == user.Password)
            {
                string newGuid = Guid.NewGuid().ToString();
                mytrip_Membership x = user;
                x.Password = mssqlHashPassword(newPassword, newGuid);
                x.PasswordSalt = newGuid;
                x.LastPasswordChangedDate = DateTime.Now;
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        private mytrip_Membership mssqlGetMembershipByUserName(string username)
        {
            return entities.mytrip_Membership.FirstOrDefault(x => x.mytrip_Users.UserName == username);
        }
        private string mssqlHashPassword(string password, string passwordsalt)
        {
            string hash = FormsAuthentication
                .HashPasswordForStoringInConfigFile((passwordsalt + password), "SHA1");
            return hash;
        }
        private string mssqlGetUserIdbyUserName(string username)
        {
            return mssqlGetUserByUserName(username).UserId;
        }
        public mytrip_Users mssqlGetUserByUserName(string username)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserName == username);

        }
    }
}
