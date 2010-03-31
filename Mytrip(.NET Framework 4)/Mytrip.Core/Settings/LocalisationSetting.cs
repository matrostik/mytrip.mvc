using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.IO;

namespace Mytrip.Core
{
    public class LocalisationSetting
    {
        public static string defaultCulture = ConfigurationManager.AppSettings["defaultCulture"];
        public static bool unlockAllCulture = bool.Parse(ConfigurationManager.AppSettings["unlockAllCulture"]);
        public static string allCulture()
        {
            string result = string.Empty;
            foreach (string x in allCultureMassive())
            {
                result += x;
            }
            return result;
        }
        public static string[] allCultureMassive()
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
    }
}
