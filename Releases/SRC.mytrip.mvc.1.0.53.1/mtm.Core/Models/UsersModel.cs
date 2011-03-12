/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using mtm.Core.Repository.DataEntities;

namespace mtm.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfileUsersModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        public mytrip_usersprofile UserProfile { get; set; }
        public string bodyonload { get; set; }
        public bool closeprofile { get; set; }
        public string title { get; set; }
    }
    public class EditProfileModel
    {
        public string username { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        [AllowHtml]
        public string description { get; set; }
        public string icq { get; set; }
        public string phone { get; set; }
        public bool profilclose { get; set; }
        public string site { get; set; }
        public string skype { get; set; }
        public string bodyonload { get; set; }
        public mytrip_usersprofile UserProfile { get; set; }
        public string[] bread { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class IndexUsersModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_users> Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DefaultCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_max_length")]
        public string Search { get; set; }
        public string[] bread { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class IndexRolesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_usersroles> Roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DefaultCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        [RoleNameDublicate(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_duplicate")]
        public string RoleName { get; set; }
        public string[] bread { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DetailsUserModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime LastActivityDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreationDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime LastLoginDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime LastPasswordChangedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserIP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SelectList AllRoles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] RolesInUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "role_null")]
        public string RoleName { get; set; }
        public string[] bread { get; set; }
    }


}
