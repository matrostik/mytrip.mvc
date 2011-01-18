using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Repository;
using System.Text;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CorePageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HtmlString HomePageAnonce(this HtmlHelper html, int id, int count)
        {
            CorePageRepository page = new CorePageRepository();
            var x = page.GetPages(id,LocalisationSetting.culture());
            if (x != null)
            {
                StringBuilder result = new StringBuilder();
                result.Append("<div class='content'>");
                if (x.Title != null && x.Title.Length > 2 && x.ViewOnlyHomePage)
                {
                    result.Append("<h3 class='hometitle'>" + EditorPage(x) + " " + x.Title + "</h3>");
                    result.Append(x.Body);
                }
                else if (x.Title != null && x.Title.Length > 2 && !x.ViewOnlyHomePage)
                {
                    result.Append("<h3 class='hometitle'>" + EditorPage(x) + " <a href='/Home/Page/" + x.PageId + "/" + x.Path + "'class='hometitle'>" + x.Title + "</a></h3>");
                    result.Append(GeneralMethods.RemoveLinkContent(x.Body, count));
                }
                result.Append("</div><div class='last'></div>");
                return new HtmlString(result.ToString());
            }
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string EditorPage(mytrip_corepages x)
        {
            if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
            {
                string edit = "/Core/EditorPage/" + x.PageId + "/EditPage";
                string delete = "/Core/EditorPage/"+ x.PageId + "/DeletePage";                
                return GeneralMethods.ImgInput("/images/edite.png", edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", delete, "delete", 14);
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HtmlString EditorPage(this HtmlHelper html,int id)
        {
            if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
            {
                string edit = "/Core/EditorPage/" + id + "/EditPage";
                string delete = "/Core/EditorPage/" + id + "/DeletePage";
                return new HtmlString(GeneralMethods.ImgInput("/images/edite.png", edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", delete, "delete", 14));
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HtmlString AddPageAndSubPage(this HtmlHelper html, mytrip_corepages x)
        {
            if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
                img.MergeAttribute("style", "height:14px;");
                string addpage = "<a href=\"/Core/EditorPage/0/CreatePage\">" + CoreLanguage.CreatePage + img + "</a>";
                string addsubpage = x.SubPagesId == 0 ? "<br/><a href=\"/Core/EditorPage/" + x.PageId + "/CreatePage\">" + CoreLanguage.CreateSubPage + img + "</a>" : "";              
                return new HtmlString(addpage+addsubpage);
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HtmlString LinkPageAndSubpage(this HtmlHelper html, mytrip_corepages x)
        {
            bool link = false;
            StringBuilder result = new StringBuilder();
            result.Append("<div class='last'></div> ");
            result.Append("<div class='content'>");
            if (x.SubPagesId == 0)
            {

                result.Append("<ul class='styled'>");
                foreach (var _x in x.mytrip_corepages1)
                {
                    link = true;
                    result.Append("<li>");
                    result.Append("<a href='/Home/Page/"+_x.PageId+"/"+_x.Path+"'>"+_x.Title+"</a>");
                    result.Append("</li>");
                }
                result.Append("</ul>");
            }
            else {
                link = true;
                result.Append("<ul class='styled'>");
                    result.Append("<li>");
                    result.Append("<b><a href='/Home/Page/" + x.mytrip_corepages2.PageId + "/" + x.mytrip_corepages2.Path + "'>" + x.mytrip_corepages2.Title + "</a></b>");
                    result.Append("</li>");
                result.Append("</ul>");
                result.Append("<ul class='styled'>");
                foreach (var _x in x.mytrip_corepages2.mytrip_corepages1)
                {
                    if (_x.PageId != x.PageId)
                    {
                        result.Append("<li>");
                        result.Append("<a href='/Home/Page/" + _x.PageId + "/" + _x.Path + "'>" + _x.Title + "</a>");
                        result.Append("</li>");
                    }
                }
                result.Append("</ul>");
            }
            result.Append("</div>");
            if (link)
                return new HtmlString(result.ToString());
            else return null;
        }
    }
}