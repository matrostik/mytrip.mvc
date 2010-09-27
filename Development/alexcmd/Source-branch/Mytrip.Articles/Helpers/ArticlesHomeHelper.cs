using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Articles.Repository;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Articles.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Articles.Helpers
{
   public static class ArticlesHomeHelper
    {
       #region MenuHelpers
        
        public static string _HP(bool blog, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            IArticleRepository ar = new IArticleRepository();
            int take = line * column;
            string CategoryName = string.Empty;
            string LangName = string.Empty;
            IQueryable<mytrip_articles> articles = null;
            if (!blog)
            {
                articles = ar.article.GetArticles(LocalisationSetting.culture(), take);
                CategoryName = "Articles";
                LangName = "<a href=\"/Article/Index/1/10/0/Articles\">" + ModuleSetting.NameArticlesPage() + "</a>";
            }
            else
            {
                articles = ar.article.GetPosts(LocalisationSetting.culture(), take);
                CategoryName = "Blogs";
                LangName = "<a href=\"/Article/Index/1/10/0/Blogs\">" + ModuleSetting.NameBlogsPage() + "</a>";
            }
            int _count = articles.Count();
            if (column > _count)
                column = _count;
            int _count2 = 0;
            int _line = 1;
            if (_count > column)
            {
                Math.DivRem(_count, column, out _count2);
                _line = (int)Math.Ceiling((double)_count / column);
            }
            int count = 1;
            int tr = 0;
            int width = 100;
            if (column > 0)
                width = 100 / column;
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            string _content = string.Empty;
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;
            foreach (mytrip_articles article in articles)
            {

                _content = articleContent(article, content, imgwidth);
                
                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                    count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
                tr = tr2;
                _line2 = _line3;
                count++;
            }
            if (tr > 0 && tr % 2 != 0)
                result.AppendLine(finaltr);
            table.AddCssClass(styletable);
            table.InnerHtml = result.ToString(); 
            string _CategoryName = string.Empty;
            if (viewtitle)
                _CategoryName = "<h3 class=\"title\">" + LangName + " " + ArticlesHelper._ArticleRssLink(CategoryName, CategoryName, 0, 14) + "</h3>";
            if (column > 0)
                return _CategoryName + start + table.ToString() + end;
            else
                return string.Empty;
        }

        public static string articleContent(mytrip_articles article, int content, int imgwidth)
        {
            string result = string.Empty;
            string specification = ArticlesHelper.ArticleInfo("Articles", article, false);
            string rating ="<div class=\"homerating\">"+ GeneralMethods.CoreRating(article.ApprovedVotes, false, (double)article.TotalVotes, -1)+"</div>";

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
                imgkeys = GeneralMethods.Image("/images/Keys.png", "", "imgkeys");
            }
                b.InnerHtml = a_title.ToString();
            //TagBuilder p = new TagBuilder("p");
            string imageAbstract = GeneralMethods.ImageForAbstract(article.ImageForAbstract, imgwidth);
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
            char[] sec = { '<', '>' };
            string[] _desc = article.Abstract.Split(sec);
            bool _sec = true;
            string desc2 = string.Empty;
            foreach (string x in _desc)
            {
                if (_sec && x.IndexOf("href=") == -1 && x.IndexOf("src=") == -1 && x.IndexOf("href =") == -1 && x.IndexOf("src =") == -1)
                {
                    desc2 += x;
                    _sec = false;
                }
                else
                    _sec = true;

            }
            string abstracts = string.Empty;
            if (desc2.Length <= content)
                abstracts = article.Abstract;
            else
                abstracts = desc2.Remove(content) + "...";
            abstracts = abstracts.Replace("<BR>", " ");
            abstracts = abstracts.Replace("<BR/>", " ");
            abstracts = abstracts.Replace("<br/>", " ");
            string _tags = string.Empty;
            if (tags)
                _tags = ArticleLanguage.tags+" ";
            result = rating + a_img.ToString() + b.ToString() +imgkeys+ abstracts + "<div class=\"info\">" + specification + _tags + tag.ToString()+"</div>";
            
            return result;
        }
        #endregion
    }
}
