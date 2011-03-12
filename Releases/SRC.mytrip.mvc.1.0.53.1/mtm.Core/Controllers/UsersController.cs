/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web.Mvc;
using System.Web.Security;
using mtm.Core.Models;
using mtm.Core.Settings;

namespace mtm.Core.Controllers
{
    /// <summary>Контроллер по работе с зарегестрированными пользователями</summary>
    [RoleAdmin]
    public class UsersController : ICoreController
    {        
        /// <summary>GET: /Users/Index Список пользователей сайта</summary>
        /// <param name="id">номер страницы</param>
        /// <param name="id2">количество записей на страницы</param>
        /// <param name="id3">параметр сортировки</param>
        /// <returns></returns>
        public ActionResult Index(int? id, int? id2, string id3)
        {            
            IndexUsersModel model = new IndexUsersModel();            
            model.DefaultCount = 10;
            int _pageIndex = id ?? 1;
            int _pageSize = id2 ?? model.DefaultCount;
            if(id3==null)
                id3 = "_LastActivityDate";
            int total;            
            model.Users = coreRepo.membershipRepo.mtGetAllUsersPaginal((int)_pageIndex, (int)_pageSize, id3, out total);
            model.Total = total;
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.user_manager };
            model.bread = a;
            return View(model);
        }

        /// <summary>POST: /Users/Index Поиск пользователей по имени или Email</summary>
        /// <param name="model">IndexUsersModel</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(IndexUsersModel model)
        {            
            if (ModelState.IsValid)
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = model.Search });
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.user_manager };
            model.bread = a;
            return View(model);
        }

        /// <summary>GET: /Users/Delete Удаление пользователя</summary>
        /// <param name="id">имя пользователя</param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            Membership.DeleteUser(id);
            GeneralMethods.MytripCacheRemove("mtm_cacherole");
            return RedirectToAction("Index");
        }

        /// <summary>GET: /Users/Details Страница с данными по конкретному пользователю</summary>
        /// <param name="id">имя пользователя</param>
        /// <returns></returns>
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
                string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 "<a href='/Users'>" + CoreLanguage.user_manager + "</a>",
                                 id };
                model.bread = a;
            return View(model);
        }

        /// <summary>POST: /Users/Details Добавление пользователя в роль</summary>
        /// <param name="model">DetailsUserModel</param>
        /// <param name="id">имя пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(DetailsUserModel model,string id)
        {
            if (ModelState.IsValid && ValidateRole(id, model.RoleName))
            {
                coreRepo.roleRepo.mtUnlockUserInRole(id, model.RoleName);
                GeneralMethods.MytripCacheRemove("mtm_cacherole");
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 "<a href='/Users'>" + CoreLanguage.user_manager + "</a>",
                                 id };
            model.bread = a;
            return View(model);
        }

        /// <summary>GET: /Users/IsApproved Блокировка и разблокировка пользователя</summary>
        /// <param name="id">имя пользователя</param>
        /// <returns></returns>
        public ActionResult IsApproved(string id)
        {
            coreRepo.membershipRepo.UnlockUser(id);
            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>GET: /Users/IsUserInRole Удаление пользователя из роли</summary>
        /// <param name="id">имя пользователя</param>
        /// <param name="id2">имя роли</param>
        /// <returns></returns>
        public ActionResult IsUserInRole(string id, string id2)
        {
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(id2))
                Roles.RemoveUserFromRole(id, id2);
                coreRepo.roleRepo.mtDeleteUserInRole(id, id2);
                GeneralMethods.MytripCacheRemove("mtm_cacherole");
            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>GET: /Users/IndexRole Список ролей сайта</summary>
        /// <param name="id">номер страницы</param>
        /// <param name="id2">количество записей на странице</param>
        /// <param name="id3">параметр сортировки</param>
        /// <returns></returns>
        public ActionResult IndexRole(int id, int id2, string id3)
        {
            IndexRolesModel model = new IndexRolesModel();
            int total;
            model.DefaultCount = 10;
                model.Roles = coreRepo.roleRepo.mtGetAllRolesPaginal(id, id2, id3, out total);
                model.Total = total;
                string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 "<a href='/Users'>" + CoreLanguage.user_manager + "</a>",
                                 CoreLanguage.all_roles };
                model.bread = a;
            return View(model);
        }

        /// <summary>POST: /Users/IndexRole Добавление новой роли</summary>
        /// <param name="model">IndexRolesModel</param>
        /// <returns></returns>
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
                string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 "<a href='/Users'>" + CoreLanguage.user_manager + "</a>",
                                 CoreLanguage.all_roles };
                model.bread = a;
            return View(model);
        }

        /// <summary>GET: /Users/DeleteRole Удаление роли</summary>
        /// <param name="id">имя роли</param>
        /// <returns></returns>
        public ActionResult DeleteRole(string id)
        {
            Roles.DeleteRole(id);
            GeneralMethods.MytripCacheRemove("mtm_cacherole");
            return RedirectToAction("IndexRole", new { id = 1, id2 = 10, id3 = "RoleName" });
        }

        /// <summary>Проверка пользователя на принадлежность к роли</summary>
        /// <param name="id">имя пользователя</param>
        /// <param name="id2">имя роли</param>
        /// <returns></returns>
        private bool ValidateRole(string id, string id2)
        {            
            if (coreRepo.roleRepo.IsUserInRole(id,id2))
                ModelState.AddModelError("RoleName", CoreLanguage.role_in_user);
            return ModelState.IsValid;
        }
    }
}
