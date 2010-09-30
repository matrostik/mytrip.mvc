/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web.Security;

namespace Mytrip.Mvc.Repository
{
    /// <summary> Методы для работы с MembershipProvider
    /// </summary>
    internal class AccountMembershipRepository
    {
        private readonly MembershipProvider _provider;

        /// <summary>Account Membership Service
        /// </summary>
        internal AccountMembershipRepository() : this(null){}
        /// <summary>Account Membership Service
        /// </summary>
        /// <param name="provider">MembershipProvider</param>
        internal AccountMembershipRepository(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }
        /// <summary>Minimum Password Length
        /// </summary>
        internal int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }
        /// <summary>Validate User
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        internal bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(CoreLanguage.account_null_password, "password");
            return _provider.ValidateUser(userName, password);
        }
        /// <summary>Create User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        internal MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(CoreLanguage.account_null_password, "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException(CoreLanguage.account_null_email, "email");
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }
        /// <summary>Change Password
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="oldPassword">Old Password</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>bool</returns>
        internal bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException(CoreLanguage.account_null_password, "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException(CoreLanguage.account_null_password, "newPassword");
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true);
                return _provider.ChangePassword(userName, oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }
}