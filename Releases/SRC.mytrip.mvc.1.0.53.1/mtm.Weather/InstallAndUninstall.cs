using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using mtm.Core.Install;
using mtm.Weather.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Web;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Weather
{
    public static class InstallAndUninstall
    {
        #region  Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        public static string moduleName = "mtm.Weather";
        static string moduleVersion = "1.0.53.1";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
        public static void CreateModuleConfiguration()
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
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockWeather"), new XAttribute("value", "True")),
               new XElement("add", new XAttribute("name", "partialAccordion"), new XAttribute("value", "Helpers/_accordion")),
                new XElement("add", new XAttribute("name", "partialNoAccordion"), new XAttribute("value", "Helpers/_accordion_no")),
               new XElement("add", new XAttribute("name", "nameWeather"),
                    new XElement("add", new XAttribute("name", "Weather"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Погода"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Web.config");
            XDocument doc = XDocument.Load(absolutDirectory);
            var core = doc.Root.Element("system.web.webPages.razor").Element("pages").Element("namespaces");
            XElement namespaces = new XElement("add",
               new XAttribute("namespace", moduleName));
            core.Add(namespaces);
            doc.Save(absolutDirectory);
        }
        //****************** E N D **********************
        #endregion
        public static void CreateAndDeleteDataBase(bool create)
        {
            CreateDataBase.CreateDataBaseMSSQL(ScriptSql.ScriptMSSQL, ScriptSql.deleteScriptMSSQL, create);
            CreateDataBase.CreateDataBaseMYSQL(ScriptSql.ScriptMySql, ScriptSql.deleteScriptMysql, create);
        }
        public static void CreateAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.Weather.resources.dll", Localisation.ru_ru_mtm_Weather_resources);

            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Weather");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Weather/Edit.cshtml", WebPages.Edit);
            EditePageRepository.CreatePage("/Views/Weather/Manager.cshtml", WebPages.Manager);
            EditePageRepository.CreatePage("/Views/Weather/Setting.cshtml", WebPages.Setting);
            EditePageRepository.CreatePage("/Views/Weather/Web.config", WebPages.Web);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru/mtm.Weather.resources.dll");
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Weather");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        } 
    }
}
