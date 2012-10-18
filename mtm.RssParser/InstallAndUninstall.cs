using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using mtm.Core;
using mtm.Core.Install;
using mtm.RssParser.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Web;
using System.IO;
using mtm.Core.Repository;

namespace mtm.RssParser
{
    public static class InstallAndUninstall
    {
        #region  Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        public static string moduleName = "mtm.RssParser";
        static string moduleVersion = "1.0.52.0";
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
        public static void CreateAndDeleteDataBase(bool create)
        {
                CreateDataBase.CreateDataBaseMSSQL(ScriptSql.ScriptMSSQL,ScriptSql.deleteScriptMSSQL,create);
                CreateDataBase.CreateDataBaseMYSQL(ScriptSql.ScriptMySql,ScriptSql.deleteScriptMysql,create);

        }
        public static void CreateAllFiles()
        {
            string absolutDirectory = GeneralMethods.GetPath("bin", "ru-ru");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.RssParser.resources.dll", Localisation.ru_ru_mtm_RssParser_resources);
            absolutDirectory = GeneralMethods.GetPath("Scripts", "Page");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Scripts/Page/Rssparser.js", JScripts.Rssparser);
            absolutDirectory = GeneralMethods.GetPath("Views", "Rssparser");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Rssparser/Edit.cshtml", WebPages.Edit);
            EditePageRepository.CreatePage("/Views/Rssparser/Index.cshtml", WebPages.Index);
            EditePageRepository.CreatePage("/Views/Rssparser/Manager.cshtml", WebPages.Manager);
            EditePageRepository.CreatePage("/Views/Rssparser/Setting.cshtml", WebPages.Setting);
            EditePageRepository.CreatePage("/Views/Rssparser/Web.config", WebPages.Web);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = GeneralMethods.GetPath("bin", "ru-ru", "mtm.RssParser.resources.dll"); 
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = GeneralMethods.GetPath("Scripts", "Page", "Rssparser.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = GeneralMethods.GetPath("Views", "Rssparser");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        }
    }
}
