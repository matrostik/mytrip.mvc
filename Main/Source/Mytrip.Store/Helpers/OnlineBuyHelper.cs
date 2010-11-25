using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mytrip.Store.Repository;
using System.Web.Mvc;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store.Helpers
{
    public static class OnlineBuyHelper
    {
        public static HtmlString OnlineBuy(this HtmlHelper html)
        {
            string cart = (HttpContext.Current.Request.Cookies["myTripProductCart"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["myTripProductCart"].Value;
            decimal totalprice = 0;
            if (cart != null)
            {
                IStoreRepository istore = new IStoreRepository();
                cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _cart = cart.Split('|');
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
            }
            StringBuilder result = new StringBuilder();
            if (ModuleSetting.paypal())
            {
                result.Append("<form action=\"https://www.paypal.com/cgi-bin/webscr\" method=\"post\">");
                result.Append("<input type=\"hidden\" name=\"business\" value=\"" + ModuleSetting.paypalseller() + "\">");
                result.Append("<input type=\"hidden\" name=\"cmd\" value=\"_xclick\">");
                result.Append("<input type=\"hidden\" name=\"item_name\" value=\"" + StoreLanguage.mycarttitle + "\">");
                result.Append("<input type=\"hidden\" name=\"amount\" value=\"" + totalprice.ToString("0.00").Replace(",",".") + "\">");
                result.Append("<input type=\"hidden\" name=\"currency_code\" value=\"" + ModuleSetting.keyMoney() + "\">");
                result.Append("<input type=\"hidden\" name=\"cancel_return\" value=\"http://" +UsersSetting.applicationName()+ "/Store/Cart\">");
                result.Append("<input type=\"hidden\" name=\"return\" value=\"http://" + UsersSetting.applicationName() + "/Store/OnlineBuy\">");
                result.Append("<input type=\"image\" name=\"submit\" border=\"0\" ");
                result.Append("src=\"/Theme/" + ThemeSetting.theme() + "/images/PayPal.png\"");
                result.Append("alt=\"PayPal - The safer, easier way to pay online\">");
                result.Append("<img alt=\"\" border=\"0\" width=\"1\" height=\"1\"");
                result.Append("src=\"https://www.paypal.com/en_US/i/scr/pixel.gif \" >");
                result.Append("</form>");
            }
            return new HtmlString(result.ToString());
        }
    }
}
