using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Store.Repository.DataEntities;
using Mytrip.Store.Models;
using Mytrip.Mvc;
using System.Web;
using Mytrip.Mvc.Settings;
using Mytrip.Mvc.Repository;
using Mytrip.Store.Repository;
using Mytrip.Mvc.Helpers;

namespace Mytrip.Store.Helpers
{
    /// <summary>Хелпер для магазина
    /// </summary>
    public static class StoreHelper
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
                int countproduct = article.mytrip_storeproduct.Count();
                string subdepartment = string.Empty;
                foreach (var item in article.mytrip_storedepartment1)
                {
                    int _countproduct = item.mytrip_storeproduct.Count();
                    subdepartment += string.Format("<li><a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a></li>", item.DepartmentId, item.Path, item.Title, _countproduct);
                    countproduct += _countproduct;
                }
                string _subdepartment = string.Empty;
                if (!String.IsNullOrEmpty(subdepartment))
                {
                    _subdepartment = string.Format("<ul>{0}</ul>", subdepartment);
                }
                string _content = string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\">{2}</a><b><a href=\"/Store/Index/1/10/{0}/0/1/{1}\" class=\"hometitle\" >{3} ({4})</a></b><br/>{5}{6}", article.DepartmentId, article.Path, GeneralMethods.ImageForAbstract(article.Image, ModuleSetting.widthImgDepartment()), article.Title, countproduct, article.Body, _subdepartment);
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
        public static HtmlString StoreTitleDepartment(this HtmlHelper html, TitleDepartmentModel x)
        {
            string a = string.Empty;
            string b = string.Empty;
            string c = "<table class=\"noborders\">";
            string producer = string.Format("{0}<h2 class=\"title\">{1}</h2>", StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameProducer());
            string _store = string.Format("{0}<h2 class=\"title\">{1}</h2>", StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameStore());
            if (x.count >= 0 || x._search)
            {
                producer = string.Format("{0}<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >{1}</a></h2>", StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameProducer());
                _store = string.Format("{0}<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Department\" >{1}</a></h2>", StoreAddCategoryAndProduct(x.createdepartment, x.count, x.subDepartmentId, x._search, x.producer), ModuleSetting.nameStore());
            }
            if (x.producer)
                c += string.Format("<tr><td>{0}</td><td><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Department\">{1}</a></h2></td></tr>", producer, ModuleSetting.nameStore());
            else
                c += string.Format("<tr><td>{0}</td><td><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >{1}</a></h2></td></tr>", _store, ModuleSetting.nameProducer());
            if (x.count >= 0)
            {
                b = string.Format(" ({0})", x.count);
                if (x.subDepartmentId > 0)
                    a = EditAndDeleteCategory(x.subDepartmentId, x.User, x.SubUser, x.producer, x.produceridforeditor) + string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a> / ", x.subDepartmentId, x.subDepartmentPath, x.subDepartmentTitle, x.subcount);
                c += string.Format("<tr><td><h2 class=\"title\">{0}{1}{2}</h2>{3}</td><td>{4}</td></tr>", a, EditAndDeleteCategory(x.createdepartment, x.User, x.SubUser, x.producer, x.produceridforeditor) + x.title, b, x.body, GeneralMethods.ImageForAbstract(x.img, ModuleSetting.widthImgDepartment()));
            }
            c += "</table>";
            if (x._search)
            {
                if (x.id > 0 && x.ProducerId > 0)
                {
                    c += "<table class=\"noborders\"><tr><td>";
                    b = string.Format(" ({0})", x.departmentcount);
                    if (x.subDepartmentId > 0)
                        a = EditAndDeleteCategory(x.subDepartmentId, x.User, x.SubUser, x.producer, x.produceridforeditor) + string.Format("<a href=\"/Store/Index/1/10/{0}/0/1/{1}\" >{2} ({3})</a> / ", x.subDepartmentId, x.subDepartmentPath, x.subDepartmentTitle, x.subcount);
                    c += "<h2 class=\"title\">" + a + "<a href=\"/Store/Index/1/10/" + x.id + "/0/1/" + x.path + "\" >" + EditAndDeleteCategory(x.createdepartment, x.User, x.SubUser, x.producer, x.produceridforeditor) + x.title + b + "</a></h2>" + x.body + "</td><td>" + GeneralMethods.ImageForAbstract(x.img, ModuleSetting.widthImgDepartment()) + "</td>";
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
        /// <param name="subdepartment"></param>
        /// <param name="producer"></param>
        /// <param name="DepartmentAndProducer"></param>
        /// <param name="DepartmentAndProducer2"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static HtmlString StoreProduct(this HtmlHelper html, IQueryable<mytrip_storeproduct> department, int take, int subdepartment,
            bool producer, bool DepartmentAndProducer, bool DepartmentAndProducer2, bool compare)
        {
            int column = ModuleSetting.columnProduct();
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
            int style = ModuleSetting.styleProduct();
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;

            bool pricecompare = false;
            bool producercompare = false;
            bool departmentcompare = false;
            bool subdepartmentcompare = false;
            bool datecompare = false;
            if (compare)
            {
                string _pricecompare = "";
                string _producercompare = "";
                string _departmentcompare = "";
                string _subdepartmentcompare = "";
                string _datecompare = "";
                foreach (var article in department)
                {
                    //price
                    if (!_pricecompare.Contains(string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.Price, article.Culture))))
                    {
                        pricecompare = true;
                        _pricecompare += string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.Price, article.Culture));
                    }
                    if (pricecompare && !_pricecompare.Contains("]["))
                        pricecompare = false;
                    //producer
                    if (!_producercompare.Contains(string.Format("[{0}]", article.ProducerId.ToString())))
                    {
                        producercompare = true;
                        _producercompare += string.Format("[{0}]", article.ProducerId.ToString());
                    }
                    if (producercompare && !_producercompare.Contains("]["))
                        producercompare = false;
                    //department
                    if (!_departmentcompare.Contains(string.Format("[{0}]", article.DepartmentId.ToString())))
                    {
                        departmentcompare = true;
                        _departmentcompare += string.Format("[{0}]", article.DepartmentId.ToString());
                    }
                    if (departmentcompare && !_departmentcompare.Contains("]["))
                        departmentcompare = false;
                    //subdepartment
                    if (!_subdepartmentcompare.Contains(string.Format("[{0}]", article.mytrip_storedepartment.SubDepartmentId.ToString())))
                    {
                        subdepartmentcompare = true;
                        _subdepartmentcompare += string.Format("[{0}]", article.mytrip_storedepartment.SubDepartmentId.ToString());
                    }
                    if (subdepartmentcompare && !_subdepartmentcompare.Contains("]["))
                        subdepartmentcompare = false;

                    string _date = string.Format("{0:dd MMMM yyyy}", article.CreationDate);
                    //date
                    if (!_datecompare.Contains(string.Format("[{0}]", _date)))
                    {
                        datecompare = true;
                        _datecompare += string.Format("[{0}]", _date);
                    }
                    if (datecompare && !_datecompare.Contains("]["))
                        datecompare = false;
                }
            }


            foreach (var article in department)
            {
                string a = "<span class='replasesearch'>"; string b = "</span>";
                string departmentlink = string.Empty;
                if (!producer && !DepartmentAndProducer)
                {
                    if (producercompare)
                        departmentlink = StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.mytrip_storeproducer.Path + "\" >" +
                        a + article.mytrip_storeproducer.Title + b + "</a><br/>";

                    else
                        departmentlink = StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.mytrip_storeproducer.Path + "\" >" +
                        article.mytrip_storeproducer.Title + "</a><br/>";

                }
                if (producer || DepartmentAndProducer2)
                {
                    if (article.mytrip_storedepartment.SubDepartmentId == 0)
                    {
                        if (departmentcompare)
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                            a + article.mytrip_storedepartment.Title + b + "</a><br/>";
                        else
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                            article.mytrip_storedepartment.Title + "</a><br/>";
                    }
                    else
                    {
                        if (subdepartmentcompare)
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.mytrip_storedepartment.SubDepartmentId + "/0/1/" + article.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                                a + article.mytrip_storedepartment.mytrip_storedepartment2.Title + b + "</a>";
                        else
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.mytrip_storedepartment.SubDepartmentId + "/0/1/" + article.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                                article.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>";
                        if (departmentcompare)
                            departmentlink += "<br/>" + StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                            a + article.mytrip_storedepartment.Title + b + "</a>";
                        else
                            departmentlink += "<br/>" + StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                                 article.mytrip_storedepartment.Title + "</a>";
                    }
                }
                if (!producer && subdepartment == 0 && article.mytrip_storedepartment.SubDepartmentId != 0 && !DepartmentAndProducer2)
                {
                    if (departmentcompare)
                        departmentlink += StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                           a + article.mytrip_storedepartment.Title + b + "</a>";
                    else
                        departmentlink += StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                        article.mytrip_storedepartment.Title + "</a>";
                }


                string prise = string.Empty;
                if (article.ViewPrice)
                {
                    if (pricecompare)
                        prise = StoreLanguage.Prise + " <b>" + a + MoneyHelpers.ConvertMoney(article.Price, article.Culture) + b + "</b>";
                    else
                        prise = StoreLanguage.Prise + " <b>" + MoneyHelpers.ConvertMoney(article.Price, article.Culture) + "</b>";
                }
                string votes = string.Empty;
                if (article.ViewVotes)
                    votes = GeneralMethods.CoreRating(article.ViewVotes, false, (double)article.TotalVotes, -1) + "</b><br/>";
                string mycart = GeneralMethods.Button("/Store/Cart/" + article.ProductId, StoreLanguage.buy, false, "right");

                string checkeds = (HttpContext.Current.Request.Cookies["myTripProductComparison"] != null
                 && HttpContext.Current.Request.Cookies["myTripProductComparison"].Value.Contains(string.Format("[{0}]", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;

                string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] != null
                    && HttpContext.Current.Request.Cookies["myTripProductCart"].Value.Contains(string.Format("_{0}_", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;
                string _date = string.Format("{0:dd MMMM yyyy}", article.CreationDate);
                if (datecompare)
                    _date = a + _date + b;

                string storecart = "<br/><input " + cart + " type='checkbox' value='_" + article.ProductId
                    + "_1' class='cart' /> " + string.Format(StoreLanguage.cart, "<a href='/Store/Cart/" + article.ProductId + "'>" + StoreLanguage.cart2 + "</a>");
                string comparision = "<br/><input " + checkeds + " type='checkbox' value='" + article.ProductId
                    + "' class='comparision' /> " + string.Format(StoreLanguage.compare, "<a href='/Store/View/0/Comparision'>" + StoreLanguage.compare2 + "</a>");
                string _content = "<a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\">" + GeneralMethods.ImageForAbstract(article.Image, ModuleSetting.widthImgDepartment()) + "</a>";
                _content += "<b><a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\" class=\"hometitle\" >" +
                    article.Title + "</a></b><br/>" + article.Abstract + "<br/>" + mycart + votes + prise + "<br/>" + _date + "<br/>" + departmentlink
                    + comparision + storecart;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HtmlString ViewProduct(this HtmlHelper html, mytrip_storeproduct x)
        {
            string departmentlink = string.Empty;

            departmentlink = StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + x.ProducerId + "/1/" + x.mytrip_storeproducer.Path + "\" >" +
            x.mytrip_storeproducer.Title + "</a><br/>";
            if (x.mytrip_storedepartment.SubDepartmentId == 0)
            {
                departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + x.DepartmentId + "/0/1/" + x.mytrip_storedepartment.Path + "\" >" +
                x.mytrip_storedepartment.Title + "</a><br/>";
            }
            else
            {
                departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + x.mytrip_storedepartment.SubDepartmentId + "/0/1/" + x.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                    x.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>";

                departmentlink += "<br/>" + StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + x.DepartmentId + "/0/1/" + x.mytrip_storedepartment.Path + "\" >" +
                     x.mytrip_storedepartment.Title + "</a>";
            }
            string checkeds = (HttpContext.Current.Request.Cookies["myTripProductComparison"] != null
                 && HttpContext.Current.Request.Cookies["myTripProductComparison"].Value.Contains(string.Format("[{0}]", x.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;

            string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] != null
                && HttpContext.Current.Request.Cookies["myTripProductCart"].Value.Contains(string.Format("_{0}_", x.ProductId)))
            ? "checked=\"checked\""
            : string.Empty;
            string storecart = "<br/><input " + cart + " type='checkbox' value='_" + x.ProductId
                   + "_1' class='cart' /> " + string.Format(StoreLanguage.cart, "<a href='/Store/Cart/" + x.ProductId + "'>" + StoreLanguage.cart2 + "</a>");
            string comparision = "<br/><input " + checkeds + " type='checkbox' value='" + x.ProductId
                + "' class='comparision' /> " + string.Format(StoreLanguage.compare, "<a href='/Store/View/0/Comparision'>" + StoreLanguage.compare2 + "</a>");
            string prise = string.Empty;
            if (x.ViewPrice)
                prise = StoreLanguage.Prise + " <b>" + MoneyHelpers.ConvertMoney(x.Price, x.Culture) + "</b>";
            string _date = string.Format("{0:dd MMMM yyyy}", x.CreationDate);

            string votes = string.Empty;
            if (x.ViewVotes)
                votes = GeneralMethods.CoreRating(x.ViewVotes, true, (double)x.TotalVotes, x.mytrip_storevotes.Count()) + "</b><br/>";
            string mycart = GeneralMethods.Button("/Store/Cart/" + x.ProductId, StoreLanguage.buy, true, "right");
            string _content = "<div id='votes' class='right'>" + votes +
                "<input id='VotesCount' name='VotesCount' type='hidden' value='" + x.mytrip_storevotes.Count() + "' />" +
                "<input id='Store_StoreId' name='Store.StoreId' type='hidden' value='" + x.ProductId + "' />" +
                "</div><h1 class=\"hometitle\" >" +EditAndDeleteProduct(x.ProductId,x.UserName,x.mytrip_storedepartment.UserName,x.mytrip_storedepartment.mytrip_storedepartment2.UserName,x.ProducerId)+
                x.Title + "</h1><table class='noborders'><tr><td>" + ImageForAbstract(x.Image)
                + x.Abstract + "</td></tr></table>" + mycart + prise + "<br/>" + _date + "<br/>" + departmentlink
                + comparision + storecart;
            return new HtmlString(_content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HtmlString ViewOptions(this HtmlHelper html, mytrip_storeproduct x)
        {
            int countfoto = x.mytrip_storeoptions.Count();
            int countreview = x.mytrip_storevotes.Count();
            string options = x.Body.Length == 0 ? string.Empty : GeneralMethods.Button(StoreLanguage.bodyProduct, false, "options", "left");
            string foto = countfoto == 0 ? string.Empty : GeneralMethods.Button(string.Format(StoreLanguage.foto, countfoto), false, "foto", "left");
            string review = countreview == 0 ? string.Empty : GeneralMethods.Button(string.Format(StoreLanguage.reviews, countreview), false, "review", "left");
            string a = "<div class='button'>" + options + foto + review + "</div>";
            string _content = "";
            if (x.Body.Length != 0)
                _content += "<div class='last'></div><div id='_options' class='content'>" + x.Body + "</div>";
            string fotodisplay = x.Body.Length == 0 ? string.Empty : "style='display:none;'";
            if (countfoto > 0)
                _content += "<div class='last'></div><div id='_foto' class='content' " + fotodisplay + ">" + FotoOptions(x) + "</div>";
            string reviewdisplay = (x.Body.Length == 0 && countfoto == 0) ? string.Empty : "style='display:none;'";
            if (countreview > 0)
                _content += "<div class='last'></div><div id='_review' class='content'" + reviewdisplay + ">" + VotesOptions(html, x) + "</div>";
            return new HtmlString(a + _content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static string FotoOptions(mytrip_storeproduct x)
        {
            int _count = x.mytrip_storeoptions.Count();
            int __count = 1;
            StringBuilder result = new StringBuilder();
            result.Append("<table class='noborders'>");
            foreach (var z in x.mytrip_storeoptions)
            {
                if (__count == 1 || __count % 3 == 1)
                    result.Append("<tr>");
                result.Append("<td style='text-align:center;'><b>" + z.Title + "</b><br/>");
                result.Append(ImageForAbstract(z.Image, ModuleSetting.widthImgDepartment()) + "</td>");
                if (__count == _count || __count % 3 == 0)
                    result.Append("</tr>");
                __count++;
            }
            result.Append("</table>");
            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private static string VotesOptions(HtmlHelper html, mytrip_storeproduct x)
        {
            StringBuilder result = new StringBuilder();
            foreach (var z in x.mytrip_storevotes.OrderByDescending(y=>y.CreationDate))
            {
                TagBuilder profile = new TagBuilder("a");
                profile.MergeAttribute("href", "/Home/Profile/" + z.UserName);
                profile.InnerHtml = z.UserName;
                string usname = " " + profile+"  "+z.CreationDate.ToString("dd MMMM yyyy");
                result.Append("<div class='comment' ><table class='noborders'><tr><td>");
                TagBuilder divGravatar = new TagBuilder("a");
                divGravatar.MergeAttribute("href", "/Home/Profile/" + z.UserName);
                divGravatar.InnerHtml = AvatarHelper.Avatar(html, MytripUser.UserEmail(z.UserName), new { width = 40 }).ToString();
                TagBuilder _divGravatar = new TagBuilder("div");
                _divGravatar.MergeAttribute("style", "position: relative;margin-left:2px; float: right");
                _divGravatar.InnerHtml = divGravatar.ToString();
                result.Append(_divGravatar.ToString());
                result.Append(GeneralMethods.CoreRating(true, false, (double)z.Vote, -1) + "<br/>" + usname + "<br/>");
                result.Append(z.Reviews);
                result.Append("</td></tr></table></div><div class='last'></div>");
            }
            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static string ImageForAbstract(string image)
        {
            if (image != null && image.Contains("src"))
            {
                image = image.Remove(0, image.IndexOf("src"));
                image = image.Remove(0, (image.IndexOf("\"") + 1));
                image = image.Remove(image.IndexOf("\""));
                string title = image.Remove(0, (image.LastIndexOf("/") + 1));
                title = title.Remove(title.LastIndexOf("."));
                return string.Format("<img src='{0}' alt='{1}' title='{1}' class='imgabstract2'/>", image, title);
            }
            else
                return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string ImageForAbstract(string image, int width)
        {
            if (image != null && image.Contains("src"))
            {
                image = image.Remove(0, image.IndexOf("src"));
                image = image.Remove(0, (image.IndexOf("\"") + 1));
                image = image.Remove(image.IndexOf("\""));
                string title = image.Remove(0, (image.LastIndexOf("/") + 1));
                title = title.Remove(title.LastIndexOf("."));
                return string.Format("<img src='{0}' alt='{1}' title='{1}' style='width:{2}px;'/>", image, title, width);
            }
            else
                return string.Empty;
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
                int countproduct = article.mytrip_storeproduct.Count();

                string _content = "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\">" + GeneralMethods.ImageForAbstract(article.Image, ModuleSetting.widthImgDepartment()) + "</a>";
                _content += "<b><a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\" class=\"hometitle\" >" +
                    article.Title + " (" + countproduct + ")</a></b><br/>" + article.Body;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString StoreSorting(this HtmlHelper html)
        {
            string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
            string Controller = urlpath[1];
            string Action = urlpath[2];
            string Page = urlpath[3];
            string TotalSize = urlpath[4];
            string Department = urlpath[5];
            string Producer = urlpath[6];
            string Sorting = urlpath[7];
            string Path = urlpath[8];
            string SmallPrice = "x";
            string BigPrice = "x";
            if (urlpath.Length >= 11)
            {
                SmallPrice = urlpath[9];
                BigPrice = urlpath[10];
            }
            string b = "/" + Controller + "/" + Action + "/" + 1 + "/" + TotalSize + "/" + Department + "/" + Producer + "/";
            string a = "/" + Path;
            if (SmallPrice != "x" && BigPrice != "x")
                a += "/" + SmallPrice + "/" + BigPrice;
            string up = "<img src=\"/Theme/" + ThemeSetting.theme() + "/images/uparrow_blue.png\" style=\"width:14px;\" />";
            string down = "<img src=\"/Theme/" + ThemeSetting.theme() + "/images/downarrow_blue.png\" style=\"width:14px;\" />";
            StringBuilder result = new StringBuilder();
            result.AppendLine("<table class=\"noborders\"><tr><td>");
            result.AppendLine(StoreLanguage.Sorted + " </td><td>");
            result.AppendLine(StoreLanguage.date + " </td><td><a href=\"" + b + 5 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 1 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.prise_ + " </td><td><a href=\"" + b + 6 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 2 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.votes + " </td><td><a href=\"" + b + 7 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 3 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.title + " </td><td><a href=\"" + b + 8 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 4 + a + "\">" + down + "</a></td></tr></table>");
            return new HtmlString(result.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="subid"></param>
        /// <param name="search"></param>
        /// <param name="producer"></param>
        /// <returns></returns>
        private static string StoreAddCategoryAndProduct(int id, int count, int subid, bool search, bool producer)
        {
            if (!search && (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || MytripUser.UserInRole(ModuleSetting.roleStoreManager())))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
                img.MergeAttribute("style", "height:14px;");
                if (producer)
                {
                    return "<div class=\"right\"><a href=\"/Store/EditorCategory/0/CreateProducer\">" + StoreLanguage.createProducer + " " + img + "</a></div>";
                }
                else
                {
                    if (subid == 0)
                        subid = id;
                    string a = string.Empty;
                    if (count >= 0)
                    {
                        a = "<br/><a href=\"/Store/EditorCategory/" + subid + "/CreateDepartment\">" + StoreLanguage.createSubDepartment2 + " " + img + "</a>";
                        a += "<br/><a href=\"/Store/EditorProduct/" + id + "/0/CreateProduct\">" + StoreLanguage.createProduct + " " + img + "</a>";
                    }
                    return "<div class=\"right\"><a href=\"/Store/EditorCategory/0/CreateDepartment\">" + StoreLanguage.createDepartment + " " + img + "</a>" + a + "</div>";

                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="subuser"></param>
        /// <param name="producer"></param>
        /// <param name="prodid"></param>
        /// <returns></returns>
        private static string EditAndDeleteCategory(int id, string user, string subuser, bool producer, int prodid)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || (MytripUser.UserInRole(ModuleSetting.roleStoreManager()) && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser)))
            {
                if (producer)
                    id = prodid;
                string edit = producer ? "/EditProducer" : "/EditDepartment";
                string delete = producer ? "/DeleteProducer" : "/DeleteDepartment";
                return GeneralMethods.ImgInput("/images/edite.png", "/Store/EditorCategory/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Store/EditorCategory/" + id + delete, "delete", 14);
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="subuser"></param>
        /// <param name="subuser2"></param>
        /// <param name="prodid"></param>
        /// <returns></returns>
        private static string EditAndDeleteProduct(int id, string user, string subuser, string subuser2, int prodid)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || (MytripUser.UserInRole(ModuleSetting.roleStoreManager())
                && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser || HttpContext.Current.User.Identity.Name == subuser2)))
            {
                string edit = "/" + prodid + "/EditProduct";
                string delete = "/0/DeleteProduct";
                return GeneralMethods.ImgInput("/images/edite.png", "/Store/EditorProduct/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Store/EditorProduct/" + id + delete, "delete", 14);
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MyCart()
        {
            string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["myTripProductCart"].Value;
            if (cart != null)
            {
                IStoreRepository istore = new IStoreRepository();
                cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _cart = cart.Split('|');
                StringBuilder result = new StringBuilder();
                result.Append("<div class='apprTR'/><div class='apprTL'/><div class='apprTC'/><div class='apprC'><div class='righttext'>");
                decimal totalprice = 0;
                int totalcount = 0;
                foreach (string x in _cart)
                {
                    string[] _x = x.Split('_');
                    int id = 0;
                    int.TryParse(_x[1], out id);
                    int count = 0;
                    int.TryParse(_x[2], out count);
                    if (id > 0 && count > 0)
                    {
                        var product = istore.product.GetProduct(id);
                        totalprice += MoneyHelpers.ConvertMoneyDecimal(product.Price, product.Culture) * count;
                        totalcount += count;
                    }

                }
                result.Append(string.Format(StoreLanguage.mycart, "<a href='/Store/Cart'>" + StoreLanguage.mycart2 + "</a>", "<b>" + totalcount + "</b>", "<b>" + MoneyHelpers.ConvertMoney(totalprice, LocalisationSetting.culture()) + "</b>"));
                result.Append("</div></div><div class='apprBR'/><div class='apprBL'/><div class='apprBC'/>");
                return result.ToString();
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ViewMyCart(int? id)
        {
            int _id = id ?? 0;
            string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] == null)
                        ? string.Format("[_{0}_1]", _id)
                        : HttpContext.Current.Request.Cookies["myTripProductCart"].Value;
            if (_id > 0 && !cart.Contains(string.Format("_{0}_", _id)))
            {
                cart += string.Format("[_{0}_1]", _id);
                HttpCookie cookie = new HttpCookie("myTripProductCart", cart);
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else if (_id > 0 && cart == string.Format("[_{0}_1]", _id))
            {
                HttpCookie cookie = new HttpCookie("myTripProductCart", cart);
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            IStoreRepository istore = new IStoreRepository();
            cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
            string[] _cart = cart.Split('|');
            StringBuilder result = new StringBuilder();
            decimal totalprice = 0;
            int totalcount = 0;
            foreach (string x in _cart)
            {
                string[] _x = x.Split('_');
                int __id = 0;
                int count = 0;
                if (_x.Length == 3)
                {
                    int.TryParse(_x[1], out __id);
                    int.TryParse(_x[2], out count);
                }
                if (__id > 0 && count > 0)
                {
                    var product = istore.product.GetProduct(__id);
                    totalprice += MoneyHelpers.ConvertMoneyDecimal(product.Price, product.Culture) * count;
                    totalcount += count;
                    result.Append("<div class='content'>");
                    result.Append("<div class='right'><a href=\"/Store/DeleteProductCart/" + product.ProductId + "\">" + GeneralMethods.Image("/images/delete.png", 0, 14, CoreLanguage.save, 0) + "</a></div>");
                    result.Append("<table class='noborders' style='width:95%;'><tr><td>");
                    result.Append("<a href=\"/Store/View/" + product.ProductId + "/" + product.Path + "\">" + GeneralMethods.ImageForAbstract(product.Image, ModuleSetting.widthImgDepartment()) + "</a>");
                    result.Append("</td><td>");
                    result.Append("<h3 class='title' id='" + product.ProductId + "'><a href=\"/Store/View/" + product.ProductId + "/" + product.Path + "\">" + product.Title + "</a></h3>");
                    result.Append(StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + product.ProducerId + "/1/" + product.mytrip_storeproducer.Path + "\" >" +
                    product.mytrip_storeproducer.Title + "</a><br/>");
                    if (product.mytrip_storedepartment.SubDepartmentId == 0)
                        result.Append(StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + product.DepartmentId + "/0/1/" + product.mytrip_storedepartment.Path + "\" >" +
                            product.mytrip_storedepartment.Title + "</a><br/>");
                    else
                    {
                        result.Append(StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + product.mytrip_storedepartment.SubDepartmentId + "/0/1/" + product.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                                product.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a><br/>");
                        result.Append(StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + product.DepartmentId + "/0/1/" + product.mytrip_storedepartment.Path + "\" >" +
                            product.mytrip_storedepartment.Title + "</a><br/>");
                    }
                    result.Append("</td><td>");
                    result.Append(StoreLanguage.titlePrice + "<br/> <b>" + MoneyHelpers.ConvertMoney(product.Price, product.Culture) + "</b>");
                    result.Append("</td><td>");
                    result.Append(StoreLanguage.totalCount + "<br/>");
                    result.Append("<div class='right' title='" + CoreLanguage.save + "' style='display:none;'>" + GeneralMethods.Image("/images/approved.png", 0, 14, CoreLanguage.save, 0) + "</div>");
                    result.Append("<input name='count" + __id + "' type='text' value='" + count + "' style='width:50px;'/>");

                    result.Append("</td><td>");
                    result.Append(StoreLanguage.amount_of + "<br/> <b>" + MoneyHelpers.ConvertMoney(product.Price * count, product.Culture) + "</b>");
                    result.Append("</td></tr></table></div><div class='last'></div>");

                }
            }
            result.Append("<div class='content'><div class='righttext'>");
            result.Append(string.Format(StoreLanguage.mycart, StoreLanguage.mycart2, "<b>" + totalcount + "</b>", "<b>" + MoneyHelpers.ConvertMoney(totalprice, LocalisationSetting.culture()) + "</b>"));
            result.Append("</div><div class='button'>");
            result.Append(GeneralMethods.Button("/Store/Order", StoreLanguage.buy, true, "right"));
            result.Append("</div></div>");
            return result.ToString();
        }
    }
}
