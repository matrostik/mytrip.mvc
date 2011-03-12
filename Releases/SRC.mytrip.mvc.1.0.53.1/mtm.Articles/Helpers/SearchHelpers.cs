using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Helpers;
using mtm.Articles.Repository;
using mtm.Core.Settings;
using System.Web;
using mtm.Articles.Repository.DataEntities;
using System.Web.Mvc;

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
                   title ="<div class='content'>"+articleContent(x, 10000, 100)
                       +"</div><div class='last'></div>";
                   SearchQuestion b = new SearchQuestion(title, x.CreateDate, "Articles");
              searchQuestion.Add(b);
           }
           return searchQuestion;
       }
       private static string articleContent(mytrip_articles article, int content, int imgwidth)
       {
           string result = string.Empty;
           string specification = ArticlesHelper.ArticleInfo("Articles", article, false);
           string rating = "<div class=\"homerating\">" + GeneralMethods.CoreRating(article.ApprovedVotes, false, (double)article.TotalVotes, -1) + "</div>";

           TagBuilder b = new TagBuilder("h3");
           TagBuilder a_title = new TagBuilder("a");
           a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
           a_title.AddCssClass("hometitle");
           a_title.InnerHtml = article.Title;
           TagBuilder a_img = new TagBuilder("a");
           a_img.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
           string imgkeys = string.Empty;
           if (article.OnlyForRegisterUser)
           {
               imgkeys = GeneralMethods.Image("/images/Keys.png", "keys", "imgkeys");
           }
           b.InnerHtml = a_title.ToString();
           string imageAbstract = GeneralMethods.ImageForAbstract2(article.ImageForAbstract, imgwidth);
           if (imageAbstract != null)
               a_img.InnerHtml = imageAbstract;
           StringBuilder tag = new StringBuilder();
           bool tags = false;
           foreach (var t in article.mytrip_articlestag)
           {
               TagBuilder a_tag = new TagBuilder("a");
               a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
               a_tag.InnerHtml = t.TagName;
               tag.AppendLine(a_tag.ToString()); tags = true;
           }

           string abstracts = GeneralMethods.RemoveLinkContent(article.Abstract, content);
           string _tags = string.Empty;
           if (tags)
               _tags = ArticleLanguage.tags + " ";
           result = rating +"<br/>"+ a_img.ToString() + b.ToString() + imgkeys + abstracts + "<div class=\"info\">" + specification + _tags + tag.ToString() + "</div>";

           return result;
       }
    }
}
