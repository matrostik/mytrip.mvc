using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using mtm.Tourism.Repository.DataEntities;

namespace mtm.Tourism.Helpers
{
    public static class CategoryHelper
    {
        public static HtmlString CategoryTitle(this HtmlHelper html,IQueryable<mytrip_tourscategory> y, mytrip_tourscategory x)
        {
            StringBuilder result = new StringBuilder();
            if (x != null&&x.SubCategoryId==0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(x.CategoryId, true, true));
                result.Append("<h1 class='title'><a href='/Tours/Index/1/10/0/Tours'>" + ModuleSetting.nameTours() + "</a></h1>");
                result.Append("<h3 class='title'>" + EditorHelper.EditAndDeleteCategory(x.CategoryId,x.UserName,x.mytrip_tourscategory2.UserName) + x.Title + "</h3>");
                if(x.Body!=null&&x.Body.Length>1)
                    result.Append("<div class='content'>" + x.Body + "</div>");
                result.Append("<h3 class='title'>");
                bool _start = true;
                foreach (var z in y)
                {
                    if (!_start)
                        result.Append(" | ");
                    result.Append("<a href='/Tours/Index/1/10/" + z.CategoryId + "/" + z.Path + "'>" + z.Title + "</a> ");
                    _start = false;
                }
                result.Append("</h3>");
            }
            else if (x != null && x.SubCategoryId > 0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(x.CategoryId,false, true));
                result.Append("<h1 class='title'><a href='/Tours/Index/1/10/0/Tours'>" + ModuleSetting.nameTours() + "</a></h1>");
                result.Append("<h3 class='title'><a href='/Tours/Index/1/10/" + x.SubCategoryId + "/" + x.mytrip_tourscategory2.Path + "'>" + x.mytrip_tourscategory2.Title + "</a> / " + EditorHelper.EditAndDeleteCategory(x.CategoryId, x.UserName, x.mytrip_tourscategory2.UserName) + x.Title + "</h3>");
                if (x.Body != null && x.Body.Length > 1)
                    result.Append("<div class='content'>" + x.Body + "</div>");
            }
            else if (x == null)
            {
                result.Append(EditorHelper.AddCategoryAndTour(0,false,false));
                result.Append("<h1 class='title'>" + ModuleSetting.nameTours() + "</h1>");
                result.Append("<h3 class='title'>");
                bool _start = true;
                foreach (var z in y)
                {
                    if (!_start)
                        result.Append(" | ");
                    result.Append("<a href='/Tours/Index/1/10/" + z.CategoryId + "/" + z.Path + "'>" + z.Title + "</a> ");
                    _start = false;
                }
                result.Append("</h3>");
            }
            return new HtmlString(result.ToString());
        }

    }
}
