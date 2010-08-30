using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Mytrip.Mvc;

namespace Mytrip.Articles.Models
{
    [MetadataType(typeof(ArticleSettingModel))]
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
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public int cacheSeconds { get; set; }
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
        public bool replaceСommentsEmail { get; set; }
        public bool showRelatedLinks { get; set; }
    }
}
