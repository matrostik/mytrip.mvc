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
    public class LocalisationSetting
    {

        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        public string defaultCulture()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("language").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "defaultCulture");
            return core.Attribute("value").Value;
        }
        public bool unlockAllCulture()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("language").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockAllCulture");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string allCulture()
        {
            string result = string.Empty;
            foreach (string x in allCultureMassive())
            {
                result += x;
            }
            return result;
        }
        public string[] allCultureMassive()
        {

            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
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
        public IDictionary<string, string> allCultureDictionary()
        {
            IDictionary<string, string> result =
              new Dictionary<string, string>();
            foreach (string x in allCultureMassive())
                    {
                        result.Add(x, x);
                    }
              
            return result;
        }
    }
}
