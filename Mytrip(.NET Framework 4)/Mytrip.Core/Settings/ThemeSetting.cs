using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.IO;

namespace Mytrip.Core
{
    public class ThemeSetting
    {
        public static string defaultTheme = ConfigurationManager.AppSettings["defaultTheme"];
        public static bool unlockTheme = bool.Parse(ConfigurationManager.AppSettings["unlockTheme"]);
        public static string brouser = HttpContext.Current.Request.Browser.Browser;
        public static int majorversion = HttpContext.Current.Request.Browser.MajorVersion;
        public static double minorversion = HttpContext.Current.Request.Browser.MinorVersion;
        public static string version = HttpContext.Current.Request.Browser.Version;
        public static string theme()
        {
            string theme = ThemeSetting.defaultTheme;
            if (unlockTheme)
            {
                if (HttpContext.Current.Session["theme"] != null)
                    theme = HttpContext.Current.Session["theme"].ToString();
                if (HttpContext.Current.Request.Cookies["myTripTheme"] != null)
                    theme = HttpContext.Current.Request.Cookies["myTripTheme"].Value;
            }
            return theme;
        }
        public static string[] allThemeMassive()
        {

            string absolutDirectory = HttpContext.Current.Server.MapPath("/Theme");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
            {
                _absolutDirectory.Create();
            }
            DirectoryInfo[] result = _absolutDirectory.GetDirectories();
            int count = result.Count();
            int namber = 0;
            string[] _result = new string[count];
            foreach (DirectoryInfo x in result)
            {
                _result[namber] = x.Name;
                namber++;
            }
            return _result;
        }
    }
}
