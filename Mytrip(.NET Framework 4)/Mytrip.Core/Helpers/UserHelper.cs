//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System.Text;
using System.Web.Mvc;
using System.Web;
using System;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Captcha Helper
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// Captcha Image
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="fontfamily">fontfamily</param>
        /// <returns>static string</returns>
        public static string CaptchaImage(this HtmlHelper helper, int width, int height, string fontfamily)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder imgCaptcha = new TagBuilder("img");
            imgCaptcha.MergeAttribute("src", "/Content/images/empty.gif");
            imgCaptcha.MergeAttribute("alt", "Captcha");
            imgCaptcha.AddCssClass("captcha");
            imgCaptcha.MergeAttribute("style", "width: " + width + "px; height: " + height + "px; background-image:url('/Captcha/Index/" + width + "/" + height + "/" + fontfamily + "');");
            result.AppendLine(imgCaptcha.ToString());
            return result.ToString();
        }
        public static string AccordionUsersAndFiles(this HtmlHelper html)
       {
           StringBuilder result = new StringBuilder();
           TagBuilder div_accordion = new TagBuilder("div");
           div_accordion.AddCssClass("accordion");
           TagBuilder div_accordiontitle = new TagBuilder("div");
           div_accordiontitle.AddCssClass("accordiontitle");
           div_accordiontitle.InnerHtml = CoreLanguage.user_file;
           TagBuilder div_accordioncontent = new TagBuilder("div");
           div_accordioncontent.AddCssClass("accordioncontent");
           TagBuilder ul = new TagBuilder("ul");
           TagBuilder li_user = new TagBuilder("li");
           TagBuilder a_user = new TagBuilder("a");
           a_user.MergeAttribute("href", "/Users");
           a_user.InnerHtml = CoreLanguage.user_manager;
           li_user.InnerHtml = a_user.ToString();
           TagBuilder li_file = new TagBuilder("li");
           TagBuilder a_file = new TagBuilder("a");
           a_file.MergeAttribute("href", "/File");
           a_file.InnerHtml = CoreLanguage.file_manager;
           li_file.InnerHtml = a_file.ToString();
           /////
           TagBuilder li_archive = new TagBuilder("li");
           TagBuilder a_archive = new TagBuilder("a");
           a_archive.MergeAttribute("href", "/ArticleArchive");
           a_archive.InnerHtml = "Article Manager";
           li_archive.InnerHtml = a_archive.ToString();
           /////
           TagBuilder li_core = new TagBuilder("li");
           TagBuilder a_core = new TagBuilder("a");
           a_core.MergeAttribute("href", "/Core");
           a_core.InnerHtml = "Core Settings";
           li_core.InnerHtml = a_core.ToString();
           ul.InnerHtml = li_user.ToString() + li_file.ToString() + li_archive.ToString() + li_core.ToString();
           div_accordioncontent.InnerHtml = ul.ToString();
           div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
           result.AppendLine(div_accordion.ToString());
           return result.ToString();        
        }
        public static string AccordionUsersAndFiles(this HtmlHelper html,bool RoleAdmin)
        {
            RoleRepository db = new RoleRepository();
            if (RoleAdmin && db.IsUserInRoleOnline(UsersSetting.roleAdmin))
            {
                StringBuilder result = new StringBuilder();
                TagBuilder div_accordion = new TagBuilder("div");
                div_accordion.AddCssClass("accordion");
                TagBuilder div_accordiontitle = new TagBuilder("div");
                div_accordiontitle.AddCssClass("accordiontitle");
                div_accordiontitle.InnerHtml = CoreLanguage.user_file;
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontent");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_user = new TagBuilder("li");
                TagBuilder a_user = new TagBuilder("a");
                a_user.MergeAttribute("href", "/Users");
                a_user.InnerHtml = CoreLanguage.user_manager;
                li_user.InnerHtml = a_user.ToString();
                TagBuilder li_file = new TagBuilder("li");
                TagBuilder a_file = new TagBuilder("a");
                a_file.MergeAttribute("href", "/File");
                a_file.InnerHtml = CoreLanguage.file_manager;
                li_file.InnerHtml = a_file.ToString();
                /////
                TagBuilder li_archive = new TagBuilder("li");
                TagBuilder a_archive = new TagBuilder("a");
                a_archive.MergeAttribute("href", "/ArticleArchive");
                a_archive.InnerHtml = "Article Manager";
                li_archive.InnerHtml = a_archive.ToString();
                /////
                TagBuilder li_core = new TagBuilder("li");
                TagBuilder a_core = new TagBuilder("a");
                a_core.MergeAttribute("href", "/Core");
                a_core.InnerHtml = "Core Settings";
                li_core.InnerHtml = a_core.ToString();
                ul.InnerHtml = li_user.ToString() + li_file.ToString() + li_archive.ToString()+li_core.ToString();
                div_accordioncontent.InnerHtml = ul.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string LogonMenu(this HtmlHelper html)
        {
            if (UsersSetting.unlockVisibleLogon)
            {
                StringBuilder result = new StringBuilder();
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    TagBuilder li_logoff = new TagBuilder("li");
                    TagBuilder li_welcome = new TagBuilder("li");
                    TagBuilder logoff = new TagBuilder("a");
                    logoff.MergeAttribute("href", "/Account/LogOff?returnUrl=" + HttpContext.Current.Request.Path.ToString());
                    logoff.InnerHtml = CoreLanguage.logoff;
                    TagBuilder welcome = new TagBuilder("a");
                    welcome.InnerHtml = String.Format(CoreLanguage.Welcome, HttpContext.Current.User.Identity.Name);
                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("right_topmenu");
                    li_logoff.InnerHtml = logoff.ToString()+a.ToString();
                    li_welcome.InnerHtml = welcome.ToString()+a.ToString();
                    result.AppendLine(li_logoff.ToString());
                    result.AppendLine(li_welcome.ToString());
                }
                else
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder logon = new TagBuilder("a");
                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("right_topmenu");
                    logon.MergeAttribute("href", "/Account/LogOn?returnUrl=" + HttpContext.Current.Request.Path.ToString());
                    logon.InnerHtml = CoreLanguage.logon;
                    li.InnerHtml = logon.ToString()+a.ToString();
                    result.AppendLine(li.ToString());
                }
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string AccordionUserProfile(this HtmlHelper html,string profile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder div_accordion = new TagBuilder("div");
                div_accordion.AddCssClass("accordion");
                TagBuilder div_accordiontitle = new TagBuilder("div");
                div_accordiontitle.AddCssClass("accordiontitle");
                div_accordiontitle.InnerHtml = CoreLanguage.my_profile;
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontent");
                div_accordioncontent.MergeAttribute("style", "padding:5px;");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                ChangePassword.InnerHtml = CoreLanguage.change_password;
                li.InnerHtml = ChangePassword.ToString();
                ul.InnerHtml = li.ToString();
                div_accordioncontent.InnerHtml = profile+ul.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
    }
}