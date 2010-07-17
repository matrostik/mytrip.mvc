using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.IO;
using Mytrip.Mvc.Models;
using System.Xml.Linq;

namespace Mytrip.Mvc
{
    public class ThemeSetting
    {
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        public string defaultTheme()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("theme").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "defaultTheme");
            return core.Attribute("value").Value;
        }
        public bool unlockTheme()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("theme").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockTheme");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string brouser() 
        { 
            return HttpContext.Current.Request.Browser.Browser; 
        }
        public int majorversion() 
        { 
            return HttpContext.Current.Request.Browser.MajorVersion; 
        }
        public double minorversion()
        {
            return HttpContext.Current.Request.Browser.MinorVersion; 
        }
        public string version()
        { 
            return HttpContext.Current.Request.Browser.Version;
        }
        public string theme()
        {
            string theme = defaultTheme();
            if (unlockTheme())
            {
                if (HttpContext.Current.Session["theme"] != null)
                    theme = HttpContext.Current.Session["theme"].ToString();
                if (HttpContext.Current.Request.Cookies["myTripTheme"] != null)
                    theme = HttpContext.Current.Request.Cookies["myTripTheme"].Value;
                bool result = false;
                foreach(string x in allThemeMassive())
                {
                    if (theme == x)
                        result = true;
                }
                if(!result)
                    theme = defaultTheme();
            }
            return theme;
        }
        public string[] allThemeMassive()
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
        public IDictionary<string, string> allThemeDictionary()
        {
            IDictionary<string, string> result =
              new Dictionary<string, string>();
            foreach (string x in allThemeMassive())
            {
                result.Add(x, x);
            }

            return result;
        }
    }
}
