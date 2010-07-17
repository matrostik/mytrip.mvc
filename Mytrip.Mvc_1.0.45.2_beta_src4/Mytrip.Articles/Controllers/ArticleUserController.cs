using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Articles;
using Mytrip.Mvc.Models;
using Mytrip.Articles.Models;
using Mytrip.Mvc.Repository;
using Mytrip.Articles.Repository;
using Mytrip.Mvc;
using Mytrip.Articles.Repository.DataEntities;

namespace Mytrip.Articles.Controllers
{
    [HandleError]
    [Localization]
    public class ArticleUserController : Controller
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
        ArticlesSetting _artset;
        public ArticlesSetting artset
        {
            get 
            { 
                if(_artset==null)
                _artset=new ArticlesSetting();
                return _artset;
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

        // ****************************************
        // URL: /Article/CreateBlog
        // *****  создать блог  *******
        [Authorize]
        public ActionResult CreateBlog()
        {
            if (articleRepo.comment.GetCommentsCount(User.Identity.Name) >= artset.countCommentForBlogs() && articleRepo.category.GetBlogsByUser(User.Identity.Name, culture).Count() == 0)
            {
                if (!roleRepo.IsUserInRoleOnline(artset.roleBlogger()))
                    roleRepo.mtUnlockUserInRole(User.Identity.Name, artset.roleBlogger());
                mytrip_articlescategory blog = articleRepo.category.CreateBlog(culture);
                return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = blog.CategoryId, id4 = blog.Path });
            }
            else if (roleRepo.IsUserInRoleOnline(artset.roleBlogger())) {
                mytrip_articlescategory blog = articleRepo.category.CreateBlog(culture);
                return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = blog.CategoryId, id4 = blog.Path });
            }
            else { return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = 0, id4 = "Blogs" }); }
        }
    }
}
