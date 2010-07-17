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
using Mytrip.Mvc.Repository;
using System.Xml.Linq;
using Mytrip.Mvc.Repository.DataEntities;

namespace Mytrip.Mvc.Models
{
    [MetadataType(typeof(ProfileUsersModel))]
    public class ProfileUsersModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    [MetadataType(typeof(IndexUsersModel))]
    public class IndexUsersModel
    {
        public IQueryable<mytrip_users> Users { get; set; }
        public int Total { get; set; }
        public int DefaultCount { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "search_max_length")]
        public string Search { get; set; }
    }
    [MetadataType(typeof(IndexRolesModel))]
    public class IndexRolesModel
    {
        public IQueryable<mytrip_usersroles> Roles { get; set; }
        public int Total { get; set; }
        public int DefaultCount { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        [RoleNameDublicate(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_duplicate")]
        public string RoleName { get; set; }
    }
    [MetadataType(typeof(DetailsUserModel))]
    public class DetailsUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public System.DateTime LastActivityDate { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public string UserIP { get; set; }
        public bool IsApproved { get; set; }
        public SelectList AllRoles { get; set; }
        public string[] RolesInUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "role_null")]
        public string RoleName { get; set; }
    }
    public class RoleNameDublicateAttribute : ValidationAttribute
    {
        RoleRepository db = new RoleRepository();
        public override bool IsValid(object value)
        {
            string rolename = (String)value;
            bool result = true;
            if (!String.IsNullOrEmpty(rolename) && db.RoleExists(rolename))
            {
                result = false;
            }
            return result;
        }

    }
    public class RoleAdminAttribute : ActionFilterAttribute
    {
        RoleRepository db = new RoleRepository();
        UsersSetting userset = new UsersSetting();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!db.IsUserInRoleOnline(userset.roleAdmin()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
}
