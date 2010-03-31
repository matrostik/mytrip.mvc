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
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Objects.DataClasses;
using System.Web;
using System.Web.Routing;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Models
{
    /// <summary>
    /// Index Users Model
    /// </summary>
    [MetadataType(typeof(IndexUsersModel))]
    public class IndexUsersModel
    {
        /// <summary>
        /// IQueryable(mytrip_Users) Users
        /// </summary>
        public IQueryable<mytrip_Users> Users { get; set; }
        /// <summary>
        /// int Total
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// int DefaultCount
        /// </summary>
        public int DefaultCount { get; set; }
        /// <summary>
        /// string Search
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_max_length")]
        public string Search { get; set; }
    }
    /// <summary>
    /// Index Roles Model
    /// </summary>
    [MetadataType(typeof(IndexRolesModel))]
    public class IndexRolesModel
    {
        /// <summary>
        /// IQueryable(mytrip_Roles) Roles
        /// </summary>
        public IQueryable<mytrip_Roles> Roles { get; set; }
        /// <summary>
        /// int Total
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// int DefaultCount
        /// </summary>
        public int DefaultCount { get; set; }
        /// <summary>
        /// string RoleName
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        [RoleNameDublicate(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_duplicate")]
        public string RoleName { get; set; }
    }
    /// <summary>
    /// Details User Model
    /// </summary>
    [MetadataType(typeof(DetailsUserModel))]
    public class DetailsUserModel
    {
        /// <summary>
        /// string UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// string Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// System.DateTime LastActivityDate
        /// </summary>
        public System.DateTime LastActivityDate { get; set; }
        /// <summary>
        /// System.DateTime CreationDate
        /// </summary>
        public System.DateTime CreationDate { get; set; }
        /// <summary>
        /// System.DateTime LastLoginDate
        /// </summary>
        public System.DateTime LastLoginDate { get; set; }
        /// <summary>
        /// System.DateTime LastPasswordChangedDate
        /// </summary>
        public System.DateTime LastPasswordChangedDate { get; set; }
        /// <summary>
        /// string UserIP
        /// </summary>
        public string UserIP { get; set; }
        /// <summary>
        ///  bool IsApproved
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// SelectList AllRoles
        /// </summary>
        public SelectList AllRoles { get; set; }
        /// <summary>
        /// EntityCollection(mytrip_Roles) RolesInUser
        /// </summary>
        public EntityCollection<mytrip_Roles> RolesInUser { get; set; }
        /// <summary>
        /// string RoleName
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "role_null")]
        public string RoleName { get; set; }
    }
    /// <summary>
    /// Role Name Dublicate
    /// </summary>
    public class RoleNameDublicateAttribute : ValidationAttribute
    {
        RoleRepository db = new RoleRepository();
        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string rolename = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(rolename) && db.mtGetRoleByName(rolename) != null)
            {
                result = false;
            }
            return result;
        }

    }
    public class RoleAdminAttribute : ActionFilterAttribute
    {
        RoleRepository db = new RoleRepository();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!db.IsUserInRoleOnline(UsersSetting.roleAdmin))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
}
