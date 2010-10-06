/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Repository.DataEntities;
using Mytrip.Mvc.Settings;
using System.Linq;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>ХТМЛ Хелпер
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontfamily"></param>
        /// <returns></returns>
        public static HtmlString CaptchaImage(this HtmlHelper helper, int width, int height, string fontfamily)
        {
            CaptchaRepository captchaRepo = new CaptchaRepository();
            string solution = string.Empty;
            Bitmap image = captchaRepo.mtGenerateImage(width, height, fontfamily, out solution);
            HttpContext.Current.Session["antibotimage"] = solution;
            FileRepository file = new FileRepository();
            foreach (FileInfo x in file.GetAllFilesFromDirectory("()Content()captcha"))
            {
                if (x.CreationTime.Date < DateTime.Now.Date)
                    file.DeleteFile("()Content()captcha()" + x.Name);
            }
            string absolutDirectory = HttpContext.Current.Server.MapPath("/");
            string filename1 = "/Content/captcha/" + Guid.NewGuid() + ".jpeg";
            image.Save((absolutDirectory + filename1), ImageFormat.Jpeg);
            StringBuilder result = new StringBuilder();
            TagBuilder imgCaptcha = new TagBuilder("img");
            imgCaptcha.MergeAttribute("src", filename1);
            imgCaptcha.MergeAttribute("alt", "Captcha");
            imgCaptcha.AddCssClass("captcha");
            imgCaptcha.MergeAttribute("style", "width: " + width + "px; height: " + height + "px;");
            result.AppendLine(imgCaptcha.ToString());
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="helper"></param>
        /// <param name="helper2"></param>
        /// <param name="RoleAdmin"></param>
        /// <returns></returns>
        public static HtmlString AccordionUsersAndFiles(this HtmlHelper html, object helper, object helper2, bool RoleAdmin)
        {
            if (RoleAdmin && MytripUser.UserInRole(UsersSetting.roleAdmin()) && CoreSetting.ControlPanelSideBare())
            {
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_user = new TagBuilder("li");
                TagBuilder a_user = new TagBuilder("a");
                a_user.MergeAttribute("href", "/Users");
                a_user.InnerHtml = CoreLanguage.user_manager;
                li_user.InnerHtml = a_user.ToString();
                TagBuilder li_file = new TagBuilder("li");
                TagBuilder a_file = new TagBuilder("a");
                a_file.MergeAttribute("href", "/File/Index/()");
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
                a_archive3.MergeAttribute("href", "/Core/CorSideBar");
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
                HtmlString htmlresult = new HtmlString(GeneralMethods.Accordion("<a href=\"/Core/ControlPanel\">" + CoreLanguage.control_panel + "</a>", ul.ToString() + _ul.ToString()));
                return htmlresult;
            }
            else { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString LogonMenu(this HtmlHelper html)
        {
            string controlpanel = string.Empty;
            if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
            {
                controlpanel = GeneralMethods.Menu("<a href=\"/Core/ControlPanel\">" + CoreLanguage.control_panel + "</a>", null, false, false, false, false);
            }
            if (!CoreSetting.Development() && UsersSetting.unlockVisibleLogon())
            {
                StringBuilder result = new StringBuilder();
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    TagBuilder logoff = new TagBuilder("a");
                    string returnUrl = HttpContext.Current.Request.Path.ToString();
                    if (returnUrl == "/")
                        returnUrl = "/Home/Index";
                    logoff.MergeAttribute("href", "/Account/LogOff?returnUrl=" + returnUrl);
                    logoff.InnerHtml = CoreLanguage.logoff;
                    TagBuilder welcome = new TagBuilder("a");
                    welcome.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                    welcome.InnerHtml = string.Format(CoreLanguage.Welcome, HttpContext.Current.User.Identity.Name);

                    result.AppendLine(GeneralMethods.Menu(logoff.ToString(), null, false, false, false, false));
                    result.AppendLine(GeneralMethods.Menu(welcome.ToString(), null, false, true, false, false));
                    result.AppendLine(controlpanel);
                }
                else
                {
                    string path = HttpContext.Current.Request.Path.ToString();
                    if (path == "/Account/Register")
                        path = HttpContext.Current.Request.QueryString["returnUrl"];
                    if (path == "/")
                        path = "/Home/Index";
                    TagBuilder logon = new TagBuilder("a");
                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("right_topmenu");
                    logon.MergeAttribute("href", "/Account/LogOn?returnUrl=" + path);
                    logon.InnerHtml = CoreLanguage.logon;
                    result.AppendLine(GeneralMethods.Menu(logon.ToString(), null, false, true, false, false));
                }
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
            }
            else
            {
                HtmlString htmlresult = new HtmlString(controlpanel);
                return htmlresult;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static HtmlString AccordionUserProfile(this HtmlHelper html, object profile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                MembershipRepository user = new MembershipRepository();
                TagBuilder welcome = new TagBuilder("a");
                welcome.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                welcome.InnerHtml = CoreLanguage.my_profile;
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                string Password = user.mtGetUserByUserNameMember(HttpContext.Current.User.Identity.Name).mytrip_usersmembership.Password;
                if (Password == user.mtGetUserByUserNameMember(HttpContext.Current.User.Identity.Name).mytrip_usersmembership.PasswordSalt)
                {
                    ChangePassword.InnerHtml = Password.Substring(0, Password.IndexOf('/'));
                }
                else
                {
                    ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                    ChangePassword.InnerHtml = CoreLanguage.change_password;
                }
                li.InnerHtml = ChangePassword.ToString();
                ul.InnerHtml = li.ToString();
                string _helper = string.Empty;
                IDictionary<string, object> _menu = (profile == null ? new RouteValueDictionary() : new RouteValueDictionary(profile));
                foreach (string key in _menu.Keys)
                {
                    if (_menu[key] != null)
                    _helper += _menu[key].ToString();
                }
                TagBuilder divGravatar = new TagBuilder("a");
                divGravatar.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                divGravatar.MergeAttribute("title", CoreLanguage.my_profile);
                divGravatar.InnerHtml =UsersSetting.unlockGravatar()? AvatarHelper.Avatar(html, user.mtGetUserByUserNameMember(HttpContext.Current.User.Identity.Name).mytrip_usersmembership.Email, new { width = 60 }).ToString():"";
                string table = "<table class=\"noborders\"><tr><td>" + _helper + ul.ToString() +
                    "</td><td>" + divGravatar + "</td></tr></table>";
                HtmlString htmlresult = new HtmlString(GeneralMethods.Accordion(welcome.ToString(), table));
                return htmlresult;
            }
            else { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="username"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static HtmlString PageUserProfile(this HtmlHelper html, string username, object profile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.Name == username)
            {
                StringBuilder result = new StringBuilder();
                MembershipRepository user = new MembershipRepository();

                TagBuilder div_accordioncontent = new TagBuilder("div");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                string Password = user.mtGetUserByUserNameMember(username).mytrip_usersmembership.Password;
                if (Password == user.mtGetUserByUserNameMember(username).mytrip_usersmembership.PasswordSalt)
                {
                    ChangePassword.InnerHtml = Password.Substring(0, Password.IndexOf('/'));
                }
                else
                {
                    ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                    ChangePassword.InnerHtml = CoreLanguage.change_password;
                }
                li.InnerHtml = ChangePassword.ToString();
                ul.InnerHtml = li.ToString();
                string _helper = string.Empty;
                IDictionary<string, object> _menu = (profile == null ? new RouteValueDictionary() : new RouteValueDictionary(profile));
                foreach (string key in _menu.Keys)
                {
                    if (_menu[key] != null)
                    _helper += _menu[key].ToString();
                }

                div_accordioncontent.InnerHtml = _helper + ul.ToString();
                result.AppendLine(div_accordioncontent.ToString());
                return new HtmlString(result.ToString());
            }
            else { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static HtmlString UserData(this HtmlHelper html, string username)
        {
            MembershipRepository mmr = new MembershipRepository();
            mytrip_users user = mmr.mtGetUserByUserNameMember(username);
            if (user == null)
                return new HtmlString(CoreLanguage.no_profile);
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
            return new HtmlString(div.ToString());
        }
        /// <summary>
        /// Collect all user's LastActivity drom all modules and build view
        /// Собирает действия пользователя из всех модулей и строит представление
        /// </summary>
        /// <param name="html"></param>
        /// <param name="lastactivity">object</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public static HtmlString LastActivity(this HtmlHelper html, string path, object lastactivity)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<h2 class=\"title\">" + CoreLanguage.lastactivity + "</h2>");
            result.AppendLine("<div style=\"border-bottom: 4px solid black;\"></div>");

            var acts = new List<LastActivity>();
            IDictionary<string, object> _menu = (lastactivity == null ? new RouteValueDictionary() : new RouteValueDictionary(lastactivity));
            foreach (string key in _menu.Keys)
            {
                var a = _menu[key] as List<LastActivity>;
                acts.AddRange(a);
            }
            var opt = acts.OrderBy(x => x.Place).Select(x => x.Place).Distinct().ToList();
            opt.Insert(0, CoreLanguage.all);
            if (acts.Count() == 0)
                return new HtmlString(string.Empty);
            if (path != CoreLanguage.all)
                acts = acts.Where(x => x.Place == path).ToList();
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("class", "noborders");
            TagBuilder tr0 = new TagBuilder("tr");
            TagBuilder th1 = new TagBuilder("th");

            #region Form + Dropdown
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", HttpContext.Current.Request.Path);
            form.MergeAttribute("id", "TheForm");
            form.MergeAttribute("method", "post");
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", "Path");
            select.MergeAttribute("name", "Path");
            foreach (var p in opt)
            {
                if (path == p)
                    select.InnerHtml += "<option selected='selected' value='" + p + "'>" + p + "</option>";
                else
                    select.InnerHtml += "<option value='" + p + "'>" + p + "</option>";
            }
            form.InnerHtml = select.ToString();
            #endregion
            
            th1.InnerHtml = form.ToString();
            TagBuilder th2 = new TagBuilder("th");
            th2.InnerHtml = CoreLanguage.activity;
            TagBuilder th3 = new TagBuilder("th");
            th3.InnerHtml = CoreLanguage.date;
            tr0.InnerHtml = th1.ToString() + th2.ToString() + th3.ToString();
            table.InnerHtml += tr0;
            int ctr = 0;
            foreach (var item in acts.OrderByDescending(x => x.Date))
            {
                TagBuilder tr1 = new TagBuilder("tr");
                if (ctr % 2 == 0)
                    tr1.AddCssClass("profile1");
                else
                    tr1.AddCssClass("profile2");
                TagBuilder td11 = new TagBuilder("td");
                td11.MergeAttribute("style", "width:100px");
                td11.InnerHtml = item.Place;
                tr1.InnerHtml = td11.ToString();
                TagBuilder td12 = new TagBuilder("td");
                td12.InnerHtml = item.Activity;
                tr1.InnerHtml += td12.ToString();
                TagBuilder td13 = new TagBuilder("td");
                td13.MergeAttribute("style", "width:130px");
                if (item.Date.Date == DateTime.Today.Date && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                {
                    if (DateTime.Now.Hour - item.Date.Hour == 0)
                        td13.InnerHtml = (DateTime.Now.Minute - item.Date.Minute).ToString() + " " + CoreLanguage.minutes_ago;
                    else
                        td13.InnerHtml = (DateTime.Now.Hour - item.Date.Hour).ToString() + " " + CoreLanguage.hours_ago;
                }
                else
                    td13.InnerHtml = item.Date.ToString("dd MMM yyyy, HH:mm");
                tr1.InnerHtml += td13.ToString();
                table.InnerHtml += tr1.ToString();
                ctr++;
            }
            #endregion
            result.AppendLine(table.ToString());
            return new HtmlString(result.ToString());
        }
    }
    #region LastActivity Object
    /// <summary>
    /// Represents user activity
    /// Объект представляюший активность пользователя
    /// </summary>
    public class LastActivity
    {
        private string place;
        private string activity;
        private DateTime date;
        /// <summary>
        /// Empty constructor
        /// </summary>
        public LastActivity()
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="place">место</param>
        /// <param name="activity">действие</param>
        /// <param name="date">дата</param>
        public LastActivity(string place, string activity, DateTime date)
        {
            Place = place;
            Activity = activity;
            Date = date;
        }
        /// <summary>
        /// Date
        /// Дата
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        /// <summary>
        /// Action
        /// Действие
        /// </summary>
        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }
        /// <summary>
        /// Place
        /// Место
        /// </summary>
        public string Place
        {
            get { return place; }
            set { place = value; }
        }
    }
    #endregion
}