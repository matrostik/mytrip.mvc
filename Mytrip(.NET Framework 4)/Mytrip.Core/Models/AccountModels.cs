//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.AccessControl;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Models
{

    /// <summary>
    /// Change Password Model
    /// </summary>
    [MetadataType(typeof(ChangePasswordModel))]
   public class ChangePasswordModel
   { 
        /// <summary>
        /// string OldPassword
        /// </summary>
       [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
       [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
       [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
       [OldPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "logon_error_password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        /// <summary>
        /// string NewPassword
        /// </summary>
       [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
       [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
       [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        /// string ConfirmPassword
        /// </summary>
       [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
       [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
       [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// LogOnModel
    /// </summary>
    [MetadataType(typeof(LogOnModel))]
    public class LogOnModel
    {
        /// <summary>
        /// string UserName
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_username")]
        [UserNameLogon(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_username")]
        public string UserName { get; set; }
        /// <summary>
        /// string Password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// bool RememberMe
        /// </summary>
        public bool RememberMe { get; set; }
    }
    /// <summary>
    /// RegisterModel
    /// </summary>
    [MetadataType(typeof(RegisterModel))]
    public class RegisterModel
    {
        /// <summary>
        /// string UserName
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_username")]
        [UserNameRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_username")]
        public string UserName { get; set; }
        /// <summary>
        /// string Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [EmailRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// string Password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// string ConfirmPassword
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// string Captcha
        /// </summary>
        [CaptchaNullString(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_captcha")]
        [CaptchaError(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_captcha")]
        public string Captcha { get; set; }
    }
    /// <summary>
    /// IMembershipService
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// int MinPasswordLength
        /// </summary>
        int MinPasswordLength { get; }
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        bool ValidateUser(string userName, string password);
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <param name="email">E-mail</param>
        /// <returns>MembershipCreateStatus</returns>
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="oldPassword">Old Password</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>bool</returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
    /// <summary>
    /// Account Membership Service
    /// </summary>
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        /// <summary>
        /// Account Membership Service
        /// </summary>
        public AccountMembershipService()
            : this(null)
        {
        }
        /// <summary>
        /// Account Membership Service
        /// </summary>
        /// <param name="provider">MembershipProvider</param>
        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }
        /// <summary>
        /// Minimum Password Length
        /// </summary>
        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(CoreLanguage.account_null_password, "password");
            return _provider.ValidateUser(userName, password);
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(CoreLanguage.account_null_password, "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException(CoreLanguage.account_null_email, "email");
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="oldPassword">Old Password</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>bool</returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
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
    /// <summary>
    /// IFormsAuthenticationService
    /// </summary>
    public interface IFormsAuthenticationService
    {
        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="createPersistentCookie">Create Persistent Cookie</param>
        void SignIn(string userName, bool createPersistentCookie);
        /// <summary>
        /// Sign Out
        /// </summary>
        void SignOut();
    }
    /// <summary>
    /// FormsAuthenticationService : IFormsAuthenticationService
    /// </summary>
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="createPersistentCookie">Create Persistent Cookie</param>
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(CoreLanguage.account_null_username, "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        /// <summary>
        /// Sign Out
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    /// <summary>
    /// Account Validation
    /// </summary>
    public static class AccountValidation
    {
        /// <summary>
        /// Error Code To String
        /// </summary>
        /// <param name="createStatus"></param>
        /// <returns></returns>
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return CoreLanguage.register_duplicate_username;

                case MembershipCreateStatus.DuplicateEmail:
                    return CoreLanguage.register_duplicate_email;

                case MembershipCreateStatus.InvalidPassword:
                    return CoreLanguage.account_null_password;

                case MembershipCreateStatus.InvalidEmail:
                    return CoreLanguage.register_error_email;

                case MembershipCreateStatus.InvalidUserName:
                    return CoreLanguage.account_null_username;

                default:
                    return CoreLanguage.default_provider_error;
            }
        }
    }
   /// <summary>
   /// User Name Logon Attribute
   /// </summary>
    public class UserNameLogonAttribute : ValidationAttribute
    {
        MembershipRepository db = new MembershipRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            string UserName = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(UserName) && !db.mtCheckUserName(UserName))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// User Name Register Attribute
    /// </summary>
    public class UserNameRegisterAttribute : ValidationAttribute
    {
        MembershipRepository db = new MembershipRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            string UserName = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(UserName) && !db.mtAccessibleUserName(UserName))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// Email Register Attribute
    /// </summary>
    public class EmailRegisterAttribute : ValidationAttribute
    {
        MembershipRepository db = new MembershipRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            string email = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(email) && UsersSetting.requiresUniqueEmail && !db.mtAccessibleEmail(email))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// Minimum Password Attribute
    /// </summary>
    public class MinPasswordAttribute : ValidationAttribute
    {
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var password = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(password) && UsersSetting.minRequiredPasswordLength > password.Length)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Format Error Message
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>string</returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, UsersSetting.minRequiredPasswordLength);
        }

    }
    /// <summary>
    /// Old Password Attribute
    /// </summary>
    public class OldPasswordAttribute : ValidationAttribute
    {
        MembershipRepository db = new MembershipRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var OldPassword = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(OldPassword) && !db.mtCheckOldPassword(OldPassword))
            {
                result = false;
            }

            return result;
        }

    }
    /// <summary>
    /// Captcha Null String Attribute
    /// </summary>
    public class CaptchaNullStringAttribute : ValidationAttribute
    {
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var captcha = (String)value;
            bool result = true;
            if (UsersSetting.unlockCaptcha && String.IsNullOrEmpty(captcha))
            {
                result = false;
            }

            return result;
        }

    }
    /// <summary>
    /// Captcha Error Attribute 
    /// </summary>
    public class CaptchaErrorAttribute : ValidationAttribute
    {
        MembershipRepository db = new MembershipRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var captcha = (String)value;
            bool result = true;
            if (UsersSetting.unlockCaptcha && db.mtHashCaptcha(captcha) != HttpContext.Current.Session["antibotimage"].ToString())
            {
                result = false;
            }

            return result;
        }

    }
}
