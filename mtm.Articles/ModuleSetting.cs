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
        public static string NameBlogsPage()
        {
            return GeneralMethods.MytripCache("as_nameblogs", InstallAndUninstall.moduleName, "nameBlogs", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameTagsPage()
        {
            return GeneralMethods.MytripCache("as_nametags", InstallAndUninstall.moduleName, "nameTags", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string NameSearchPage()
        {
            return GeneralMethods.MytripCache("as_namesearch", InstallAndUninstall.moduleName, "nameSearch", true, null, 36000, CacheItemPriority.High).ToString();
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