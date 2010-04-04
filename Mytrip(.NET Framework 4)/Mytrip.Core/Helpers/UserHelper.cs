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
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Mytrip.Core.Repository.XmlUsers;

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Captcha Helper
    /// </summary>
    public static class UserHelper
    {
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
           
           TagBuilder li_core = new TagBuilder("li");
           TagBuilder a_core = new TagBuilder("a");
           a_core.MergeAttribute("href", "/Core");
           a_core.InnerHtml = "Core Settings";
           li_core.InnerHtml = a_core.ToString();
           ul.InnerHtml = li_user.ToString() + li_file.ToString() + li_core.ToString();
           div_accordioncontent.InnerHtml = ul.ToString();
           div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
           result.AppendLine(div_accordion.ToString());
           return result.ToString();        
        }
        public static string AccordionUsersAndFiles(this HtmlHelper html,string helper)
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

            TagBuilder li_core = new TagBuilder("li");
            TagBuilder a_core = new TagBuilder("a");
            a_core.MergeAttribute("href", "/Core");
            a_core.InnerHtml = "Core Settings";
            li_core.InnerHtml = a_core.ToString();
            ul.InnerHtml = li_user.ToString() + li_file.ToString()+ helper + li_core.ToString();
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
               
                TagBuilder li_core = new TagBuilder("li");
                TagBuilder a_core = new TagBuilder("a");
                a_core.MergeAttribute("href", "/Core");
                a_core.InnerHtml = "Core Settings";
                li_core.InnerHtml = a_core.ToString();
                ul.InnerHtml = li_user.ToString() + li_file.ToString() +li_core.ToString();
                div_accordioncontent.InnerHtml = ul.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string AccordionUsersAndFiles(this HtmlHelper html,string helper, bool RoleAdmin)
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

                TagBuilder li_core = new TagBuilder("li");
                TagBuilder a_core = new TagBuilder("a");
                a_core.MergeAttribute("href", "/Core");
                a_core.InnerHtml = "Core Settings";
                li_core.InnerHtml = a_core.ToString();
                ul.InnerHtml = li_user.ToString() + li_file.ToString()+helper + li_core.ToString();
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
        public static string RoleIndex(this HtmlHelper html, IQueryable<mytrip_Roles> Roles, IEnumerable<XElement> RolesXml, string onclick)
        {
            StringBuilder result = new StringBuilder();
            if (UsersSetting.membership == "MSSQL")
            {
                foreach (var item in Roles)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Users/DeleteRole/" + item.RoleName);
                    a.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
                    a.InnerHtml = _Image("/Content/images/delete.png", "delete", 20, 0, 0);
                    td1.InnerHtml = _Image("/Content/images/Keys.png", item.RoleName, 20, 0, 0)+a.ToString();
                    td2.InnerHtml = item.RoleName;
                    td3.InnerHtml = item.mytrip_Users.Count().ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                }
            }
            else if (UsersSetting.membership == "XML")
            {
                foreach (var item in RolesXml)
                {
                    XmlRoleRepository db = new XmlRoleRepository();
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Users/DeleteRole/" + item.Attribute("RoleName").Value);
                    a.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
                    a.InnerHtml = _Image("/Content/images/delete.png", "delete", 20, 0, 0);
                    td1.InnerHtml = _Image("/Content/images/Keys.png", item.Attribute("RoleName").Value, 20, 0, 0) + a.ToString();
                    td2.InnerHtml = item.Attribute("RoleName").Value;
                    td3.InnerHtml = db.xmlGetUsersInRole(item.Attribute("RoleName").Value).Count().ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                }
            }
            return result.ToString();        
        }
        public static string UserIndex(this HtmlHelper html, IQueryable<mytrip_Users> Users, IEnumerable<XElement> UsersXml, string onclick)
        {
            StringBuilder result = new StringBuilder();

            if (UsersSetting.membership == "MSSQL")
            {
                #region
                foreach (var item in Users)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    TagBuilder td4 = new TagBuilder("td");
                    TagBuilder td5 = new TagBuilder("td");
                    TagBuilder td6 = new TagBuilder("td");
                    TagBuilder td7 = new TagBuilder("td");
                    TagBuilder td8 = new TagBuilder("td");
                    TagBuilder td9 = new TagBuilder("td");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Users/Delete/" + item.UserName);
                    a.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
                    a.InnerHtml = _Image("/Content/images/delete.png", "delete", 20, 0, 0);
                    TagBuilder a1 = new TagBuilder("a");
                    a1.MergeAttribute("href", "/Users/Details/" + item.UserName);
                    a1.InnerHtml = _Image("/Content/images/Users.png", item.UserName, 20, 0, 0);
                    td1.InnerHtml =a1.ToString()+ a.ToString();
                    StringBuilder _result = new StringBuilder();
                    _result.AppendLine("<b>" + item.UserName+"</b>");
                    int rolecount = item.mytrip_Roles.Count();
                    if (rolecount > 0)
                    {
                        _result.AppendLine("<br />role: ");
                        int _rolecount = 0;
                        foreach (mytrip_Roles _item in item.mytrip_Roles.ToList())
                        {
                            _result.AppendLine(_item.RoleName);
                            _rolecount++;
                            if (_rolecount == rolecount)
                            { _result.AppendLine("."); }
                            else
                            { _result.AppendLine(","); }
                        }
                    }
                    td2.InnerHtml = _result.ToString();
                    td3.InnerHtml = item.mytrip_Membership.Email;
                    td4.InnerHtml = String.Format("{0:d}", item.LastActivityDate);
                    td5.InnerHtml = String.Format("{0:d}", item.mytrip_Membership.CreationDate);
                    td6.InnerHtml = String.Format("{0:d}", item.mytrip_Membership.LastLoginDate);
                    td7.InnerHtml = String.Format("{0:d}", item.mytrip_Membership.LastPasswordChangedDate);
                    td8.InnerHtml = item.mytrip_Membership.UserIP;
                    TagBuilder input1 = new TagBuilder("input");
                    if (item.mytrip_Membership.IsApproved)
                        input1.MergeAttribute("checked", "checked");
                    input1.MergeAttribute("disabled", "true");
                    input1.MergeAttribute("id", "IsApproved");
                    input1.MergeAttribute("name", "IsApproved");
                    input1.MergeAttribute("type", "checkbox");
                    input1.MergeAttribute("value", "true");
                    TagBuilder input2=new TagBuilder("input");
                    input2.MergeAttribute("name", "IsApproved");
                    input2.MergeAttribute("type", "hidden");
                    input2.MergeAttribute("value", "false");
                    td9.InnerHtml = input1.ToString()+input2.ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString()
                        +td4.ToString()+td5.ToString()+td6.ToString()+td7.ToString()
                        +td8.ToString()+td9.ToString();
                    result.AppendLine(tr.ToString());
                }
                #endregion
            }
            else if (UsersSetting.membership == "XML")
            {
                #region
                foreach (var item in UsersXml)
                {
                    XmlRoleRepository db = new XmlRoleRepository();
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    TagBuilder td4 = new TagBuilder("td");
                    TagBuilder td5 = new TagBuilder("td");
                    TagBuilder td6 = new TagBuilder("td");
                    TagBuilder td7 = new TagBuilder("td");
                    TagBuilder td8 = new TagBuilder("td");
                    TagBuilder td9 = new TagBuilder("td");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Users/Delete/" + item.Element("UserName").Value);
                    a.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
                    a.InnerHtml = _Image("/Content/images/delete.png", "delete", 20, 0, 0);
                    TagBuilder a1 = new TagBuilder("a");
                    a1.MergeAttribute("href", "/Users/Details/" + item.Element("UserName").Value);
                    a1.InnerHtml = _Image("/Content/images/Users.png", item.Element("UserName").Value, 20, 0, 0);
                    td1.InnerHtml = a1.ToString() + a.ToString();
                    StringBuilder _result = new StringBuilder();
                    _result.AppendLine("<b>" + item.Element("UserName").Value + "</b>");
                    string[] role = db.xmlGetRolesForUser(item.Element("UserName").Value);
                    int rolecount = role.Count();
                    if (rolecount > 0)
                    {
                        _result.AppendLine("<br />role: ");
                        int _rolecount = 0;
                        foreach (string _item in role)
                        {
                            _result.AppendLine(_item);
                            _rolecount++;
                            if (_rolecount == rolecount)
                            { _result.AppendLine("."); }
                            else
                            { _result.AppendLine(","); }
                        }
                    }
                    td2.InnerHtml = _result.ToString();
                    td3.InnerHtml = item.Element("Email").Value;
                    td4.InnerHtml = String.Format("{0:d}", DateTime.Parse(item.Element("LastActivityDate").Value));
                    td5.InnerHtml = String.Format("{0:d}", DateTime.Parse(item.Element("CreationDate").Value));
                    td6.InnerHtml = String.Format("{0:d}", DateTime.Parse(item.Element("LastLoginDate").Value));
                    td7.InnerHtml = String.Format("{0:d}", DateTime.Parse(item.Element("LastPasswordChangedDate").Value));
                    td8.InnerHtml = item.Element("UserIP").Value;
                    TagBuilder input1 = new TagBuilder("input");
                    if (bool.Parse(item.Element("IsApproved").Value))
                        input1.MergeAttribute("checked", "checked");
                    input1.MergeAttribute("disabled", "true");
                    input1.MergeAttribute("id", "IsApproved");
                    input1.MergeAttribute("name", "IsApproved");
                    input1.MergeAttribute("type", "checkbox");
                    input1.MergeAttribute("value", "true");
                    TagBuilder input2 = new TagBuilder("input");
                    input2.MergeAttribute("name", "IsApproved");
                    input2.MergeAttribute("type", "hidden");
                    input2.MergeAttribute("value", "false");
                    td9.InnerHtml = input1.ToString() + input2.ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString()
                        + td4.ToString() + td5.ToString() + td6.ToString() + td7.ToString()
                        + td8.ToString() + td9.ToString();
                    result.AppendLine(tr.ToString());
                }
                #endregion
            }
            return result.ToString();
        }
        
        private static string _Image(string url, string alt, int width, int height, int border)
        {
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = "border-width: " + border + "px; width: " + width + "px; height: " + height + "px;";
            if (width == 0 && height > 0)
                style = "border-width: " + border + "px; height: " + height + "px;";
            if (width > 0 && height == 0)
                style = "border-width: " + border + "px; width: " + width + "px;";
            if (width == 0 && height == 0)
                style = "border-width: " + border + "px;";
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("style", style);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }
    }
}