using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using mtm.Core.Install;
using mtm.Votes.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Web;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Votes
{
    public static class InstallAndUninstall
    {
        #region  Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        public static string moduleName = "mtm.Votes";
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
            XElement votesmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            _doc.Root.Element("installModules").Add(votesmodule);
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockVotes"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "partialAccordion"), new XAttribute("value", "Helpers/_accordion")),
                new XElement("add", new XAttribute("name", "partialNoAccordion"), new XAttribute("value", "Helpers/_accordion_no")),
                new XElement("add", new XAttribute("name", "nameVotes"),
                    new XElement("add", new XAttribute("name", "Our Vote"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Наш Опрос"), new XAttribute("value", "ru-ru"))));
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
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.Votes.resources.dll", Localisation.ru_ru_mtm_Votes_resources);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Scripts/Page/Votes.js", JScripts.Votes);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Votes");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Votes/Edit.cshtml", WebPages.Edit);
            EditePageRepository.CreatePage("/Views/Votes/Index.cshtml", WebPages.Index);
            EditePageRepository.CreatePage("/Views/Votes/Setting.cshtml", WebPages.Setting);
            EditePageRepository.CreatePage("/Views/Votes/Web.config", WebPages.Web);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru/mtm.Votes.resources.dll");
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Votes.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Votes");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        } 
    }
}
