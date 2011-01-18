using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Settings;
using System.Web;
using mtm.Store.Repository.DataEntities;
using mtm.Store.Models;

namespace mtm.Store.Helpers
{
    public static class DepartmentAndProducerHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="department"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static HtmlString StoreDepartment(this HtmlHelper html, IQueryable<mytrip_storedepartment> department, int take)
        {
            int _count = department.Count();
            int column = (ModuleSetting.columnDepartment() > _count) ? _count : ModuleSetting.columnDepartment();
            int _count2 = 0;
            int _line = 1;
            if (_count > column)
            {
                Math.DivRem(_count, column, out _count2);
                _line = (int)Math.Ceiling((double)_count / column);
            }
            int count = 1;
            int tr = 0;
            int width = (column > 0) ? (100 / column) : 100;
            int style = ModuleSetting.styleDepartment();
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;
            foreach (var article in department)
            {
                int countproduct = article.mytrip_storeproduct.Where(x => x.Culture == LocalisationSetting.culture() || x.AllCulture == true).Count();
                string subdepartment = string.Empty;
                foreach (var item in article.mytrip_storedepartment1)
                {
                    int _countproduct = item.mytrip_storeproduct.Where(x => x.Culture == LocalisationSetting.culture() || x.AllCulture == true).Count();
                    subdepartment += string.Format("<li><a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a></li>", item.DepartmentId, item.Path, item.Title, _countproduct);
                    countproduct += _countproduct;
                }
                string _subdepartment = string.Empty;
                if (!String.IsNullOrEmpty(subdepartment))
                {
                    _subdepartment = string.Format("<ul>{0}</ul>", subdepartment);
                }
                string _content = string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\">{2}</a><b><a href=\"/Store/Index/1/10/{0}/0/1/{1}\" class=\"hometitle\" >{3} ({4})</a></b><br/>{5}{6}", article.DepartmentId, article.Path, StoreHelper.GetImage(article.DepartmentId,article.Title, "/Content/Store/Department", ModuleSetting.widthImgDepartment()), article.Title, countproduct, article.Body, _subdepartment);


