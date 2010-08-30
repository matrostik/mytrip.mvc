/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Repository.DataEntities;
using System.Web;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// UserDataHelper
    /// </summary>
    public static class UserDataHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="Roles"></param>
        /// <param name="onclick"></param>
        /// <returns></returns>
        public static HtmlString RoleIndex(this HtmlHelper html, IQueryable<mytrip_usersroles> Roles, string onclick)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in Roles)
            {
                TagBuilder tr = new TagBuilder("tr");
                TagBuilder td1 = new TagBuilder("td");
                TagBuilder td2 = new TagBuilder("td");
                TagBuilder td3 = new TagBuilder("td");
                td1.InnerHtml =GeneralMethods.ImgInput("/images/delete.png", "/Users/DeleteRole/" + item.RoleName, "delete");
                td2.InnerHtml = item.RoleName;
                td3.InnerHtml = item.mytrip_users.Count().ToString();
                tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                result.AppendLine(tr.ToString());

            }
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="Users"></param>
        /// <param name="onclick"></param>
        /// <returns></returns>
        public static HtmlString UserIndex(this HtmlHelper html, IQueryable<mytrip_users> Users, string onclick)
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
                td1.InnerHtml = GeneralMethods.ImgInput("/images/detail.png", "/Users/Details/" + item.UserName, "rename") + GeneralMethods.ImgInput( "/images/delete.png", "/Users/Delete/" + item.UserName, "delete");
                StringBuilder _result = new StringBuilder();
                TagBuilder divGravatar = new TagBuilder("a");
                divGravatar.MergeAttribute("href", "/Home/Profile/" + item.UserName);
                divGravatar.InnerHtml = AvatarHelper.Avatar(html, item.mytrip_usersmembership.Email, new { width = 40 }).ToString();
                TagBuilder _divGravatar = new TagBuilder("div");
                _divGravatar.MergeAttribute("style", "position: relative;margin-left:2px; float: right");
                _divGravatar.InnerHtml = divGravatar.ToString();
                _result.AppendLine(_divGravatar+"<b>" + item.UserName + "</b>");
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
                td4.InnerHtml = string.Format("{0:d}", item.LastActivityDate);
                td5.InnerHtml = string.Format("{0:d}", item.mytrip_usersmembership.CreationDate);
                td6.InnerHtml = string.Format("{0:d}", item.mytrip_usersmembership.LastLoginDate);
                td7.InnerHtml = string.Format("{0:d}", item.mytrip_usersmembership.LastPasswordChangedDate);
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

            return new HtmlString(result.ToString());
        }
    }
}
