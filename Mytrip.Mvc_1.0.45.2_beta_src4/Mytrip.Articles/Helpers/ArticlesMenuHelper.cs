using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Articles.Repository;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Articles.Repository.DataEntities;

namespace Mytrip.Articles.Helpers
{
   public static class ArticlesMenuHelper
    {
       #region MenuHelpers
        
        public static string _HP(bool blog, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            IArticleRepository ar = new IArticleRepository();
            ArticlesSetting artset = new ArticlesSetting();
            int take = line * column;
            string CategoryName = string.Empty;
            string LangName = string.Empty;
            IQueryable<mytrip_articles> articles = null;
            if (!blog)
            {
                articles = ar.article.GetArticles(HttpContext.Current.Session["culture"].ToString(), take);
                CategoryName = "Articles";
                LangName = "<a href=\"/Article/Index/1/10/0/Articles\">" + artset.NameArticlesPage() + "</a>";
            }
            else
            {
                articles = ar.article.GetPosts(take, HttpContext.Current.Session["culture"].ToString());
                CategoryName = "Blogs";
                LangName = "<a href=\"/Article/Index/1/10/0/Blogs\">" + artset.NameBlogsPage() + "</a>";
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
                if (imgwidth > 0)
                {
                    _content = articleMax(article, content, imgwidth);
                }
                else
                {
                    _content = articleMin(article, content);
                }
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

        public static string articleMin(mytrip_articles article, int content)
        {
            string result = string.Empty;
            string specification = ArticlesHelper.ArticleInfo("Articles", article, false);
            string rating = GeneralMethods.CoreRating(article.ApprovedVotes, false, (double)article.TotalVotes, -1);

            TagBuilder b = new TagBuilder("b");
            TagBuilder a_title = new TagBuilder("a");
            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
            a_title.AddCssClass("hometitle");
            a_title.InnerHtml = article.Title;
            if (article.OnlyForRegisterUser)
            {
                ThemeSetting theme = new ThemeSetting();
                b.InnerHtml = a_title + GeneralMethods.Image("/Theme/" + theme.theme() + "/images/Keys.png", 20, 0, "", 0);
            }
            else
                b.InnerHtml = a_title.ToString();
            StringBuilder tag = new StringBuilder();
            foreach (var t in article.mytrip_articlestag)
            {
                TagBuilder a_tag = new TagBuilder("a");
                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                a_tag.InnerHtml = t.TagName;
                tag.AppendLine(a_tag.ToString());
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
            result = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
            result = result.Replace("<br/><br/>", "<br/>");
            result = result.Replace("<br/><br/>", "<br/>");
            result = result.Replace("<br/><br/>", "<br/>");
            return result;
        }

        public static string articleMax(mytrip_articles article, int content, int imgwidth)
        {
            string result = string.Empty;
            string specification = ArticlesHelper.ArticleInfo("Articles", article, false);
            string rating = GeneralMethods.CoreRating(article.ApprovedVotes, false, (double)article.TotalVotes, -1);

            TagBuilder b = new TagBuilder("b");
            TagBuilder a_title = new TagBuilder("a");
            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
            a_title.AddCssClass("hometitle");
            a_title.InnerHtml = article.Title;
            TagBuilder a_img = new TagBuilder("a");
            a_img.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);

            if (article.OnlyForRegisterUser)
            {
                ThemeSetting theme = new ThemeSetting();
                b.InnerHtml = a_title + GeneralMethods.Image("/Theme/" + theme.theme() + "/images/Keys.png", 20, 0, "", 0);
            }
            else
                b.InnerHtml = a_title.ToString();
            //TagBuilder p = new TagBuilder("p");
            string imageAbstract = GeneralMethods.ImageForAbstract(article.ImageForAbstract, imgwidth);
            if (imageAbstract != null)
                a_img.InnerHtml = imageAbstract;
            StringBuilder tag = new StringBuilder();
            foreach (var t in article.mytrip_articlestag)
            {
                TagBuilder a_tag = new TagBuilder("a");
                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                a_tag.InnerHtml = t.TagName;
                tag.AppendLine(a_tag.ToString());
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
            result = a_img.ToString() + b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
            result = result.Replace("<br/><br/>", "<br/>");
            result = result.Replace("<br/><br/>", "<br/>");
            result = result.Replace("<br/><br/>", "<br/>");
            return result;
        }
        #endregion
    }
}
