using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Articles.Repository;
using System.Web.Security;
using Mytrip.Mvc.Interface;

namespace Mytrip.Articles.Controllers
{
    public class Mytrip_ArticlesExportController : Controller, IControllerExport
    {
        #region Properties
        IArticleRepository _artrepo;
        public IArticleRepository artrepo
        {
            get
            {
                if (_artrepo == null)
                    _artrepo = new IArticleRepository();
                return _artrepo;
            }
        }
        ArticlesCreateDataBase _createdb;
        public ArticlesCreateDataBase createdb
        {
            get
            {
                if (_createdb == null)
                    _createdb = new ArticlesCreateDataBase();
                return _createdb;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        #endregion

        #region Methods
        public string HomePage()
        {
            var a = artrepo.category.GetCategories(true);
            string result = "-1_globe_" + ArticleLanguage.articles + "|-2_globe_" + ArticleLanguage.blogs;
            foreach (var b in a)
            {
                string _culture = "globe_";
                if (!b.AllCulture)
                    _culture = b.Culture.ToLower() + "_";
                result += "|" + b.CategoryId + "_" + _culture + b.Title;
            }
            return result;
        }
        public string MenuPage()
        {
            string result = "MenuArticle_" + ArticleLanguage.articles +
                            "|MenuBlog_" + ArticleLanguage.blogs +
                            "|MenuCategory_" + ArticleLanguage.categories;
            return result;
        }
        public string ProfilePage()
        {
            string result = "LastActivity_" + ArticleLanguage.recent_activity;
            return result;
        }
        public string SideBarPage()
        {
            string result = "AccordionSearch_" + ArticleLanguage.search +
                            "|AccordionArticle_" + ArticleLanguage.articles +
                            "|AccordionBlogs_" + ArticleLanguage.blogs +
                            "|AccordionCategory_" + ArticleLanguage.categories +
                            "|AccordionTag_" + ArticleLanguage.tags1+
                            "|AccordionArticlesActivity_" + string.Format(ArticleLanguage.articles_popular_recent, ModuleSetting.NameArticlesPage()) +
                            "|AccordionBlogsActivity_" + string.Format(ArticleLanguage.blogs_popular_recent, ModuleSetting.NameBlogsPage());
            return result;
        }
        [RoleAdmin]
        public ActionResult CreateDataBaseAndXml()
        {
            createdb.CreateAndDeleteDataBase(true);
            ModuleSetting.CreateArticlesConfiguration();
            Roles.CreateRole(ModuleSetting.roleArticleEditor());
            Roles.CreateRole(ModuleSetting.roleBlogger());
            return RedirectToAction("SaveModule", "Core");
        }
        [RoleAdmin]
        public ActionResult UninstallModule()
        {
            createdb.CreateAndDeleteDataBase(false);
            return RedirectToAction("InstallModules", "Core");
        }
        #endregion
    }
}
