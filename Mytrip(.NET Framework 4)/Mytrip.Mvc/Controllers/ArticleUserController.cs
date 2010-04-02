using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Articles;
using Mytrip.Core.Models;
using Mytrip.Articles.Models;
using Mytrip.Core.Repository;

namespace Mytrip.Mvc.Controllers
{
    [HandleError]
    [Localization]
    public class ArticleUserController : Controller
    {
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
        // ****************************************
        // URL: /Article/CreateBlog
        // *****  создать блог  *******
        [Authorize]
        public ActionResult CreateBlog()
        {
            if (articleRepo.comment.GetCountCommentsByUser(User.Identity.Name) >= ArticlesSetting.countCommentForBlogs && articleRepo.category.GetBlogsByUser(User.Identity.Name, culture).Count() == 0)
            {
                if (!roleRepo.IsUserInRoleOnline(ArticlesSetting.roleBlogger))
                    roleRepo.mtUnlockUserInRole(User.Identity.Name, ArticlesSetting.roleBlogger);
                articleRepo.category.CreateBlog(culture);
                var blog = articleRepo.category.GetBlogLast();
                int id = blog.CategoryId;
                string Path = blog.Path;
                return RedirectToAction("Index", "Article", new { pageIndex = 1, pageSize = 10, id, Path });
            }
            else { return RedirectToAction("Index", "Article", new { pageIndex = 1, pageSize = 10, id = 0, Path = "Blogs" }); }
        }

    }
}
