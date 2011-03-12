/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
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
using mtm.Core.Repository;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;
using System.Linq;

namespace mtm.Core.Helpers
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
            StringBuilder result = new StringBuilder();
            TagBuilder imgCaptcha = new TagBuilder("img");
            imgCaptcha.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/empty.gif");
            imgCaptcha.MergeAttribute("alt", "Captcha");
            imgCaptcha.AddCssClass("captcha");
            imgCaptcha.MergeAttribute("style", "width: " + width + "px; height: " + height + "px; background-image:url('/mtm/Captcha/" + width + "/" + height + "/" + fontfamily + "');");
            result.AppendLine(imgCaptcha.ToString());
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString LogonMenu(this HtmlHelper html)
        {
            StringBuilder result = new StringBuilder();
            if (!CoreSetting.Development() && UsersSetting.unlockVisibleLogon())
            {
                
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
                    result.AppendLine(LanguageHelper.LanguageMenu(html));
                    result.AppendLine(ThemeHelper.ThemeMenu(html));
                        result.AppendLine(GeneralMethods.Menu(html,logoff.ToString(), null, false, false, false, false));
                        result.AppendLine(GeneralMethods.Menu(html,welcome.ToString(), null, false, true, false, false));

                        if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
                        {
                            result.AppendLine(GeneralMethods.Menu(html, "<a href=\"/Core/ControlPanel\">" + CoreLanguage.control_panel + "</a>", null, false, false, false, false));
                        }
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
                    result.AppendLine(LanguageHelper.LanguageMenu(html));
                    result.AppendLine(ThemeHelper.ThemeMenu(html));
                        result.AppendLine(GeneralMethods.Menu(html,logon.ToString(), null, false, true, false, false));
                   
               }
            }
            else
            {
                result.AppendLine(LanguageHelper.LanguageMenu(html));
                result.AppendLine(ThemeHelper.ThemeMenu(html));
            }
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }
        
        public static HtmlString AccordionLatestUser(this HtmlHelper html)
        {
            ICoreRepository core = new ICoreRepository();
            StringBuilder result = new StringBuilder();
            var a = core.membershipRepo.mtGetLatestUsers(CoreSetting.CountLatestUsers());
                       
            int _count = 1;
            if (!ProfileSetting.unlockGravatar())
                result.AppendLine("<ul>");
            foreach (var z in a)
            {
                if (ProfileSetting.unlockGravatar())
                {
                    TagBuilder divGravatar = new TagBuilder("a");
                    divGravatar.MergeAttribute("href", "/Home/Profile/" + z.Key);
                    divGravatar.MergeAttribute("title", z.Key);
                    divGravatar.AddCssClass("latestusers");
                    divGravatar.InnerHtml = AvatarHelper.Avatar(html, z.Value, new { width = 60 }).ToString();
                    result.Append(divGravatar.ToString());
                    if (_count == 3 || _count % 3 == 0)
                        result.AppendLine("<div class='latestusers'></div>");
                    _count++;
                }
                else {
                    result.AppendLine("<li><a href='/Home/Profile/" + z.Key+"'>"+z.Key+"</a></li>");
                }
            }
            if (!ProfileSetting.unlockGravatar())
                result.AppendLine("</ul>");
            return new HtmlString(GeneralMethods.Accordion(html,CoreLanguage.RecentVisitors, result.ToString()));
        }
        /// <summary>
        /// Profile block in SideBar
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
                ul.AddCssClass("styled");
                TagBuilder li_1 = new TagBuilder("li");
                TagBuilder changePassword = new TagBuilder("a");
                IDictionary<string, string> Password = user._mtCheckOldPassword(HttpContext.Current.User.Identity.Name);
                if (Password.First().Key == Password.First().Value)
                {
                    changePassword.InnerHtml = Password.First().Key.Substring(0, Password.First().Key.IndexOf('/'));
                }
                else
                {
                    changePassword.MergeAttribute("href", "/Account/ChangePassword");
                    changePassword.InnerHtml = CoreLanguage.change_password;
                }
                li_1.InnerHtml = changePassword.ToString();
                // change email
                TagBuilder li_2 = new TagBuilder("li");
                TagBuilder changeEmail = new TagBuilder("a");
                changeEmail.MergeAttribute("href", "/Account/ChangeEmail");
                changeEmail.InnerHtml = CoreLanguage.change_email;
                li_2.InnerHtml = changeEmail.ToString();
                ul.InnerHtml = li_1.ToString() + li_2.ToString();
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
                divGravatar.MergeAttribute("style", "float:right;");
                divGravatar.InnerHtml = ProfileSetting.unlockGravatar() ? AvatarHelper.Avatar(html, MytripUser.UserEmail(), new { width = 60 }).ToString() : "";
                string table = "<table class=\"noborders\"><tr><td>" + divGravatar + _helper + ul.ToString() +
                    "</td></tr></table>";
                HtmlString htmlresult = new HtmlString(GeneralMethods.Accordion(html, welcome.ToString(), table));
                return htmlresult;
            }
            else { return null; }
        }
        /// <summary>
        /// Profile
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
                ul.AddCssClass("styled");
                TagBuilder li = new TagBuilder("li");
                TagBuilder ChangePassword = new TagBuilder("a");
                IDictionary<string, string> Password = user._mtCheckOldPassword(HttpContext.Current.User.Identity.Name);
                if (Password.First().Key == Password.First().Value)
                {
                    ChangePassword.InnerHtml = Password.First().Key.Substring(0, Password.First().Key.IndexOf('/'));
                }
                else
                {
                    ChangePassword.MergeAttribute("href", "/Account/ChangePassword");
                    ChangePassword.InnerHtml = CoreLanguage.change_password;
                }
                li.InnerHtml = ChangePassword.ToString();
                TagBuilder li_2 = new TagBuilder("li");
                TagBuilder changeEmail = new TagBuilder("a");
                changeEmail.MergeAttribute("href", "/Account/ChangeEmail");
                changeEmail.InnerHtml = CoreLanguage.change_email;
                li_2.InnerHtml = changeEmail.ToString();
                ul.InnerHtml = li.ToString() + li_2.ToString();
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
        /// User's info in profile page
        /// </summary>
        /// <param name="html"></param>
        /// <param name="username">username</param>
        /// <returns></returns>
        public static HtmlString UserData(this HtmlHelper html, string username)
        {
            MembershipRepository mmr = new MembershipRepository();
            mytrip_users user = mmr.mtGetUserByUserNameMember(username);
            if (user == null)
                return new HtmlString(CoreLanguage.no_profile);
            #region table
            TagBuilder table = new TagBuilder("table");
            table.AddCssClass("noborders");
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "text-align: center;border-bottom:1px solid grey");
            TagBuilder td11 = new TagBuilder("td");
            td11.InnerHtml = "<b>" + CoreLanguage.member_since + ":</b><br/>" + user.mytrip_usersmembership.CreationDate.ToString("dd MMM, yyyy");
            tr1.InnerHtml = td11.ToString();
            TagBuilder tr2 = new TagBuilder("tr");
            tr2.MergeAttribute("style", "text-align: center;border-bottom:1px solid grey");
            TagBuilder td21 = new TagBuilder("td");
            td21.InnerHtml = "<b>" + CoreLanguage.last_visit + ":</b><br/>" + user.LastActivityDate.ToString("dd MMM, yyyy HH:mm");
            tr2.InnerHtml = td21.ToString();
            table.InnerHtml = tr1.ToString() + tr2.ToString();
            #endregion
            return new HtmlString(table.ToString());
        }
        /// <summary>
        /// Collect all user's LastActivity from all modules and build view
        /// Собирает действия пользователя из всех модулей и строит представление
        /// </summary>
        /// <param name="html"></param>
        /// <param name="lastactivity">object</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public static HtmlString LastActivity(this HtmlHelper html, string path,object lastactivity)
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
            tr0.AddCssClass("profile2");
            TagBuilder th1 = new TagBuilder("td");
            th1.MergeAttribute("colspan","2");

            #region Form + Dropdown
            
            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("id", "path");
            select.MergeAttribute("name", "path");
            foreach (var p in opt)
            {
                if (path == p)
                    select.InnerHtml += "<option selected='selected' value='" + p + "'>" + p + "</option>";
                else
                    select.InnerHtml += "<option value='" + p + "'>" + p + "</option>";
            }
            #endregion

            th1.InnerHtml = "<div style='float:left'>" + select.ToString() + "</div>";
            TagBuilder th3 = new TagBuilder("td");
            th3.MergeAttribute("style","text-align:center;");
            th3.InnerHtml = CoreLanguage.date;
            tr0.InnerHtml = th1.ToString() + th3.ToString();
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
        
        public static HtmlString ProfileInfo(this HtmlHelper html,mytrip_usersprofile x)
        {
            if (!x.ProfileClose || (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.Name == x.mytrip_users.UserName))
            {
                StringBuilder result = new StringBuilder();
                if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.Name == x.mytrip_users.UserName)
                    result.AppendLine("<div class=\"right\"><div class=\"edit\">" + GeneralMethods.ImgInput("/images/edite.png", "/Home/EditProfile/" + x.mytrip_users.UserName, "rename", 16) + "<a href='/Home/EditProfile/" + x.mytrip_users.UserName + "'>" + CoreLanguage.editProfile + "</a></div></div>");
                result.AppendLine("<table class='noborders'><tr><td>");
                
                if(x.FirstName!=null)
                result.AppendLine("<h4>"+x.FirstName+" "+x.LastName+"</h4>");
                if (x.Site != null)
                    result.AppendLine(CoreLanguage.site+": "+x.Site + "<br/>");
                if (x.Phone != null)
                    result.AppendLine(CoreLanguage.phone+ ": " + x.Phone + "<br/>");
                if (x.icq != null)
                    result.AppendLine("ICQ: " + x.icq + "<br/>");
                if (x.skype != null)
                    result.AppendLine("Skype: " + x.skype + "<br/>");
                if(x.Description!=null)
                        result.AppendLine("<br/>"+x.Description + "<br/>");
                if(GeoSetting.unlockGeo())
                    result.AppendLine("</td><td style='width:300px;'><div id='mapDiv' style='position:relative;height:200px;width:300px;'></div></td></tr></table>");
                else
                    result.AppendLine("</td></tr></table>");
                
                return new HtmlString(result.ToString());
            }
            else
                return null;
        
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