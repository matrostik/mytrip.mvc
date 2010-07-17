using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Configuration;
using System.Web;
using System.Web.Routing;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// Menu Helper
    /// </summary>
    public static class MenuHelper
    {
        
        public static string MytripMenu(this HtmlHelper html, object menu)
        {
            CoreSetting core = new CoreSetting();
            if (!core.Development())
            {
                string urlPath = HttpContext.Current.Request.Path.ToString();
                int urlIndex = -1;
                string Controller = string.Empty;
                string Action = string.Empty;
                urlPath = urlPath.Remove(0, 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    Controller = urlPath.Remove(urlIndex);
                    urlPath = urlPath.Remove(0, urlIndex + 1);
                    if (urlPath.IndexOf("/") != -1)
                        urlIndex = urlPath.IndexOf("/");
                    if (urlIndex != -1)
                    {
                        Action = urlPath;
                    }
                }
                else { Controller = urlPath; }

                StringBuilder result = new StringBuilder();
                TagBuilder home = new TagBuilder("a");
                TagBuilder about = new TagBuilder("a");
                home.MergeAttribute("href", "/");
                home.InnerHtml = core.NameHomePage();
                about.MergeAttribute("href", "/Home/About");
                about.InnerHtml = core.NameAboutPage();
                if (String.IsNullOrEmpty(Controller) | Controller == "/")
                    result.AppendLine(GeneralMethods.Menu(home.ToString(), null, true, false, true, false));
                else
                    result.AppendLine(GeneralMethods.Menu(home.ToString(), null, false, false, true,false));
                IDictionary<string, object> _menu = (menu == null ? new RouteValueDictionary() : new RouteValueDictionary(menu));
                foreach (string key in _menu.Keys)
                {
                    result.AppendLine(_menu[key].ToString());
                }


                if (Action == "About")
                    result.AppendLine(GeneralMethods.Menu(about.ToString(), null, true, false, true, false));
                else
                    result.AppendLine(GeneralMethods.Menu(about.ToString(), null, false, false, true, false));
                return result.ToString();
            }
            else
                return string.Empty;
        }
    }
}