                //Отображение скидки для отдела
                if ((article.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storesale.Sale > 0 && article.mytrip_storesale.CloseDate > DateTime.Now))
                {
                    DateTime saledate = article.mytrip_storedepartment2.mytrip_storesale.CloseDate;
                    int sale1 = (article.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                    int sale2 = (article.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storesale.Sale : 0;
                    if (sale2 > sale1)
                    {
                        sale1 = sale2;
                        saledate = article.mytrip_storesale.CloseDate;
                    }
                    StringBuilder sale = new StringBuilder();
                    sale.Append("<div class='right'>");
                    sale.Append("<b class='sale'>");
                    sale.Append(string.Format(StoreLanguage.saleView, sale1, string.Format("{0:dd MMMM yyyy}", saledate)));
                    sale.Append("</b>");
                    sale.Append("</div>");
                    _content = sale + _content;
                }
                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content, count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HtmlString StoreTitleDepartmentAndProducer(this HtmlHelper html, TitleDepartmentModel x)
        {
            string a = string.Empty;
            string b = string.Empty;
            string c = "<table class=\"noborders\">";
            string producer = string.Format("{0}<h2 class=\"title\">{1}</h2>", StoreHelper.StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameProducer());
            string _store = string.Format("{0}<h2 class=\"title\">{1}</h2>", StoreHelper.StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameStore());
            if (x.count >= 0 || x._search)
            {
                producer = string.Format("{0}<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >{1}</a></h2>", StoreHelper.StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameProducer());
                _store = string.Format("{0}<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Department\" >{1}</a></h2>", StoreHelper.StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameStore());
            }
            if (x.producer)
                c += string.Format("<tr><td>{0}</td><td><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Department\">{1}</a></h2></td></tr>", producer, ModuleSetting.nameStore());
            else
                c += string.Format("<tr><td>{0}</td><td><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >{1}</a></h2></td></tr>", _store, ModuleSetting.nameProducer());
            if (x.count >= 0)
            {
                string image = "";
                if (x.producer)
                    image = StoreHelper.GetImage(x.produceridforeditor,x.ProducerTitle, "/Content/Store/Producer", ModuleSetting.widthImgDepartment());
                else
                    image = StoreHelper.GetImage(x.createdepartment, x.ProducerTitle, "/Content/Store/Department", ModuleSetting.widthImgDepartment());
                b = string.Format(" ({0})", x.count);
                if (x.subDepartmentId > 0)
                    a = StoreHelper.EditAndDeleteCategory(x.subDepartmentId, x.User, x.SubUser, x.producer, x.produceridforeditor) + string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a> / ", x.subDepartmentId, x.subDepartmentPath, x.subDepartmentTitle, x.subcount);
                c += string.Format("<tr><td><h2 class=\"title\">{0}{1}{2}</h2>{3}</td><td>{4}</td></tr>", a, StoreHelper.EditAndDeleteCategory(x.createdepartment, x.User, x.SubUser, x.producer, x.produceridforeditor) + x.title, b, x.body, image);
            }
            c += "</table>";
            if (x._search)
            {
                if (x.id > 0 && x.ProducerId > 0)
                {
                    c += "<table class=\"noborders\"><tr><td>";
                    b = string.Format(" ({0})", x.departmentcount);
                    if (x.subDepartmentId > 0)
                        a = StoreHelper.EditAndDeleteCategory(x.subDepartmentId, x.User, x.SubUser, x.producer, x.produceridforeditor) + string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a> / ", x.subDepartmentId, x.subDepartmentPath, x.subDepartmentTitle, x.subcount);
                    c += "<h2 class=\"title\">" + a + "<a href=\"/Store/Index/1/10/" + x.id + "/0/1/" + x.path + "\" >" + StoreHelper.EditAndDeleteCategory(x.createdepartment, x.User, x.SubUser, x.producer, x.produceridforeditor) + x.title + b + "</a></h2>" + x.body + "</td><td>" + GeneralMethods.ImageForAbstract(x.img, ModuleSetting.widthImgDepartment()) + "</td>";
                    c += "<td>" + "<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/" + x.ProducerId + "/1/" + x.ProducerPath + "\" >" + x.ProducerTitle + " (" + x.producercount + ")" + "</a></h2>" + x.ProducerBody + "</td><td style=\"padding:0;border:0;\">" + GeneralMethods.ImageForAbstract(x.ProducerImg, ModuleSetting.widthImgDepartment());
                    c += "</td></tr></table>";
                }
                string search = string.Empty;
                if (x.search.Length > 0)
                    search = " " + StoreLanguage._for + " \"" + x.search + "\"";
                c += "<table class=\"noborders\"><tr><td>";
                c += "<h3 class=\"title\">" + StoreLanguage.Search + search + ", " + StoreLanguage.found + " " + x.totalsearch + " " + StoreLanguage.results + "</h3>";
                c += "</td></tr></table>";
            }
            return new HtmlString(c);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="department"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static HtmlString StoreProducer(this HtmlHelper html, IQueryable<mytrip_storeproducer> department, int take)
        {
            int column = ModuleSetting.columnDepartment();
            int _count = department.Count();
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
            int style = ModuleSetting.styleDepartment();
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;
            foreach (var article in department)
            {
                int countproduct = article.mytrip_storeproduct.Where(x => x.Culture == LocalisationSetting.culture() || x.AllCulture == true).Count();

                string _content = "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\">" + StoreHelper.GetImage(article.ProducerId,article.Title, "/Content/Store/Producer", ModuleSetting.widthImgDepartment()) + "</a>";
                _content += "<b><a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\" class=\"hometitle\" >" +
                    article.Title + " (" + countproduct + ")</a></b><br/>" + article.Body;
                //Отображение скидки для производителя
                if (article.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storesale.CloseDate > DateTime.Now)
                {
                    StringBuilder sale = new StringBuilder();
                    sale.Append("<div class='right'>");
                    sale.Append("<b class='sale'>");
                    sale.Append(string.Format(StoreLanguage.saleView, article.mytrip_storesale.Sale, string.Format("{0:dd MMMM yyyy}", article.mytrip_storesale.CloseDate)));
                    sale.Append("</b>");
                    sale.Append("</div>");
                    _content = sale + _content;
                }
                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
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
    }
}
