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
            string _username = string.Empty;
            if (UsersSetting.membership == "MSSQL")
            {
                _username = mssqlUser.mssqlGetUser(username, userIsOnline);
            }
            else if (UsersSetting.membership == "XML")
            {
                _username = xmlUser.xmlGetUser(username, userIsOnline);
            }
            if (!String.IsNullOrEmpty(_username))
            {
                return CreateMembershipFromInternalUser(_username);
            }
            else
            {
                return null;
            }
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
             string _username = string.Empty;
            if (UsersSetting.membership == "MSSQL")
            {
                _username = mssqlUser.mssqlGetUser(providerUserKey, userIsOnline);
            }
            else if (UsersSetting.membership == "XML")
            {
                _username = xmlUser.xmlGetUser(providerUserKey, userIsOnline);
            }
            if (!String.IsNullOrEmpty(_username)){
                return CreateMembershipFromInternalUser(_username);
            }
            else
            {
                return null;
            }
        }
        public override string GetUserNameByEmail(string email)
        {
            string result = string.Empty;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetUserNameByEmail(email);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetUserNameByEmail(email);
            }
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
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlUnlockUser(userName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlUnlockUser(userName);
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
        /*-------------------------------------------------------------------------------------*/
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
        public string mtHashCaptcha(string saptcha)
        {
            string c = FormsAuthentication
                .HashPasswordForStoringInConfigFile((UsersSetting.applicationName + saptcha), "SHA1");
            return c;
        }
        public bool mtCheckOldPassword(string OldPassword)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlCheckOldPassword(OldPassword);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlCheckOldPassword(OldPassword);
            }
            return result;
        }
        public string mtGetUserEmail(string username)
        {
            string result = string.Empty;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetUserEmail(username);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetUserEmail(username);
            }
            return result;
        }
        /*------------------------------------------------------------------------------------*/
  
    }
}