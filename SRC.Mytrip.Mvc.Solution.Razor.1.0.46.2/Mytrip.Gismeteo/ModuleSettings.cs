using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Xml.Linq;
using System.Web;
using Mytrip.Mvc.Repository;
using System.Web.Caching;
using Mytrip.Mvc.Settings;

namespace Mytrip.Gismeteo
{
    public static class ModuleSettings
   {
       #region  Module installation
       // **********************************************
       // Данные по модулю
       // **********************************************
        static string moduleName = "Mytrip.Gismeteo";
        static string moduleVersion = "1.0.46.2";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
       public static void CreateGismeteoConfiguration()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           try
           {
               _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
           }
           catch
           {
           }
           XElement gismmodule = new XElement("add",
               new XAttribute("name", moduleName),
               new XAttribute("version", moduleVersion),
               new XAttribute("status", moduleStatus));
           _doc.Root.Element("installModules").Add(gismmodule);
           try
           {
               _doc.Root.Elements("namespace").Elements("add").Where(x => x.Attribute("name").Value.Contains(moduleName)).Remove();
           }
           catch
           {
           }
           _doc.Root.Element("namespace").Add(
               new XElement("add", new XAttribute("name", "Mytrip.Gismeteo"))
               , new XElement("add", new XAttribute("name", "Mytrip.Gismeteo.Helpers")));
           try
           {
               _doc.Root.Elements(moduleName).Remove();
           }
           catch
           {
           }
           XElement article = new XElement(moduleName,
               new XElement("add", new XAttribute("name", "unlockGismeteo"), new XAttribute("value", "True")),
               new XElement("add", new XAttribute("name", "nameGismeteo"),
                   new XElement("add", new XAttribute("name", "Weather"), new XAttribute("value", "en-us")),
                   new XElement("add", new XAttribute("name", "Погода"), new XAttribute("value", "ru-ru"))));
           _doc.Root.Add(article);
           _doc.Save(_absolutDirectory);
       }
       //****************** E N D **********************
       #endregion

       #region Данные из MytripConfiguration.xml
       // **********************************************
       // Данные из MytripConfiguration.xml
       // **********************************************

       /// <summary>Название блока в сайдбаре с учетом культуры
       /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_namehomepage")
       /// </summary>
       /// <returns>возвращает string</returns>
       public static string NameGismeteoPage()
       {
           return GeneralMethods.MytripCache("gm_namegismeteo", moduleName, "nameGismeteo", true, null, 36000, CacheItemPriority.High).ToString();
       }
       /// <summary>Статус модуля - включен или отключен  (true = включен)
       /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_integratedsecurity")
       /// </summary>
       /// <returns>возвращает bool</returns>
       public static bool unlockGismeteo()
       {
           return bool.Parse(GeneralMethods.MytripCache("gm_unlockgismeteo", moduleName, "unlockGismeteo", false, null, 36000, CacheItemPriority.High).ToString());
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
               result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlGismeteo");
           }
           else if (CoreSetting.Provider() == "MySql")
           {
               result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlGismeteo");
           }
           return result;
       }
       //****************** E N D **********************
       #endregion
    }
}
