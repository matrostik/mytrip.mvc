using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Articles.Models;
using Mytrip.Core.Models;
using Mytrip.Articles;
using Mytrip.Core.Repository;
using Mytrip.Articles.Repository;

namespace Mytrip.Articles.Controllers
{
    [HandleError]
    [Localization]
    public class ArticleArchiveController : Controller
    {
        #region Properties
        RoleRepository _roleRepo;
        public RoleRepository roleRepo
        {
            get
            {
                if (_roleRepo == null)
                    _roleRepo = new RoleRepository();
                return _roleRepo;
            }
        }
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
        #endregion
        //
        // GET: /ArticleArchive/
        [RoleAdmin]
        public ActionResult Index(int? count)
        {
           int _count = count ?? 5;
            ArchiveIndexModel model = new ArchiveIndexModel();
            model.Count = _count;
            return View(model);
        }
        //
        // GET: /ArticleArchive/Details/path/culture
        [RoleAdmin]
        public ActionResult Details(string path,string culture)
        {
            ArchiveIndexModel model = new ArchiveIndexModel();
            #region Set Page title
            switch (path)
            {
                case "Categories":
                    model.PageTitle = ArticleLanguage.categories_and_subcategories;
                    break;
                case "Articles":
                    model.PageTitle = ArticleLanguage.articles;
                    break;
                case "Comments":
                    model.PageTitle = ArticleLanguage.comments;
                    break;
                case "Blogs":
                    model.PageTitle = ArticleLanguage.blogs;
                    break;
                case "Topics":
                    model.PageTitle = ArticleLanguage.topics;
                    break;
                case "Posts":
                    model.PageTitle = ArticleLanguage.posts;
                    break;
                case "BlogsComments":
                    model.PageTitle = ArticleLanguage.comments_in_blogs;
                    break;
                case "Tags":
                    model.PageTitle = ArticleLanguage.tags1;
                    break;
                case "ClosedArticles":
                    model.PageTitle = ArticleLanguage.closed_articles;
                    break;
                default:
                    model.Path = path;
                    break;
            }
            #endregion
            model.Path = path;
            model.Culture = culture;
            return View(model);
        }
    }
}
