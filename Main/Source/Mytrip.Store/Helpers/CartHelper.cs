using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Mvc.Settings;
using Mytrip.Store.Repository.DataEntities;
using System.Web.Mvc;

namespace Mytrip.Store.Helpers
{
    public static class CartHelper
    {
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
                    if (_x.Count() == 3)
                    {
                        int id = 0;
                        int.TryParse(_x[1], out id);
                        int count = 0;
                        int.TryParse(_x[2], out count);
                        if (id > 0 && count > 0)
                        {
                            var product = istore.product.GetProduct(id);
                            if(product!=null){
                            int sale1 = 0;
                            //Отображение скидки для отдела
                            if ((product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                                product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                                (product.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                                product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                                (product.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                                product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                                (product.mytrip_storesale.Sale > 0 && product.mytrip_storesale.CloseDate > DateTime.Now))
                            {
                                sale1 = (product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                                int sale2 = (product.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storesale.Sale : 0;
                                int sale3 = (product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                                int sale4 = (product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                                if (sale2 > sale1)
                                {
                                    sale1 = sale2;
                                }
                                if (sale3 > sale1)
                                {
                                    sale1 = sale3;
                                }
                                if (sale4 > sale1)
                                {
                                    sale1 = sale4;
                                }
                            }

                            if (sale1 > 0)
                            {
                                totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId,(product.Price / 100) * (100 - sale1)) * count;

                            }
                            else
                                totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId,product.Price) * count;
                            totalcount += count;}
                        }
                    }

                }
                if (totalcount > 0)
                {
                    result.Append(string.Format(StoreLanguage.mycart, "<a href='/Store/Cart'>" + StoreLanguage.mycart2 + "</a>", "<b>" + totalcount + "</b>", "<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney(), totalprice) + "</b>"));
                    result.Append("</div></div><div class='apprBR'/><div class='apprBL'/><div class='apprBC'/>");
                    return result.ToString();
                }
                else return null;
            }
            else
                return null;
        }
        public static string MyCartLayout()
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
                result.Append("<div class='righttext'><b>");
                decimal totalprice = 0;
                int totalcount = 0;
                foreach (string x in _cart)
                {
                    string[] _x = x.Split('_');
                    if (_x.Count() == 3)
                    {
                        int id = 0;
                        int.TryParse(_x[1], out id);
                        int count = 0;
                        int.TryParse(_x[2], out count);
                        if (id > 0 && count > 0)
                        {
                            var product = istore.product.GetProduct(id);
                            if (product != null)
                            {
                                int sale1 = 0;
                                //Отображение скидки для отдела
                                if ((product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                                    product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                                    (product.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                                    product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                                    (product.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                                    product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                                    (product.mytrip_storesale.Sale > 0 && product.mytrip_storesale.CloseDate > DateTime.Now))
                                {
                                    sale1 = (product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                                    int sale2 = (product.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storesale.Sale : 0;
                                    int sale3 = (product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                                    int sale4 = (product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                                    if (sale2 > sale1)
                                    {
                                        sale1 = sale2;
                                    }
                                    if (sale3 > sale1)
                                    {
                                        sale1 = sale3;
                                    }
                                    if (sale4 > sale1)
                                    {
                                        sale1 = sale4;
                                    }
                                }

                                if (sale1 > 0)
                                {
                                    totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId, (product.Price / 100) * (100 - sale1)) * count;

                                }
                                else
                                    totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId, product.Price) * count;
                                totalcount += count;
                            }
                        }
                    }

                }
                if (totalcount > 0)
                {
                    result.Append(string.Format(StoreLanguage.mycart, "<a href='/Store/Cart'>" + StoreLanguage.mycart2 + "</a>", "<b>" + totalcount + "</b>", "<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney(), totalprice) + "</b>"));
                    result.Append("</b></div>");
                    return result.ToString();
                }
                else {
                    result.Append(string.Format(StoreLanguage.mycart, "<a href='/Store/Cart'>" + StoreLanguage.mycart2 + "</a>", "<b>" + totalcount + "</b>", "<b>" + 0 + ".</b>"));
                    result.Append("</b></div>");
                    return result.ToString();
                }
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
            bool buy = false;
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
                    if(product!=null){
                    string saleprice = "<br/>";
                    int sale1 = 0;
                    //Отображение скидки для продукта
                    if ((product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                        product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (product.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                        product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (product.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                        product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                        (product.mytrip_storesale.Sale > 0 && product.mytrip_storesale.CloseDate > DateTime.Now))
                    {
                        sale1 = (product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                        int sale2 = (product.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storesale.Sale : 0;
                        int sale3 = (product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                        int sale4 = (product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                        if (sale2 > sale1)
                        {
                            sale1 = sale2;
                        }
                        if (sale3 > sale1)
                        {
                            sale1 = sale3;
                        }
                        if (sale4 > sale1)
                        {
                            sale1 = sale4;
                        }
                    }
                    string decorprice = string.Empty;

                    if (sale1 > 0)
                    {
                        totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId,(product.Price / 100) * (100 - sale1)) * count;

                        decorprice = "style='text-decoration: line-through;'";

                        saleprice = " <b class='sale'>" + MoneyHelpers.ConvertMoney(product.MoneyId,(product.Price / 100) * (100 - sale1)) + "</b><br/>";
                    }
                    else
                    totalprice += MoneyHelpers.ConvertMoneyDecimal(product.MoneyId,product.Price) * count;
                    totalcount += count;
                    string nambercatalog = "";
                    if (product.NamberCatalog != null)
                        nambercatalog = " (#" + product.NamberCatalog + ")";
                    result.Append("<div class='content'>");
                    result.Append("<div class='_right'>" + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteProductCart/" + product.ProductId, "delete", 14) + "</div>");
                    
                    result.Append("<table class='noborders' style='width:95%;'><tr><td>");
                    result.Append("<a href=\"/Store/View/" + product.ProductId + "/" + product.Path + "\">" + StoreHelper.GetImageProduct(product.ProductId, ModuleSetting.widthImgDepartment()) + "</a>");
                    result.Append("</td><td>");
                    result.Append("<h3 class='title' id='" + product.ProductId + "'><a href=\"/Store/View/" + product.ProductId + "/" + product.Path + "\">" + product.Title +nambercatalog+ "</a></h3>");
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
                    result.Append("</td><td style='width:80px;'>");
                    result.Append(StoreLanguage.titlePrice + "<br/> <b " + decorprice + ">" + MoneyHelpers.ConvertMoney(product.MoneyId,product.Price) + "</b>" + saleprice);
                    result.Append("</td><td style='width:100px;'>");
                    result.Append(StoreLanguage.totalCount + "<br/>");
                    result.Append("<div class='right' title='" + CoreLanguage.save + "' style='display:none;'>" + GeneralMethods.Image("/images/approved.png", 0, 14, CoreLanguage.save, 0) + "</div>");
                    result.Append("<input name='count" + __id + "' type='text' value='" + count + "' style='width:50px;'/>");

                    result.Append("</td><td style='width:80px;'>");
                    if (sale1 > 0)
                        result.Append(StoreLanguage.amount_of + "<br/> <b>" + MoneyHelpers.ConvertMoney(product.MoneyId,((product.Price / 100) * (100 - sale1)) * count) + "</b>");
                    else
                        result.Append(StoreLanguage.amount_of + "<br/> <b>" + MoneyHelpers.ConvertMoney(product.MoneyId,product.Price * count) + "</b>");

                    result.Append("</td></tr></table></div><div class='last'></div>");
                    buy = true;}
                }
            }
            if (totalcount > 0)
            {
                result.Append("<div class='righttext'>");
                result.Append(string.Format(StoreLanguage.mycart, StoreLanguage.mycart2, "<b>" + totalcount + "</b>", "<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney(), totalprice) + "</b>"));
                if (buy)
                {

                    result.Append("</div><div class='button'>");
                    if (ModuleSetting.organizationBuy() && ModuleSetting.privatepersonBuy())
                        result.Append(GeneralMethods.Button(StoreLanguage.buy, true, "variant", "right"));
                    else
                        result.Append(GeneralMethods.Button(StoreLanguage.buy, true, "order", "right"));
                }
                result.Append("</div>");
            }
            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString GetOrders(this HtmlHelper html)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder _result = new StringBuilder();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                IStoreRepository store = new IStoreRepository();
                var orders0 = store.managerorder.GetOrdersForUser(HttpContext.Current.User.Identity.Name, 0);
                var orders1 = store.managerorder.GetOrdersForUser(HttpContext.Current.User.Identity.Name, 1);
                var orders2 = store.managerorder.GetOrdersForUser(HttpContext.Current.User.Identity.Name, 2);
                var orders3 = store.managerorder.GetOrdersForUser(HttpContext.Current.User.Identity.Name, 3);
                var orders4 = store.managerorder.GetOrdersForUserApproved(HttpContext.Current.User.Identity.Name);
                string _orders0 = (orders0.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_0 + " (" + orders0.Count() + ")", "orders0") : "";
                string _orders1 = (orders1.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_1 + " (" + orders1.Count() + ")", "orders1") : "";
                string _orders2 = (orders2.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_2 + " (" + orders2.Count() + ")", "orders2") : "";
                string _orders3 = (orders3.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_3 + " (" + orders2.Count() + ")", "orders3") : "";
                string _orders4 = (orders4.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_4 + " (" + orders4.Count() + ")", "orders4") : "";
                string display = "";
                bool order = true;
                if (orders0.Count() != 0)
                {
                    _result.Append(_GetOrders(orders0, display, "_orders0"));
                    display = "style='display:none;'"; order = false;
                }
                if (orders1.Count() != 0)
                {
                    _result.Append(_GetOrders(orders1, display, "_orders1"));
                    display = "style='display:none;'"; order = false;
                }
                if (orders2.Count() != 0)
                {
                    _result.Append(_GetOrders(orders2, display, "_orders2"));
                    display = "style='display:none;'"; order = false;
                }
                if (orders3.Count() != 0)
                {
                    _result.Append(_GetOrders(orders3, display, "_orders3"));
                    display = "style='display:none;'"; order = false;
                }
                if (orders4.Count() != 0)
                {
                    _result.Append(_GetOrders(orders4, display, "_orders4"));
                    display = "style='display:none;'"; order = false;
                }
                if (order)
                {
                    result.Append("<div class='content'>");
                    result.Append(StoreLanguage.ordernull);
                    result.Append("</div>");
                }
                else
                {
                    result.Append("<div class='button'>" + _orders0 + _orders1 + _orders2 + _orders3 +_orders4+ "</div>");
                    result.Append(_result.ToString());
                }  
                
            }
            else
            {
                string logon = "<a href='/Account/LogOn?returnUrl=/Store/Cart' class='error'>" + StoreLanguage.logon + "</a>";
                string register = "<a href='/Account/Register?returnUrl=/Store/Cart' class='error'>" + StoreLanguage.register + "</a>"; ;
                result.Append("<div class='content'>");
                result.Append("<div class='noappr'>");
                result.Append("<span class='error'>" + string.Format(StoreLanguage.myorder_anonym, logon, register) + "</span>");
                result.Append("</div>");
                result.Append("</div>");
            }
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="neworders"></param>
        /// <param name="display"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string _GetOrders(IQueryable<mytrip_storeorder> neworders, string display, string id)
        {
            IStoreRepository store = new IStoreRepository();
            StringBuilder result = new StringBuilder();
            if (neworders != null)
            {
                result.Append("<div id='" + id + "' class='tabc' " + display + " >");
                foreach (var x in neworders)
                {
                    if (x.Status == 0 && x.mytrip_storeprofile.IsAnonym == true)
                    {
                        result.Append("<div class='content'>");
                        result.Append("<div class='_right'>" +
                           GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteOrder/" + x.OrderId, "delete", 14) + "</div>");
                       
                        result.Append("#");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("<div class='right'>");
                        result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_0);
                        result.Append("</span>");
                        result.Append("</div>");
                    }
                    else if (x.Status == 1 && x.mytrip_storeprofile.IsAnonym == true)
                    {
                        result.Append("<div class='content'>");
                        result.Append("<div class='_right'>" +
                           GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteOrder/" + x.OrderId + "/status1", "delete", 14) + "</div>");
                       
                        result.Append("#");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("<div class='right'>");
                        result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_1);
                        result.Append("</span>");
                        result.Append("</div>");
                    }
                    else if (x.Status == 2 && x.mytrip_storeprofile.IsAnonym == true)
                    {
                        result.Append("<div class='appr'>");
                        result.Append("<div class='_right'>" +
                           GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteOrder/" + x.OrderId + "/status2", "delete", 14) + "</div>");
                       
                        result.Append("#");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("<div class='right'>");
                        //result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_2);
                        //result.Append("</span>");
                        result.Append("</div>");
                    }
                    else if (x.Status == 3 && x.mytrip_storeprofile.IsAnonym == true)
                    {
                        result.Append("<div class='appr'>");
                        result.Append("<div class='_right'>" +
                           GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteOrder/" + x.OrderId + "/status3", "delete", 14) + "</div>");
                       
                        result.Append("#");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("<div class='right'>");
                        //result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_3);
                        //result.Append("</span>");
                        result.Append("</div>");
                    }
                    else if (x.mytrip_storeprofile.IsAnonym == false)
                    {
                        result.Append("<div class='noappr'>");
                        result.Append("<div class='_right'>" + GeneralMethods.ImgInput("/images/approved.png", "/Store/ApprovedOrder/" + x.OrderId + "/status4", "rename", 14) +
                            GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteOrder/" + x.OrderId + "/status4", "delete", 14) + "</div>");
                        result.Append("#");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("<div class='right'>");
                        result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_4);
                        result.Append("</span>");
                        result.Append("</div>");
                    }
                    result.Append("<div class='dark'>");
                    result.Append("<table class='noborders'>");
                    decimal totalprice = 0;
                    foreach (var y in store.order.GetOrdersForUser(x.OrderId))
                    {
                        string nambercatalog = "";
                        if (y.mytrip_storeproduct.NamberCatalog != null)
                            nambercatalog = " (#" + y.mytrip_storeproduct.NamberCatalog + ")";
                        result.Append("<tr>");
                        result.Append("<td>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("<a href=\"/Store/View/" + y.ProductId + "/" + y.mytrip_storeproduct.Path + "\">" + y.mytrip_storeproduct.Title +nambercatalog+ "</a>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append(y.Count + " * " + MoneyHelpers.ConvertMoney(y.MoneyId,y.Price));
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append(MoneyHelpers.ConvertMoney(y.MoneyId,y.Price * y.Count));
                        result.Append("</td>");
                        result.Append("</tr>");
                        totalprice += MoneyHelpers.ConvertMoneyDecimal(y.MoneyId,y.Price) * y.Count;
                    }
                    result.Append("<tr>");
                    result.Append("<td>");
                    result.Append("</td>");
                    result.Append("<td>");
                    result.Append("</td>");
                    result.Append("<td class='topborder'>");
                    result.Append(StoreLanguage.Total);
                    result.Append("</td>");
                    result.Append("<td class='topborder'>");
                    result.Append(MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney(),totalprice));
                    result.Append("</td>");
                    result.Append("</tr>");
                    if (x.Delivery > 0)
                    {
                        decimal delivery = MoneyHelpers.ConvertMoneyDecimal(x.MoneyId,x.Delivery);
                        result.Append("<tr>");
                        result.Append("<td>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append(StoreLanguage.Delivery);
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append(MoneyHelpers.ConvertMoney(x.MoneyId,x.Delivery));
                        result.Append("</td>");
                        result.Append("</tr>");
                        result.Append("<tr>");
                        result.Append("<td>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("</td>");
                        result.Append("<td class='topborder'>");
                        result.Append(StoreLanguage.Totaldelivery);
                        result.Append("</td>");
                        result.Append("<td class='topborder'>");
                        result.Append(MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney(),totalprice + delivery));
                        result.Append("</td>");
                        result.Append("</tr>");
                    }
                    result.Append("</table></div>");
                    result.Append("</div><div class='last'></div>");
                }
                result.Append("</div>");
            }

            return result.ToString();
        }
    }
}
