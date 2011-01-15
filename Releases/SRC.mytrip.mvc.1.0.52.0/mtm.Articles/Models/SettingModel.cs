using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using mtm.Core;
using mtm.Core.Repository;
using System.Web.Mvc;

namespace mtm.Articles.Models
{
    public class ArticleSettingModel
    {
        public bool articles { get; set; }
        public bool blogs { get; set; }
        public int? countCommentForBlogs { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleArticleEditor { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleBlogger { get; set; }
        public bool viewInfoClosedComments { get; set; }
        public bool viewInfoAuthorArticle { get; set; }
        public bool viewInfoViewsArticle { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameArticles { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameBlogs { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameTags { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameSearch { get; set; }
        public bool closecountCommentForBlogs { get; set; }
        public bool showRelatedLinks { get; set; }
        public string viewAdmin { get; set; }
    }
    public class EditorsModel
    {
        public string PageTitle { get; set; }
        public string User { get; set; }
        public string NewUser { get; set; }
        public string Data { get; set; }
        public SelectList Editors { get; set; }
        public SelectList NewEditors { get; set; }
        public bool SubCategory { get; set; }
    }
}
