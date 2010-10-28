using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using Mytrip.Store.Repository;

namespace Mytrip.Store.Helpers
{
    public static class AccountHelper
    {
        public static string CreateAccount(int id)
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
