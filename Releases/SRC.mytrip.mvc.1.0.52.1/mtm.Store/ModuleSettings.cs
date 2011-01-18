using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Repository;
using System.Xml.Linq;
using mtm.Core;
using System.Web;
using System.Web.Caching;
using mtm.Core.Settings;

namespace mtm.Store
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModuleSetting
    {
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml<add name="privatepersonBuy" value="True" />
        // **********************************************
        public static string NameSearchPage()
        {
            return GeneralMethods.MytripCache("ss_namesearch", InstallAndUninstall.moduleName, "nameSearch", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool viewProduktTable()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_viewprodukttable", InstallAndUninstall.moduleName, "viewProduktTable", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool privatepersonBuy()
        {
            if (organizationBuy())
                return bool.Parse(GeneralMethods.MytripCache("ss_privatepersonbuy", InstallAndUninstall.moduleName, "privatepersonBuy", false, null, 36000, CacheItemPriority.High).ToString());
            else
                return true;
        }
        public static bool paypal()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_paypal", InstallAndUninstall.moduleName, "paypal", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string paypalseller()
        {
            return GeneralMethods.MytripCache("ss_paypalseller", InstallAndUninstall.moduleName, "paypalseller", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool organizationBuy()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_organizationbuy", InstallAndUninstall.moduleName, "organizationBuy", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int MoneyProcent()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_moneyprocent", InstallAndUninstall.moduleName, "MoneyProcent", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Название для магазина с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ss_namestore")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string nameStore()
        {
            return GeneralMethods.MytripCache("ss_namestore", InstallAndUninstall.moduleName, "nameStore", true, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string nameProducer()
        {
            return GeneralMethods.MytripCache("ss_nameproducer", InstallAndUninstall.moduleName, "nameProducer", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string nameMoney()
        {
            return GeneralMethods.MytripCache("ss_namemoney", InstallAndUninstall.moduleName, "Money", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string keyMoney()
        {
            return StoreCache("ss_keymoney", InstallAndUninstall.moduleName, "Money", null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ss_unlockstore")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockStore()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_unlockstore", InstallAndUninstall.moduleName, "unlockStore", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool onlineBuy()
        {
            return bool.Parse(GeneralMethods.MytripCache("ss_onlinebuy", InstallAndUninstall.moduleName, "onlineBuy", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int columnDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_columndepartment", InstallAndUninstall.moduleName, "columnDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int widthImgDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_widthimgdepartment", InstallAndUninstall.moduleName, "widthImgDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int styleDepartment()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_styledepartment", InstallAndUninstall.moduleName, "styleDepartment", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int columnProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_columnproduct", InstallAndUninstall.moduleName, "columnProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int widthImgProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_widthimgproduct", InstallAndUninstall.moduleName, "widthImgProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int styleProduct()
        {
            return int.Parse(GeneralMethods.MytripCache("ss_styleproduct", InstallAndUninstall.moduleName, "styleProduct", false, null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleChiefStoreManager()
        {
            return GeneralMethods.MytripCache("ss_rolechiefstoremanager", InstallAndUninstall.moduleName, "roleChiefStoreManager", false, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleStoreManager()
        {
            return GeneralMethods.MytripCache("ss_rolestoremanager", InstallAndUninstall.moduleName, "roleStoreManager", false, null, 36000, CacheItemPriority.High).ToString();
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
