using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Helpers;
using mtm.Articles.Repository;
using mtm.Core.Settings;
using System.Web;

namespace mtm.Articles.Helpers
{
   public static class SearchHelpers
    {
       public static List<SearchQuestion> Search(string search)
       {
           IArticleRepository artrepo = new IArticleRepository();
           List<SearchQuestion> searchQuestion = new List<SearchQuestion>();
           var a = artrepo.article.GetArticles(LocalisationSetting.culture(), search);
           foreach (var x in a)
           {
               string title = null;
                   if (x.Title != null && x.Title.Length > 1)
                       x.Title = GeneralMethods.ReplaceString(x.Title, search);
                   if (x.Abstract != null && x.Abstract.Length > 1)
                       x.Abstract = GeneralMethods.ReplaceString(x.Abstract, search);
                   title ="<div class='content'>"+ArticlesHomeHelper.articleContent(x, 10000, 100)
                       +"</div><div class='last'></div>";
              SearchQuestion b = new SearchQuestion(title,x.CreateDate,"Articles");
              searchQuestion.Add(b);
           }
           return searchQuestion;
       }
    }
}
