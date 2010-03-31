using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using Mytrip.Core.Models;
using Mytrip.Articles.Models;

namespace Mytrip.Articles.Controllers
{
    [HandleError]
    [Localization]
    public class RssArticleController : Controller
    {
        IArticleRepository _articleRepo;
        public IArticleRepository articleRepo
        {
            get
            {
                if (_articleRepo == null)
                    _articleRepo = new IArticleRepository();
                return _articleRepo;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        public ActionResult RssArticles()
        {

            SyndicationFeed rss = new SyndicationFeed("Articles", string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/0/Articles"));
            rss.Items = (from x in this.articleRepo.article.GetArticlesOpenedNoMenu(culture).ToList()
                         select new SyndicationItem(
                             x.Title, 
                             x.Abstract, 
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        public ActionResult RssBlogs()
        {

            SyndicationFeed rss = new SyndicationFeed("Blogs", string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/0/Blogs"));
            rss.Items = (from x in this.articleRepo.article.GetPosts(culture).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        public ActionResult RssArticlesInCategory(int id, string path,string title)
        {

            SyndicationFeed rss = new SyndicationFeed(title, string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/"+id+"/"+path));
            rss.Items = (from x in this.articleRepo.article.GetArticlesOpenedByCategory(id).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        public ActionResult RssArticlesInTag(int id, string path, string title)
        {

            SyndicationFeed rss = new SyndicationFeed(title, string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/" + id + "/" + path));
            rss.Items = (from x in this.articleRepo.article.GetArticlesPostsOpenedByTag(id,culture).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
    }
}
