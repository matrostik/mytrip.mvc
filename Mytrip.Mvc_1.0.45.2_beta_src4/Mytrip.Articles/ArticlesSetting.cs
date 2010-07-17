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

namespace Mytrip.Articles
{
    public class ArticlesSetting
    {
        CoreSetting _core;
        public CoreSetting core
        {
            get
            {
                if (_core == null)
                    _core = new CoreSetting();
                return _core;
            }
        }
        UsersSetting _ucore;
        public UsersSetting ucore
        {
            get
            {
                if (_ucore == null)
                    _ucore = new UsersSetting();
                return _ucore;
            }
        }
        string moduleName = "Mytrip.Articles";
        string moduleVersion = "1.0.44.2";
        string moduleStatus = "Beta";
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        string culture = HttpContext.Current.Session["culture"].ToString();
        public void CreateArticlesConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var modules = _doc.Root.Elements("installModules");
            try
            {
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
                new XElement("add", new XAttribute("name", "cacheSeconds"), new XAttribute("value", "3600")),
                new XElement("add", new XAttribute("name", "roleArticleEditor"), new XAttribute("value", "article_editor")),
                new XElement("add", new XAttribute("name", "roleBlogger"), new XAttribute("value", "blogger")),
                new XElement("add", new XAttribute("name", "viewInfoClosedComments"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewInfoAuthorArticle"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewInfoViewsArticle"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "closecountCommentForBlogs"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "replaceСommentsEmail"), new XAttribute("value", "False")),
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
        public string NameArticlesPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameArticles");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string NameBlogsPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameBlogs");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string NameTagsPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameTags");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string NameSearchPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameSearch");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public bool articles()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "articles");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool blogs()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "blogs");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool viewInfoClosedComments()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "viewInfoClosedComments");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool replaceСommentsEmail()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "replaceСommentsEmail");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool viewInfoAuthorArticle()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "viewInfoAuthorArticle");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool viewInfoViewsArticle()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "viewInfoViewsArticle");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool showRelatedLinks()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "showRelatedLinks");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool closecountCommentForBlogs()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "closecountCommentForBlogs");
            return bool.Parse(core.Attribute("value").Value);
        }
        public int countCommentForBlogs()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "countCommentForBlogs");
            return int.Parse(core.Attribute("value").Value);
        }
        public string roleChiefEditor()
        {
            return ucore.roleChiefEditor();
        }
        public string roleArticleEditor()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "roleArticleEditor");
            return core.Attribute("value").Value;
        }
        public string roleBlogger()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "roleBlogger");
            return core.Attribute("value").Value;
        }
        public int cacheSeconds()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "cacheSeconds");
            return int.Parse(core.Attribute("value").Value);
        }
        public string connectionString()
        {
            string result = string.Empty;
            if (core.Provider() == "MSSQL")
            {
                result = core.connectionStringMSSQL("Repository.DataEntities.MssqlArticles");
            }
            else if (core.Provider() == "MYSQL")
            {
                result = core.connectionStringMYSQL("Repository.DataEntities.MysqlArticles");
            }
            return result;

        }
    }
}