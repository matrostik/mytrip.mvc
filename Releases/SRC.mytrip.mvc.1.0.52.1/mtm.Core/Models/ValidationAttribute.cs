/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
using System.Web.Security;
using mtm.Core.Repository;
using mtm.Core.Settings;

namespace mtm.Core.Models
{
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public class UserNameNullLogonAttribute : ValidationAttribute
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
            if (!String.IsNullOrEmpty(UserName) && !db.mtCheckNullUserName(UserName))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// 
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
    /// 
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
            if (!String.IsNullOrEmpty(email) && UsersSetting.requiresUniqueEmail() && !db.mtAccessibleEmail(email))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// 
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
            if (!String.IsNullOrEmpty(password) && UsersSetting.minRequiredPasswordLength() > password.Length)
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
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, UsersSetting.minRequiredPasswordLength());
        }

    }
    /// <summary>
    /// 
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
    /// 
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
            if (!CoreSetting.Development() && UsersSetting.unlockCaptcha() && String.IsNullOrEmpty(captcha))
            {
                result = false;
            }

            return result;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class CaptchaErrorAttribute : ValidationAttribute
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
            if (!CoreSetting.Development() && UsersSetting.unlockCaptcha() && MytripUser.HashCaptcha(captcha) != HttpContext.Current.Session["antibotimage"].ToString())
            {
                result = false;
            }

            return result;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class RoleNameDublicateAttribute : ValidationAttribute
    {
        RoleRepository db = new RoleRepository();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string rolename = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(rolename) && db.RoleExists(rolename))
            {
                result = false;
            }
            return result;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public static class AccountValidation
    {
        /// <summary>
        /// 
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
}