using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using mtm.Tourism.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Tourism.Helpers
{
    public static class ToursHelper
    {
        public static HtmlString ToursIndex(this HtmlHelper html, IQueryable<mytrip_tours> x, mytrip_tourscategory y)
        {
            int column = ModuleSetting.columnTours();
            int _count = x.Count();
            if (column > _count)
                column = _count;
            int _count2 = 0;
            int _line = 1;
            if (_count > column)
            {
                Math.DivRem(_count, column, out _count2);
                _line = (int)Math.Ceiling((double)_count / column);
            }
            int count = 1;
            int tr = 0;
            int width = 100;
            if (column > 0)
                width = 100 / column;
            int style = ModuleSetting.styleTours();
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;
            int category = 0;
            if (y != null && y.CategoryId == 0)
                category = 1;
            else if (y != null && y.CategoryId > 0)
                category = 2;
            foreach (var item in x)
            {
                StringBuilder _content = new StringBuilder();
                _content.Append(GeneralMethods.ImageForAbstract(item.Imige, ModuleSetting.widthImgTours()));
                _content.Append("<h3 class='title'><a href='/Tours/View/" + item.TourId + "/" + item.Path + "'>" + item.Title + "</a></h3>");
                var date = item.StopDate - item.StartDate;
                _content.Append("<b>" + string.Format(ToursLanguage.DateTour, string.Format("{0:dd MMMM yyyy}", item.StartDate), string.Format("{0:dd MMMM yyyy}", item.StopDate), date.Days+1) + "</b><br/>");
                _content.Append("<b class='sale'>" + string.Format(ToursLanguage.priceMin, MoneyHelpers.ConvertMoney(item.MoneyId, item.MinPrice)) + "</b><br/>");
               if(ModuleSetting.viewDescription())
                _content.Append(item.Body + "<br/>");
                if (category == 0 && item.mytrip_tourscategory.SubCategoryId == 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                else if (category == 0 && item.mytrip_tourscategory.SubCategoryId > 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.mytrip_tourscategory.SubCategoryId + "/" + item.mytrip_tourscategory.mytrip_tourscategory2.Path + "'>" + item.mytrip_tourscategory.mytrip_tourscategory2.Title + "</a><br/>");
                    _content.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                else if (category == 1 && item.mytrip_tourscategory.SubCategoryId > 0)
                {
                    _content.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, ModuleSetting.styleTours(), tr, width, _content.ToString(),
                    count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
                tr = tr2;
                _line2 = _line3;
                count++;
            }
            if (tr > 0 && tr % 2 != 0)
                result.AppendLine(finaltr);
            table.AddCssClass(styletable);
            table.InnerHtml = result.ToString();
            string _CategoryName = string.Empty;
            if (column > 0)
                return new HtmlString(_CategoryName + start + table.ToString() + end);
            else
                return null;
        }
        public static string ImageForAbstract(string image, int width)
        {
            if (image != null && image.Contains("src"))
            {
                image = image.Remove(0, image.IndexOf("src"));
                image = image.Remove(0, (image.IndexOf("\"") + 1));
                image = image.Remove(image.IndexOf("\""));
                string title = image.Remove(0, (image.LastIndexOf("/") + 1));
                title = title.Remove(title.LastIndexOf("."));
                return string.Format("<img src='{0}' alt='{1}' title='{1}' class='imgabstract2' style='width:{2}px;'/>", image, title, width);
            }
            else
                return string.Empty;
        }
        public static HtmlString TourView(this HtmlHelper html, mytrip_tours x)
        {
            var date = x.StopDate - x.StartDate;
            StringBuilder result = new StringBuilder();
            result.Append(ImageForAbstract(x.Imige, ModuleSetting.widthImgTours()));
            result.Append("<b>" + string.Format(ToursLanguage.DateTour, string.Format("{0:dd MMMM yyyy}", x.StartDate), string.Format("{0:dd MMMM yyyy}", x.StopDate), date.Days+1) + "</b><br/>");
            result.Append("<b class='sale'>" + string.Format(ToursLanguage.priceMin, MoneyHelpers.ConvertMoney(x.MoneyId, x.MinPrice)) + "</b><br/>");
            
            result.Append(x.Body + "<br/>");
            if (x.mytrip_tourscategory.SubCategoryId > 0)
            {
                result.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + x.mytrip_tourscategory.SubCategoryId + "/" + x.mytrip_tourscategory.mytrip_tourscategory2.Path + "'>" + x.mytrip_tourscategory.mytrip_tourscategory2.Title + "</a><br/>");
                result.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + x.CategoryId + "/" + x.mytrip_tourscategory.Path + "'>" + x.mytrip_tourscategory.Title + "</a><br/>");
            }
            else { result.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + x.CategoryId + "/" + x.mytrip_tourscategory.Path + "'>" + x.mytrip_tourscategory.Title + "</a><br/>"); }
            return new HtmlString(result.ToString());
        }
        public static HtmlString TourVarianty(this HtmlHelper html, mytrip_tours x)
        {
            if (x.mytrip_toursvariants.Count() > 0)
            {
                bool services = false;
                foreach (var item in x.mytrip_toursvariants)
                {
                    if (item.Services != null && item.Services.Length > 2)
                        services = true;
                }
                StringBuilder result = new StringBuilder();
                result.Append("<div class='content'>");
                result.Append("<table class='noborders'>");
                result.Append("<tr>");
                result.Append("<td>" +ToursLanguage.Hotel+ "</td>");
                if(services)
                result.Append("<td>" + ToursLanguage.Services + "</td>");
                result.Append("<td style='width:100px;'>" + ToursLanguage.Price + "</td>");
                result.Append("</tr>");
                foreach (var item in x.mytrip_toursvariants.OrderBy(z=>z.Price))
                {
                    result.Append("<tr>");
                    result.Append("<td class='topborder'>" + item.Hotel + "</td>");
                    if (services)
                        result.Append("<td class='topborder'>" + item.Services + "</td>");
                    result.Append("<td class='topborder'><b>" + MoneyHelpers.ConvertMoney(item.MoneyId, item.Price) + "</b></td>");
                    result.Append("</tr>");  
                }
                result.Append("</table></div>");
                return new HtmlString(result.ToString());
            }
            else return null;

        
        }
        public static HtmlString TourVariantyForEditor(this HtmlHelper html, IEnumerable<mytrip_toursvariants> x)
        {
            return _TourVariantyForEditor(x);


        }
        public static HtmlString _TourVariantyForEditor(IEnumerable<mytrip_toursvariants> x)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<table class='noborders'>");
            result.Append("<tr>");
            result.Append("<td>" + ToursLanguage.Hotel + "</td>");
            result.Append("<td>" + ToursLanguage.Services + "</td>");
            result.Append("<td style='width:100px;'>" + ToursLanguage.Price + "</td>");
            result.Append("</tr>");
            bool z = false;
            foreach (var item in x)
            {
                result.Append("<tr>");
                result.Append("<td class='topborder'>" + item.Hotel + "</td>");
                result.Append("<td class='topborder'>" + item.Services + "</td>");
                result.Append("<td class='topborder'><b>" + MoneyHelpers.ConvertMoney(item.MoneyId, item.Price) + "</b></td>");                
                result.Append("<td class='topborder'><div class='deletevariant' id="+item.VariantId+">"+GeneralMethods.Image("/images/delete.png",14,0,"delete",0)+"</div></td>");
                result.Append("</tr>");
                z = true;
            }
            result.Append("</table>");
            if (z)
                return new HtmlString(result.ToString());

            else return null;


        }
    }
}
