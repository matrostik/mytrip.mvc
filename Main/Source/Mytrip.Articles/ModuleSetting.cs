using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Mytrip.Mvc;
using Mytrip.Articles.Models;
using System.Xml.Linq;
using System.Web;
using Mytrip.Mvc.Repository;
using System.Web.Caching;
using Mytrip.Mvc.Settings;

namespace Mytrip.Articles
{
    public static class ModuleSetting
    {
        #region Install Module
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************
        static string moduleName = "Mytrip.Articles";
        static string moduleVersion = "1.0.48.0";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();        
        public static void CreateArticlesConfiguration()
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

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************
        public static string NameArticlesPage()
        {
            return GeneralMethods.MytripCache("as_namearticles", moduleName, "nameArticles", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameBlogsPage()
        {
            return GeneralMethods.MytripCache("as_nameblogs", moduleName, "nameBlogs", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameTagsPage()
        {
            return GeneralMethods.MytripCache("as_nametags", moduleName, "nameTags", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameSearchPage()
        {
            return GeneralMethods.MytripCache("as_namesearch", moduleName, "nameSearch", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool articles()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_articles", moduleName, "articles", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool blogs()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_blogs", moduleName, "blogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoClosedComments()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoclosedcomments", moduleName, "viewInfoClosedComments", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoAuthorArticle()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoauthorarticle", moduleName, "viewInfoAuthorArticle", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoViewsArticle()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoviewsarticle", moduleName, "viewInfoViewsArticle", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool showRelatedLinks()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_showrelatedlinks", moduleName, "showRelatedLinks", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool closecountCommentForBlogs()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_closecountcommentforblogs", moduleName, "closecountCommentForBlogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int countCommentForBlogs()
        {
            return int.Parse(GeneralMethods.MytripCache("as_countcommentforblogs", moduleName, "countCommentForBlogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string roleChiefEditor()
        {
            return UsersSetting.roleChiefEditor();
        }
        public static string roleArticleEditor()
        {
            return GeneralMethods.MytripCache("as_rolearticleeditor", moduleName, "roleArticleEditor", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string roleBlogger()
        {
            return GeneralMethods.MytripCache("as_roleblogger", moduleName, "roleBlogger", false, null, 36000, CacheItemPriority.High).ToString();
        }
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************
        public static string connectionString()
        {
            string result = string.Empty;
            if (CoreSetting.Provider() == "MSSQL")
            {
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlArticles");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlArticles");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}