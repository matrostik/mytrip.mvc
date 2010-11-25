using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Mytrip.Mvc.Models;
using System.Web.Routing;
using System.Web;
using Mytrip.Mvc.Repository;
using System.Xml.Linq;
using Mytrip.Articles.Repository.DataEntities;

namespace Mytrip.Articles.Models
{
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
    public class ArticleIndexModel
    {
        public IQueryable<mytrip_articlescategory> Categories { get; set; }
        public IQueryable<mytrip_articles> Articles { get; set; }
        public int CategoryId { get; set; }
        public mytrip_articlescategory ParentCategory { get; set; }
        public bool ShowAddCategory { get; set; }
        public bool ShowAddSubCategory { get; set; }
        public bool ShowEditDelete { get; set; }
        public bool ShowAddArticle { get; set; }
        public bool ShowAddBlog { get; set; }
        public bool ShowAddPost { get; set; }
        public string PageTitle { get; set; }
        public string Path { get; set; }
        public bool ShowDetailsBlog { get; set; }
        public int Total { get; set; }
        public int DefaultCount { get; set; }
    }
    public class ArticleViewModel
    {
        public mytrip_articles Article { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string AnonymName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "invalid_email")]
        public string AnonymEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        [SkipRequestValidation]
        public string Comment { get; set; }
        [_CaptchaNullString(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        [_CaptchaError(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "account_error_captcha")]
        public string Captcha { get; set; }
        public int VotesCount { get; set; }
        public int Vote { get; set; }
        public string Anchor { get; set; }
        public bool Blog { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public bool showRelatedLinks { get; set; }
        public bool isSubscribed { get; set; }
        public bool CommentApproved { get; set; }
        public int Total { get; set; }
        public int[] PagesIds { get; set; }
        public int ReturnId { get; set; }
        public string tableWidth { get; set; }
    }
    public class ArticleVote
    {
        public int ArticleId { get; set; }
        public int Vote { get; set; }
        public int TotalVotes { get; set; }
        public double AverageVotes { get; set; }
    }
    public class ArticleModel
    {
        public string PageTitle { get; set; }
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
        [RegularExpression("^(.){5,255}$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "title_lenght_5_255")]
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "title_empty")]
        public string Title { get; set; }
        [SkipRequestValidation]
        public string ImageForAbstract { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "short_desc_empty")]
        [SkipRequestValidation]
        public string Abstract { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "content_empty")]
        [SkipRequestValidation]
        public string Body { get; set; }
        [RegularExpression(@"^\s*(\d{2,4})\-(\d{1,2})\-(\d{1,2})\s*$", ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "invalid_date")]
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "close_date_empty")]
        public string CloseDate { get; set; }
        public bool ApprovedComment { get; set; }
        public bool IncludeAnonymComment { get; set; }
        public bool ApprovedVotes { get; set; }
        public bool AllCulture { get; set; }
        public bool OnlyForRegisterUser { get; set; }
        public bool ModerateComments { get; set; }
        public bool CommentVotes { get; set; }
        public string ShowArticleOptions { get; set; }
        public string ShowOnlyForRegisted { get; set; }
        public string ShowIncludeAnonymComment { get; set; }
        public string ShowAllCulture { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public IQueryable<mytrip_articlestag> Tags { get; set; }
        public string NewTags { get; set; }
        public string Theme { get; set; }
        [SkipRequestValidation]
        public string[] Pages { get; set; }
    }
    public class CommentModel
    {
        public int CommentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string Comment { get; set; }
        public string PageTitle { get; set; }
        public int ArticleId { get; set; }
        public bool CommentApproved { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
    }

    public class ArticleProfile
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string UserName { get; set; }
    }

    public class RoleArticleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleArticleEditor()) || MytripUser.UserInRole(ModuleSetting.roleBlogger()) || MytripUser.UserInRole(ModuleSetting.roleChiefEditor()))
            { }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
    public class _CaptchaNullStringAttribute : ValidationAttribute
    {
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var captcha = (String)value;
            bool result = true;
            if (!HttpContext.Current.User.Identity.IsAuthenticated && String.IsNullOrEmpty(captcha))
            {
                result = false;
            }

            return result;
        }

    }
    /// <summary>
    /// Captcha Error Attribute 
    /// </summary>
    public class _CaptchaErrorAttribute : ValidationAttribute
    {
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public override bool IsValid(object value)
        {
            var captcha = (String)value;
            bool result = true;
            if (string.IsNullOrEmpty(captcha))
                return false;
            if (!HttpContext.Current.User.Identity.IsAuthenticated && MytripUser.HashCaptcha(captcha) != HttpContext.Current.Session["antibotimage"].ToString())
            {
                result = false;
            }

            return result;
        }

    }
}
