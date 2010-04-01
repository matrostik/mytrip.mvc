using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Mytrip.Core;

namespace Mytrip.Articles
{
   public class ArticlesSetting
   {
       /// <summary>
       /// /////////////////////////////////////
       /// </summary>
       public static string connectionString = CoreSetting.connectionStringSQL("ArticlesEntities");
       public static bool articles = bool.Parse(ConfigurationManager.AppSettings["articles"]);
       public static bool blogs = bool.Parse(ConfigurationManager.AppSettings["blogs"]);
       public static int countCommentForBlogs = int.Parse(ConfigurationManager.AppSettings["countCommentForBlogs"]);
       public static string roleChiefEditor = ConfigurationManager.AppSettings["roleChiefEditor"];
       public static string roleArticleEditor = ConfigurationManager.AppSettings["roleArticleEditor"];
       public static string roleBlogger = ConfigurationManager.AppSettings["roleBlogger"];      
    }
}