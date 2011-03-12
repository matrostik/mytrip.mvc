/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web.Security;
using mtm.Core.Settings;

namespace mtm.Core.Repository
{
    /// <summary>FormsAuthenticationRepository
    /// </summary>
    internal class FormsAuthenticationRepository
    {
        MembershipRepository _membershipRepo;
        internal MembershipRepository membershipRepo
        {
            get
            {
                if (_membershipRepo == null)
                    _membershipRepo = new MembershipRepository();
                return _membershipRepo;
            }
        }
        /// <summary>Sign In
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="createPersistentCookie">Create Persistent Cookie</param>
        internal void SignIn(string userName, bool createPersistentCookie)
        {
            if (UsersSetting.EmailasUsername())
                userName = membershipRepo.mtGetUserNametoEmail(userName);
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        /// <summary>Sign Out
        /// </summary>
        internal void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}