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
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using System.Text;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [HandleError]
    [Localization]
    [RoleAdmin]
    public class UsersController : Controller
    {
        
        ICoreRepository _coreRepo;
        public ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
            }
        }
        
        /// <summary>
        /// URL: /Users/Index/pageIndex?/pageSize?/sorting?
        /// </summary>
        /// <param name="id">Page Index</param>
        /// <param name="id2">Page Size</param>
        /// <param name="id3">Sorting</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(int? id, int? id2, string id3)
        {            
            IndexUsersModel model = new IndexUsersModel();            
            model.DefaultCount = 10;
            int _pageIndex = id ?? 1;
            int _pageSize = id2 ?? model.DefaultCount;
            if(id3==null)
            id3 = "UserName";
            int total;            
            model.Users = coreRepo.membershipRepo.mtGetAllUsersPaginal((int)_pageIndex, (int)_pageSize, id3, out total);
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
               return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = model.Search });
            }
            return View(model);
        }
        /// <summary>
        /// URL: /Users/Delete/userName
        /// </summary>
        /// <param name="id">User Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string id)
        {
            Membership.DeleteUser(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// URL: /Users/Details/userName
        /// </summary>
        /// <param name="id">User Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult Details(string id)
        {
            DetailsUserModel model = new DetailsUserModel();
            model.AllRoles = new SelectList(coreRepo.roleRepo.mtGetAllRolesDictionary(), "Key", "Value");
            model.RolesInUser = coreRepo.roleRepo.mtGetRolesForUser(id);
                var user = coreRepo.membershipRepo.mtGetUserByUserNameMember(id);
                model.UserName = user.UserName;
                model.Email = user.mytrip_usersmembership.Email;
                model.LastActivityDate = user.LastActivityDate;
                model.CreationDate = user.mytrip_usersmembership.CreationDate;
                model.LastLoginDate = user.mytrip_usersmembership.LastLoginDate;
                model.LastPasswordChangedDate = user.mytrip_usersmembership.LastPasswordChangedDate;
                model.UserIP = user.mytrip_usersmembership.UserIP;
                model.IsApproved = user.mytrip_usersmembership.IsApproved;
            return View(model);
        }
        /// <summary>
        /// POST: /Users/Details/userName
        /// </summary>
        /// <param name="model">DetailsUserModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Details(DetailsUserModel model,string id)
        {
            if (ModelState.IsValid && ValidateRole(id, model.RoleName))
            {
                coreRepo.roleRepo.mtUnlockUserInRole(id, model.RoleName);
                RedirectToAction("Details", new { id = id });
            }
            var user = coreRepo.membershipRepo.mtGetUserByUserNameMember(id);
            model.AllRoles = new SelectList(coreRepo.roleRepo.mtGetAllRolesDictionary(), "Key", "Value");
            model.RolesInUser = coreRepo.roleRepo.mtGetRolesForUser(id);
            model.UserName = user.UserName;
            model.Email = user.mytrip_usersmembership.Email;
            model.LastActivityDate = user.LastActivityDate;
            model.CreationDate = user.mytrip_usersmembership.CreationDate;
            model.LastLoginDate = user.mytrip_usersmembership.LastLoginDate;
            model.LastPasswordChangedDate = user.mytrip_usersmembership.LastPasswordChangedDate;
            model.UserIP = user.mytrip_usersmembership.UserIP;
            model.IsApproved = user.mytrip_usersmembership.IsApproved;
            return View(model);
        }
        /// <summary>
        /// URL: /Users/IsApproved/userName
        /// </summary>
        /// <param name="id">User Name</param>
        /// <returns></returns>
        public ActionResult IsApproved(string id)
        {
            coreRepo.membershipRepo.UnlockUser(id);
            return RedirectToAction("Details", new { id = id });
        }
        /// <summary>
        /// URL: /Users/IsUserInRole/userName/roleName
        /// </summary>
        /// <param name="id">User Name</param>
        /// <param name="id2">Role Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult IsUserInRole(string id, string id2)
        {
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(id2))
                Roles.RemoveUserFromRole(id, id2);
                coreRepo.roleRepo.mtDeleteUserInRole(id, id2);
            return RedirectToAction("Details", new { id = id });
        }
        /// <summary>
        /// URL: /Users/IndexRole/pageIndex/pageSize/sorting
        /// </summary>
        /// <param name="id">Page Index</param>
        /// <param name="id2">Page Size</param>
        /// <param name="id3">Sorting</param>
        /// <returns>ActionResult</returns>
        public ActionResult IndexRole(int id, int id2, string id3)
        {
            IndexRolesModel model = new IndexRolesModel();
            int total;
            model.DefaultCount = 10;
                model.Roles = coreRepo.roleRepo.mtGetAllRolesPaginal(id, id2, id3, out total);
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
                RedirectToAction("IndexRole", new { id = 1, id2 = model.DefaultCount, id3 = "RoleName" });
            }
            int total;
                model.Roles = coreRepo.roleRepo.mtGetAllRolesPaginal(1, model.DefaultCount, "RoleName", out total);
                model.Total = total;
           
            return View(model);
        }
        /// <summary>
        /// URL: /Users/DeleteRole/roleName
        /// </summary>
        /// <param name="id">Role Name</param>
        /// <returns>ActionResult</returns>
        public ActionResult DeleteRole(string id)
        {
            Roles.DeleteRole(id);
            return RedirectToAction("IndexRole", new { id = 1, id2 = 10, id3 = "RoleName" });
        }
        /// <summary>
        /// Validate Role
        /// </summary>
        /// <param name="id">User Name</param>
        /// <param name="id2">Role Name</param>
        /// <returns>bool</returns>
        private bool ValidateRole(string id, string id2)
        {            
            if (coreRepo.roleRepo.IsUserInRole(id,id2))
                ModelState.AddModelError("RoleName", CoreLanguage.role_in_user);
            return ModelState.IsValid;
        }
    }
}
