using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Settings;
using System.Xml.Linq;
using System.Web;
using System.Web.Caching;

namespace Mytrip.Tourism
{
    public static class ModuleSetting
    {
        #region Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        static string moduleName = "Mytrip.Tourism";
        static string moduleVersion = "1.0.47.0";
        static string moduleStatus = "Alfa";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();

        /// <summary>
        /// 
        /// </summary>
        public static void CreateStoreConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            XElement artmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            _doc.Root.Element("installModules").Add(artmodule);
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockTours"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paginalTours"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewDescription"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "columnTours"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "styleTours"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "widthImgTours"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "MoneyProcent"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "closeTour"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "roleChiefTourManager"), new XAttribute("value", "chief_tour_manager")),
                new XElement("add", new XAttribute("name", "roleTourManager"), new XAttribute("value", "tour_manager")),
                new XElement("add", new XAttribute("name", "nameTours"),
                    new XElement("add", new XAttribute("name", "Tours"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Туры"), new XAttribute("value", "ru-ru"))),
                    new XElement("add", new XAttribute("name", "Money"),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("name", "$"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("name", "руб."), new XAttribute("value", "ru-ru")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("name", "€"), new XAttribute("value", "null"))),
                new XElement("add", new XAttribute("name", "Course"),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("to", "USD"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("to", "EUR"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("to", "RUB"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("to", "EUR"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("to", "RUB"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("to", "USD"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")))
                    );
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
        }
        //****************** E N D **********************
        #endregion

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        public static bool unlockTours()
        {
            return bool.Parse(GeneralMethods.MytripCache("st_unlocktours", moduleName, "unlockTours", false, null, 36000, CacheItemPriority.High).ToString());
        }

        public static bool paginalTours()
        {
            return bool.Parse(GeneralMethods.MytripCache("st_paginaltours", moduleName, "paginalTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewDescription()
        {
            return bool.Parse(GeneralMethods.MytripCache("st_viewdescription", moduleName, "viewDescription", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int columnTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_columntours", moduleName, "columnTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int styleTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_styletours", moduleName, "styleTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string nameTours()
        {
            return GeneralMethods.MytripCache("st_nametours", moduleName, "nameTours", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static int MoneyProcent()
        {
            return int.Parse(GeneralMethods.MytripCache("st_moneyprocent", moduleName, "MoneyProcent", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int widthImgTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_widthimgtours", moduleName, "widthImgTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int closeTour()
        {
            return int.Parse(GeneralMethods.MytripCache("st_closetour", moduleName, "closeTour", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string nameMoney()
        {
            return GeneralMethods.MytripCache("st_namemoney", moduleName, "Money", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string keyMoney()
        {
            return ToursCache("st_keymoney", moduleName, "Money", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string roleChiefTourManager()
        {
            return GeneralMethods.MytripCache("st_rolechieftourmanager", moduleName, "roleChiefTourManager", false, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleTourManager()
        {
            return GeneralMethods.MytripCache("st_rolestoremanager", moduleName, "roleTourManager", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static object ToursCache(string key, string element, string attribute, int? absolutSek, int? spanSek, CacheItemPriority priority)
        {
            string _culture = LocalisationSetting.culture();
            if (HttpContext.Current.Cache[key + _culture] == null)
            {
                TimeSpan _spanSek = spanSek == null ? TimeSpan.Zero : TimeSpan.FromSeconds((int)spanSek);
                DateTime _absolutSek = absolutSek == null ? DateTime.MaxValue : DateTime.Now.AddSeconds((int)absolutSek);
                HttpContext.Current.Cache.Insert(key + _culture, ToursConfigurationValue(element, attribute),
                null, _absolutSek, _spanSek, priority, null);
            }
            return HttpContext.Current.Cache[key + _culture];
        }
        private static string ToursConfigurationValue(string element, string attribute)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            string localization = LocalisationSetting.culture();
            var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == localization.ToLower());
            return _core.Attribute("key").Value;
        }
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************

        /// <summary>Строка подключения к серверу SQL
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string connectionString()
        {
            string result = string.Empty;
            if (CoreSetting.Provider() == "MSSQL")
            {
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlTours");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlTours");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
