using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using mtm.Articles.Repository;
using System.Web.Security;
using mtm.Core.Settings;
using mtm.Articles.Helpers;
using mtm.Core.Repository;

namespace mtm.Articles.Controllers
{
    public class mtm_ArticlesExportController : Controller
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
            StringBuilder result = new StringBuilder();


            result.AppendLine("-1_globe_" + ArticleLanguage.articles + "|-2_globe_" + ArticleLanguage.blogs);
            foreach (var b in a)
            {
                string _culture = "globe_";
                if (!b.AllCulture)
                    _culture = b.Culture.ToLower() + "_";
                result.AppendLine("|" + b.CategoryId + "_" + _culture + b.Title);
            }
            return result.ToString();
        }
        public string MenuPage()
        {
            StringBuilder result = new StringBuilder();            
            result.AppendLine("MenuArticle_" + ArticleLanguage.articles +
                            "|MenuBlog_" + ArticleLanguage.blogs +
                            "|MenuCategory_" + ArticleLanguage.categories);
            return result.ToString();
        }
        public string ProfilePage()
        {
            string result = "LastActivity_" + ArticleLanguage.recent_activity;
            return result;
        }
        public string SideBarPage()
        {
            string result = "mtmArticlesExportAccordionSearch_" + ArticleLanguage.search +
                            "|mtmArticlesExportAccordionArticle_" + ArticleLanguage.articles +
                            "|mtmArticlesExportAccordionBlogs_" + ArticleLanguage.blogs +
                            "|mtmArticlesExportAccordionCategory_" + ArticleLanguage.categories +
                            "|mtmArticlesExportAccordionTag_" + ArticleLanguage.tags1 +
                            "|mtmArticlesExportAccordionArticlesActivity_" + string.Format(ArticleLanguage.articles_popular_recent, ModuleSetting.NameArticlesPage()) +
                            "|mtmArticlesExportAccordionBlogsActivity_" + string.Format(ArticleLanguage.blogs_popular_recent, ModuleSetting.NameBlogsPage());
            return result;
        }
        public string SearchPage()
        {
            string result = "Search_" + ArticleLanguage.search;
            return result;
        }
        [RoleAdmin]
        public ActionResult InstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(true);
            InstallAndUninstall.CreateModuleConfiguration();
            Roles.CreateRole(ModuleSetting.roleArticleEditor());
            Roles.CreateRole(ModuleSetting.roleBlogger());
            if (!MytripUser.UserInRole(ModuleSetting.roleArticleEditor()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name, ModuleSetting.roleArticleEditor());
            if (!MytripUser.UserInRole(ModuleSetting.roleBlogger()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name, ModuleSetting.roleBlogger());
            GeneralMethods.MytripCacheRemove("mtm_cacherole");
            InstallAndUninstall.CreateAllFiles();
            return RedirectToAction("SaveModule", "Core");
        }
        [RoleAdmin]
        public ActionResult UninstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(false);
            InstallAndUninstall.DeleteAllFiles();
            return RedirectToAction("InstallModules", "Core");
        }
        #endregion
    }
}
