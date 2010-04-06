//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Articles.Repository;
using Mytrip.Core.Models;
using System.Text;
using Mytrip.Core.Repository;
using Mytrip.Core;
using Mytrip.Articles.Models;
using Mytrip.Core.Repository.MsSqlUsers;

namespace Mytrip.Articles.Controllers
{
    [HandleError]
    [Localization]
    public class ArticleController : Controller
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
        private bool userHasRights(object obj, bool forAdd)
        {
            string categoryUserName = "";
            string category2UserName = "";
            string articleUserName = "";
            bool isBlog = true;
            RoleRepository db = new RoleRepository();
            if (!User.Identity.IsAuthenticated)
                return false;
            if (db.IsUserInRoleOnline(ArticlesSetting.roleChiefEditor))
                return true;
            else
            {
                if (obj is mytrip_ArticlesCategory)
                {
                    mytrip_ArticlesCategory category = obj as mytrip_ArticlesCategory;
                    categoryUserName = category.UserName;
                    category2UserName = category.mytrip_ArticlesCategory2.UserName;
                    isBlog = category.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_Articles)
                {
                    mytrip_Articles article = obj as mytrip_Articles;
                    articleUserName = article.UserName;
                    categoryUserName = article.mytrip_ArticlesCategory.UserName;
                    category2UserName = article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.UserName;
                    isBlog = article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_ArticlesComments)
                {
                    mytrip_ArticlesComments comment = obj as mytrip_ArticlesComments;
                    if (!comment.IsAnonym && User.Identity.Name == comment.UserName)
                        return true;
                    articleUserName = comment.mytrip_Articles.UserName;
                    categoryUserName = comment.mytrip_Articles.mytrip_ArticlesCategory.UserName;
                    category2UserName = comment.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.UserName;
                    isBlog = comment.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_ArticlesTag)
                {
                    forAdd = true;
                    isBlog = false;
                }
                bool articleEditor = db.IsUserInRoleOnline(ArticlesSetting.roleArticleEditor);
                if (db.IsUserInRoleOnline(ArticlesSetting.roleBlogger) || articleEditor)
                {
                    if (User.Identity.Name == articleUserName || User.Identity.Name == categoryUserName || User.Identity.Name == category2UserName)
                        return true;
                    if (forAdd && articleEditor && (!isBlog || obj == null))
                        return true;
                }
            }
            return false;
        }
        #endregion
        #region Index
        // **************************************
        // URL: /Article/Index/pageIndex?/pageSize?/categoryId/type
        // ** Cтатьи, посты в рубрике или подрубрике или блоге **
        public ActionResult Index(int? pageIndex, int? pageSize, int id, string Path)
        {
            int total = 0;
            int _pageIndex = pageIndex ?? 1;
            int _pageSize = pageSize ?? 10;
            ArticleIndexModel model = new ArticleIndexModel();
            model.ShowAddCategory = true;
            model.ShowAddSubCategory = true;
            model.ShowAddArticle = true;
            model.ShowEditDelete = true;
            model.ShowEditDeleteBlog = false;
            model.ShowAddBlog = false;
            model.ShowAddPost = false;
            model.ShowDetailsBlog = false;
            model.ParentCategory = new mytrip_ArticlesCategory { CategoryId = -1, Title = string.Empty, Path = string.Empty };
            mytrip_ArticlesCategory mc = articleRepo.category.GetCategory(id);
            if (Path == "Articles")
            {
                model.Categories = articleRepo.category.GetCategoriesNotInMenu(culture);
                if (model.Categories == null)
                    model.ShowAddArticle = false;
                model.ShowEditDelete = false;
                model.ShowAddSubCategory = false;
                model.PageTitle = ArticleLanguage.articles;
                model.Articles = articleRepo.article.GetArticlesOpenedPagedNoMenu((int)_pageIndex, (int)_pageSize, culture, out total);
            }
            else if (Path == "Blogs")
            {
                model.Categories = articleRepo.category.GetBlogs((int)_pageIndex, (int)_pageSize, culture, out total);
                model.ShowEditDelete = false;
                model.PageTitle = ArticleLanguage.blogs;
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
            }
            else if (Path.StartsWith("(Tag)"))
            {
                model.PageTitle = ArticleLanguage.articles_for_tag + " " + articleRepo.article.GetTag(id).TagName;
                model.Articles = articleRepo.article.GetArticlesPostsOpenedByTagPaged((int)_pageIndex, (int)_pageSize, id, culture, out total);
                model.Categories = new List<mytrip_ArticlesCategory>(0).AsQueryable<mytrip_ArticlesCategory>();
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = true;
            }
            else if (Path.StartsWith("(Search)"))
            {
                string search = Path.Remove(0, 8).TrimEnd();
                search = StaticMethod.DecodingSearch2(search);
                IQueryable<mytrip_Articles> articles = articleRepo.article.GetArticlesOpenedBySearchPaged(search, (int)_pageIndex, (int)_pageSize, out total, culture);
                foreach (var art in articles)
                {
                    art.Title = StaticMethod.ReplaceString(art.Title, search);
                    art.Abstract = StaticMethod.ReplaceString(art.Abstract, search);
                }
                model.Articles = articles;
                model.Categories = new List<mytrip_ArticlesCategory>(0).AsQueryable<mytrip_ArticlesCategory>();
                model.PageTitle = String.Format(ArticleLanguage.search_for_found_results, search, total);
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = false;
            }
            else
            {
                model.Categories = articleRepo.category.GetSubCategories(id, culture);
                model.Articles = articleRepo.article.GetArticlesOpenedByCategoryPaged(id, (int)_pageIndex, (int)_pageSize, out total, culture);
                model.PageTitle = mc.Title;
                if (mc.CategoryId != mc.SubCategoryId)
                {
                    model.ShowAddSubCategory = false;
                    if (mc.mytrip_ArticlesCategory2.Blog)
                    {
                        model.ShowDetailsBlog = true;
                        model.ShowEditDelete = false;
                        model.ShowAddCategory = false;
                        model.ShowAddArticle = false;
                        model.ShowEditDeleteBlog = true;
                        model.ShowAddPost = true;
                        articleRepo.category.BlogViewsIncrease(id);
                    }
                    model.ParentCategory = articleRepo.category.GetCategory(mc.SubCategoryId);
                }
                else if (!mc.Blog && !mc.SeparateBlock)
                {
                    model.ParentCategory = new mytrip_ArticlesCategory { CategoryId = 0, Title = ArticleLanguage.articles, Path = "Articles" };
                }
                else if (mc.Blog)
                {
                    model.ShowDetailsBlog = true;
                    model.ShowEditDelete = false;
                    model.ShowAddCategory = false;
                    model.ShowAddSubCategory = false;
                    model.ShowAddArticle = false;
                    model.ShowEditDeleteBlog = true;
                    model.ShowAddBlog = true;
                    model.ShowAddPost = true;
                    model.ParentCategory = articleRepo.category.GetCategory(id);
                    articleRepo.category.BlogViewsIncrease(id);
                }
            }
            model.Total = total;
            model.DefaultCount = 10;
            model.Path = Path;
            model.CategoryId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string search, string url)
        {
            if (search != string.Empty)
            {
                search = StaticMethod.DecodingSearch(search);
                return RedirectToAction("Index", "Article", new { pageIndex = 1, pageSize = 10, id = 0, Path = "(Search)" + search.Trim() });
            }
            else
                return Redirect(url);
        }
        #endregion
        #region Category and Tags Actions
        // ******************************************
        // URL: /Article/CreateCategory/CategoryId/Param
        // * создать рубрику, подрубрику или тему блога *
        [Authorize]
        public ActionResult CreateCategory(int id, string Path)
        {
            mytrip_ArticlesCategory category = articleRepo.category.GetCategory(id);
            if (!userHasRights(category, true))
            {
                string returnUrl = "/Article/CreateCategory/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            CategoryModel model = new CategoryModel();
            model.Path = Path;
            model.CategoryId = id;
            if (id != 0)
            {
                string title = category.Title;
                if (!category.mytrip_ArticlesCategory2.AllCulture)
                    model.ShowAllCulture = "none";
                model.ShowSeparateBlock = "none";
                if (category.Blog)
                    model.PageTitle = ArticleLanguage.create_topic_for + " " + title;
                else
                    model.PageTitle = ArticleLanguage.create_subcategory_for + " " + title;
            }
            else
                model.PageTitle = ArticleLanguage.create_new_category;
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                mytrip_ArticlesCategory mac;
                if (model.CategoryId != 0)
                {
                    mac = articleRepo.category.CreateSubCategory(model.Title, model.CategoryId, model.AllCulture);
                    model.Path = mac.Path;
                    model.CategoryId = mac.SubCategoryId;
                }
                else
                {
                    mac = articleRepo.category.CreateСategory(model.Title, model.SeparateBlock, model.AllCulture, culture);
                    if (model.SeparateBlock)
                    {
                        model.Path = mac.Path;
                        model.CategoryId = mac.CategoryId;
                    }
                    else
                        model.Path = "Articles";
                }

                return RedirectToAction("Index", new { pageIndex = 1, pageSize = 10, id = model.CategoryId, Path = model.Path });
            }
            model.PageTitle = ArticleLanguage.create_category;
            return View(model);
        }
        // ********************************************
        // URL: /Article/EditCategory/categoryId/Path
        // * редактировать рубрику, подрубрику или тему блога *
        [Authorize]
        public ActionResult EditCategory(int id, string Path, string url)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryId = id;
            if (!Path.StartsWith("(Tag)"))
            {
                mytrip_ArticlesCategory mc = articleRepo.category.GetCategory(id);
                if (!userHasRights(mc, false))
                {
                    string returnUrl = "/Article/EditCategory/" + id + "/" + Path;
                    return RedirectToAction("LogOn", "Account", new { returnUrl });
                }
                model.Title = mc.Title;
                model.AllCulture = mc.AllCulture;
                model.SeparateBlock = mc.SeparateBlock;
                if (mc.Blog || mc.CategoryId != mc.SubCategoryId)
                {
                    model.ShowSeparateBlock = "none";
                    if (!mc.mytrip_ArticlesCategory2.AllCulture)
                        model.ShowAllCulture = "none";
                }
                model.PageTitle = ArticleLanguage.edit + " " + mc.Title;
                model.Path = mc.Path;
                if (Path == "Archive")
                    model.Url = url.Replace("(x)", "/");
                else
                    model.Url = Url.Action("Index", new { pageIndex = 1, pageSize = 10, id = mc.CategoryId, Path = mc.Path });
            }
            else
            {
                mytrip_ArticlesTag tag = articleRepo.article.GetTag(id);
                if (!userHasRights(tag, false))
                {
                    string returnUrl = "/Article/EditCategory/" + id + "/" + Path;
                    return RedirectToAction("LogOn", "Account", new { returnUrl });
                }
                model.Title = tag.TagName;
                model.ShowSeparateBlock = "none";
                model.ShowAllCulture = "none";
                model.PageTitle = ArticleLanguage.edit_tag + " " + tag.TagName;
                model.Path = tag.Path;
                if (Path.EndsWith("Archive"))
                    model.Url = url.Replace("(x)", "/");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Path == "Blogs")
                    articleRepo.category.UpdateBlog(model.CategoryId, model.Title);
                else if (model.Path.StartsWith("(Tag)"))
                    articleRepo.article.UpdateTag(model.CategoryId, model.Title);
                else
                    articleRepo.category.UpdateCategory(model.CategoryId, model.Title, model.SeparateBlock, model.AllCulture);
                if (model.Path.EndsWith("Archive"))
                    return Redirect(model.Url.Replace("(x)", "/"));
                else
                    return RedirectToAction("Index", new { pageIndex = 1, pageSize = 10, id = model.CategoryId, Path = model.Path });
            }
            if (model.Path.StartsWith("(Tag)"))
            {
                model.PageTitle = ArticleLanguage.edit_tag;
                model.ShowSeparateBlock = "none";
                model.ShowAllCulture = "none";
            }
            else
                model.PageTitle = ArticleLanguage.edit_category;
            return View(model);
        }
        // **********************************************
        // URL: /Article/DeleteCategory/id/Path/
        // *****  удалить рубрику или подрубрику  *******
        [Authorize]
        public ActionResult DeleteCategory(int id, string Path, string url)
        {
            if (!Path.StartsWith("(Tag)"))
            {
                mytrip_ArticlesCategory mc = articleRepo.category.GetCategory(id);
                if (!userHasRights(mc, false))
                {
                    string returnUrl = "/Article/DeleteCategory/" + id + "/" + Path;
                    return RedirectToAction("LogOn", "Account", new { returnUrl });
                }
                articleRepo.category.DeleteCategory(id);
                if (Path == "Archive")
                    return Redirect(url.Replace("(x)", "/"));
                if (mc.CategoryId != mc.SubCategoryId)
                    id = mc.SubCategoryId;
                else if (mc.Blog)
                    Path = "Blogs";
                else
                {
                    id = 0;
                    Path = "Articles";
                }
            }
            else
            {
                mytrip_ArticlesTag tag = articleRepo.article.GetTag(id);
                if (!userHasRights(tag, false))
                {
                    string returnUrl = "/Article/DeleteCategory/" + id + "/" + Path;
                    return RedirectToAction("LogOn", "Account", new { returnUrl });
                }
                articleRepo.article.DeleteTag(id);
                if (Path.EndsWith("Archive"))
                    return Redirect(url.Replace("(x)", "/"));
                Path = "Articles";
            }
            return RedirectToAction("Index", new { pageIndex = 1, pageSize = 10, id, Path });
        }

        #endregion
        #region Articles Actions
        // **************************************
        // URL: /Article/View/ArticleId
        // ******  одна статья или пост  ********
        public ActionResult View(int id)
        {
            ArticleViewModel model = new ArticleViewModel();
            mytrip_Articles article = articleRepo.article.GetArticleById(id);
            if (article.OnlyForRegisterUser && !User.Identity.IsAuthenticated)
            {
                string returnUrl = "/Article/View/" + id + "/" + article.Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            model.Article = article;
            articleRepo.article.IncrementArticleViews(id);
            model.Comments = articleRepo.comment.GetCommentsByArticleAsc(id);

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult View(ArticleViewModel model, int id, string input0, string input1, string input2, string input3, string input4)
        {
            if (Request.IsAjaxRequest())
            {
                return Content(Vote(id, input0, input1, input2, input3, input4) + "<br/>" + ArticleLanguage.thanks_for_vote);
            }
            try
            {
                if (User.Identity.IsAuthenticated)
                    articleRepo.comment.CreateComment(id, model.Comment);
                else
                    articleRepo.comment.CreateComment(id, model.Comment, model.AnonymName, model.AnonymEmail);
                return RedirectToAction("View", new { id = id, Path = articleRepo.article.GetArticleById(id).Path });
            }
            catch
            {
                mytrip_Articles article = articleRepo.article.GetArticleById(id);
                model.Article = article;
                articleRepo.article.IncrementArticleViews(id);
                model.Comments = articleRepo.comment.GetCommentsByArticleAsc(id);
                return View(model);
            }

        }
        // *************************************
        // URL: /Article/Create/CategoryId/Path
        // ****  Создать статью или пост  ******
        [Authorize]
        public ActionResult Create(int id, string Path)
        {
            mytrip_ArticlesCategory cat = articleRepo.category.GetCategory(id);
            if (!userHasRights(cat, true))
            {
                string returnUrl = "/Article/Create/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            ArticleModel model = new ArticleModel();
            model.CategoryId = id;
            model.PageTitle = ArticleLanguage.create_new_article;
            model.Path = Path;
            model.CloseDate = new DateTime(2099, 12, 12).ToString("yyyy-MM-dd");
            if (id == 0)
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(culture), "CategoryId", "Title");
            else
            {
                if (!cat.AllCulture)
                    model.ShowAllCulture = "none";
                if (cat.mytrip_ArticlesCategory2.Blog)
                {
                    model.PageTitle = ArticleLanguage.create_new_post;
                    model.ShowArticleOptions = "none";
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(id, culture), "CategoryId", "Title");
                    if (!cat.Blog)
                        model.Categories = null;
                }
                else if (cat.SeparateBlock)
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(id, culture), "CategoryId", "Title");
                else
                    model.Categories = null;
            }
            #region tags
            model.Tags = articleRepo.article.GetAllTags();
            #endregion
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                mytrip_ArticlesCategory cat = articleRepo.category.GetCategory(model.CategoryId);
                mytrip_Articles article;
                if (!cat.mytrip_ArticlesCategory2.Blog)
                {
                    article = articleRepo.article.CreateArticle(model.CategoryId, model.Title, model.Abstract, model.Body
                          , model.ApprovedComment, model.ImageForAbstract, model.OnlyForRegisterUser, model.ApprovedVotes, model.IncludeAnonymComment
                          , DateTime.Parse(model.CloseDate), model.AllCulture);
                }
                else
                {
                    article = articleRepo.article.CreatePost(model.CategoryId, model.Title, model.Abstract, model.Body
                         , model.ImageForAbstract, model.OnlyForRegisterUser, model.IncludeAnonymComment);
                }
                #region tags
                IQueryable<mytrip_ArticlesTag> ts = articleRepo.article.GetAllTags();
                foreach (var tag in ts.ToList())
                {
                    if (Boolean.Parse(Request.Form.GetValues("tag" + tag.TagId.ToString())[0]))
                    {
                        articleRepo.article.AddTagInArticle(article.ArticleId, tag.TagId);
                    }
                }
                if (model.NewTags != null)
                {
                    string[] tags = model.NewTags.Split(',');
                    foreach (string s in tags)
                    {
                        mytrip_ArticlesTag mat = articleRepo.article.CreateTag(s);
                        articleRepo.article.AddTagInArticle(article.ArticleId, mat.TagId);
                    }
                }
                #endregion
                return RedirectToAction("View", new { id = article.ArticleId, Path = article.Path });
            }
            return View(model);

        }
        // *************************************
        // URL: /Article/Edit/ArticleId/Path
        // *** редактировать статью или пост ***
        [Authorize]
        public ActionResult Edit(int id, string Path, string url)
        {
            ArticleModel model = new ArticleModel();
            mytrip_Articles article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
            {
                string returnUrl = "/Article/Edit/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            #region Set article data
            model.ArticleId = article.ArticleId;
            model.CategoryId = article.CategoryId;
            model.Title = article.Title;
            model.ImageForAbstract = article.ImageForAbstract;
            model.Abstract = article.Abstract;
            model.Body = article.Body;
            model.CloseDate = article.CloseDate.ToString("yyyy-MM-dd");
            model.ApprovedComment = article.ApprovedComment;
            model.OnlyForRegisterUser = article.OnlyForRegisterUser;
            model.ApprovedVotes = article.ApprovedVotes;
            model.IncludeAnonymComment = article.IncludeAnonymComment;
            model.AllCulture = article.AllCulture;
            #endregion
            model.PageTitle = ArticleLanguage.edit + " \"" + article.Title + "\"";
            model.Path = Path;
            if (Path == "Archive")
                model.Url = url.Replace("(x)", "/");
            else
                model.Url = Url.Action("View", new { id = article.ArticleId, Path = article.Path });
            if (articleRepo.category.GetCategory(article.CategoryId).mytrip_ArticlesCategory2.Blog)
            {
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(article.mytrip_ArticlesCategory.SubCategoryId, culture), "CategoryId", "Title");
                model.ShowArticleOptions = "none";
            }
            else
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(culture), "CategoryId", "Title", article.CategoryId);
            #region tags
            model.Tags = articleRepo.article.GetAllTags();
            #endregion
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleModel model)
        {
            try
            {
                articleRepo.article.UpdateArtiсle(model.ArticleId, model.CategoryId, model.Title, model.Abstract, model.Body
                    , model.ApprovedComment, model.ImageForAbstract, model.OnlyForRegisterUser, model.ApprovedVotes, model.IncludeAnonymComment
                    , DateTime.Parse(model.CloseDate), model.AllCulture);
                IQueryable<mytrip_ArticlesTag> ts = articleRepo.article.GetAllTags();
                foreach (var tag in ts.ToList())
                {
                    if (Boolean.Parse(Request.Form.GetValues("tag" + tag.TagId.ToString())[0]))
                        articleRepo.article.AddTagInArticle(model.ArticleId, tag.TagId);
                    else
                        articleRepo.article.DeleteTagFromArticle(model.ArticleId, tag.TagId);
                }
                if (model.NewTags != null)
                {
                    string[] tags = model.NewTags.Split(',');
                    foreach (string s in tags)
                    {
                        mytrip_ArticlesTag mat = articleRepo.article.CreateTag(s);
                        articleRepo.article.AddTagInArticle(model.ArticleId, mat.TagId);
                    }
                }
                if (model.Path == "Archive")
                    return Redirect(model.Url.Replace("(x)", "/"));
                else
                    return RedirectToAction("View", new { id = model.ArticleId, Path = model.Path });
            }
            catch
            {
                return View(model);
            }
        }
        // ****************************************
        // URL: /Article/Delete/Id/Path/
        // *****  удалить статью или пост  *******
        [Authorize]
        public ActionResult Delete(int id, string Path, string url)
        {
            mytrip_Articles mc = articleRepo.article.GetArticleById(id);
            if (!userHasRights(mc, false))
            {
                string returnUrl = "/Article/Delete/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            if (Path == "Archive")
            {
                articleRepo.article.DeleteArticle(id);
                return Redirect(url.Replace("(x)", "/"));
            }
            else
            {
                Path = mc.mytrip_ArticlesCategory.Path;
                int catid = mc.CategoryId;
                articleRepo.article.DeleteArticle(id);
                return RedirectToAction("Index", new { pageIndex = 1, pageSize = 10, id = catid, Path });
            }
        }
        /// <summary>
        /// Vote
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input0"></param>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="input3"></param>
        /// <param name="input4"></param>
        /// <returns></returns>
        private string Vote(int id, string input0, string input1, string input2, string input3, string input4)
        {
            int votes = 0;
            if (input0 != null)
                votes = 1;
            if (input1 != null)
                votes = 2;
            if (input2 != null)
                votes = 3;
            if (input3 != null)
                votes = 4;
            if (input4 != null)
                votes = 5;
            double total = (double)articleRepo.article.GetNewTotalVotes(id, votes);
            int countvotes = articleRepo.article.GetArticleById(id).mytrip_ArticlesVotes.Count();
            StringBuilder result = new StringBuilder();
            result.AppendLine(ArticleLanguage.total_votes + ": " + countvotes + ". ");
            int rate = 0;
            while (rate < 5)
            {
                double rate12 = rate + 0.125;
                double rate37 = rate + 0.375;
                double rate62 = rate + 0.625;
                double rate87 = rate + 0.875;
                TagBuilder input = new TagBuilder("img");
                if (total > rate12 && total < rate37)
                    input.MergeAttribute("src", "/Content/images/star25.png");
                if (total > rate37 && total < rate62)
                    input.MergeAttribute("src", "/Content/images/star50.png");
                if (total > rate62 && total < rate87)
                    input.MergeAttribute("src", "/Content/images/star75.png");
                if (total < rate87)
                    input.MergeAttribute("src", "/Content/images/star100.png");
                if (total > rate87)
                    input.MergeAttribute("src", "/Content/images/star.png");
                rate++;
                input.MergeAttribute("style", "width: 15px; height: 15px; border-width: 0px;");
                result.AppendLine(input.ToString());
            }
            return result.ToString();
        }
        #endregion
        #region Comments Actions
        // *****************************************
        // URL: /Article/EditComment/id/Path/
        // ******  редактировать комментарий  ******
        [Authorize]
        public ActionResult EditComment(int id, string Path, string url)
        {
            mytrip_ArticlesComments comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
            {
                string returnUrl = "/Article/EditComment/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            CommentModel model = new CommentModel();
            model.CommentId = id;
            model.Comment = comment.Body;
            model.ArticleId = comment.ArticleId;
            model.Path = comment.mytrip_Articles.Path;
            model.PageTitle = ArticleLanguage.edit_comment;
            if (Path == "Archive")
                model.Url = url.Replace("(x)", "/");
            else
                model.Url = Url.Action("View", new { id = comment.mytrip_Articles.ArticleId, Path = comment.mytrip_Articles.Path });
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult EditComment(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                articleRepo.comment.UpdateComment(model.CommentId, model.Comment);
                mytrip_ArticlesComments comment = articleRepo.comment.GetComment(model.CommentId);
                comment.Body = model.Comment;
                if (model.Path == "Archive")
                    return Redirect(model.Url.Replace("(x)", "/"));
                else
                    return RedirectToAction("View", new { id = comment.ArticleId, Path = comment.mytrip_Articles.Path });
            }
            return View(model);
        }
        // **********************************************
        // URL: /Article/DeleteComment/id/Path/
        // *****  удалить комментарий  *******
        [Authorize]
        public ActionResult DeleteComment(int id, string Path, string url)
        {
            mytrip_ArticlesComments comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
            {
                string returnUrl = "/Article/DeleteComment/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            articleRepo.comment.DeleteComment(id);
            if (Path == "Archive")
                return Redirect(url.Replace("(x)", "/"));
            else
                return RedirectToAction("View", new { id = comment.ArticleId, Path = Path });
        }
        // **********************************************
        // URL: /Article/OpenComments/ArticleId/Path/
        // *******  open comments  *********
        [Authorize]
        public ActionResult OpenComments(int id, string Path)
        {
            var article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
            {
                string returnUrl = "/Article/OpenComments/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            articleRepo.article.OpenComments(id);
            return RedirectToAction("View", new { id = id, Path = Path });
        }
        // **********************************************
        // URL: /Article/CloseComments/ArticleId/Path/
        // *****  close comments  *******
        [Authorize]
        public ActionResult CloseComments(int id, string Path)
        {
            var article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
            {
                string returnUrl = "/Article/CloseComments/" + id + "/" + Path;
                return RedirectToAction("LogOn", "Account", new { returnUrl });
            }
            articleRepo.article.CloseComments(id);
            return RedirectToAction("View", new { id = id, Path = Path });
        }
        #endregion
        #region Profile
        // *********************************
        // URL: /Article/View/Username
        // ********  User profile  *********
        public ActionResult Profile(string username, string path)
        {
            MsSqlMembershipRepository mmr = new MsSqlMembershipRepository();
            string email = mmr.mssqlGetUserEmail(username);
            ProfileModel model = new ProfileModel();
            model.Username = username;
            model.Email = email;
            if (string.IsNullOrEmpty(path))
                model.Path = "All";
            else
                model.Path = path;
            model.Places = new SelectList(new List<string> { "All", "Articles", "Blogs" }, model.Path);
            return View(model);
        }
        //[HttpPost]
        //public ActionResult Profile(string username)
        //{
        //    MsSqlMembershipRepository mmr = new MsSqlMembershipRepository();
        //    string email = mmr.mssqlGetUserEmail(username);
        //    ProfileModel model = new ProfileModel();
        //    model.Username = username;
        //    model.Email = email;
        //    model.Path = "All";
        //    model.Places = new SelectList(new List<string> { "All", "Articles", "Blogs" }, model.Path);
        //    return View(model);
        //}
        #endregion
    }
}
