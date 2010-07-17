using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Mytrip.Mvc.Repository.DataEntities;

namespace Mytrip.Mvc.Helpers
{
    public static class UserDataHelper
    {
        public static string RoleIndex(this HtmlHelper html, IQueryable<mytrip_usersroles> Roles, string onclick)
        {
            StringBuilder result = new StringBuilder();
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
                td1.InnerHtml = _Image("/Content/images/Keys.png", item.RoleName, 20, 0, 0) + a.ToString();
                td2.InnerHtml = item.RoleName;
                td3.InnerHtml = item.mytrip_users.Count().ToString();
                tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                result.AppendLine(tr.ToString());

            }
            return result.ToString();
        }
        public static string UserIndex(this HtmlHelper html, IQueryable<mytrip_users> Users, string onclick)
        {
            StringBuilder result = new StringBuilder();

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
                td1.InnerHtml = a1.ToString() + a.ToString();
                StringBuilder _result = new StringBuilder();
                _result.AppendLine("<b>" + item.UserName + "</b>");
                //????????????????????????
                int rolecount = item.mytrip_usersroles.Count();
                if (rolecount > 0)
                {
                    _result.AppendLine("<br />role: ");
                    int _rolecount = 0;
                    foreach (mytrip_usersroles _item in item.mytrip_usersroles.ToList())
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
                td3.InnerHtml = item.mytrip_usersmembership.Email;
                td4.InnerHtml = String.Format("{0:d}", item.LastActivityDate);
                td5.InnerHtml = String.Format("{0:d}", item.mytrip_usersmembership.CreationDate);
                td6.InnerHtml = String.Format("{0:d}", item.mytrip_usersmembership.LastLoginDate);
                td7.InnerHtml = String.Format("{0:d}", item.mytrip_usersmembership.LastPasswordChangedDate);
                td8.InnerHtml = item.mytrip_usersmembership.UserIP;
                TagBuilder input1 = new TagBuilder("input");
                if (item.mytrip_usersmembership.IsApproved)
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
