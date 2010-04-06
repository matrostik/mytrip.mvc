﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Mytrip.Core.Models;
using System.Web.Routing;
using System.Web;
using Mytrip.Core.Repository;

namespace Mytrip.Articles.Models
{
    [MetadataType(typeof(CategoryModel))]
    public class CategoryModel
    {
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        [RegularExpression("^(.){3,255}$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "data_lenght_3_255")]
        public string Title { get; set; }
        public bool SeparateBlock { get; set; }
        public bool AllCulture { get; set; }
        public int CategoryId { get; set; }
        public string Path { get; set; }
        public string PageTitle { get; set; }
        public string Url { get; set; }
        public string ShowSeparateBlock { get; set; }
        public string ShowAllCulture { get; set; }
    }
    [MetadataType(typeof(ArticleIndexModel))]
    public class ArticleIndexModel
    {
        public IQueryable<mytrip_ArticlesCategory> Categories { get; set; }
        public IQueryable<mytrip_Articles> Articles { get; set; }
        public int CategoryId { get; set; }
        public mytrip_ArticlesCategory ParentCategory { get; set; }
        public bool ShowAddCategory { get; set; }
        public bool ShowAddSubCategory { get; set; }
        public bool ShowEditDelete { get; set; }
        public bool ShowEditDeleteBlog { get; set; }
        public bool ShowAddArticle { get; set; }
        public bool ShowAddBlog { get; set; }
        public bool ShowAddPost { get; set; }
        public string PageTitle { get; set; }
        public string Path { get; set; }
        public bool ShowDetailsBlog { get; set; }
        public int Total { get; set; }
        public int DefaultCount { get; set; }
    }
    [MetadataType(typeof(ArticleViewModel))]
    public class ArticleViewModel
    {
        public ArticleVote ArticleVote { get; set; }
        public mytrip_Articles Article { get; set; }
        public IQueryable<mytrip_ArticlesComments> Comments { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string AnonymName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "invalid_email")]
        public string AnonymEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string Comment { get; set; }
    }
    [MetadataType(typeof(ArticleVote))]
    public class ArticleVote
    {
        public int ArticleId { get; set; }
        public int Vote { get; set; }
        public int TotalVotes { get; set; }
        public double AverageVotes { get; set; }
    }
    [MetadataType(typeof(ArticleModel))]
    public class ArticleModel
    {
        public string PageTitle { get; set; }
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
        [RegularExpression("^(.){5,255}$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "title_lenght_5_255")]
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "title_empty")]
        public string Title { get; set; }
        public string ImageForAbstract { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "short_desc_empty")]
        public string Abstract { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "content_empty")]
        public string Body { get; set; }
        [RegularExpression(@"^\s*(\d{2,4})\-(\d{1,2})\-(\d{1,2})\s*$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "invalid_date")]
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "close_date_empty")]
        public string CloseDate { get; set; }
        public bool ApprovedComment { get; set; }
        public bool IncludeAnonymComment { get; set; }
        public bool ApprovedVotes { get; set; }
        public bool AllCulture { get; set; }
        public bool OnlyForRegisterUser { get; set; }
        public string ShowArticleOptions { get; set; }
        public string ShowOnlyForRegisted { get; set; }
        public string ShowIncludeAnonymComment { get; set; }
        public string ShowAllCulture { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public IQueryable<mytrip_ArticlesTag> Tags { get; set; }
        public string NewTags { get; set; }
    }
    [MetadataType(typeof(CommentModel))]
    public class CommentModel
    {
        public int CommentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        //[RegularExpression("(<P>&nbsp;</P>)", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "invalid_date")]
        public string Comment { get; set; }
        public string PageTitle { get; set; }
        public int ArticleId { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
    }
    [MetadataType(typeof(ProfileModel))]
    public class ProfileModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PageTitle { get; set; }
    }
    public class RoleArticleAttribute : ActionFilterAttribute
    {
        RoleRepository db = new RoleRepository();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!db.IsUserInRoleOnline(ArticlesSetting.roleArticleEditor) || !db.IsUserInRoleOnline(ArticlesSetting.roleBlogger) || !db.IsUserInRoleOnline(ArticlesSetting.roleChiefEditor))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
}
