using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Web;
using System.Xml.Linq;
using Mytrip.Mvc.Repository;
using System.Web.Caching;
using Mytrip.Mvc.Settings;

namespace Mytrip.Votes
{
    public static class ModuleSetting
    {
        #region  Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        static string moduleName = "Mytrip.Votes";
        static string moduleVersion = "1.0.47.0";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
        public static void CreateVotesConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            XElement votesmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            _doc.Root.Element("installModules").Add(votesmodule);
            try
            {
                _doc.Root.Elements("namespace").Elements("add").Where(x => x.Attribute("name").Value.Contains(moduleName)).Remove();
            }
            catch
            {
            }
            _doc.Root.Element("namespace").Add(
                new XElement("add", new XAttribute("name", "Mytrip.Votes"))
                , new XElement("add", new XAttribute("name", "Mytrip.Votes.Helpers")));
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockVotes"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "nameVotes"),
                    new XElement("add", new XAttribute("name", "Our Vote"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Наш Опрос"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
        }
        //****************** E N D **********************
        #endregion

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        /// <summary>
        /// Название блока в сайдбаре с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_namehomepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string NameVotesPage()
        {
            return GeneralMethods.MytripCache("vs_namevotes", moduleName, "nameVotes", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_integratedsecurity")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockVotes()
        {
            return bool.Parse(GeneralMethods.MytripCache("vs_unlockvotes", moduleName, "unlockVotes", false, null, 36000, CacheItemPriority.High).ToString());
        }
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************

        /// <summary>
        /// Строка подключения к серверу SQL
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string connectionString()
        {
            string result = string.Empty;
            if (CoreSetting.Provider() == "MSSQL")
            {
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlVotes");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlVotes");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
