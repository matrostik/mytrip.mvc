using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Repository;
using System.Xml.Linq;
using Mytrip.Mvc;
using System.Web;
using System.Web.Caching;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModuleSetting
    {
        #region Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        static string moduleName = "Mytrip.Store";
        static string moduleVersion = "1.0.48.0";
        static string moduleStatus = "Beta";
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
                new XElement("add", new XAttribute("name", "unlockStore"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "columnDepartment"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgDepartment"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleDepartment"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "columnProduct"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgProduct"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleProduct"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "roleChiefStoreManager"), new XAttribute("value", "chief_store_manager")),
                new XElement("add", new XAttribute("name", "roleStoreManager"), new XAttribute("value", "store_manager")),
                new XElement("add", new XAttribute("name", "onlineBuy"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "MoneyProcent"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "organizationBuy"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "privatepersonBuy"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewProduktTable"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paypal"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paypalseller"), new XAttribute("value", "***")),
                new XElement("add", new XAttribute("name", "nameStore"),
                    new XElement("add", new XAttribute("name", "Store"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Магазин"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameProducer"),
                    new XElement("add", new XAttribute("name", "Producers"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Производители"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameSearch"),
                    new XElement("add", new XAttribute("name", "Search"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Поиск"), new XAttribute("value", "ru-ru"))),
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
        // Данные из MytripConfiguration.xml<add name="privatepersonBuy" value="True" />
        // **********************************************
        public static string NameSearchPage()
        {
            return GeneralMethods.MytripCache("ss_namesearch", moduleName, "nameSearch", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool viewProduktTable()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_viewprodukttable", moduleName, "viewProduktTable", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool privatepersonBuy()
        {
            if (organizationBuy())
                return bool.Parse(GeneralMethods.MytripCache("ss_privatepersonbuy", moduleName, "privatepersonBuy", false, null, 36000, CacheItemPriority.High).ToString());
            else
                return true;
        }
        public static bool paypal()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_paypal", moduleName, "paypal", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string paypalseller()
        {
            return GeneralMethods.MytripCache("ss_paypalseller", moduleName, "paypalseller", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool organizationBuy()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_organizationbuy", moduleName, "organizationBuy", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int MoneyProcent()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_moneyprocent", moduleName, "MoneyProcent", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Название для магазина с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ss_namestore")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string nameStore()
        {
            return GeneralMethods.MytripCache("ss_namestore", moduleName, "nameStore", true, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string nameProducer()
        {
            return GeneralMethods.MytripCache("ss_nameproducer", moduleName, "nameProducer", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string nameMoney()
        {
            return GeneralMethods.MytripCache("ss_namemoney", moduleName, "Money", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string keyMoney()
        {
            return StoreCache("ss_keymoney", moduleName, "Money", null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ss_unlockstore")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockStore()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_unlockstore", moduleName, "unlockStore", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool onlineBuy()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_onlinebuy", moduleName, "onlineBuy", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int columnDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_columndepartment", moduleName, "columnDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int widthImgDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_widthimgdepartment", moduleName, "widthImgDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int styleDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_styledepartment", moduleName, "styleDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int columnProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_columnproduct", moduleName, "columnProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int widthImgProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_widthimgproduct", moduleName, "widthImgProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int styleProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_styleproduct", moduleName, "styleProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleChiefStoreManager()
        {
            return GeneralMethods.MytripCache("ss_rolechiefstoremanager", moduleName, "roleChiefStoreManager", false, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleStoreManager()
        {
            return GeneralMethods.MytripCache("ss_rolestoremanager", moduleName, "roleStoreManager", false, null, 36000, CacheItemPriority.High).ToString();
        }





        public static object StoreCache(string key, string element, string attribute, int? absolutSek, int? spanSek, CacheItemPriority priority)
        {
            string _culture = LocalisationSetting.culture();
            if (HttpContext.Current.Cache[key + _culture] == null)
            {
                TimeSpan _spanSek = spanSek == null ? TimeSpan.Zero : TimeSpan.FromSeconds((int)spanSek);
                DateTime _absolutSek = absolutSek == null ? DateTime.MaxValue : DateTime.Now.AddSeconds((int)absolutSek);
                HttpContext.Current.Cache.Insert(key + _culture, StoreConfigurationValue(element, attribute),
                null, _absolutSek, _spanSek, priority, null);
            }
            return HttpContext.Current.Cache[key + _culture];
        }
        private static string StoreConfigurationValue(string element, string attribute)
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
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlStore");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlStore");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
