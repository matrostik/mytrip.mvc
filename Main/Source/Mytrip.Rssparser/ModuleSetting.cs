using System.Linq;
using System.Xml.Linq;
using System.Web.Caching;
using Mytrip.Mvc.Settings;

namespace Mytrip.Rssparser
{
    public static class ModuleSetting
    {
        #region  Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        static string moduleName = "Mytrip.Rssparser";
        static string moduleVersion = "1.0.48.0";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();

        public static void CreateRssparserConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            XElement rssmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            _doc.Root.Element("installModules").Add(rssmodule);
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockRssparser"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "nameRssparser"),
                    new XElement("add", new XAttribute("name", "News (rss)"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Новости (rss)"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
        }
        #endregion

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        /// <summary>Название модуля с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "rsp_namerssparser")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string nameRssparser()
        {
            return GeneralMethods.MytripCache("rsp_namerssparser", moduleName, "nameRssparser", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "rsp_unlockrssparser")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockRssparser()
        {
            return bool.Parse(GeneralMethods.MytripCache("rsp_unlockrssparser", moduleName, "unlockRssparser", false, null, 36000, CacheItemPriority.High).ToString());
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
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlRssparser");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlRssparser");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
