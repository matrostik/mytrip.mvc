using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using mtm.Core.Models;
using mtm.Articles.Repository;
using mtm.Core;
using mtm.Core.Settings;

namespace mtm.Articles.Controllers
{
    public class RssArticleController : Controller
    {
        #region Properties
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
        #endregion

        #region Methods
        public ActionResult RssArticles()
        {
            SyndicationFeed rss = new SyndicationFeed("Articles", string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/0/Articles"));
            rss.Items = (from x in this.articleRepo.article.GetArticles(LocalisationSetting.culture()).ToList()
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
            rss.Items = (from x in this.articleRepo.article.GetPosts(LocalisationSetting.culture()).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        public ActionResult RssArticlesInCategory(int id, string id2,string id3)
        {
            string _title = GeneralMethods.UndecodingSearch(id3);
            SyndicationFeed rss = new SyndicationFeed(_title, string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/"+id+"/"+id2));
            rss.Items = (from x in this.articleRepo.article.GetArticles(id, LocalisationSetting.culture()).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        public ActionResult RssArticlesInTag(int id, string id2, string id3)
        {
            string _title = GeneralMethods.UndecodingSearch(id3);
            SyndicationFeed rss = new SyndicationFeed(_title, string.Empty,
                new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/Index/1/10/" + id + "/" + id2));
            rss.Items = (from x in this.articleRepo.article.GetByTag(LocalisationSetting.culture(), id).ToList()
                         select new SyndicationItem(
                             x.Title,
                             x.Abstract,
                             new Uri("http://" + Request.ServerVariables["HTTP_HOST"] + "/Article/View/" + x.ArticleId + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        #endregion
    }
}
