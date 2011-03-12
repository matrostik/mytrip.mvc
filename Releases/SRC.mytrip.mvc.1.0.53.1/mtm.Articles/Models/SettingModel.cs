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
        public bool closecountCommentForBlogs { get; set; }
        public bool showRelatedLinks { get; set; }
        public string viewAdmin { get; set; }
        public string[] bread { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string partialAccordion { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string partialNoAccordion { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string partialMenuLogon { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string partialMenuLogonWrap { get; set; }
    }
    public class ArticleSeoModel
    {
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameArticles_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameBlogs_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameTags_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameSearch_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameArticles_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameBlogs_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameTags_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string nameSearch_en_us { get; set; }  
        public string[] bread { get; set; }
        public string view_ru_ru { get; set; }
        public string view_en_us { get; set; }
        public string view_blog { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string article_seokeywords_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string article_seodescription_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string article_seokeywords_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string article_seodescription_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string blogs_seokeywords_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string blogs_seodescription_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string blogs_seokeywords_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string blogs_seodescription_en_us { get; set; }

        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string titleArticles_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string titleBlogs_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string titleArticles_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(ArticleLanguage), ErrorMessageResourceName = "field_empty")]
        public string titleBlogs_en_us { get; set; }
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
        public string[] bread { get; set; }
    }
}
