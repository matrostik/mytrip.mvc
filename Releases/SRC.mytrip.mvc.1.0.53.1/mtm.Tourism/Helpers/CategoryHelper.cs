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
        public static HtmlString CategoryTitle(this HtmlHelper html, IQueryable<mytrip_tourscategory> subcategory, mytrip_tourscategory category, IQueryable<mytrip_tourscountry> country, mytrip_tourscountry countryonly,bool _country)
        {
            StringBuilder result = new StringBuilder();
            if (!_country&&category != null&&category.SubCategoryId==0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(category.CategoryId, true, true));
                result.Append("<h1 class='title'><a href='/Tours/Index/1/10/0/0/Tours'>" + ModuleSetting.nameTours() + "</a></h1>");
                result.Append("<h3 class='title'>" + EditorHelper.EditAndDeleteCategory(category.CategoryId,category.UserName,category.mytrip_tourscategory2.UserName) + category.Title + "</h3>");
                if(category.Body!=null&&category.Body.Length>1)
                    result.Append("<div class='content'>" + category.Body + "</div>");
                if (subcategory != null)
                {
                    result.Append("<b class='title'>");

                    bool _start = true;
                    foreach (var z in subcategory)
                    {
                        if (!_start)
                            result.Append(" | ");
                        result.Append("<a href='/Tours/Index/1/10/" + z.CategoryId + "/0/" + z.Path + "'>" + z.Title + "</a> ");
                        _start = false;
                    }
                    result.Append("</b>");
                }
            }
            else if (!_country && category != null && category.SubCategoryId > 0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(category.CategoryId,false, true));
                result.Append("<h1 class='title'><a href='/Tours/Index/1/10/0/0/Tours'>" + ModuleSetting.nameTours() + "</a></h1>");
                result.Append("<h3 class='title'><a href='/Tours/Index/1/10/" + category.SubCategoryId + "/0/" + category.mytrip_tourscategory2.Path + "'>" + category.mytrip_tourscategory2.Title + "</a> / " + EditorHelper.EditAndDeleteCategory(category.CategoryId, category.UserName, category.mytrip_tourscategory2.UserName) + category.Title + "</h3>");
                if (category.Body != null && category.Body.Length > 1)
                    result.Append("<div class='content'>" + category.Body + "</div>");
            }
            else if (!_country && category == null)
            {
                result.Append(EditorHelper.AddCategoryAndTour(0,false,false));
                result.Append("<h1 class='title'>" + ModuleSetting.nameTours() + "</h1>");
                if (subcategory != null)
                {
                    result.Append("<b class='title'>");
                    bool _start = true;
                    foreach (var z in subcategory)
                    {
                        if (!_start)
                            result.Append(" | ");
                        result.Append("<a href='/Tours/Index/1/10/" + z.CategoryId + "/0/" + z.Path + "'>" + z.Title + "</a> ");
                        _start = false;
                    }
                    result.Append("</b>");
                }
            }
            else if (_country && country != null)
            {
                result.Append(EditorHelper.AddCategoryAndTour(0, false, false));
                result.Append("<h1 class='title'>" + ModuleSetting.nameCountry() + "</h1>");
                result.Append("<b class='title'>");
                    bool _start = true;
                    foreach (var z in country)
                    {
                        if (!_start)
                            result.Append(" | ");
                        result.Append("<a href='/Tours/Index/1/10/0/" + z.CountryId + "/" + z.Path + "'>" + z.Title + "</a> ");
                        _start = false;
                    }
                    result.Append("</b>");
                
            }
            else if (_country && country == null&& countryonly!=null)
            {
                result.Append(EditorHelper.AddCategoryAndTour(0, false, false));
                result.Append("<h1 class='title'><a href='/Tours/Index/1/10/0/0/Country'>" + ModuleSetting.nameCountry() + "</a></h1>");
                result.Append("<h3 class='title'>");
                result.Append(countryonly.Title);                
                result.Append("</h3>");

            }
            return new HtmlString(result.ToString());
        }
        public static HtmlString CategoryTitleArhiv(this HtmlHelper html, IQueryable<mytrip_tourscategory> y, mytrip_tourscategory x)
        {
            StringBuilder result = new StringBuilder();
            if (x != null && x.SubCategoryId == 0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(x.CategoryId, true, true));
                result.Append("<h1 class='title'><a href='/Tours/Arhiv/1/10/0/0/Tours'>" + ToursLanguage.arhiv + "</a></h1>");
                result.Append("<h3 class='title'>" + EditorHelper.EditAndDeleteCategory(x.CategoryId, x.UserName, x.mytrip_tourscategory2.UserName) + x.Title + "</h3>");
                if (x.Body != null && x.Body.Length > 1)
                    result.Append("<div class='content'>" + x.Body + "</div>");
                result.Append("<h3 class='title'>");
                bool _start = true;
                foreach (var z in y)
                {
                    if (!_start)
                        result.Append(" | ");
                    result.Append("<a href='/Tours/Arhiv/1/10/" + z.CategoryId + "/0/" + z.Path + "'>" + z.Title + "</a> ");
                    _start = false;
                }
                result.Append("</h3>");
            }
            else if (x != null && x.SubCategoryId > 0)
            {
                result.Append(EditorHelper.AddCategoryAndTour(x.CategoryId, false, true));
                result.Append("<h1 class='title'><a href='/Tours/Arhiv/1/10/0/0/Tours'>" + ToursLanguage.arhiv + "</a></h1>");
                result.Append("<h3 class='title'><a href='/Tours/Arhiv/1/10/" + x.SubCategoryId + "/0/" + x.mytrip_tourscategory2.Path + "'>" + x.mytrip_tourscategory2.Title + "</a> / " + EditorHelper.EditAndDeleteCategory(x.CategoryId, x.UserName, x.mytrip_tourscategory2.UserName) + x.Title + "</h3>");
                if (x.Body != null && x.Body.Length > 1)
                    result.Append("<div class='content'>" + x.Body + "</div>");
            }
            else if (x == null)
            {
                result.Append(EditorHelper.AddCategoryAndTour(0, false, false));
                result.Append("<h1 class='title'>" + ModuleSetting.nameTours() + "</h1>");
                result.Append("<h3 class='title'>");
                bool _start = true;
                foreach (var z in y)
                {
                    if (!_start)
                        result.Append(" | ");
                    result.Append("<a href='/Tours/Arhiv/1/10/" + z.CategoryId + "/0/" + z.Path + "'>" + z.Title + "</a> ");
                    _start = false;
                }
                result.Append("</h3>");
            }
            return new HtmlString(result.ToString());
        }

    }
}
