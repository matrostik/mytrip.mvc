/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Mytrip.Mvc.Repository.DataEntities;

namespace Mytrip.Mvc.Models
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataType(typeof(ProfileUsersModel))]
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
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataType(typeof(IndexUsersModel))]
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
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataType(typeof(IndexRolesModel))]
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
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataType(typeof(DetailsUserModel))]
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
    }
    
   
}
