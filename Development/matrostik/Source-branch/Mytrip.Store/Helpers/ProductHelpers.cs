using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Store.Repository.DataEntities;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Helpers;
using Mytrip.Mvc.Settings;
using System.IO;
using System.Web;

namespace Mytrip.Store.Helpers
{
    public static class ProductHelpers
    {
        private static HtmlString _StoreProduct(IQueryable<mytrip_storeproduct> department, int take, int subdepartment,
            bool producer, bool DepartmentAndProducer, bool DepartmentAndProducer2)
        {int column = 1;
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


            foreach (var article in department)
            {
                string departmentlink = string.Empty;
                if (!producer && !DepartmentAndProducer)
                {
                        departmentlink = StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.mytrip_storeproducer.Path + "\" >" +
                        article.mytrip_storeproducer.Title + "</a><br/>";

                }
                if (producer || DepartmentAndProducer2)
                {
                    if (article.mytrip_storedepartment.SubDepartmentId == 0)
                    {
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                            article.mytrip_storedepartment.Title + "</a><br/>";
                    }
                    else
                    {
                            departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.mytrip_storedepartment.SubDepartmentId + "/0/1/" + article.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                                article.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>";
                        
                            departmentlink += "<br/>" + StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                                 article.mytrip_storedepartment.Title + "</a>";
                    }
                }
                if (!producer && subdepartment == 0 && article.mytrip_storedepartment.SubDepartmentId != 0 && !DepartmentAndProducer2)
                {
                    
                        departmentlink += StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                        article.mytrip_storedepartment.Title + "</a>";
                }
                string saleprice = "";
                int sale1 = 0;
                DateTime saledate = DateTime.Now;
                //Отображение скидки для отдела
                if ((article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storesale.Sale > 0 && article.mytrip_storesale.CloseDate > DateTime.Now))
                {
                    sale1 = (article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                    int sale2 = (article.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storesale.Sale : 0;
                    int sale3 = (article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                    int sale4 = (article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                    saledate = article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate;
                    if (sale2 > sale1)
                    {
                        sale1 = sale2;
                        saledate = article.mytrip_storesale.CloseDate;
                    }
                    if (sale3 > sale1)
                    {
                        sale1 = sale3;
                        saledate = article.mytrip_storedepartment.mytrip_storesale.CloseDate;
                    }
                    if (sale4 > sale1)
                    {
                        sale1 = sale4;
                        saledate = article.mytrip_storeproducer.mytrip_storesale.CloseDate;
                    }
                }
                string decorprice = string.Empty;
                string sale = string.Empty;

                if (sale1 > 0)
                {

                    sale = string.Format(StoreLanguage.saleView, sale1, string.Format("{0:dd MMMM yyyy}", saledate));
                    
                        sale = " <b class='sale'>" + sale + "</b><br/>";
                    decorprice = "style='text-decoration: line-through;'";
                   
                        saleprice = " <b class='sale'>" + MoneyHelpers.ConvertMoney(article.MoneyId,(article.Price / 100) * (100 - sale1)) + "</b>";
                }

                string prise = string.Empty;
                if (article.ViewPrice)
                {
                    
                        prise = StoreLanguage.Prise + " <b " + decorprice + ">" + MoneyHelpers.ConvertMoney(article.MoneyId,article.Price) + "</b>" + saleprice;
                }
                string votes = string.Empty;
                if (article.ViewVotes)
                    votes = GeneralMethods.CoreRating(article.ViewVotes, false, (double)article.TotalVotes, -1) + "</b><br/>";
                string countProduct = string.Empty;
                if (article.ViewCount)
                {
                    
                        countProduct = StoreLanguage.totalCount + ": <b>" + article.TotalCount + "</b><br/>";
                }
                string packing = string.Empty;
                if (article.Packing != null)
                {
                    
                        packing = StoreLanguage.packing + ": <b>" + article.Packing + "</b><br/>";
                }
                string mycart = GeneralMethods.Button("/Store/Cart/" + article.ProductId, StoreLanguage.buy, false, "right");
                if (HttpContext.Current.Request.Cookies["myTripAddAccount"] != null && HttpContext.Current.Request.Cookies["myTripAddAccount"].Value != "0")
                { mycart = GeneralMethods.Button("/Store/AddPosition/" +HttpContext.Current.Request.Cookies["myTripAddAccount"].Value+"/"+ article.ProductId, StoreLanguage.addaccount, false, "right"); }
                string checkeds = (HttpContext.Current.Request.Cookies["myTripProductComparison"] != null
                 && HttpContext.Current.Request.Cookies["myTripProductComparison"].Value.Contains(string.Format("[{0}]", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;

                string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] != null
                    && HttpContext.Current.Request.Cookies["myTripProductCart"].Value.Contains(string.Format("_{0}_", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;
                string _date = string.Format("{0:dd MMMM yyyy}", article.CreationDate) + "<br/>";
                
                string nambercatalog = "";
                if (article.NamberCatalog != null)
                    nambercatalog = article.NamberCatalog + "  ";
                string storecart = "<br/><input " + cart + " type='checkbox' value='_" + article.ProductId
                    + "_1' class='cart' /> " + string.Format(StoreLanguage.cart, "<a href='/Store/Cart/" + article.ProductId + "'>" + StoreLanguage.cart2 + "</a>");
                string comparision = "<br/><input " + checkeds + " type='checkbox' value='" + article.ProductId
                    + "' class='comparision' /> " + string.Format(StoreLanguage.compare, "<a href='/Store/View/0/Comparision'>" + StoreLanguage.compare2 + "</a>");
                string _content = "<table class='noborders'><tr><td>";
                _content += "<b><a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\" class=\"hometitle\" >" +
                   nambercatalog + article.Title + "</a></b><br/>" + departmentlink + votes + "</td><td style='width:25%;'>" + prise + storecart + "</td><td style='width:10%;'>" + mycart;
                _content += "</td></tr></table>";


                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, 1, tr, width, _content,
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

            if (ModuleSetting.viewProduktTable() && !compare)
            { 
            return _StoreProduct(department, take, subdepartment,
            producer, DepartmentAndProducer, DepartmentAndProducer2);
            }

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
            bool countcompare = false;
            bool packingcompare = false;
            bool salecompare = false;
            bool salepricecompare = false;
            if (compare)
            {
                string _pricecompare = "";
                string _producercompare = "";
                string _departmentcompare = "";
                string _subdepartmentcompare = "";
                string _datecompare = "";
                string _countcompare = "";
                string _packingcompare = "";
                string _salecompare = "";
                string _salepricecompare = "";
                int sale1 = 0;
                foreach (var article in department)
                {

                    //Отображение скидки для отдела
                    if ((article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                        article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (article.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                        article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (article.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                        article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (article.mytrip_storesale.Sale > 0 && article.mytrip_storesale.CloseDate > DateTime.Now))
                    {
                        sale1 = (article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                        int sale2 = (article.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storesale.Sale : 0;
                        int sale3 = (article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                        int sale4 = (article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                        if (sale2 > sale1)
                            sale1 = sale2;
                        if (sale3 > sale1)
                            sale1 = sale3;
                        if (sale4 > sale1)
                            sale1 = sale4;

                    }
                    if (!_salecompare.Contains(string.Format("[{0}]", sale1)))
                    {
                        salecompare = true;
                        _salecompare += string.Format("[{0}]", sale1);
                    }
                    if (salecompare && !_salecompare.Contains("]["))
                        salecompare = false;
                    //saleprice
                    if (!_salepricecompare.Contains(string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.MoneyId,(article.Price / 100) * (100 - sale1)))))
                    {
                        salepricecompare = true;
                        _salepricecompare += string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.MoneyId,(article.Price / 100) * (100 - sale1)));
                    }
                    if (salepricecompare && !_salepricecompare.Contains("]["))
                        salepricecompare = false;
                    //price
                    if (!_pricecompare.Contains(string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.MoneyId,article.Price))))
                    {
                        pricecompare = true;
                        _pricecompare += string.Format("[{0}]", MoneyHelpers.ConvertMoney(article.MoneyId,article.Price));
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
                    //count
                    if (!_countcompare.Contains(string.Format("[{0}]", article.TotalCount)))
                    {
                        countcompare = true;
                        _countcompare += string.Format("[{0}]", article.TotalCount);
                    }
                    if (countcompare && !_countcompare.Contains("]["))
                        countcompare = false;
                    //packing
                    if (!_packingcompare.Contains(string.Format("[{0}]", article.Packing)))
                    {
                        packingcompare = true;
                        _packingcompare += string.Format("[{0}]", article.Packing);
                    }
                    if (packingcompare && !_packingcompare.Contains("]["))
                        packingcompare = false;
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
                string saleprice = "<br/>";
                int sale1 = 0;
                DateTime saledate = DateTime.Now;
                //Отображение скидки для отдела
                if ((article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                    article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                    (article.mytrip_storesale.Sale > 0 && article.mytrip_storesale.CloseDate > DateTime.Now))
                {
                    sale1 = (article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                    int sale2 = (article.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storesale.Sale : 0;
                    int sale3 = (article.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                    int sale4 = (article.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? article.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                    saledate = article.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate;
                    if (sale2 > sale1)
                    {
                        sale1 = sale2;
                        saledate = article.mytrip_storesale.CloseDate;
                    }
                    if (sale3 > sale1)
                    {
                        sale1 = sale3;
                        saledate = article.mytrip_storedepartment.mytrip_storesale.CloseDate;
                    }
                    if (sale4 > sale1)
                    {
                        sale1 = sale4;
                        saledate = article.mytrip_storeproducer.mytrip_storesale.CloseDate;
                    }
                }
                string decorprice = string.Empty;
                string sale = string.Empty;

                if (sale1 > 0)
                {

                    sale = string.Format(StoreLanguage.saleView, sale1, string.Format("{0:dd MMMM yyyy}", saledate));
                    if (salecompare)
                        sale = " <b>" + a + sale + b + "</b><br/>";
                    else
                        sale = " <b class='sale'>" + sale + "</b><br/>";
                    decorprice = "style='text-decoration: line-through;'";
                    if (salepricecompare)
                        saleprice = " <b>" + a + MoneyHelpers.ConvertMoney(article.MoneyId,(article.Price / 100) * (100 - sale1)) + b + "</b><br/>";
                    else
                        saleprice = " <b class='sale'>" + MoneyHelpers.ConvertMoney(article.MoneyId,(article.Price / 100) * (100 - sale1)) + "</b><br/>";
                }

                string prise = string.Empty;
                if (article.ViewPrice)
                {
                    if (pricecompare)
                        prise = StoreLanguage.Prise + " <b " + decorprice + ">" + a + MoneyHelpers.ConvertMoney(article.MoneyId,article.Price) + b + "</b>" + saleprice + sale;
                    else
                        prise = StoreLanguage.Prise + " <b " + decorprice + ">" + MoneyHelpers.ConvertMoney(article.MoneyId,article.Price) + "</b>" + saleprice + sale;
                }
                string votes = string.Empty;
                if (article.ViewVotes)
                    votes = GeneralMethods.CoreRating(article.ViewVotes, false, (double)article.TotalVotes, -1) + "</b><br/>";
                string countProduct = string.Empty;
                if (article.ViewCount)
                {
                    if (countcompare)
                        countProduct = StoreLanguage.totalCount + ": <b>" + a + article.TotalCount + b + "</b><br/>";
                    else
                        countProduct = StoreLanguage.totalCount + ": <b>" + article.TotalCount + "</b><br/>";
                }
                string packing = string.Empty;
                if (article.Packing != null)
                {
                    if (packingcompare)
                        packing = StoreLanguage.packing + ": <b>" + a + article.Packing + b + "</b><br/>";
                    else
                        packing = StoreLanguage.packing + ": <b>" + article.Packing + "</b><br/>";
                }
                string mycart = GeneralMethods.Button("/Store/Cart/" + article.ProductId, StoreLanguage.buy, false, "right");
                if (HttpContext.Current.Request.Cookies["myTripAddAccount"] != null && HttpContext.Current.Request.Cookies["myTripAddAccount"].Value != "0")
                { mycart = GeneralMethods.Button("/Store/AddPosition/" +HttpContext.Current.Request.Cookies["myTripAddAccount"].Value+"/"+ article.ProductId, StoreLanguage.addaccount, false, "right"); }
                string checkeds = (HttpContext.Current.Request.Cookies["myTripProductComparison"] != null
                 && HttpContext.Current.Request.Cookies["myTripProductComparison"].Value.Contains(string.Format("[{0}]", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;

                string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] != null
                    && HttpContext.Current.Request.Cookies["myTripProductCart"].Value.Contains(string.Format("_{0}_", article.ProductId)))
                ? "checked=\"checked\""
                : string.Empty;
                string _date = string.Format("{0:dd MMMM yyyy}", article.CreationDate) + "<br/>";
                if (datecompare)
                    _date = a + _date + b;
                string nambercatalog = "";
                if (article.NamberCatalog != null)
                    nambercatalog = " (" + article.NamberCatalog + ")";
                string storecart = "<br/><input " + cart + " type='checkbox' value='_" + article.ProductId
                    + "_1' class='cart' /> " + string.Format(StoreLanguage.cart, "<a href='/Store/Cart/" + article.ProductId + "'>" + StoreLanguage.cart2 + "</a>");
                string comparision = "<br/><input " + checkeds + " type='checkbox' value='" + article.ProductId
                    + "' class='comparision' /> " + string.Format(StoreLanguage.compare, "<a href='/Store/View/0/Comparision'>" + StoreLanguage.compare2 + "</a>");
                string _content = "<table class='noborders'><tr><td><a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\">" + StoreHelper.GetImageProduct(article.ProductId, ModuleSetting.widthImgDepartment()) + "</a>";
                _content += "<b><a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\" class=\"hometitle\" >" +
                   article.Title + nambercatalog + "</a></b><br/>" + article.Body + "</td></tr></table>" + mycart + votes + prise + packing + countProduct + _date + departmentlink
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
            string countProduct = string.Empty;
            if (x.ViewCount)
            {
                countProduct = StoreLanguage.totalCount + ": <b>" + x.TotalCount + "</b><br/>";
            }
            string packing = string.Empty;
            if (x.Packing != null)
            {
                packing = StoreLanguage.packing + ": <b>" + x.Packing + "</b><br/>";
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
            string saleprice = "<br/>";
            int sale1 = 0;
            DateTime saledate = DateTime.Now;
            //Отображение скидки для отдела
            if ((x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                x.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                x.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storesale.Sale > 0 && x.mytrip_storesale.CloseDate > DateTime.Now))
            {
                sale1 = (x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                int sale2 = (x.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storesale.Sale : 0;
                int sale3 = (x.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                int sale4 = (x.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                saledate = x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate;
                if (sale2 > sale1)
                {
                    sale1 = sale2;
                    saledate = x.mytrip_storesale.CloseDate;
                }
                if (sale3 > sale1)
                {
                    sale1 = sale3;
                    saledate = x.mytrip_storedepartment.mytrip_storesale.CloseDate;
                }
                if (sale4 > sale1)
                {
                    sale1 = sale4;
                    saledate = x.mytrip_storeproducer.mytrip_storesale.CloseDate;
                }
            }
            string decorprice = string.Empty;
            string sale = string.Empty;

            if (sale1 > 0)
            {

                sale = string.Format(StoreLanguage.saleView, sale1, string.Format("{0:dd MMMM yyyy}", saledate));

                sale = " <b class='sale'>" + sale + "</b><br/>";
                decorprice = "style='text-decoration: line-through;'";

                saleprice = " <b class='sale'>" + MoneyHelpers.ConvertMoney(x.MoneyId,(x.Price / 100) * (100 - sale1)) + "</b><br/>";
            }

            string prise = string.Empty;
            if (x.ViewPrice)
                prise = StoreLanguage.Prise + " <b " + decorprice + ">" + MoneyHelpers.ConvertMoney(x.MoneyId,x.Price) + "</b>" + saleprice + sale;
            string _date = string.Format("{0:dd MMMM yyyy}", x.CreationDate) + "<br/>";
            string nambercatalog="";
            if (x.NamberCatalog != null)
                nambercatalog = " (" + x.NamberCatalog + ")";
            string votes = string.Empty;
            if (x.ViewVotes)
                votes = GeneralMethods.CoreRating(x.ViewVotes, true, (double)x.TotalVotes, x.mytrip_storevotes.Count()) + "</b><br/>";
            string mycart = GeneralMethods.Button("/Store/Cart/" + x.ProductId, StoreLanguage.buy, true, "right");
            if (HttpContext.Current.Request.Cookies["myTripAddAccount"] != null && HttpContext.Current.Request.Cookies["myTripAddAccount"].Value != "0")
            { mycart = GeneralMethods.Button("/Store/AddPosition/" + HttpContext.Current.Request.Cookies["myTripAddAccount"].Value + "/" + x.ProductId, StoreLanguage.addaccount, false, "right"); }
            string _content = "<div id='votes' class='right'>" + votes +
                "<input id='VotesCount' name='VotesCount' type='hidden' value='" + x.mytrip_storevotes.Count() + "' />" +
                "<input id='Store_StoreId' name='Store.StoreId' type='hidden' value='" + x.ProductId + "' />" +
                "</div><h1 class=\"hometitle\" >" + StoreHelper.EditAndDeleteProduct(x.ProductId, x.UserName, x.mytrip_storedepartment.UserName, x.mytrip_storedepartment.mytrip_storedepartment2.UserName, x.ProducerId) +
                x.Title + nambercatalog + "</h1><table class='noborders'><tr><td>" + StoreHelper.GetImageProduct(x.ProductId)
                + x.Body + "</td></tr></table>" + mycart + prise + packing + countProduct + _date + departmentlink
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
            int countfoto = 0;
            string _foto = FotoOptions(x, out countfoto);
            int countreview = x.mytrip_storevotes.Count();
            string options = x.Details.Length == 0 ? string.Empty : GeneralMethods.Tab(StoreLanguage.bodyProduct, "options");
            string foto = countfoto == 0 ? string.Empty : GeneralMethods.Tab(string.Format(StoreLanguage.foto, countfoto), "foto");
            string review = countreview == 0 ? string.Empty : GeneralMethods.Tab(string.Format(StoreLanguage.reviews, countreview), "review");
            string a = "<div class='button'>" + options + foto + review + "</div>";
            string _content = "";
            if (x.Details.Length != 0)
                _content += "<div id='_options' class='tabc'>" + x.Details + "</div>";
            string fotodisplay = x.Details.Length == 0 ? string.Empty : "style='display:none;'";
            if (countfoto > 0)
                _content += "<div id='_foto' class='tabc' " + fotodisplay + ">" + _foto + "</div>";
            string reviewdisplay = (x.Details.Length == 0 && countfoto == 0) ? string.Empty : "style='display:none;'";
            if (countreview > 0)
                _content += "<div id='_review' class='tabc'" + reviewdisplay + ">" + VotesOptions(html, x) + "</div>";
            return new HtmlString(a + _content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="countfoto"></param>
        /// <returns></returns>
        private static string FotoOptions(mytrip_storeproduct x, out int countfoto)
        {
            bool tr = false;
            int __count = 1;
            StringBuilder result = new StringBuilder();
            result.Append("<table class='noborders'>");
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Product/" + x.ProductId);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] _result = _absolutDirectory2.GetFiles();
            foreach (var _x in _result)
            {
                if (!_x.Name.Contains("product"))
                {
                    if (__count == 1 || __count % 3 == 1)
                    { result.Append("<tr>"); tr = true; }
                    result.Append("<td style='text-align:center;'>");
                    result.Append("<img src='/Content/Store/Product/" + x.ProductId + "/" + _x.Name + "'style='width:" + ModuleSetting.widthImgProduct() + "px'/></td>");
                    if (__count % 3 == 0)
                    {
                        tr = false;
                        result.Append("</tr>");
                    }
                    __count++;
                }
            }
            if (tr)
                result.Append("</tr>");
            result.Append("</table>");
            countfoto = __count - 1;
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
            foreach (var z in x.mytrip_storevotes.OrderByDescending(y => y.CreationDate))
            {
                TagBuilder profile = new TagBuilder("a");
                profile.MergeAttribute("href", "/Home/Profile/" + z.UserName);
                profile.InnerHtml = z.UserName;
                string usname = " " + profile + "  " + z.CreationDate.ToString("dd MMMM yyyy");
                result.Append("<div class='comment' ><table class='noborders'><tr><td>");
                if (UsersSetting.unlockGravatar())
                {
                    TagBuilder divGravatar = new TagBuilder("a");
                    divGravatar.MergeAttribute("href", "/Home/Profile/" + z.UserName);
                    divGravatar.InnerHtml = AvatarHelper.Avatar(html, MytripUser.UserEmail(z.UserName), new { width = 40 }).ToString();
                    TagBuilder _divGravatar = new TagBuilder("div");
                    _divGravatar.MergeAttribute("style", "position: relative;margin-left:2px; float: right");
                    _divGravatar.InnerHtml = divGravatar.ToString();
                    result.Append(_divGravatar.ToString());
                }
                result.Append(GeneralMethods.CoreRating(true, false, (double)z.Vote, -1) + "<br/>" + usname + "<br/>");
                result.Append(z.Reviews);
                result.Append("</td></tr></table></div><div class='last'></div>");
            }
            return result.ToString();
        }
        public static string ViewProduct(mytrip_storeproduct x,int content,int imgwidth)
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
            string countProduct = string.Empty;
            if (x.ViewCount)
            {
                countProduct = StoreLanguage.totalCount + ": <b>" + x.TotalCount + "</b><br/>";
            }
            string packing = string.Empty;
            if (x.Packing != null)
            {
                packing = StoreLanguage.packing + ": <b>" + x.Packing + "</b><br/>";
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
            string saleprice = "<br/>";
            int sale1 = 0;
            DateTime saledate = DateTime.Now;
            //Отображение скидки для отдела
            if ((x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                x.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                x.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                (x.mytrip_storesale.Sale > 0 && x.mytrip_storesale.CloseDate > DateTime.Now))
            {
                sale1 = (x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                int sale2 = (x.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storesale.Sale : 0;
                int sale3 = (x.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                int sale4 = (x.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? x.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                saledate = x.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate;
                if (sale2 > sale1)
                {
                    sale1 = sale2;
                    saledate = x.mytrip_storesale.CloseDate;
                }
                if (sale3 > sale1)
                {
                    sale1 = sale3;
                    saledate = x.mytrip_storedepartment.mytrip_storesale.CloseDate;
                }
                if (sale4 > sale1)
                {
                    sale1 = sale4;
                    saledate = x.mytrip_storeproducer.mytrip_storesale.CloseDate;
                }
            }
            string decorprice = string.Empty;
            string sale = string.Empty;

            if (sale1 > 0)
            {

                sale = string.Format(StoreLanguage.saleView, sale1, string.Format("{0:dd MMMM yyyy}", saledate));

                sale = " <b class='sale'>" + sale + "</b><br/>";
                decorprice = "style='text-decoration: line-through;'";

                saleprice = " <b class='sale'>" + MoneyHelpers.ConvertMoney(x.MoneyId, (x.Price / 100) * (100 - sale1)) + "</b><br/>";
            }

            string prise = string.Empty;
            if (x.ViewPrice)
                prise = StoreLanguage.Prise + " <b " + decorprice + ">" + MoneyHelpers.ConvertMoney(x.MoneyId, x.Price) + "</b>" + saleprice + sale;
            string _date = string.Format("{0:dd MMMM yyyy}", x.CreationDate) + "<br/>";
            string nambercatalog = "";
            if (x.NamberCatalog != null)
                nambercatalog = " (" + x.NamberCatalog + ")";
            string votes = string.Empty;
            string body = x.Body.Length > content ? x.Body.Remove(content) + "..." : x.Body;
            if (x.ViewVotes)
                votes = GeneralMethods.CoreRating(x.ViewVotes, false, (double)x.TotalVotes, -1) + "</b><br/>";
            string mycart = GeneralMethods.Button("/Store/Cart/" + x.ProductId, StoreLanguage.buy, true, "right");
            if (HttpContext.Current.Request.Cookies["myTripAddAccount"] != null && HttpContext.Current.Request.Cookies["myTripAddAccount"].Value != "0")
            { mycart = GeneralMethods.Button("/Store/AddPosition/" + HttpContext.Current.Request.Cookies["myTripAddAccount"].Value + "/" + x.ProductId, StoreLanguage.addaccount, false, "right"); }
            string _content = "<div id='votes' class='right'>" + votes +
                "</div>" + StoreHelper.GetImageProduct(x.ProductId, imgwidth) +
                "<h3 class=\"hometitle\" ><a href=\"/Store/View/" + x.ProductId + "/" + x.Path + "\" class=\"hometitle\" >" +
                   x.Title + nambercatalog + "</a></h3><table class='noborders'><tr><td>"
                + body + "</td></tr></table>" + mycart + prise + departmentlink;
            return _content;
        }
    }
}
