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
using Mytrip.Mvc.Models;
using System.Text;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc;
using Mytrip.Articles.Models;
using Mytrip.Articles.Repository.DataEntities;
using System.Xml.Linq;
using Mytrip.Articles.Helpers;
using Mytrip.Mvc.Helpers;
using System.Text.RegularExpressions;

namespace Mytrip.Articles.Controllers
{
    [HandleError]
    [Localization]
    public class ArticleController : Controller
    {
        #region Properties
        ICoreRepository _coreRepo;
        public ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
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
                if (_artset == null)
                    _artset = new ArticlesSetting();
                return _artset;
            }
        }
        LocalisationSetting _locset;
        public LocalisationSetting locset
        {
            get
            {
                if (_locset == null)
                    _locset = new LocalisationSetting();
                return _locset;
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
            if (!User.Identity.IsAuthenticated)
                return false;
            if (coreRepo.roleRepo.IsUserInRoleOnline(artset.roleChiefEditor()))
                return true;
            else
            {
                if (obj is mytrip_articlescategory)
                {
                    mytrip_articlescategory category = obj as mytrip_articlescategory;
                    categoryUserName = category.UserName;
                    category2UserName = category.mytrip_articlescategory2.UserName;
                    isBlog = category.Blog;
                }
                else if (obj is mytrip_articles)
                {
                    mytrip_articles article = obj as mytrip_articles;
                    articleUserName = article.UserName;
                    categoryUserName = article.mytrip_articlescategory.UserName;
                    category2UserName = article.mytrip_articlescategory.mytrip_articlescategory2.UserName;
                    isBlog = article.mytrip_articlescategory.Blog;
                }
                else if (obj is mytrip_articlescomments)
                {
                    mytrip_articlescomments comment = obj as mytrip_articlescomments;
                    if (!comment.IsAnonym && User.Identity.Name == comment.UserName)
                        return true;
                    articleUserName = comment.mytrip_articles.UserName;
                    categoryUserName = comment.mytrip_articles.mytrip_articlescategory.UserName;
                    category2UserName = comment.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.UserName;
                    isBlog = comment.mytrip_articles.mytrip_articlescategory.Blog;
                }
                else if (obj is mytrip_articlestag)
                {
                    forAdd = true;
                    isBlog = false;
                }
                bool articleEditor = coreRepo.roleRepo.IsUserInRoleOnline(artset.roleArticleEditor());
                if (coreRepo.roleRepo.IsUserInRoleOnline(artset.roleBlogger()) || articleEditor)
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
        public ActionResult Index(int? id, int? id2, int id3, string id4)
        {
            int total = 0;
            int _pageIndex = id ?? 1;
            int _pageSize = id2 ?? 10;
            ArticleIndexModel model = new ArticleIndexModel();
            model.ShowAddCategory = true;
            model.ShowAddSubCategory = true;
            model.ShowAddArticle = true;
            model.ShowEditDelete = true;
            model.ShowEditDeleteBlog = false;
            model.ShowAddBlog = false;
            model.ShowAddPost = false;
            model.ShowDetailsBlog = false;
            model.ParentCategory = new mytrip_articlescategory { CategoryId = -1, Title = string.Empty, Path = string.Empty };
            mytrip_articlescategory mc = articleRepo.category.GetCategory(id3);
            if (id4 == "Articles")
            {
                model.Categories = articleRepo.category.GetCategories(false,culture);
                if (model.Categories.Count() == 0)
                    model.ShowAddArticle = false;
                model.ShowEditDelete = false;
                model.ShowAddSubCategory = false;
                model.PageTitle = artset.NameArticlesPage();
                model.Articles = articleRepo.article.GetArticles(culture, (int)_pageIndex, (int)_pageSize, out total);
            }
            else if (id4 == "Blogs")
            {
                model.Categories = articleRepo.category.GetBlogs((int)_pageIndex, (int)_pageSize, culture, out total);
                model.ShowEditDelete = false;
                model.PageTitle = artset.NameBlogsPage();
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
            }
            else if (id4.StartsWith("(Tag)"))
            {
                model.PageTitle = ArticleLanguage.articles_for_tag + " " + articleRepo.article.GetTag(id3).TagName;
                model.Articles = articleRepo.article.GetArticlesPostsByTag(culture,id3,(int)_pageIndex, (int)_pageSize, out total);
                model.Categories = new List<mytrip_articlescategory>(0).AsQueryable<mytrip_articlescategory>();
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = true;
            }
            else if (id4.StartsWith("(Search)"))
            {
                string search = id4.Remove(0, 8).TrimEnd();
                search = GeneralMethods.DecodingSearch2(search);
                IQueryable<mytrip_articles> articles = articleRepo.article.GetArticles(culture, search, (int)_pageIndex, (int)_pageSize, out total);
                foreach (var art in articles)
                {
                    art.Title = GeneralMethods.ReplaceString(art.Title, search);
                    art.Abstract = GeneralMethods.ReplaceString(art.Abstract, search);
                }
                model.Articles = articles;
                model.Categories = new List<mytrip_articlescategory>(0).AsQueryable<mytrip_articlescategory>();
                model.PageTitle = String.Format(ArticleLanguage.search_for_found_results, search, total);
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = false;
            }
            else
            {
                model.Categories = articleRepo.category.GetSubCategories(id3);
                model.Articles = articleRepo.article.GetArticles(id3,culture, (int)_pageIndex, (int)_pageSize, out total);
                model.PageTitle = mc.Title;
                if (mc.SubCategoryId != 0)
                {
                    model.ShowAddSubCategory = false;
                    if (mc.Blog)
                    {
                        model.ShowDetailsBlog = true;
                        model.ShowEditDelete = false;
                        model.ShowAddCategory = false;
                        model.ShowAddArticle = false;
                        model.ShowEditDeleteBlog = true;
                        model.ShowAddPost = true;
                        articleRepo.category.BlogViewsIncrease(id3);
                    }
                    model.ParentCategory = articleRepo.category.GetCategory(mc.SubCategoryId);
                }
                else if (!mc.Blog && !mc.SeparateBlock)
                {
                    model.ParentCategory = new mytrip_articlescategory { CategoryId = 0, Title = artset.NameArticlesPage(), Path = "Articles" };
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
                    model.ParentCategory = articleRepo.category.GetCategory(id3);
                    articleRepo.category.BlogViewsIncrease(id3);
                }
            }
            model.Total = total;
            model.DefaultCount = 10;
            model.Path = id4;
            model.CategoryId = id3;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string search, string url)
        {
            if (search != string.Empty)
            {
                search = GeneralMethods.DecodingSearch(search);
                return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = 0, id4 = "(Search)" + search.Trim() });
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
        public ActionResult CreateCategory(int id, string id2)
        {
            mytrip_articlescategory category = articleRepo.category.GetCategory(id);
            if (!userHasRights(category, true))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            CategoryModel model = new CategoryModel();
            model.Path = id2;
            model.CategoryId = id;
            if (id != 0)
            {
                string title = category.Title;
                if (!locset.unlockAllCulture() && !category.mytrip_articlescategory2.AllCulture)
                    model.ShowAllCulture = "none";
                model.ShowSeparateBlock = "none";
                if (category.Blog)
                    model.PageTitle = ArticleLanguage.create_topic_for + " " + title;
                else
                    model.PageTitle = ArticleLanguage.create_subcategory_for + " " + title;
            }
            else
            {
                if (!locset.unlockAllCulture())
                    model.ShowAllCulture = "none";
                model.PageTitle = ArticleLanguage.create_new_category;
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                mytrip_articlescategory mac;
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

                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = model.CategoryId, id4 = model.Path });
            }
            model.PageTitle = ArticleLanguage.create_category;
            return View(model);
        }
        // ********************************************
        // URL: /Article/EditCategory/categoryId/Path
        // * редактировать рубрику, подрубрику или тему блога *
        [Authorize]
        public ActionResult EditCategory(int id, string id2, string id3)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryId = id;
            if (!id2.StartsWith("(Tag)"))
            {
                mytrip_articlescategory mc = articleRepo.category.GetCategory(id);
                if (!userHasRights(mc, false))
                    return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
                model.Title = mc.Title;
                model.AllCulture = mc.AllCulture;
                model.SeparateBlock = mc.SeparateBlock;
                if (!locset.unlockAllCulture())
                    model.ShowAllCulture = "none";
                if (mc.Blog || mc.SubCategoryId != 0)
                {
                    model.ShowSeparateBlock = "none";
                    if (!mc.mytrip_articlescategory2.AllCulture)
                        model.ShowAllCulture = "none";
                }
                model.PageTitle = ArticleLanguage.edit + " " + mc.Title;
                
                if (id2 == "Archive")
                {
                    model.Url = id3.Replace("(x)", "/");
                    model.Path = id2;
                }
                else
                {
                    model.Url = Url.Action("Index", new { id = 1, id2 = 10, id3 = mc.CategoryId, id4 = mc.Path });
                    model.Path = mc.Path;
                }
            }
            else
            {
                mytrip_articlestag tag = articleRepo.article.GetTag(id);
                if (!userHasRights(tag, false))
                    return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
                model.Title = tag.TagName;
                model.ShowSeparateBlock = "none";
                model.ShowAllCulture = "none";
                model.PageTitle = ArticleLanguage.edit_tag + " " + tag.TagName;
                
                if (id2.EndsWith("Archive"))
                {
                    model.Url = id3.Replace("(x)", "/");
                    model.Path = id2;
                }
                else
                    model.Path = tag.Path;
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
                    return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = model.CategoryId, id4 = model.Path });
            }
            if (model.Path.StartsWith("(Tag)"))
            {
                model.PageTitle = ArticleLanguage.edit_tag;
                model.ShowSeparateBlock = "none";
                model.ShowAllCulture = "none";
            }
            else
            {
                if (!locset.unlockAllCulture())
                    model.ShowAllCulture = "none";
                model.PageTitle = ArticleLanguage.edit_category;
            }
            return View(model);
        }
        // **********************************************
        // URL: /Article/DeleteCategory/id/Path/
        // *****  удалить рубрику или подрубрику  *******
        [Authorize]
        public ActionResult DeleteCategory(int id, string id2, string id3)
        {
            if (!id2.StartsWith("(Tag)"))
            {
                mytrip_articlescategory mc = articleRepo.category.GetCategory(id);
                if (!userHasRights(mc, false))
                    return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
                articleRepo.category.DeleteCategory(id);
                if (id2 == "Archive")
                    return Redirect(id3.Replace("(x)", "/"));
                if (mc.SubCategoryId != 0)
                    id = mc.SubCategoryId;
                else if (mc.Blog)
                    id2 = "Blogs";
                else
                {
                    id = 0;
                    id2 = "Articles";
                }
            }
            else
            {
                mytrip_articlestag tag = articleRepo.article.GetTag(id);
                if (!userHasRights(tag, false))
                    return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
                articleRepo.article.DeleteTag(tag);
                if (id2.EndsWith("Archive"))
                    return Redirect(id3.Replace("(x)", "/"));
                id2 = "Articles";
                id = 0;
            }
            return RedirectToAction("Index", new { id = 1, id2 = 10, id3=id, id4 = id2 });
        }

        #endregion

        #region Articles Actions
        // **************************************
        // URL: /Article/View/ArticleId
        // ******  одна статья или пост  ********
        public ActionResult View(int id,string id2,string id3)
        {
            mytrip_articles article = articleRepo.article.GetArticleById(id);
            if (article.OnlyForRegisterUser && !User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            ArticleViewModel model = new ArticleViewModel();
            model.Article = article;
            model.Anchor = id3;
            articleRepo.article.IncreaseViews(id);
            model.VotesCount = articleRepo.article.GetVotesCount(id);
            model.Blog = article.mytrip_articlescategory.Blog;
            model.replaceCommentsEmail = artset.replaceСommentsEmail();
            model.showRelatedLinks = artset.showRelatedLinks();
            model.Title = article.Title;
            model.Path = article.Path;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult View(ArticleViewModel model, int id)
        {
            try
            {
                if (artset.replaceСommentsEmail() && !model.Blog)
                {
                    if (!User.Identity.IsAuthenticated && ModelState.IsValid)
                        coreRepo.emailRepo.SendEmail(coreRepo.emailRepo.from_email(), (model.Title + "(" + model.AnonymEmail + " " + model.AnonymName + ")"), model.Comment);
                    else
                    {
                        coreRepo.emailRepo.SendEmail(coreRepo.emailRepo.from_email(), (model.Title + "(" + coreRepo.membershipRepo.mtGetUserEmail(HttpContext.User.Identity.Name) + " " + HttpContext.User.Identity.Name + ")"), model.Comment);
                    }
                    return RedirectToAction("View", new { id = id, id2 = model.Path });
                }
                else
                {
                    mytrip_articlescomments comment;
                    if (!User.Identity.IsAuthenticated && ModelState.IsValid)
                        comment = articleRepo.comment.CreateComment(id, model.Comment, model.AnonymName, model.AnonymEmail);
                    else
                        comment = articleRepo.comment.CreateComment(id, model.Comment);


                    return RedirectToAction("View", new { id = id, id2 = model.Path, id3 = comment.CommentId });
                }
            }
            catch
            {
                mytrip_articles article = articleRepo.article.GetArticleById(id);
                model.Article = article;
                model.VotesCount = articleRepo.article.GetVotesCount(id);
                articleRepo.article.IncreaseViews(id);
                model.Anchor = "create";
                model.Blog = article.mytrip_articlescategory.Blog;
                return View(model);
            }
        }
        [HttpPost]
        public string Rate(int id,int vote,int count)
        {
            double total = (double)articleRepo.article.CreateVote(id, vote);
            int newcount = articleRepo.article.GetVotesCount(id);
            ThemeSetting theme = new ThemeSetting();
            StringBuilder result = new StringBuilder();
            result.AppendLine(String.Format(ArticleLanguage.score_votes, total.ToString("N2"), newcount.ToString()));
            for (int rate = 0; rate < 5; rate++)
            {
                double rate12 = rate + 0.125;
                double rate37 = rate + 0.375;
                double rate62 = rate + 0.625;
                double rate87 = rate + 0.875;
                TagBuilder input = new TagBuilder("img");
                if (total > rate12 && total < rate37)
                    input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star25.png");
                if (total > rate37 && total < rate62)
                    input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star50.png");
                if (total > rate62 && total < rate87)
                    input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star75.png");
                if (total < rate87)
                    input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star100.png");
                if (total > rate87)
                    input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star.png");
                input.MergeAttribute("style", "width: 15px; height: 15px;");
                result.AppendLine(input.ToString());
            }
            if (count == newcount)
                result.AppendLine("<br/>" + ArticleLanguage.you_have_a_voted);
            else
                result.AppendLine("<br/>" + ArticleLanguage.thanks_for_vote);
            return result.ToString(); ;
        }
        // *************************************
        // URL: /Article/Create/CategoryId/Path
        // ****  Создать статью или пост  ******
        [Authorize]
        public ActionResult Create(int id, string id2)
        {
            mytrip_articlescategory cat = articleRepo.category.GetCategory(id);
            if (!userHasRights(cat, true))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            if (id == 0 && articleRepo.category.GetCategoriesCount(culture) == 0)
            {
                return RedirectToAction("CreateCategory", "Article", new { id = 0 });
            }
            ArticleModel model = new ArticleModel();
            model.CategoryId = id;
            model.Path = id2;
            model.CloseDate = new DateTime(2099, 12, 12).ToString("yyyy-MM-dd");
            if (!locset.unlockAllCulture()&&!cat.AllCulture)
                model.ShowAllCulture = "none";
            if (cat.Blog)
            {
                model.PageTitle = ArticleLanguage.create_new_post;
                model.ShowArticleOptions = "none";
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(id, culture), "CategoryId", "Title");
            }
            else
            {
                model.PageTitle = ArticleLanguage.create_new_article;
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(culture), "CategoryId", "Title");
                model.ShowIncludeAnonymComment = "none";
            }
            #region tags
            model.Tags = articleRepo.article.GetAllTags(false);
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
                mytrip_articlescategory cat = articleRepo.category.GetCategory(model.CategoryId);
                mytrip_articles article;
                if (!cat.Blog)
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
                IQueryable<mytrip_articlestag> ts = articleRepo.article.GetAllTags(false);
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
                        mytrip_articlestag mat = articleRepo.article.CreateTag(s);
                        articleRepo.article.AddTagInArticle(article.ArticleId, mat.TagId);
                    }
                }
                #endregion
                return RedirectToAction("View", new { id = article.ArticleId, id2 = article.Path });
            }
            return View(model);

        }
        // *************************************
        // URL: /Article/Edit/ArticleId/Path
        // *** редактировать статью или пост ***
        [Authorize]
        public ActionResult Edit(int id, string id2, string id3)
        {
            ArticleModel model = new ArticleModel();
            mytrip_articles article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
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
            if (!article.ApprovedComment || article.OnlyForRegisterUser)
                model.ShowIncludeAnonymComment = "none";
            #endregion
            model.PageTitle = ArticleLanguage.edit + " \"" + article.Title + "\"";
            model.Path = id2;
            if (id2 == "Archive")
                model.Url = id3.Replace("(x)", "/");
            else
                model.Url = Url.Action("View", new { id = article.ArticleId, Path = article.Path });
            if (article.mytrip_articlescategory.Blog)
            {
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(article.mytrip_articlescategory.CategoryId, culture), "CategoryId", "Title");
                model.ShowArticleOptions = "none";
            }
            else
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(culture), "CategoryId", "Title", article.CategoryId);
            #region tags
            model.Tags = articleRepo.article.GetAllTags(false);
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
                IQueryable<mytrip_articlestag> ts = articleRepo.article.GetAllTags(false);
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
                        mytrip_articlestag mat = articleRepo.article.CreateTag(s);
                        articleRepo.article.AddTagInArticle(model.ArticleId, mat.TagId);
                    }
                }
                if (model.Path == "Archive")
                    return Redirect(model.Url.Replace("(x)", "/"));
                else
                    return RedirectToAction("View", new { id = model.ArticleId, id2 = model.Path });
            }
            catch
            {
                mytrip_articles article = articleRepo.article.GetArticleById(model.ArticleId);
                if (article.mytrip_articlescategory.Blog)
                {
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(article.mytrip_articlescategory.CategoryId, culture), "CategoryId", "Title");
                    model.ShowArticleOptions = "none";
                }
                else
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(culture), "CategoryId", "Title", article.CategoryId);
                model.Tags = articleRepo.article.GetAllTags(false);
                return View(model);
            }
        }
        // ****************************************
        // URL: /Article/Delete/Id/Path/
        // *****  удалить статью или пост  *******
        [Authorize]
        public ActionResult Delete(int id, string id2, string id3)
        {
            mytrip_articles mc = articleRepo.article.GetArticleById(id);
            if (!userHasRights(mc, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            if (id2 == "Archive")
            {
                articleRepo.article.DeleteArticle(id);
                return Redirect(id3.Replace("(x)", "/"));
            }
            else
            {
                id2 = mc.mytrip_articlescategory.Path;
                int catid = mc.CategoryId;
                articleRepo.article.DeleteArticle(id);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = catid, id4 = id2 });
            }
        }
        #endregion

        #region Comments Actions
        // *****************************************
        // URL: /Article/EditComment/id/Path/
        // ******  редактировать комментарий  ******
        [Authorize]
        public ActionResult EditComment(int id, string id2, string id3)
        {
            mytrip_articlescomments comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            CommentModel model = new CommentModel();
            model.CommentId = id;
            model.Comment = comment.Body;
            model.ArticleId = comment.ArticleId;
            
            model.PageTitle = ArticleLanguage.edit_comment;
            if (id2 == "Archive")
            {
                model.Path = id2;
                model.Url = id3.Replace("(x)", "/");
            }
            else
            {
                model.Path = comment.mytrip_articles.Path;
                model.Url = Url.Action("View", new { id = comment.mytrip_articles.ArticleId, id2 = comment.mytrip_articles.Path });
            }
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
                mytrip_articlescomments comment = articleRepo.comment.GetComment(model.CommentId);
                comment.Body = model.Comment;
                if (model.Path == "Archive")
                    return Redirect(model.Url.Replace("(x)", "/"));
                else
                    return RedirectToAction("View", new { id = comment.ArticleId, id2 = comment.mytrip_articles.Path });
            }
            return View(model);
        }
        // **********************************************
        // URL: /Article/DeleteComment/id/Path/
        // *****  удалить комментарий  *******
        [Authorize]
        public ActionResult DeleteComment(int id, string id2, string id3)
        {
            mytrip_articlescomments comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.comment.DeleteComment(id);
            if (id2 == "Archive")
                return Redirect(id3.Replace("(x)", "/"));
            else
                return RedirectToAction("View", new { id = comment.ArticleId, id2 = id2 });
        }
        // **********************************************
        // URL: /Article/OpenComments/ArticleId/Path/
        // *******  open comments  *********
        [Authorize]
        public ActionResult OpenComments(int id, string id2)
        {
            var article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.article.OpenComments(id);
            return RedirectToAction("View", new { id = id, id2 = id2 });
        }
        // **********************************************
        // URL: /Article/CloseComments/ArticleId/Path/
        // *****  close comments  *******
        [Authorize]
        public ActionResult CloseComments(int id, string id2)
        {
            var article = articleRepo.article.GetArticleById(id);
            if (!userHasRights(article, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.article.CloseComments(id);
            return RedirectToAction("View", new { id = id, id2 = id2 });
        }
        #endregion

        #region Archive
        //
        // GET: /ArticleArchive/
        [RoleAdmin]
        public ActionResult Archive(int? id)
        {
            int _count = id ?? 5;
            ArchiveIndexModel model = new ArchiveIndexModel();
            model.Count = _count;
            return View(model);
        }
        //
        // GET: /ArticleArchive/Details/path/culture
        [RoleAdmin]
        public ActionResult ArchiveDetails(string id, string id2)
        {
            ArchiveIndexModel model = new ArchiveIndexModel();
            #region Set Page title
            switch (id)
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
                    model.Path = id;
                    break;
            }
            #endregion
            model.Path = id;
            model.Culture = id2;
            return View(model);
        }
        #endregion

        #region Setting
        [CoreSqlSetting]
        public ActionResult ArticleSetting()
        {
            ArticleSettingModel model = new ArticleSettingModel();
            model.articles = artset.articles();
            model.blogs = artset.blogs();
            model.countCommentForBlogs = artset.countCommentForBlogs();
            model.roleArticleEditor = artset.roleArticleEditor();
            model.roleBlogger = artset.roleBlogger();
            model.cacheSeconds = artset.cacheSeconds();
            model.viewInfoClosedComments = artset.viewInfoClosedComments();
            model.viewInfoAuthorArticle = artset.viewInfoAuthorArticle();
            model.viewInfoViewsArticle = artset.viewInfoViewsArticle();
            model.showRelatedLinks = artset.showRelatedLinks();
            model.nameArticles = artset.NameArticlesPage();
            model.nameBlogs = artset.NameBlogsPage();
            model.nameSearch = artset.NameSearchPage();
            model.nameTags = artset.NameTagsPage();
            model.closecountCommentForBlogs = artset.closecountCommentForBlogs();
            model.replaceСommentsEmail = artset.replaceСommentsEmail();
            return View(model);
        }
        [HttpPost]
        [CoreSqlSetting]
        public ActionResult ArticleSetting(ArticleSettingModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.countCommentForBlogs == null)
                    model.countCommentForBlogs = 5;
                coreRepo.roleRepo.RenameRole(artset.roleArticleEditor(), model.roleArticleEditor);
                coreRepo.roleRepo.RenameRole(artset.roleBlogger(), model.roleBlogger);
                string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("Mytrip.Articles").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "articles")
                    .SetAttributeValue("value", model.articles.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "blogs")
                    .SetAttributeValue("value", model.blogs.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "countCommentForBlogs")
                    .SetAttributeValue("value", model.countCommentForBlogs.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "roleBlogger")
                    .SetAttributeValue("value", model.roleBlogger);
                article.FirstOrDefault(x => x.Attribute("name").Value == "roleArticleEditor")
                    .SetAttributeValue("value", model.roleArticleEditor);
                article.FirstOrDefault(x => x.Attribute("name").Value == "replaceСommentsEmail")
                                    .SetAttributeValue("value", model.replaceСommentsEmail.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "cacheSeconds")
                    .SetAttributeValue("value", model.cacheSeconds.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "viewInfoClosedComments")
                    .SetAttributeValue("value", model.viewInfoClosedComments.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "viewInfoAuthorArticle")
                    .SetAttributeValue("value", model.viewInfoAuthorArticle.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "viewInfoViewsArticle")
                    .SetAttributeValue("value", model.viewInfoViewsArticle.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "showRelatedLinks")
                    .SetAttributeValue("value", model.showRelatedLinks.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "closecountCommentForBlogs")
                    .SetAttributeValue("value", model.closecountCommentForBlogs.ToString());
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameArticles").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameArticles);
                var blogpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameBlogs").Elements("add");
                blogpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameBlogs);
                var serchpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameSearch").Elements("add");
                serchpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameSearch);
                var tagpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameTags").Elements("add");
                tagpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameTags);
                _doc.Save(_absolutDirectory);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        #endregion

        #region SideBar
        [HttpPost]
        public string ArticlesActivity(string option)
        {
            string res = "";
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            if (option == ArticleLanguage.most_viewed)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.top_viewed + "</b></u>";
                foreach (var item in ar.article.GetArticlesPopular(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString() + "<br/>" + ArticleLanguage.views + ": " + item.Views;
                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.recent_articles)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else if (option == ArticleLanguage.most_rated)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.top_rated + "</b></u>";
                foreach (var item in ar.article.GetArticlesRated(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString() + "<br/>" + GeneralMethods.CoreRating(true, false, (double)item.TotalVotes, -1)
                        + "- " + item.TotalVotes;

                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_viewed) + BuildSubmit(ArticleLanguage.recent_articles)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else if (option == ArticleLanguage.recent_articles)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.latest_updates + "</b></u>";
                foreach (var item in ar.article.GetArticlesRecent(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml =  a.ToString() + "<br/>" + item.CreateDate.ToString("dd MMMM yyyy HH:mm");

                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.most_viewed)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else  //recent comments
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.recent_comments + "</b></u>";
                foreach (var item in ar.comment.GetLastComments(culture, false, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    li.MergeAttribute("style", "min-height:55px");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.mytrip_articles.Path + "/" + item.CommentId);
                    a.MergeAttribute("title", item.mytrip_articles.Title);
                    a.InnerHtml = item.mytrip_articles.Title;
                    TagBuilder profile = new TagBuilder("a");
                    profile.MergeAttribute("href", "/Home/Profile/" + item.UserName);
                    profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                    profile.InnerHtml = item.UserName;
                    item.Body = Regex.Replace(item.Body, @"<(.|\n)*?>", string.Empty);
                    if (item.Body.Length > 75)
                        item.Body = item.Body.Remove(75) + "...";
                    li.InnerHtml = "<b>" + a.ToString() + "</b><br/>" + BuildAvatar(item.UserEmail, item.UserName)
                        + "<b><u>" + profile.ToString() + "</u></b>: " + item.Body;
                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.most_viewed)
                  + BuildSubmit(ArticleLanguage.recent_articles) + "</center>";
            }
            return res;
        }
        [HttpPost]
        public string PostsActivity(string option)
        {
            string res = "";
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            if (option == ArticleLanguage.most_viewed)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.top_viewed + "</b></u>";
                foreach (var item in ar.article.GetPostsPopular(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString() + "<br/>" + ArticleLanguage.views + ": " + item.Views;
                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.recent_posts)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else if (option == ArticleLanguage.most_rated)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.top_rated + "</b></u>";
                foreach (var item in ar.article.GetPostsRated(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString() + "<br/>" + GeneralMethods.CoreRating(true, false, (double)item.TotalVotes, -1)
                        + "- " + item.TotalVotes;
                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_viewed) + BuildSubmit(ArticleLanguage.recent_posts)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else if (option == ArticleLanguage.recent_posts)
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.latest_updates + "</b></u>";
                foreach (var item in ar.article.GetPostsRecent(culture, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                    a.MergeAttribute("title", item.Title);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString() + "<br/>" + item.CreateDate.ToString("dd MMMM yyyy HH:mm");

                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.most_viewed)
                  + BuildSubmit(ArticleLanguage.recent_comments) + "</center>";
            }
            else  //recent comments
            {
                title.InnerHtml = "<b><u>" + ArticleLanguage.recent_comments + "</b></u>";
                foreach (var item in ar.comment.GetLastComments(culture, true, 5))
                {
                    TagBuilder li = new TagBuilder("li");
                    li.MergeAttribute("style", "min-height:55px");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.mytrip_articles.Path + "/" + item.CommentId);
                    a.MergeAttribute("title", item.mytrip_articles.Title);
                    a.InnerHtml = item.mytrip_articles.Title;
                    TagBuilder profile = new TagBuilder("a");
                    profile.MergeAttribute("href", "/Home/Profile/" + item.UserName);
                    profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                    profile.InnerHtml = item.UserName;
                    item.Body = Regex.Replace(item.Body, @"<(.|\n)*?>", string.Empty);
                    if (item.Body.Length > 75)
                        item.Body = item.Body.Remove(75) + "...";
                    li.InnerHtml = a.ToString() + "<br/>" + BuildAvatar(item.UserEmail, item.UserName)
                        + "<b><u>" + profile.ToString() + "</u></b>: " + item.Body;
                    list.InnerHtml += li.ToString();
                }
                res = title + list.ToString() + "<center>" + BuildSubmit(ArticleLanguage.most_rated) + BuildSubmit(ArticleLanguage.most_viewed)
                  + BuildSubmit(ArticleLanguage.recent_posts) + "</center>";
            }
            return res;
        }
        public string BuildSubmit(string value)
        {
            return "<input id='save' type='submit' name='option' value='" + value + "' class=\"link\" />";
        }
        public string BuildAvatar(string email, string username)
        {
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", "/Home/Profile/" + username);
            link.InnerHtml = AvatarHelper.Avatar(null, email, new { width = 30, style = "float:left;padding: 4px 3px 0 0;" });
            link.MergeAttribute("title", username);
            string res = link.ToString();
            return res;
        }
        #endregion
    }
}
