using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using mtm.Core;
using mtm.Core.Install;
using mtm.Articles.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using mtm.Core.Repository;
using System.IO;
using System.Web;
using System.Security.AccessControl;

namespace mtm.Articles
{
    public static class InstallAndUninstall
    {
        #region Install Module
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************
        public static string moduleName = "mtm.Articles";
        static string moduleVersion = "1.0.52.0";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();        
        public static void CreateModuleConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                var modules = _doc.Root.Elements("installModules");
                modules.Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            var module = _doc.Root.Element("installModules");
            XElement artmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            module.Add(artmodule);
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "articles"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "blogs"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "countCommentForBlogs"), new XAttribute("value", "5")),
                new XElement("add", new XAttribute("name", "roleArticleEditor"), new XAttribute("value", "article_editor")),
                new XElement("add", new XAttribute("name", "roleBlogger"), new XAttribute("value", "blogger")),
                new XElement("add", new XAttribute("name", "viewInfoClosedComments"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewInfoAuthorArticle"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewInfoViewsArticle"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "closecountCommentForBlogs"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "showRelatedLinks"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "nameArticles"),
                    new XElement("add", new XAttribute("name", "Articles"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Статьи"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameBlogs"),
                    new XElement("add", new XAttribute("name", "Blogs"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Блоги"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameTags"),
                    new XElement("add", new XAttribute("name", "Tags"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Теги"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameSearch"),
                    new XElement("add", new XAttribute("name", "Search"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Поиск"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
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
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.Articles.resources.dll", Localisation.ru_ru_mtm_Articles_resources);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Scripts/Page/Articles.js", JScripts.Articles);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Article");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Article/Archive.cshtml", WebPages.Archive);
            EditePageRepository.CreatePage("/Views/Article/ArchiveDetails.cshtml", WebPages.ArchiveDetails);
            EditePageRepository.CreatePage("/Views/Article/Create.cshtml", WebPages.Create);
            EditePageRepository.CreatePage("/Views/Article/Edit.cshtml", WebPages.Edit);
            EditePageRepository.CreatePage("/Views/Article/Editors.cshtml", WebPages.Editors);
            EditePageRepository.CreatePage("/Views/Article/Index.cshtml", WebPages.Index);
            EditePageRepository.CreatePage("/Views/Article/Profile.cshtml", WebPages.Profile);
            EditePageRepository.CreatePage("/Views/Article/Setting.cshtml", WebPages.Setting);
            EditePageRepository.CreatePage("/Views/Article/View.cshtml", WebPages.View);
            EditePageRepository.CreatePage("/Views/Article/Web.config", WebPages.Web);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru/mtm.Articles.resources.dll");
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Articles.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Article");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        }
    }
}
