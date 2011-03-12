using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Settings;
using mtm.Store.Repository.DataEntities;
using mtm.Store.Repository;
using System.Web;
using System.Web.Mvc;
using mtm.Core;

namespace mtm.Store.Helpers
{
    public static class ManagerOrdersHelpers
    {
        /// <summary>
        /// Order Manager Tab Control
        /// </summary>
        /// <param name="html"></param>
        /// <returns>HtmlString</returns>
        public static HtmlString GetOrdersManager(this HtmlHelper html)
        {
            StringBuilder result = new StringBuilder();
            IStoreRepository store = new IStoreRepository();
            var orders0 = store.managerorder.GetOrdersForManager(0);
            var orders1 = store.managerorder.GetOrdersForManager(1);
            var orders2 = store.managerorder.GetOrdersForManager(2);
            var orders3 = store.managerorder.GetOrdersForManager(3);
            string _orders0 = (orders0.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_0 + " (" + orders0.Count() + ")", "orders0") : "";
            string _orders1 = (orders1.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_1 + " (" + orders1.Count() + ")", "orders1") : "";
            string _orders2 = (orders2.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_2 + " (" + orders2.Count() + ")", "orders2") : "";
            string _orders3 = (orders3.Count() != 0) ? GeneralMethods.Tab(StoreLanguage.status_3 + " (" + orders2.Count() + ")", "orders3") : "";
            string display = "";
            bool order = true;
            if (orders0.Count() != 0)
            {
                result.Append(_GetOrders(orders0, display, "_orders0"));
                display = "style='display:none;'"; order = false;
            }
            if (orders1.Count() != 0)
            {
                result.Append(_GetOrders(orders1, display, "_orders1"));
                display = "style='display:none;'"; order = false;
            }
            if (orders2.Count() != 0)
            {
                result.Append(_GetOrders(orders2, display, "_orders2"));
                display = "style='display:none;'"; order = false;
            }
            if (orders3.Count() != 0)
                result.Append(_GetOrders(orders3, display, "_orders3")); order = false;
            if (order)
            {
                result.Append("<div class='content'>");
                result.Append(StoreLanguage.ordernull);
                result.Append("</div>");
            }
            else
                result.Insert(0, "<div class='button'>" + _orders0 + _orders1 + _orders2 + _orders3 + "</div>");
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// Display  orders list
        /// </summary>
        /// <param name="neworders"></param>
        /// <param name="display"></param>
        /// <param name="id"></param>
        /// <returns>string</returns>
        private static string _GetOrders(IQueryable<mytrip_storeorder> neworders, string display, string id)
        {
            IStoreRepository store = new IStoreRepository();
            StringBuilder result = new StringBuilder();
            if (neworders != null)
            {
                result.Append("<div id='" + id + "' class='tabc' " + display + " >");
                foreach (var x in neworders)
                {
                    if (x.Status == 0)
                    {
                        result.Append("<div class='content'>");
                        result.Append("<div class='_right'>" +
                            GeneralMethods.ImgInput("/images/delete.png", "/Store/MoveOrders/" + x.OrderId, "delete", 14) + "</div>");
                        result.Append("<a href='/Store/OrderDetails/"+x.OrderId+"' >");
                        result.Append(" #");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("</a>");
                        result.Append("<div class='right'>");
                        result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_0);
                        result.Append("</span>");
                        result.Append("</div><div class='info2'>");
                    }
                    else if (x.Status == 1)
                    {
                        result.Append("<div class='content'>");
                        result.Append("<div class='_right'>" +
                            GeneralMethods.ImgInput("/images/approved.png", "/Store/BillingOrder/" + x.OrderId, "rename", 14)+
                            GeneralMethods.ImgInput("/images/delete.png", "/Store/MoveOrders/" + x.OrderId, "delete", 14) + "</div>");
                        result.Append("<a href='/Store/OrderDetails/" + x.OrderId + "' >");
                        result.Append(" #");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("</a>");
                        result.Append("<div class='right'>");
                        result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_1);
                        result.Append("</span>");
                        result.Append("</div><div class='info2'>");
                    }
                    else if (x.Status == 2)
                    {
                        result.Append("<div class='appr'>");
                        result.Append("<div class='_right'>" +
                            GeneralMethods.ImgInput("/images/delete.png", "/Store/MoveOrders/" + x.OrderId, "delete", 14) + "</div>");
                        result.Append("<a href='/Store/OrderDetails/" + x.OrderId + "' >");
                        result.Append(" #");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("</a>");
                        result.Append("<div class='right'>");
                        //result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_2);
                        //result.Append("</span>");
                        result.Append("</div><div class='info2'>");
                    }
                    else if (x.Status == 3)
                    {
                        result.Append("<div class='appr'>");
                        result.Append("<div class='_right'>" +
                            GeneralMethods.ImgInput("/images/delete.png", "/Store/MoveOrders/" + x.OrderId, "delete", 14) + "</div>");
                        result.Append("<a href='/Store/OrderDetails/" + x.OrderId + "' >");
                        result.Append(" #");
                        result.Append(x.OrderId);
                        result.Append(" ");
                        result.Append(x.CreationDate.ToString("dd MMMM yyyy"));
                        result.Append("</a>");
                        result.Append("<div class='right'>");
                        //result.Append("<span class='error'>");
                        result.Append(StoreLanguage.status_3);
                        //result.Append("</span>");
                        result.Append("</div><div class='info2'>");
                    }
                    result.Append(StoreLanguage.firstname + ": " + x.mytrip_storeprofile.FirstName + "<br/>");
                    result.Append(StoreLanguage.lastname + ": " + x.mytrip_storeprofile.LastName + "<br/>");
                    result.Append(CoreLanguage.Email + ": " + x.mytrip_storeprofile.UserEmail + "<br/>");
                    result.Append(StoreLanguage.phone + ": " + x.mytrip_storeprofile.Phone + "<br/>");
                    if (ModuleSetting.organizationBuy())
                    {
                        result.Append(StoreLanguage.organization + ": " + x.mytrip_storeprofile.Organization + "<br/>");
                        if (LocalisationSetting.culture().ToLower() == "ru-ru")
                        {
                            result.Append(StoreLanguage.organizationINN + ": " + x.mytrip_storeprofile.OrganizationINN + "<br/>");
                            result.Append(StoreLanguage.organizationKPP + ": " + x.mytrip_storeprofile.OrganizationKPP + "<br/>");
                        }
                    }
                    result.Append(StoreLanguage.address + ": " + x.mytrip_storeprofile.Address + "</div>");
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
                    result.Append(MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(),totalprice));
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
                        result.Append(MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(),totalprice + delivery));
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
        /// <summary>
        /// Ordel total price
        /// </summary>
        /// <param name="total">price</param>
        /// <param name="delivery">delivery</param>
        /// <param name="moneyid">moneyid</param>
        /// <returns>HtmlString</returns>
        public static HtmlString Total(decimal total, decimal delivery,string moneyid)
        {
            decimal _delivery = MoneyHelpers.ConvertMoneyDecimal(moneyid, delivery);
            StringBuilder result = new StringBuilder();
            result.Append(StoreLanguage.Total+" ");
            result.Append("<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(), total) + "</b><br/>");
            result.Append(StoreLanguage.Delivery + " ");
            result.Append("<b>" + MoneyHelpers.ConvertMoney(moneyid, delivery) + "</b><br/>");
            result.Append(StoreLanguage.Totaldelivery + " ");
            result.Append("<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(), total + delivery) + "</b><br/>");
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="totalprice"></param>
        /// <returns></returns>
        public static HtmlString ViewOrdersForOrderDetails(IQueryable<mytrip_storeorderisproduct> x,out decimal totalprice)
        {
            StringBuilder result = new StringBuilder();
            totalprice = 0;
            int totalcount = 0;
            foreach (var y in x)
            {
                    totalprice += MoneyHelpers.ConvertMoneyDecimal(y.MoneyId, y.Price) * y.Count;
                    totalcount += y.Count;
                    string nambercatalog = "";
                    if (y.mytrip_storeproduct.NamberCatalog != null)
                        nambercatalog = " (#" + y.mytrip_storeproduct.NamberCatalog + ")";
                    result.Append("<div class='content'>");
                    result.Append("<div class='right'><a href=\"/Store/DeleteProductOrder/" + y.ProductId +"/"+y.OrderId+ "\">" + GeneralMethods.Image("/images/delete.png", 0, 14, CoreLanguage.save, 0) + "</a></div>");
                    result.Append("<table class='noborders' style='width:95%;'><tr><td>");
                    result.Append("<a href=\"/Store/View/" + y.ProductId + "/" + y.mytrip_storeproduct.Path + "\">" + StoreHelper.GetImageProduct(y.ProductId, ModuleSetting.widthImgDepartment()) + "</a>");
                    result.Append("</td><td>");
                    result.Append("<h3 class='title' id='" + y.ProductId+"/"+y.OrderId + "'><a href=\"/Store/View/" + y.ProductId + "/" + y.mytrip_storeproduct.Path + "\">" + y.mytrip_storeproduct.Title + nambercatalog + "</a></h3>");
                    result.Append(StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + y.mytrip_storeproduct.ProducerId + "/1/" + y.mytrip_storeproduct.mytrip_storeproducer.Path + "\" >" +
                    y.mytrip_storeproduct.mytrip_storeproducer.Title + "</a><br/>");
                    if (y.mytrip_storeproduct.mytrip_storedepartment.SubDepartmentId == 0)
                        result.Append(StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + y.mytrip_storeproduct.DepartmentId + "/0/1/" + y.mytrip_storeproduct.mytrip_storedepartment.Path + "\" >" +
                            y.mytrip_storeproduct.mytrip_storedepartment.Title + "</a><br/>");
                    else
                    {
                        result.Append(StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + y.mytrip_storeproduct.mytrip_storedepartment.SubDepartmentId + "/0/1/" + y.mytrip_storeproduct.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                                y.mytrip_storeproduct.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a><br/>");
                        result.Append(StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + y.mytrip_storeproduct.DepartmentId + "/0/1/" + y.mytrip_storeproduct.mytrip_storedepartment.Path + "\" >" +
                            y.mytrip_storeproduct.mytrip_storedepartment.Title + "</a><br/>");
                    }
                    result.Append("</td><td style='width:80px;'>");
                    result.Append(StoreLanguage.titlePrice + "<br/> <b >" + MoneyHelpers.ConvertMoney(y.MoneyId, y.Price) + "</b><br/>");
                    result.Append("</td><td style='width:100px;'>");
                    result.Append(StoreLanguage.totalCount + "<br/>");
                    result.Append("<div class='right' title='" + CoreLanguage.save + "' style='display:none;'>" + GeneralMethods.Image("/images/approved.png", 0, 14, CoreLanguage.save, 0) + "</div>");
                    result.Append("<input name='count" + y.ProductId + "' type='text' value='" + y.Count + "' style='width:50px;'/>");

                    result.Append("</td><td style='width:80px;'>");
                        result.Append(StoreLanguage.amount_of + "<br/> <b>" + MoneyHelpers.ConvertMoney(y.MoneyId, y.Price * y.Count) + "</b>");
                    result.Append("</td></tr></table></div><div class='last'></div>");
                
            }
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// Print Preview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string PrintPreview(int id)
        {
            IStoreRepository store = new IStoreRepository();
            var seller = store.seller.GetSeller();
            StringBuilder result = new StringBuilder();
            result.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            result.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            result.AppendLine("<head>");
            result.AppendLine("<title></title>");
            result.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"/Content/Store/print.css\" media=\"print\" />");
            result.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"/Content/Store/noprint.css\" />");
            result.AppendLine("</head>");
            result.AppendLine("<body>");

            result.AppendLine("<div id='content' style='font-family:Arial;font-size: 12px;width:700px;'>");
            //result.AppendLine("<div style='position:relative;float:right'>");
            //result.AppendLine("<button onclick='print();'>"+StoreLanguage.print+"</button>");
            //result.AppendLine("</div>");
            result.AppendLine("<b>" + StoreLanguage.account1 + "</b><br/>");
            result.AppendLine("<b style='text-decoration:underline;'>" + seller.Organization + "</b><br/>");
            result.AppendLine("<b>" + StoreLanguage.address + ": " + seller.Address + " " + StoreLanguage.phone
            + ": " + seller.Phone + "</b><br/>");
            result.AppendLine("<span style='font-size:11px;'>" + StoreLanguage.address1 + ": " + seller.Address + "</span><br/>");
            result.AppendLine("<span>" + CoreLanguage.Email + ": " + seller.Email + "</span><br/>");
            result.AppendLine("<div style='text-align:center'>");
            result.AppendLine("<b>" + StoreLanguage.account2 + "</b>");
            result.AppendLine("</div>");
            result.AppendLine("<table style='width:100%;font-size: 12px;margin:0px; padding:0px;border-collapse: collapse;'>");
            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            ////
            result.AppendLine("<table style='width:100%;font-size: 12px;margin:0px; padding:0px;border-collapse: collapse;'>");
            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;border-right: 1px solid #000;border-bottom: 1px solid #000;'>");
            result.AppendLine(" " + StoreLanguage.account3 + " " + seller.OrganizationINN);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border-bottom: 1px solid #000;'>");
            result.AppendLine(" " + StoreLanguage.account4 + " " + seller.OrganizationKPP);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("</table>");
            ////
            result.AppendLine(" " + StoreLanguage.account5 + "<br/> " + seller.Organization);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            result.AppendLine(" " + StoreLanguage.account6);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            result.AppendLine(" " + seller.BankAccountSeller);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            result.AppendLine(" " + StoreLanguage.account7 + "<br/> " + seller.Bank);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            result.AppendLine("<table style='width:100%;font-size: 12px;margin:0px; padding:0px;border-collapse: collapse;'>");

            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;border-bottom: 1px solid #000;'>");
            result.AppendLine(" " + StoreLanguage.account8);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;'>");
            result.AppendLine(" " + StoreLanguage.account6);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("</table>");
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
            result.AppendLine(" " + seller.BankAccountBIK + "<br/> " + seller.BankAccount);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("</table><br/>");

            var order = store.managerorder.GetOrderForManager(id);
            result.AppendLine("<div style='font-size: 14px;text-align:center;'>");
            result.AppendLine("<b>" + StoreLanguage.account9 + " " + order.NamberAccount + " " +
                StoreLanguage.account10 + " " + string.Format("{0:dd MMMM yyyy}", order.DateAccount)
                + " " + StoreLanguage.account11 + "</b>");
            result.AppendLine("</div>");
            result.AppendLine("<b>" + StoreLanguage.customer + ": " + order.mytrip_storeprofile.Organization + "</b><br/>");
            result.AppendLine("<b>" + StoreLanguage.account12 + ": " + order.mytrip_storeprofile.Organization + "</b><br/>");
            result.AppendLine("<div style='position:relative;float:right'>");
            result.AppendLine(StoreLanguage.account3 + "/" + StoreLanguage.account4 + " " +
                order.mytrip_storeprofile.OrganizationINN + "/" + order.mytrip_storeprofile.OrganizationKPP);
            result.AppendLine("</div>");
            result.AppendLine(StoreLanguage.address + ": " + order.mytrip_storeprofile.Address);

            result.AppendLine("<table style='width:100%;font-size: 12px;margin:0px; padding:0px;border-collapse: collapse;'>");
            result.AppendLine("<tr>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.account13);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.account14);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.account15);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.account16);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.titlePrice);
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
            result.AppendLine(StoreLanguage.account17);
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            int count = 1;
            decimal total = 0;
            foreach (var x in store.order.GetOrdersForUser(order.OrderId))
            {
                result.AppendLine("<tr>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
                result.AppendLine(count.ToString());
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
                result.AppendLine(x.mytrip_storeproduct.Title);
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
                result.AppendLine(x.mytrip_storeproduct.Packing);
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
                result.AppendLine(x.Count.ToString());
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
                result.AppendLine(MoneyHelpers.ConvertMoney(x.MoneyId, x.Price));
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
                result.AppendLine(MoneyHelpers.ConvertMoney(x.MoneyId, x.Price * x.Count));
                result.AppendLine("</td>");
                result.AppendLine("</tr>");
                total += MoneyHelpers.ConvertMoneyDecimal(x.MoneyId, x.Price * x.Count);
                count++;
            }
            if (order.Delivery > 0)
            {
                result.AppendLine("<tr>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
                result.AppendLine(count.ToString());
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;'>");
                result.AppendLine(StoreLanguage.Delivery1);
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");

                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");

                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
                result.AppendLine(MoneyHelpers.ConvertMoney(order.MoneyId, order.Delivery));
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
                result.AppendLine(MoneyHelpers.ConvertMoney(order.MoneyId, order.Delivery));
                result.AppendLine("</td>");
                result.AppendLine("</tr>");
                total += MoneyHelpers.ConvertMoneyDecimal(order.MoneyId, order.Delivery);
            }
            result.AppendLine("<tr>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;text-align:right;'>");
            result.AppendLine("<b>" + StoreLanguage.Total + " </b>");
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
            result.AppendLine("<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(), total) + "</b>");
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            if (seller.LiteNDS)
            {
                result.AppendLine("<tr>");
                result.AppendLine("<td>");
                result.AppendLine("</td>");
                result.AppendLine("<td>");
                result.AppendLine("</td>");
                result.AppendLine("<td>");
                result.AppendLine("</td>");
                result.AppendLine("<td>");
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;text-align:right;'>");
                result.AppendLine("<b>" + StoreLanguage.account18 + " </b>");
                result.AppendLine("</td>");
                result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:center;'>");
                result.AppendLine("<b>-</b>");
                result.AppendLine("</td>");
                result.AppendLine("</tr>");

            }
            result.AppendLine("<tr>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td>");
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;text-align:right;'>");
            result.AppendLine("<b>" + StoreLanguage.account24 + " </b>");
            result.AppendLine("</td>");
            result.AppendLine("<td style='margin:0px; padding:0px;border: 1px solid #000;text-align:right;'>");
            result.AppendLine("<b>" + MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(), total) + "</b>");
            result.AppendLine("</td>");
            result.AppendLine("</tr>");
            result.AppendLine("</table><br/>");
            result.AppendLine(string.Format(StoreLanguage.account19, count, MoneyHelpers.ConvertMoney(ModuleSetting.keyMoney().ToUpper(), total)));
            result.AppendLine("<br/><b>" + order.PriceInWords + "</b><br/>");
            result.AppendLine(StoreLanguage.account20 + "<br/>");
            result.AppendLine(StoreLanguage.account21 + "<br/>");
            result.AppendLine("<div style='height:200px;'>");
            result.AppendLine("<img src='/Content/Store/Seller/shtamp.png'style='position:absolute;left:0px;'>");
            result.AppendLine("<div style='position:relative;float:left;padding-top:12px;margin-right:5px;'>");
            result.AppendLine(StoreLanguage.account22 + "</div>");
            result.AppendLine("<div style='height:42px;position:relative;float:left;'>");
            result.AppendLine("<img src='/Content/Store/Seller/word.png'style='position:absolute;left:0px;'>");
            result.AppendLine("<div style='height:27px;width:150px;position:relative;float:left;border-bottom: 1px solid #000;'>");
            result.AppendLine("</div>");
            result.AppendLine("</div>");
            result.AppendLine("<div style='position:relative;float:left;padding-top:12px;margin-left:5px;'>");
            result.AppendLine("(" + seller.Director + ")</div>");
            result.AppendLine("<div style='clear:both;'></div>");
            result.AppendLine("<div style='position:relative;float:left;padding-top:12px;margin-right:5px;'>");
            result.AppendLine(StoreLanguage.account23 + "</div>");
            result.AppendLine("<div style='height:42px;position:relative;float:left;'>");
            result.AppendLine("<img src='/Content/Store/Seller/word.png'style='position:absolute;left:0px;'>");
            result.AppendLine("<div style='height:27px;width:150px;position:relative;float:left;border-bottom: 1px solid #000;'>");
            result.AppendLine("</div>");
            result.AppendLine("</div>");
            result.AppendLine("<div style='position:relative;float:left;padding-top:12px;margin-left:5px;'>");
            result.AppendLine("(" + seller.Accountant + ")</div>");
            result.AppendLine("</div>");

            result.AppendLine("<div style='position:relative;float:right'>");
            result.AppendLine("<button onclick='print();'>" + StoreLanguage.print + "</button>");
            result.AppendLine("</div>");
            result.AppendLine("</div>");
            result.AppendLine("<div id='nocopy'></div>");
            result.AppendLine("</body>");
            result.AppendLine("</html>");
            return result.ToString();
        }
    
    }
}
