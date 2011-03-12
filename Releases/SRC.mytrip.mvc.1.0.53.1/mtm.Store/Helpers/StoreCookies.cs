using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace mtm.Store.Helpers
{
    public static class StoreCookies
    {
        static string cart;
        public static string Cart
        {
            get
            {
                return (HttpContext.Current.Request.Cookies["mtProductCart"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtProductCart"].Value;
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtProductCart"] ?? new HttpCookie("mtProductCart");
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                cart = value;
            }
        }

        static string comparison;
        public static string Comparison
        {
            get
            {
                return (HttpContext.Current.Request.Cookies["mtProductComparison"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtProductComparison"].Value;
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtProductComparison"] ?? new HttpCookie("mtProductComparison");
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                comparison = value;
            }
        }
        static string addAcount;
        public static string AddAcount
        {
            get
            {
                return (HttpContext.Current.Request.Cookies["mtAddAccount"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtAddAccount"].Value;
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtAddAccount"] ?? new HttpCookie("mtAddAccount");
                cookie.Value = value;
                if (value == "0")
                    cookie.Expires = DateTime.Now.AddHours(1);
                else
                    cookie.Expires = DateTime.Now.AddHours(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                addAcount = value;
            }
        }
        static string onlineBuy;
        public static string OnlineBuy
        {
            get
            {
                return (HttpContext.Current.Request.Cookies["mtOnlineBuy"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtOnlineBuy"].Value;
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtOnlineBuy"] ?? new HttpCookie("mtOnlineBuy");
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                onlineBuy = value;
            }
        }
        static string tabCart;
        public static string TabCart
        {
            get
            {
                return (HttpContext.Current.Request.Cookies["mtTabCart"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtTabCart"].Value;
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtTabCart"] ?? new HttpCookie("mtTabCart");
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                tabCart = value;
            }
        }
    }
}
