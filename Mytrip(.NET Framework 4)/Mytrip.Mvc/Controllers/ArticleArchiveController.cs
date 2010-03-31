using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Articles.Models;
using Mytrip.Core.Models;
using Mytrip.Articles;
using Mytrip.Core.Repository;

namespace Mytrip.Mvc.Controllers
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
        // GET: /ArticleArchive/Details/5
        [RoleAdmin]
        public ActionResult Details(string path,string culture)
        {
            ArchiveIndexModel model = new ArchiveIndexModel();
            model.PageTitle=path;
            model.Path = path;
            model.Culture = culture;
            return View(model);
        }
    }
}
