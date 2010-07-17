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
using Mytrip.Mvc.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Routing;
using Mytrip.Mvc.Repository.DataEntities;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// Captcha Helper
    /// </summary>
    public static class UserHelper
    {
        public static string CaptchaImage(this HtmlHelper helper, int width, int height, string fontfamily)
        {
            //////
            CaptchaRepository captchaRepo = new CaptchaRepository();
            string solution = string.Empty;
            Bitmap image = captchaRepo.mtGenerateImage(width, height, fontfamily, out solution);
            HttpContext.Current.Session["antibotimage"] = solution;
            FileRepository file = new FileRepository();
            //string filename = DateTime.Now.AddDays(-1).ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            foreach (FileInfo x in file.GetAllFilesFromDirectory("()Content()captcha"))
            {
                if (x.CreationTime.Date<DateTime.Now.Date)
                    file.DeleteFile("()Content()captcha()" + x.Name);
            }
            string absolutDirectory = HttpContext.Current.Server.MapPath("/");
            string filename1 = "/Content/captcha/"+Guid.NewGuid()+".jpeg";
            image.Save((absolutDirectory+filename1), ImageFormat.Jpeg);
            ///////
            StringBuilder result = new StringBuilder();
            TagBuilder imgCaptcha = new TagBuilder("img");
            imgCaptcha.MergeAttribute("src",filename1);// "/Content/images/empty.gif");
            imgCaptcha.MergeAttribute("alt", "Captcha");
            imgCaptcha.AddCssClass("captcha");
            imgCaptcha.MergeAttribute("style", "width: " + width + "px; height: " + height + "px;");// background-image:url(\"/Captcha/Index/" + width + "/" + height + "/" + fontfamily + "\");");
            result.AppendLine(imgCaptcha.ToString());
            return result.ToString();
        }
        public static string AccordionUsersAndFiles(this HtmlHelper html, object helper, object helper2, bool RoleAdmin)
        {
            RoleRepository db = new RoleRepository();
            UsersSetting userset = new UsersSetting();
            if (RoleAdmin && db.IsUserInRoleOnline(userset.roleAdmin()))
            {
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

                TagBuilder li_core = new TagBuilder("li");
                TagBuilder a_core = new TagBuilder("a");
                a_core.MergeAttribute("href", "/Core");
                a_core.InnerHtml = CoreLanguage.Core_Settings;
                li_core.InnerHtml = a_core.ToString();
                TagBuilder li_core1 = new TagBuilder("li");
                TagBuilder a_core1 = new TagBuilder("a");
                a_core1.MergeAttribute("href", "/Core/Email");
                a_core1.InnerHtml = CoreLanguage.Email_setting;
                li_core1.InnerHtml = a_core1.ToString();
                TagBuilder li_core2 = new TagBuilder("li");
                TagBuilder a_core2 = new TagBuilder("a");
                a_core2.MergeAttribute("href", "/Core/About");
                a_core2.InnerHtml = CoreLanguage.edit_about;
                li_core2.InnerHtml = a_core2.ToString();
                TagBuilder li_archive = new TagBuilder("li");
                TagBuilder a_archive = new TagBuilder("a");
                a_archive.MergeAttribute("href", "/Core/HomePage");
                a_archive.InnerHtml = CoreLanguage.homepage_setting;
                li_archive.InnerHtml = a_archive.ToString();
                TagBuilder li_archive2 = new TagBuilder("li");
                TagBuilder a_archive2 = new TagBuilder("a");
                a_archive2.MergeAttribute("href", "/Core/TopMenu");
                a_archive2.InnerHtml = CoreLanguage.topmenu_setting;
                li_archive2.InnerHtml = a_archive2.ToString();
                TagBuilder li_archive3 = new TagBuilder("li");
                TagBuilder a_archive3 = new TagBuilder("a");
                a_archive3.MergeAttribute("href", "/Core/SideBar");
                a_archive3.InnerHtml = CoreLanguage.sidebar_setting;
                li_archive3.InnerHtml = a_archive3.ToString();
                TagBuilder li_archive4 = new TagBuilder("li");
                TagBuilder a_archive4 = new TagBuilder("a");
                a_archive4.MergeAttribute("href", "/Core/InstallModules");
                a_archive4.InnerHtml = CoreLanguage.InstallModules;
                li_archive4.InnerHtml = a_archive4.ToString();
                string _helper = string.Empty;
                IDictionary<string, object> _menu = (helper == null ? new RouteValueDictionary() : new RouteValueDictionary(helper));
                foreach (string key in _menu.Keys)
                {
                    _helper += _menu[key].ToString();
                }
                TagBuilder _ul = new TagBuilder("ul");
                ul.InnerHtml = li_user.ToString() + li_file.ToString() + _helper;
                string _helper2 = string.Empty;
                IDictionary<string, object> _menu2 = (helper2 == null ? new RouteValueDictionary() : new RouteValueDictionary(helper2));
                foreach (string key in _menu2.Keys)
                {
                    _helper2 += _menu2[key].ToString();
                }
                _ul.InnerHtml = li_core.ToString() + li_core1.ToString() + _helper2 + li_core2.ToString() + li_archive.ToString()
                    + li_archive2.ToString() + li_archive3.ToString() + li_archive4.ToString();
                return GeneralMethods.Accordion2("acccore", CoreLanguage.control_panel, ul.ToString() + _ul.ToString());
            }
            else { return string.Empty; }
        }
        public static string LogonMenu(this HtmlHelper html)
        {
            UsersSetting userset = new UsersSetting();
            CoreSetting core = new CoreSetting();
            if (!core.Development()&&userset.unlockVisibleLogon())
            {
                StringBuilder result = new StringBuilder();
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {                    
                    TagBuilder logoff = new TagBuilder("a");
                    logoff.MergeAttribute("href", "/Account/LogOff?returnUrl=" + HttpContext.Current.Request.Path.ToString());
                    logoff.InnerHtml = CoreLanguage.logoff;
                    TagBuilder welcome = new TagBuilder("a");
                    welcome.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                    welcome.InnerHtml = String.Format(CoreLanguage.Welcome, HttpContext.Current.User.Identity.Name);

                    result.AppendLine(GeneralMethods.Menu(logoff.ToString(), null, false, false, false, false));
                    result.AppendLine(GeneralMethods.Menu(welcome.ToString(), null, false, true, false, false));
                }
                else
                {
                    string path = HttpContext.Current.Request.Path.ToString();
                    if (path == "/Account/Register")
                        path = HttpContext.Current.Request.QueryString["returnUrl"];
                    TagBuilder logon = new TagBuilder("a");
                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("right_topmenu");
                    logon.MergeAttribute("href", "/Account/LogOn?returnUrl=" + path);
                    logon.InnerHtml = CoreLanguage.logon;
                    result.AppendLine(GeneralMethods.Menu(logon.ToString(), null, false, true, false, false));
                }
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string AccordionUserProfile(this HtmlHelper html,object profile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                TagBuilder welcome = new TagBuilder("a");
                welcome.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                welcome.InnerHtml = CoreLanguage.my_profile;
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                ChangePassword.InnerHtml = CoreLanguage.change_password;
                li.InnerHtml = ChangePassword.ToString();
                ul.InnerHtml = li.ToString();
                string _helper = string.Empty;
                IDictionary<string, object> _menu = (profile == null ? new RouteValueDictionary() : new RouteValueDictionary(profile));
                foreach (string key in _menu.Keys)
                {
                    _helper += _menu[key].ToString();
                }
                return GeneralMethods.Accordion2("accprof", welcome.ToString(), _helper + ul.ToString());
            }
            else { return string.Empty; }
        }
        public static string PageUserProfile(this HtmlHelper html,string username, object profile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.Name==username)
            {
                StringBuilder result = new StringBuilder();
                
                TagBuilder div_accordioncontent = new TagBuilder("div");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                ChangePassword.InnerHtml = CoreLanguage.change_password;
                li.InnerHtml = ChangePassword.ToString();
                ul.InnerHtml = li.ToString();
                string _helper = string.Empty;
                IDictionary<string, object> _menu = (profile == null ? new RouteValueDictionary() : new RouteValueDictionary(profile));
                foreach (string key in _menu.Keys)
                {
                    _helper += _menu[key].ToString();
                }
                div_accordioncontent.InnerHtml =  _helper + ul.ToString();
                result.AppendLine(div_accordioncontent.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string UserData(this HtmlHelper html, string username)
        {
            MembershipRepository mmr = new MembershipRepository();
            mytrip_users user = mmr.mtGetUserByUserNameMember(username);
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "border:0px;");
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "border:0px;border-bottom:1px solid grey");
            TagBuilder td11 = new TagBuilder("td");
            td11.MergeAttribute("style", "border:0px;");
            td11.InnerHtml = "<b>" + CoreLanguage.member_since + ":</b> ";
            tr1.InnerHtml = td11.ToString();
            TagBuilder td12 = new TagBuilder("td");
            td12.MergeAttribute("style", "width:100px;border:0px;");
            td12.InnerHtml = user.mytrip_usersmembership.CreationDate.ToString("dd MMM, yyyy");
            tr1.InnerHtml += td12.ToString();
            TagBuilder tr2 = new TagBuilder("tr");
            tr2.MergeAttribute("style", "border:0px;border-bottom:1px solid grey");
            TagBuilder td21 = new TagBuilder("td");
            td21.MergeAttribute("style", "border:0px;");
            td21.InnerHtml = "<b>" + CoreLanguage.last_visit + ":</b> ";
            tr2.InnerHtml = td21.ToString();
            TagBuilder td22 = new TagBuilder("td");
            td22.MergeAttribute("style", "border:0px;");
            td22.InnerHtml = user.LastActivityDate.ToString("dd MMM, yyyy HH:mm");
            tr2.InnerHtml += td22.ToString();
            table.InnerHtml = tr1.ToString() + tr2.ToString();
            #endregion
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "padding:0px 5px 5px 4px;");
            div.InnerHtml = table.ToString();
            return div.ToString();
        }
        public static string LastActivity(this HtmlHelper html, object lastactivity)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<h2 class=\"title\">" + CoreLanguage.lastactivity + "</h2>");
            result.AppendLine("<div style=\"border-bottom: 4px solid black;\"></div>");
            string _helper = string.Empty;
            IDictionary<string, object> _menu = (lastactivity == null ? new RouteValueDictionary() : new RouteValueDictionary(lastactivity));
            foreach (string key in _menu.Keys)
            {
                _helper += _menu[key].ToString();
            }
            result.AppendLine(_helper);
            return result.ToString();
        }
    }
}