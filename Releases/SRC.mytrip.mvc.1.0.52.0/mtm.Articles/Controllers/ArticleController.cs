//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about mytrip.mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml.Linq;
using mtm.Core;
using mtm.Core.Helpers;
using mtm.Core.Models;
using mtm.Core.Repository;
using mtm.Core.Settings;
using mtm.Articles.Models;
using mtm.Articles.Repository;
using mtm.Articles.Repository.DataEntities;

namespace mtm.Articles.Controllers
{
    public class ArticleController : Controller
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
        private bool userHasRights(object obj, bool forAdd)
        {
            string categoryUserName = "";
            string category2UserName = "";
            string articleUserName = "";
            bool isBlog = true;
            if (!User.Identity.IsAuthenticated)
                return false;
            if (MytripUser.UserInRole(ModuleSetting.roleChiefEditor()))
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
                bool articleEditor = MytripUser.UserInRole(ModuleSetting.roleArticleEditor());
                if (MytripUser.UserInRole(ModuleSetting.roleBlogger()) || articleEditor)
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
            model.ShowAddBlog = false;
            model.ShowAddPost = false;
            model.ShowDetailsBlog = false;
            model.ParentCategory = new mytrip_articlescategory { CategoryId = -1, Title = string.Empty, Path = string.Empty };
            mytrip_articlescategory mc = articleRepo.category.GetCategory(id3);
            if (id4.EndsWith("Articles"))
            {
                model.Categories = articleRepo.category.GetCategories(false, LocalisationSetting.culture());
                if (model.Categories.Count() == 0)
                    model.ShowAddArticle = false;
                model.ShowEditDelete = false;
                model.ShowAddSubCategory = false;
                model.PageTitle = ModuleSetting.NameArticlesPage();
                if (id4.StartsWith("(Rated)"))
                    model.Articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.TotalVotes, out total);
                else if (id4.StartsWith("(Viewed)"))
                    model.Articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.Views, out total);
                else
                    model.Articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.CreateDate, out total);
            }
            else if (id4 == "Blogs")
            {
                model.Categories = articleRepo.category.GetBlogs(_pageIndex, _pageSize, LocalisationSetting.culture(), out total);
                model.ShowEditDelete = false;
                model.PageTitle = ModuleSetting.NameBlogsPage();
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
            }
            else if (id4.Contains("(Tag)"))
            {
                if (id4.Contains("(Viewed)"))
                    model.Articles = articleRepo.article.GetByTag(LocalisationSetting.culture(), id3, _pageIndex, _pageSize, SortBy.Views, out total);
                else if (id4.Contains("(Rated)"))
                    model.Articles = articleRepo.article.GetByTag(LocalisationSetting.culture(), id3, _pageIndex, _pageSize, SortBy.TotalVotes, out total);
                else
                    model.Articles = articleRepo.article.GetByTag(LocalisationSetting.culture(), id3, _pageIndex, _pageSize, SortBy.CreateDate, out total);
                model.PageTitle = ArticleLanguage.articles_for_tag + " " + articleRepo.article.GetTag(id3).TagName;
                model.Categories = new List<mytrip_articlescategory>(0).AsQueryable<mytrip_articlescategory>();
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = true;
            }
            else if (id4.Contains("(Search)"))
            {
                string search = id4.Replace("(Search)", "").Replace("(Viewed)", "").Replace("(Rated)", "").Replace("(Search)", "").TrimEnd();
                search = GeneralMethods.UndecodingSearch(search);
                IQueryable<mytrip_articles> articles = null;
                if (id4.Contains("(Viewed)"))
                    articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), search, _pageIndex, _pageSize, SortBy.Views, out total);
                else if (id4.Contains("(Rated)"))
                    articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), search, _pageIndex, _pageSize, SortBy.TotalVotes, out total);
                else
                    articles = articleRepo.article.GetArticles(LocalisationSetting.culture(), search, _pageIndex, _pageSize, SortBy.CreateDate, out total);
                foreach (var art in articles)
                {
                    art.Title = GeneralMethods.ReplaceString(art.Title, search);
                    art.Abstract = GeneralMethods.ReplaceString(art.Abstract, search);
                }
                model.Articles = articles;
                model.Categories = new List<mytrip_articlescategory>(0).AsQueryable<mytrip_articlescategory>();
                model.PageTitle = string.Format(ArticleLanguage.search_for_found_results, search, total);
                model.ShowAddCategory = false;
                model.ShowAddSubCategory = false;
                model.ShowAddArticle = false;
                model.ShowEditDelete = false;
            }
            else
            {
                model.Categories = articleRepo.category.GetSubCategories(id3);
                if (id4.StartsWith("(Rated)"))
                {
                    model.Articles = articleRepo.article.GetArticles(id3, LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.TotalVotes, out total);
                }
                else if (id4.StartsWith("(Viewed)"))
                {
                    model.Articles = articleRepo.article.GetArticles(id3, LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.Views, out total);
                }
                else
                    model.Articles = articleRepo.article.GetArticles(id3, LocalisationSetting.culture(), _pageIndex, _pageSize, SortBy.CreateDate, out total);
                model.PageTitle = mc.Title;
                if (mc.SubCategoryId != 0)
                {
                    model.ShowAddSubCategory = false;
                    if (mc.Blog)
                    {
                        model.ShowDetailsBlog = true;
                        model.ShowEditDelete = true;
                        model.ShowAddCategory = false;
                        model.ShowAddArticle = false;
                        model.ShowAddPost = true;
                        articleRepo.category.BlogViewsIncrease(id3);
                    }
                    model.ParentCategory = articleRepo.category.GetCategory(mc.SubCategoryId);
                }
                else if (!mc.Blog && !mc.SeparateBlock)
                {
                    model.ParentCategory = new mytrip_articlescategory { CategoryId = 0, Title = ModuleSetting.NameArticlesPage(), Path = "Articles" };
                }
                else if (mc.Blog)
                {
                    model.ShowDetailsBlog = true;
                    model.ShowEditDelete = true;
                    model.ShowAddCategory = false;
                    model.ShowAddSubCategory = false;
                    model.ShowAddArticle = false;
                    model.ShowAddBlog = true;
                    model.ShowAddPost = true;
                    model.ParentCategory = articleRepo.category.GetCategory(id3);
                    articleRepo.category.BlogViewsIncrease(id3);
                }
            }
            model.Total = total;
            model.DefaultCount = 10;
            model.Path = id4; //id3 > 0 ? mc.Path : "";
            model.CategoryId = id3;
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(ArticleIndexModel model, string search, string url)
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
        // *********************************************
        // URL: /Article/Category
        // **  создать редактировать рубрику/тему  **
        [HttpPost]
        [Authorize]
        public ActionResult Category(int id, string path, string title, bool menu, bool allculture)
        {
            if (string.IsNullOrEmpty(title))
                return Content(ArticleLanguage.field_empty);
            else if (title.Length < 3 || title.Length > 255)
                return Content(ArticleLanguage.title_lenght_5_255);
            else
            {
                if (path == "Add")
                {
                    if (id == 0)
                        articleRepo.category.CreateСategory(title, menu, allculture, LocalisationSetting.culture());
                    else
                        articleRepo.category.CreateSubCategory(title, id, allculture);
                }
                else if (path.Contains("Tag"))
                    articleRepo.article.UpdateTag(id, title);
                else
                    articleRepo.category.UpdateCategory(id, title, menu, allculture);
                return Content(string.Empty);
            }
        }
        // ****************************************
        // URL: /Article/CreateBlog
        // *****  создать блог  *******
        [Authorize]
        public ActionResult CreateBlog()
        {
            if (articleRepo.comment.GetCount(User.Identity.Name) >= ModuleSetting.countCommentForBlogs() && articleRepo.category.GetBlogsByUser(User.Identity.Name, LocalisationSetting.culture()).Count() == 0)
            {
                if (!MytripUser.UserInRole(ModuleSetting.roleBlogger()))
                    MytripUser.UnlockUserInRole(User.Identity.Name, ModuleSetting.roleBlogger());
                mytrip_articlescategory blog = articleRepo.category.CreateBlog(LocalisationSetting.culture());
                GeneralMethods.MytripCacheRemove("mtm_cacherole");
                return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = blog.CategoryId, id4 = blog.Path });
            }
            else if (MytripUser.UserInRole(ModuleSetting.roleBlogger()))
            {
                mytrip_articlescategory blog = articleRepo.category.CreateBlog(LocalisationSetting.culture());
                return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = blog.CategoryId, id4 = blog.Path });
            }
            else { return RedirectToAction("Index", "Article", new { id = 1, id2 = 10, id3 = 0, id4 = "Blogs" }); }
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
            return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = id, id4 = id2 });
        }

        #endregion

        #region Articles Actions
        // **************************************
        // URL: /Article/View/ArticleId
        // ******  одна статья или пост  ********
        public ActionResult View(int id, string id2, string id3)
        {
            mytrip_articles article = articleRepo.article.GetArticle(id);
            if (article.OnlyForRegisterUser && !User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            ArticleViewModel model = new ArticleViewModel();
            model.Article = article;

            articleRepo.article.IncreaseViews(id);
            model.VotesCount = articleRepo.article.GetVotesCount(article.ArticleId);
            model.Blog = article.mytrip_articlescategory.Blog;
            model.showRelatedLinks = ModuleSetting.showRelatedLinks();
            model.tableWidth = (model.showRelatedLinks) ? "width:50%;" : "width:100%;";
            model.Title = article.Title;
            model.Path = article.Path;
            model.Username = article.UserName;
            if (article.ModerateComments)
                model.CommentApproved = userHasRights(article, false);
            else
                model.CommentApproved = true;
            model.isSubscribed = articleRepo.subscription.isSubscribed(article.ArticleId);
            model.Anchor = id3;
            model.PagesIds = articleRepo.article.GetArticlePagesIds(article.ArticleId);
            model.ReturnId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult View(ArticleViewModel model, int id)
        {
            try
            {
                mytrip_articlescomments comment;
                if (!User.Identity.IsAuthenticated && ModelState.IsValid)
                    comment = articleRepo.comment.CreateComment(id, model.Comment, model.AnonymName, model.AnonymEmail, model.CommentApproved);
                else
                    comment = articleRepo.comment.CreateComment(id, model.Comment, model.CommentApproved);
                if (comment != null && EmailSetting.unlockSendEmail())
                {
                    string domain = Request.Url.Host;
                    string articlelink = string.Format("<a href=\"{0}\" title=\"{1}\">{1}</a>", Request.Url.AbsoluteUri, model.Title);
                    string profilelink = string.Format("<a href=\"{0}/Home/Profile/{1}\" title=\"{2}\">{1}</a>", domain, comment.UserName, ArticleLanguage.view_user_profile);
                    string sitelink = string.Format(CoreSetting.NameTitlePage(), "<a href=\"" + domain + "\" title=\"" + domain + "\">" + domain + "</a>");
                    if (model.CommentApproved)
                    {
                        List<MailMessage> msgs = new List<MailMessage>();
                        foreach (var item in articleRepo.subscription.GetSubsciptions(comment.ArticleId))
                        {
                            string email = MytripUser.UserEmail(item.UserName);
                            if (!string.IsNullOrEmpty(email) && item.UserName != comment.UserName)
                            {
                                MailMessage msg = new MailMessage();
                                msg.To.Add(email);//msg.To.Add("matros@013net.net,matrostik@gmail.com");
                                msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                                msg.Subject = string.Format(CoreSetting.NameTitlePage(), ArticleLanguage.new_comment);
                                msg.Body = string.Format(ArticleLanguage.email_commentupdates, item.UserName, articlelink
                                    , profilelink, comment.CreateDate, comment.Body, sitelink);
                                msg.IsBodyHtml = true;
                                msgs.Add(msg);
                            }
                        }
                        if (msgs.Count != 0)
                            EmailSetting.SendEmail(msgs);
                    }
                    else
                    {
                        //письмо модеру о новом комменте
                        string email = MytripUser.UserEmail(model.Username);
                        if (!string.IsNullOrEmpty(email))
                        {
                            MailMessage msg = new MailMessage();
                            msg.To.Add(email);//msg.To.Add("matros@013net.net,matrostik@gmail.com");
                            msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                            msg.Subject = string.Format(CoreSetting.NameTitlePage(), ArticleLanguage.new_comment);
                            msg.Body = string.Format(ArticleLanguage.email_commentmoderate, model.Username, articlelink
                                       , profilelink, comment.CreateDate, comment.Body, sitelink);
                            msg.IsBodyHtml = true;
                            EmailSetting.SendEmail(msg);
                        }
                    }
                }
                return RedirectToAction("View", new { id = model.ReturnId, id2 = model.Path, id3 = comment.CommentId });
            }
            catch
            {
                mytrip_articles article = articleRepo.article.GetArticle(id);
                model.Article = article;
                model.VotesCount = articleRepo.article.GetVotesCount(id);
                model.Anchor = "create";
                model.showRelatedLinks = ModuleSetting.showRelatedLinks();
                model.tableWidth = (model.showRelatedLinks) ? "width:50%;" : "width:100%;";
                model.Blog = article.mytrip_articlescategory.Blog;
                return View(model);
            }
        }
        // *************************************
        // URL: /Article/Rate/Id/vote/count/
        // *****  Rate article  *******
        [HttpPost]
        [Authorize]
        public string Rate(int id, int vote, int count)
        {
            double total = (double)articleRepo.article.CreateVote(id, vote);
            int newcount = articleRepo.article.GetVotesCount(id);
            StringBuilder result = new StringBuilder();
            result.AppendLine(GeneralMethods.CoreRating(true, false, total, newcount));
            if (count == newcount)
                result.AppendLine("<br/>" + CoreLanguage.you_have_a_voted); 
            else
                result.AppendLine("<br/>" + CoreLanguage.thanks_for_vote);
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
            if (id == 0 && articleRepo.category.GetCategoriesCount(LocalisationSetting.culture()) == 0)
            {
                return RedirectToAction("CreateCategory", "Article", new { id = 0 });
            }
            ArticleModel model = new ArticleModel();
            model.CategoryId = id;
            model.Path = id2;
            model.CloseDate = new DateTime(2099, 12, 12).ToString("yyyy-MM-dd");
            if (!LocalisationSetting.unlockAllCulture() || !cat.AllCulture)
                model.ShowAllCulture = "none;";

            if (cat.Blog)
            {
                model.PageTitle = ArticleLanguage.create_new_post;
                model.ShowArticleOptions = "none";
                model.ApprovedComment = true;
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(id, LocalisationSetting.culture()), "CategoryId", "Title");
            }
            else
            {
                model.PageTitle = ArticleLanguage.create_new_article;
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(LocalisationSetting.culture()), "CategoryId", "Title");
                model.ShowIncludeAnonymComment = "none";
            }
            #region tags
            model.Tags = articleRepo.article.GetAllTags();
            #endregion
            model.Theme = ThemeSetting.theme();
            return View(model);
        }
        [HttpPost]
        [Authorize]
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
                          , DateTime.Parse(model.CloseDate), model.AllCulture, model.ModerateComments, model.CommentVotes, model.Pages);
                }
                else
                {
                    article = articleRepo.article.CreatePost(model.CategoryId, model.Title, model.Abstract, model.Body
                         , model.ImageForAbstract, model.OnlyForRegisterUser, model.IncludeAnonymComment, model.ModerateComments, model.CommentVotes, model.Pages);
                }
                #region tags
                IQueryable<mytrip_articlestag> ts = articleRepo.article.GetAllTags();
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
            model.Tags = articleRepo.article.GetAllTags();
            model.Theme = ThemeSetting.theme();
            return View(model);

        }
        // *************************************
        // URL: /Article/Edit/ArticleId/Path
        // *** редактировать статью или пост ***
        [Authorize]
        public ActionResult Edit(int id, int id2, string id3, string id4)
        {
            ArticleModel model = new ArticleModel();
            mytrip_articles article = articleRepo.article.GetArticle(id);
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
            model.ModerateComments = article.ModerateComments;
            model.CommentVotes = article.CommentVotes;
            if (!article.ApprovedComment || article.OnlyForRegisterUser)
                model.ShowIncludeAnonymComment = "none";
            if (!LocalisationSetting.unlockAllCulture() || !article.mytrip_articlescategory.AllCulture)
                model.ShowAllCulture = "none";
            #endregion
            model.PageTitle = ArticleLanguage.edit + " \"" + article.Title + "\"";
            model.Path = id3;
            if (id3 == "Archive")
                model.Url = id3.Replace("(x)", "/");
            else
                model.Url = Url.Action("View", new { id = id2, id2 = article.Path });
            if (article.mytrip_articlescategory.Blog)
            {
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(article.mytrip_articlescategory.CategoryId, LocalisationSetting.culture()), "CategoryId", "Title");
                model.ShowArticleOptions = "none";
            }
            else
                model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(LocalisationSetting.culture()), "CategoryId", "Title", article.CategoryId);
            #region tags
            model.Tags = articleRepo.article.GetAllTags();
            #endregion
            model.Pages = articleRepo.article.GetArticlePages(id);
            model.Theme = ThemeSetting.theme();
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(ArticleModel model)
        {
            try
            {
                articleRepo.article.UpdateArtiсle(model.ArticleId, model.CategoryId, model.Title, model.Abstract, model.Body
                    , model.ApprovedComment, model.ImageForAbstract, model.OnlyForRegisterUser, model.ApprovedVotes, model.IncludeAnonymComment
                    , DateTime.Parse(model.CloseDate), model.AllCulture, model.ModerateComments, model.CommentVotes, model.Pages);
                if (!model.ModerateComments)
                    articleRepo.comment.CloseModeration(model.ArticleId);
                IQueryable<mytrip_articlestag> ts = articleRepo.article.GetAllTags();
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
                return Redirect(model.Url);
            }
            catch
            {
                mytrip_articles article = articleRepo.article.GetArticle(model.ArticleId);
                if (article.mytrip_articlescategory.Blog)
                {
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(article.mytrip_articlescategory.CategoryId, LocalisationSetting.culture()), "CategoryId", "Title");
                    model.ShowArticleOptions = "none";
                }
                else
                    model.Categories = new SelectList(articleRepo.category.GetCategoriesForDdl(LocalisationSetting.culture()), "CategoryId", "Title", article.CategoryId);
                model.Tags = articleRepo.article.GetAllTags();
                return View(model);
            }
        }
        //[HttpPost]
        public ActionResult CheckAllCulture(string id)
        {
            try
            {
                var mc = articleRepo.category.GetCategory(int.Parse(id));
                return Content(mc.AllCulture.ToString().ToLower());
            }
            catch
            {
                return Content("false");
            }
        }
        // ****************************************
        // URL: /Article/Delete/Id/Path/
        // *****  удалить статью или пост  *******
        [Authorize]
        public ActionResult Delete(int id, string id2, string id3)
        {
            mytrip_articles mc = articleRepo.article.GetArticle(id);
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
        // ************************************
        // URL: /Article/Comment/Id/
        // ******  получить комментарий  ******
        public string Comment(int id)
        {
            mytrip_articlescomments comment = articleRepo.comment.GetComment(id);
            return comment.Body;
        }
        // *****************************************
        // URL: /Article/Comment/Id/comment/approved/
        // ******  редактировать комментарий  ******
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Comment(int id, string comment, bool approved)
        {
            if (string.IsNullOrEmpty(comment) || comment == "<P>")
                return Content(ArticleLanguage.field_empty);
            else
            {
                var com = articleRepo.comment.UpdateComment(id, comment, approved);
                string email = MytripUser.UserEmail(com.mytrip_articles.UserName);
                if (!string.IsNullOrEmpty(email) && EmailSetting.unlockSendEmail() && !approved)
                {
                    string domain = Request.Url.Host;
                    string articlelink = string.Format("<a href=\"{0}/Article/View/{1}/{2}\" title=\"{3}\">{3}</a>", domain, com.ArticleId, com.mytrip_articles.Path, com.mytrip_articles.Title);
                    string profilelink = string.Format("<a href=\"{0}/Home/Profile/{1}\" title=\"{2}\">{1}</a>", domain, com.UserName, ArticleLanguage.view_user_profile);
                    string sitelink = string.Format(CoreSetting.NameTitlePage(), "<a href=\"" + domain + "\" title=\"" + domain + "\">" + domain + "</a>");

                    //письмо модеру о редактировании коммента
                    MailMessage msg = new MailMessage();
                    msg.To.Add(email);
                    msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                    msg.Subject = string.Format(CoreSetting.NameTitlePage(), ArticleLanguage.new_comment);
                    msg.Body = string.Format(ArticleLanguage.email_commentmoderate, com.mytrip_articles.UserName, articlelink
                               , profilelink, com.CreateDate, com.Body, sitelink);
                    msg.IsBodyHtml = true;
                    EmailSetting.SendEmail(msg);
                }
                return Content(string.Empty);
            }
        }
        // **********************************************
        // URL: /Article/DeleteComment/Id/Id2/Path/Url/
        // *****  удалить комментарий  *******
        [Authorize]
        public ActionResult DeleteComment(int id, int id2, string id3, string id4)
        {
            mytrip_articlescomments comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.comment.DeleteComment(id);
            return Redirect(id3.Replace("(x)", "/"));
        }
        // **********************************************
        // URL: /Article/ApproveComment/Id/Id2/Path/Url/
        // *****  Approve Comment  *******
        [Authorize]
        public ActionResult ApproveComment(int id, int id2, string id3)
        {
            var comment = articleRepo.comment.GetComment(id);
            if (!userHasRights(comment, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.comment.ApproveComment(id);
            if (EmailSetting.unlockSendEmail())
            {
                string domain = Request.Url.Host;
                string articlelink = string.Format("<a href=\"{0}\" title=\"{1}\">{1}</a>", Request.Url.AbsoluteUri, comment.mytrip_articles.Title);
                string profilelink = string.Format("<a href=\"{0}/Home/Profile/{1}\" title=\"{2}\">{1}</a>", domain, comment.UserName, ArticleLanguage.view_user_profile);
                string sitelink = string.Format(CoreSetting.NameTitlePage(), "<a href=\"" + domain + "\" title=\"" + domain + "\">" + domain + "</a>");
                List<MailMessage> msgs = new List<MailMessage>();
                foreach (var item in articleRepo.subscription.GetSubsciptions(comment.ArticleId))
                {
                    string email = MytripUser.UserEmail(item.UserName);
                    if (!string.IsNullOrEmpty(email) && item.UserName != comment.UserName)
                    {
                        MailMessage msg = new MailMessage();
                        msg.To.Add(email);
                        msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                        msg.Subject = string.Format(CoreSetting.NameTitlePage(), ArticleLanguage.new_comment);
                        msg.Body = string.Format(ArticleLanguage.email_commentupdates, item.UserName, articlelink
                            , profilelink, comment.CreateDate, comment.Body, sitelink);
                        msg.IsBodyHtml = true;
                        msgs.Add(msg);
                    }
                }
                if (msgs.Count != 0)
                    EmailSetting.SendEmail(msgs);
            }
            //if (id3 == "Archive")
            //    return Redirect(id4.Replace("(x)", "/"));
            //else
            //    return RedirectToAction("View", new { id = id2, id2 = id3 });

            return Redirect(id3.Replace("(x)", "/"));
        }
        // **********************************************
        // URL: /Article/OnOffComments/Id/Id2/Path/Url/
        // *******  open close comments  *********
        [Authorize]
        public ActionResult OnOffComments(int id, int id2, string id3)
        {
            var article = articleRepo.article.GetArticle(id);
            if (!userHasRights(article, false))
                return RedirectToAction("LogOn", "Account", new { Request.Url.AbsolutePath });
            articleRepo.article.OnOffComments(id);
            return RedirectToAction("View", new { id = id2, id2 = id3 });
        }
        //[HttpPost]
        public ActionResult VoteComment(int id, bool id2)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Content("Авторизуйтесь");
                if (articleRepo.comment.CheckCommentVote(id))
                    return Content("<div class='right' style='margin-right:10px;text-align:center;width:50px'><b>" 
                        + articleRepo.comment.CreateCommentVote(id, id2).ToString() + "</b></div>");
                else
                    return Content(CoreLanguage.you_have_a_voted);
            }
            catch
            {
                return Content("Error");
            }
        }
        #endregion

        #region Subscription
        // *************************************
        // URL: /Article/SubscribeComments/Id/
        // *****  Subscribe to comments  *******
        [HttpPost]
        [Authorize]
        public string SubscribeComments(int id)
        {
            bool isSubs = articleRepo.subscription.Subscribe(id);
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "submit");
            input.MergeAttribute("name", "id");
            input.MergeAttribute("id", "subscribe");
            input.MergeAttribute("value", id.ToString());
            input.AddCssClass("otheroptions");
            if (isSubs)
            {
                input.MergeAttribute("title", ArticleLanguage.unsubscribe_comments);
                input.MergeAttribute("style", "background:url('/Theme/" + ThemeSetting.theme() + "/images/noalert.png')");
            }
            else
            {
                input.MergeAttribute("title", ArticleLanguage.subscribe_comments);
                input.MergeAttribute("style", "background:url('/Theme/" + ThemeSetting.theme() + "/images/alert.png')");
            }
            return input.ToString();
        }
        // *************************************
        // URL: /Article/Subscriptions/Id/
        // *****  Subscriptions  *******
        [Authorize]
        public ActionResult Subscriptions()
        {
            TempData["username"] = User.Identity.Name;
            TempData["useremail"] = MytripUser.UserEmail(User.Identity.Name);
            return View();
        }
        [Authorize]
        public ActionResult DeleteSubscription(int id)
        {
            articleRepo.subscription.Subscribe(id);
            return RedirectToAction("Subscriptions");
        }
        #endregion

        #region Archive
        //
        // GET: /ArticleArchive/
        [RoleAdminAndEditor]
        public ActionResult Archive(int? id)
        {
            int _count = id ?? 5;
            ArchiveIndexModel model = new ArchiveIndexModel();
            model.Count = _count;
            return View(model);
        }
        //
        // GET: /ArticleArchive/Details/path/culture
        [RoleAdminAndEditor]
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
                case "UnapprovedComments":
                    model.PageTitle = ArticleLanguage.unapproved_comments;
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

        #region ToolTip
        //
        // GET: /Tooltip/
        public string Tooltip(string id, string id1)
        {
            string result = " ";
            int itemid = int.Parse(id1);
            if (id == "Article")
                result = articleRepo.article.GetArticle(itemid).Abstract;
            else if (id == "Comment")
                result = articleRepo.comment.GetComment(itemid).Body;
            else
            {
                var cat = articleRepo.category.GetCategory(itemid);
                if (cat.Blog)
                    result += ArticleLanguage.views + ": " + cat.Views + "<br/> ";
                else
                    result += ArticleLanguage.all_languages + ": " + BoolConvert(cat.AllCulture) + "<br/> "
                    + ArticleLanguage.menu + ": " + BoolConvert(cat.SeparateBlock) + "<br/> ";
                if (cat.SubCategoryId == 0)
                {
                    int ctr = cat.mytrip_articles.Count;
                    foreach (var item in cat.mytrip_articlescategory1)
                    {
                        ctr += item.mytrip_articles.Count;
                    }
                    if (!cat.Blog)
                        result += ArticleLanguage.subcategories + ": " + cat.mytrip_articlescategory1.Count + "<br/> "
                        + ArticleLanguage.articles + ": " + ctr + "<br/>";
                    else
                        result += ArticleLanguage.topics + ": " + cat.mytrip_articlescategory1.Count + "<br/> "
                       + ArticleLanguage.posts + ": " + ctr + "<br/>";
                }
                else
                {
                    if (!cat.Blog)
                        result += ArticleLanguage.articles + ": " + cat.mytrip_articles.Count + "<br/> ";
                    else
                        result += ArticleLanguage.posts + ": " + cat.mytrip_articles.Count + "<br/> ";
                }
            }
            return result;
        }
        static string BoolConvert(bool toConvert)
        {
            if (toConvert)
                return ArticleLanguage.yes;
            else
                return ArticleLanguage.no;
        }
        #endregion

        #region Setting
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            ArticleSettingModel model = new ArticleSettingModel();
            model.viewAdmin = MytripUser.UserInRole(UsersSetting.roleAdmin()) ? "" : "none";
            model.articles = ModuleSetting.articles();
            model.blogs = ModuleSetting.blogs();
            model.countCommentForBlogs = ModuleSetting.countCommentForBlogs();
            model.roleArticleEditor = ModuleSetting.roleArticleEditor();
            model.roleBlogger = ModuleSetting.roleBlogger();
            model.viewInfoClosedComments = ModuleSetting.viewInfoClosedComments();
            model.viewInfoAuthorArticle = ModuleSetting.viewInfoAuthorArticle();
            model.viewInfoViewsArticle = ModuleSetting.viewInfoViewsArticle();
            model.showRelatedLinks = ModuleSetting.showRelatedLinks();
            model.nameArticles = ModuleSetting.NameArticlesPage();
            model.nameBlogs = ModuleSetting.NameBlogsPage();
            model.nameSearch = ModuleSetting.NameSearchPage();
            model.nameTags = ModuleSetting.NameTagsPage();
            model.closecountCommentForBlogs = ModuleSetting.closecountCommentForBlogs();
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Setting(ArticleSettingModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.countCommentForBlogs == null)
                    model.countCommentForBlogs = 5;
                if (MytripUser.UserInRole(UsersSetting.roleAdmin()))
                {
                    MytripUser.RenameRole(ModuleSetting.roleArticleEditor(), model.roleArticleEditor);
                    MytripUser.RenameRole(ModuleSetting.roleBlogger(), model.roleBlogger);
                }
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("mtm.Articles").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "articles")
                    .SetAttributeValue("value", model.articles.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "blogs")
                    .SetAttributeValue("value", model.blogs.ToString());
                article.FirstOrDefault(x => x.Attribute("name").Value == "countCommentForBlogs")
                    .SetAttributeValue("value", model.countCommentForBlogs.ToString());
                if (MytripUser.UserInRole(UsersSetting.roleAdmin()))
                {
                    article.FirstOrDefault(x => x.Attribute("name").Value == "roleBlogger")
                        .SetAttributeValue("value", model.roleBlogger);
                    article.FirstOrDefault(x => x.Attribute("name").Value == "roleArticleEditor")
                        .SetAttributeValue("value", model.roleArticleEditor);
                }
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
                artpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameArticles);
                var blogpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameBlogs").Elements("add");
                blogpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameBlogs);
                var serchpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameSearch").Elements("add");
                serchpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameSearch);
                var tagpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameTags").Elements("add");
                tagpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameTags);
                _doc.Save(_absolutDirectory);

                GeneralMethods.MytripCacheRemove("as_namearticles", true);
                GeneralMethods.MytripCacheRemove("as_nameblogs", true);
                GeneralMethods.MytripCacheRemove("as_nametags", true);
                GeneralMethods.MytripCacheRemove("as_namesearch", true);
                GeneralMethods.MytripCacheRemove("as_articles");
                GeneralMethods.MytripCacheRemove("as_blogs");
                GeneralMethods.MytripCacheRemove("as_viewinfoclosedcomments");
                GeneralMethods.MytripCacheRemove("as_viewinfoauthorarticle");
                GeneralMethods.MytripCacheRemove("as_viewinfoviewsarticle");
                GeneralMethods.MytripCacheRemove("as_showrelatedlinks");
                GeneralMethods.MytripCacheRemove("as_closecountcommentforblogs");
                GeneralMethods.MytripCacheRemove("as_countcommentforblogs");
                GeneralMethods.MytripCacheRemove("as_rolearticleeditor");
                GeneralMethods.MytripCacheRemove("as_roleblogger");

                return RedirectToAction("Index", "Home");
            }
            model.viewAdmin = MytripUser.UserInRole(UsersSetting.roleAdmin()) ? "" : "none";
            return View(model);
        }
        #endregion

        #region Editors managment
        //
        // GET: /ArticleArchive/
        [RoleAdminAndEditor]
        public ActionResult Editors(string id)
        {
            EditorsModel model = new EditorsModel();
            model.PageTitle = ArticleLanguage.journalists_manager;
            List<ListUsers> users = MytripUser.GetUsers(ModuleSetting.roleArticleEditor()).ToList();
            users.Insert(0, new ListUsers(ArticleLanguage.select_user, "0", "0"));
            if (string.IsNullOrEmpty(id))
                id = ArticleLanguage.select_user;
            else
                model.User = id;
            model.Editors = new SelectList(users, "_userName", "_userName", id);
            model.NewEditors = new SelectList(MytripUser.GetUsers(ModuleSetting.roleArticleEditor()), "_userName", "_userName", id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Editors(EditorsModel model)
        {
            if (string.IsNullOrEmpty(model.NewUser))
                return RedirectToAction("Editors", "Article", new { id = model.User });
            string email = MytripUser.UserEmail(model.NewUser);
            string[] data = model.Data.Split('_');
            if (data[0] == "Article")
                articleRepo.article.UpdateArtiсle(int.Parse(data[1]), model.NewUser);
            else
                articleRepo.category.UpdateCategory(int.Parse(data[1]), model.NewUser, email, model.SubCategory);
            return RedirectToAction("Editors", "Article", new { id = model.User });
        }

        #endregion

        #region Profile
        [Authorize]
        public ActionResult Profile(string id)
        {
            ArticleProfile model = new ArticleProfile();
            model.Path = id;
            if (id == "AwaitingModeration")
                model.Title = ArticleLanguage.awaiting_moderation;
            else if (id == "Subscriptions")
                model.Title = ArticleLanguage.my_subscriptions;

            TempData["username"] = model.UserName = User.Identity.Name;
            TempData["useremail"] = MytripUser.UserEmail(User.Identity.Name);
            return View(model);
        }
        #endregion

        #region SideBar
        [HttpPost]
        public string ArticlesActivity(string option)
        {
            string res = "";
            TagBuilder list = new TagBuilder("ul");
            list.AddCssClass("styled");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            if (option == ArticleLanguage.most_viewed)
            {
                title.InnerHtml = "<h4>" + ArticleLanguage.top_viewed + "</h4>";
                foreach (var item in ar.article.GetArticles(LocalisationSetting.culture(), SortBy.Views, 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.top_rated + "</h4>";
                foreach (var item in ar.article.GetArticles(LocalisationSetting.culture(), SortBy.TotalVotes, 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.latest_updates + "</h4>";
                foreach (var item in ar.article.GetArticles(LocalisationSetting.culture(), SortBy.CreateDate, 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.recent_comments + "</h4>";
                foreach (var item in ar.comment.GetComments(false, LocalisationSetting.culture(), 5))
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
                        + "<b>" + profile.ToString() + "</b>: " + item.Body;
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
            TagBuilder list = new TagBuilder("ul");
            list.AddCssClass("styled");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            if (option == ArticleLanguage.most_viewed)
            {
                title.InnerHtml = "<h4>" + ArticleLanguage.top_viewed + "</h4>";
                foreach (var item in ar.article.GetPostsPopular(LocalisationSetting.culture(), 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.top_rated + "</h4>";
                foreach (var item in ar.article.GetPostsRated(LocalisationSetting.culture(), 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.latest_updates + "</h4>";
                foreach (var item in ar.article.GetPostsRecent(LocalisationSetting.culture(), 5))
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
                title.InnerHtml = "<h4>" + ArticleLanguage.recent_comments + "</h4>";
                foreach (var item in ar.comment.GetComments(true, LocalisationSetting.culture(), 5))
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
                        + "<b>" + profile.ToString() + "</b>: " + item.Body;
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
            link.InnerHtml = AvatarHelper.Avatar(null, email, new { width = 30, style = "float:left;padding: 4px 3px 0 0;" }).ToString();
            link.MergeAttribute("title", username);
            string res = link.ToString();
            return res;
        }
        #endregion
    }
}
