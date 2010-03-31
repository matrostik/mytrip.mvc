﻿//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using System.Text;
using Mytrip.Core.Models;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [HandleError]
    [Localization]
    [RoleAdmin]
    public class UsersController : Controller
    {
        
        MembershipRepository _membershipRepo;
        public MembershipRepository membershipRepo
        {
            get
            {
                if (_membershipRepo == null)
                    _membershipRepo = new MembershipRepository();
                return _membershipRepo;
            }
        }
        RoleRepository _roleRepo;
        public RoleRepository roleRepo
        {
            get
            {
                if (_roleRepo == null)
                    _roleRepo = new RoleRepository();
                return _roleRepo;
            }
        }
        /// <summary>
        /// URL: /Users/Index/pageIndex?/pageSize?/sorting?
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sorting">Sorting</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(int? pageIndex, int? pageSize, string sorting)
        {            
            IndexUsersModel model = new IndexUsersModel();            
            model.DefaultCount = 10;
            int _pageIndex = pageIndex ?? 1;
            int _pageSize = pageSize ?? model.DefaultCount;
            int total;
            model.Users = membershipRepo.mtGetAllUsersPaginal((int)_pageIndex,(int)_pageSize,sorting,out total);
            model.Total = total;           
            return View(model);
        }
        /// <summary>
        /// POST: /Users/Index/pageIndex?/pageSize?/sorting?
        /// </summary>
        /// <param name="model">IndexUsersModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(IndexUsersModel model)
        {            
            if (ModelState.IsValid)
            {
                RedirectToAction("Index", new { pageIndex = 1, pageSize = 10, sorting = model.Search });
            }
            return View(model);
        }
        /// <summary>
        /// URL: /Users/Delete/userName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string userName)
        {
            Membership.DeleteUser(userName);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// URL: /Users/Details/userName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult Details(string userName)
        {
            var user = membershipRepo.mtGetUserByUserName(userName);
            DetailsUserModel model = new DetailsUserModel();
            model.UserName = user.UserName;
            model.Email = user.mytrip_Membership.Email;
            model.LastActivityDate = user.LastActivityDate;
            model.CreationDate = user.mytrip_Membership.CreationDate;
            model.LastLoginDate = user.mytrip_Membership.LastLoginDate;
            model.LastPasswordChangedDate = user.mytrip_Membership.LastPasswordChangedDate;
            model.UserIP = user.mytrip_Membership.UserIP;
            model.IsApproved = user.mytrip_Membership.IsApproved;
            model.AllRoles = new SelectList(roleRepo.mtGetAllRoles(), "RoleName", "RoleName");
            model.RolesInUser = user.mytrip_Roles;            
            return View(model);
        }
        /// <summary>
        /// POST: /Users/Details/userName
        /// </summary>
        /// <param name="model">DetailsUserModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Details(DetailsUserModel model)
        {
            if (ModelState.IsValid && ValidateRole(model.UserName, model.RoleName))
            {
                //Roles.AddUserToRole(model.UserName, model.RoleName);
                roleRepo.mtUnlockUserInRole(model.UserName, model.RoleName);
                RedirectToAction("Details", new { userName = model.UserName });
            }
            var user = membershipRepo.mtGetUserByUserName(model.UserName);
            model.AllRoles = new SelectList(roleRepo.mtGetAllRoles(), "RoleName", "RoleName");
            model.RolesInUser = user.mytrip_Roles;
            return View(model);
        }
        /// <summary>
        /// URL: /Users/IsApproved/userName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns></returns>
        public ActionResult IsApproved(string userName)
        {
            membershipRepo.UnlockUser(userName);
            return RedirectToAction("Details", new { userName });
        }
        /// <summary>
        /// URL: /Users/IsUserInRole/userName/roleName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult IsUserInRole(string userName, string roleName)
        {
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(roleName))
                Roles.RemoveUserFromRole(userName, roleName);
                roleRepo.mtDeleteUserInRole(userName, roleName);
            return RedirectToAction("Details", new { userName });
        }
        /// <summary>
        /// URL: /Users/IndexRole/pageIndex/pageSize/sorting
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sorting">Sorting</param>
        /// <returns>ActionResult</returns>
        public ActionResult IndexRole(int pageIndex, int pageSize, string sorting)
        {
            IndexRolesModel model = new IndexRolesModel();
            int total;
            model.DefaultCount = 10;
            model.Roles = roleRepo.mtGetAllRolesPaginal(pageIndex, pageSize, sorting, out total);
            model.Total = total;
            return View(model);
        }
        /// <summary>
        /// POST: /Users/IndexRole/pageIndex/pageSize/sorting
        /// </summary>
        /// <param name="model">IndexRolesModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult IndexRole(IndexRolesModel model)
        {
            model.DefaultCount = 10;
            if (ModelState.IsValid)
            {
                Roles.CreateRole(model.RoleName);
                RedirectToAction("IndexRole", new { pageIndex = 1, pageSize = model.DefaultCount, sorting = "RoleName" });
            }
            int total;
            model.Roles = roleRepo.mtGetAllRolesPaginal(1, model.DefaultCount, "RoleName", out total);
            model.Total = total;
            return View(model);
        }
        /// <summary>
        /// URL: /Users/DeleteRole/roleName
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult DeleteRole(string roleName)
        {
            Roles.DeleteRole(roleName);
            return RedirectToAction("IndexRole", new { pageIndex = 1, pageSize = 10, sorting = "RoleName" });
        }
        /// <summary>
        /// Validate Role
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>bool</returns>
        private bool ValidateRole(string userName, string roleName)
        {            
            if (roleRepo.IsUserInRole(userName,roleName))
                ModelState.AddModelError("RoleName", CoreLanguage.role_in_user);
            return ModelState.IsValid;
        }
    }
}