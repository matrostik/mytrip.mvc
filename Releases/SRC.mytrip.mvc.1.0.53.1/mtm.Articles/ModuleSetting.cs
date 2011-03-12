using System.Web.Caching;
using mtm.Core.Settings;

namespace mtm.Articles
{
    public static class ModuleSetting
    {
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************
        public static string NameArticlesPage()
        {
            return GeneralMethods.MytripCache("as_namearticles", InstallAndUninstall.moduleName, "nameArticles", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameArticlesPage(string culture)
        {
            return GeneralMethods.MytripCache("as_namearticles", InstallAndUninstall.moduleName, "nameArticles", true,culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageKeyWords()
        {
            return GeneralMethods.MytripCache("as_articlespagekeywords", InstallAndUninstall.moduleName, "nameArticles", "keywords", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageDesc()
        {
            return GeneralMethods.MytripCache("as_articlespagedescription", InstallAndUninstall.moduleName, "nameArticles", "description", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageTitle()
        {
            return GeneralMethods.MytripCache("as_articlespagetitle", InstallAndUninstall.moduleName, "nameArticles", "title", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageKeyWords(string culture)
        {
            return GeneralMethods.MytripCache("as_articlespagekeywords", InstallAndUninstall.moduleName, "nameArticles", "keywords", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageDesc(string culture)
        {
            return GeneralMethods.MytripCache("as_articlespagedescription", InstallAndUninstall.moduleName, "nameArticles", "description", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ArticlesPageTitle(string culture)
        {
            return GeneralMethods.MytripCache("as_articlespagetitle", InstallAndUninstall.moduleName, "nameArticles", "title", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameBlogsPage()
        {
            return GeneralMethods.MytripCache("as_nameblogs", InstallAndUninstall.moduleName, "nameBlogs", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameBlogsPage(string culture)
        {
            return GeneralMethods.MytripCache("as_nameblogs", InstallAndUninstall.moduleName, "nameBlogs", true,culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageKeyWords()
        {
            return GeneralMethods.MytripCache("as_blogspagekeywords", InstallAndUninstall.moduleName, "nameBlogs", "keywords", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageDesc()
        {
            return GeneralMethods.MytripCache("as_blogspagedescription", InstallAndUninstall.moduleName, "nameBlogs", "description", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageTitle()
        {
            return GeneralMethods.MytripCache("as_blogspagetitle", InstallAndUninstall.moduleName, "nameBlogs", "title", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageKeyWords(string culture)
        {
            return GeneralMethods.MytripCache("as_blogspagekeywords", InstallAndUninstall.moduleName, "nameBlogs", "keywords", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageDesc(string culture)
        {
            return GeneralMethods.MytripCache("as_blogspagedescription", InstallAndUninstall.moduleName, "nameBlogs", "description", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string BlogsPageTitle(string culture)
        {
            return GeneralMethods.MytripCache("as_blogspagetitle", InstallAndUninstall.moduleName, "nameBlogs", "title", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameTagsPage()
        {
            return GeneralMethods.MytripCache("as_nametags", InstallAndUninstall.moduleName, "nameTags", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameSearchPage()
        {
            return GeneralMethods.MytripCache("as_namesearch", InstallAndUninstall.moduleName, "nameSearch", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameTagsPage(string culture)
        {
            return GeneralMethods.MytripCache("as_nametags", InstallAndUninstall.moduleName, "nameTags", true,culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameSearchPage(string culture)
        {
            return GeneralMethods.MytripCache("as_namesearch", InstallAndUninstall.moduleName, "nameSearch", true,culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static bool articles()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_articles", InstallAndUninstall.moduleName, "articles", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool blogs()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_blogs", InstallAndUninstall.moduleName, "blogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoClosedComments()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoclosedcomments", InstallAndUninstall.moduleName, "viewInfoClosedComments", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoAuthorArticle()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoauthorarticle", InstallAndUninstall.moduleName, "viewInfoAuthorArticle", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewInfoViewsArticle()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_viewinfoviewsarticle", InstallAndUninstall.moduleName, "viewInfoViewsArticle", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool showRelatedLinks()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_showrelatedlinks", InstallAndUninstall.moduleName, "showRelatedLinks", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool closecountCommentForBlogs()
        {
            return bool.Parse(GeneralMethods.MytripCache("as_closecountcommentforblogs", InstallAndUninstall.moduleName, "closecountCommentForBlogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int countCommentForBlogs()
        {
            return int.Parse(GeneralMethods.MytripCache("as_countcommentforblogs", InstallAndUninstall.moduleName, "countCommentForBlogs", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string roleChiefEditor()
        {
            return UsersSetting.roleChiefEditor();
        }
        public static string roleArticleEditor()
        {
            return GeneralMethods.MytripCache("as_rolearticleeditor", InstallAndUninstall.moduleName, "roleArticleEditor", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string roleBlogger()
        {
            return GeneralMethods.MytripCache("as_roleblogger", InstallAndUninstall.moduleName, "roleBlogger", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialAccordion()
        {
            return GeneralMethods.MytripCache("as_partialaccordion", InstallAndUninstall.moduleName, "partialAccordion", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialNoAccordion()
        {
            return GeneralMethods.MytripCache("as_partialnoaccordion", InstallAndUninstall.moduleName, "partialNoAccordion", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialMenuLogon()
        {
            return GeneralMethods.MytripCache("as_partialmenulogon", InstallAndUninstall.moduleName, "partialMenuLogon", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialMenuLogonWrap()
        {
            return GeneralMethods.MytripCache("as_partialmenulogonwrap", InstallAndUninstall.moduleName, "partialMenuLogonWrap", false, null, 36000, CacheItemPriority.High).ToString();
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