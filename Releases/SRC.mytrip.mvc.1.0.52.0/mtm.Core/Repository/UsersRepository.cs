﻿/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using mtm.Core.Settings;

namespace mtm.Core.Repository
{
    /// <summary>Методы для работы пользователями
    /// </summary>
    public static class MytripUser
    {

        #region Публичные методы для работы с ролями
        // **********************************************
        // Публичные методы для работы с ролями
        // **********************************************

        /// <summary>Переименование роли
        /// </summary>
        /// <param name="oldName">старое имя</param>
        /// <param name="newName">новое имя</param>
        public static void RenameRole(string oldName, string newName)
        {
            RoleRepository role = new RoleRepository();
            role.RenameRole(oldName, newName);
        }

        /// <summary>Добавление пользователя в роль
        /// </summary>
        /// <param name="username">имя пользователя</param>
        /// <param name="roleName">имя роли</param>
        public static void UnlockUserInRole(string username, string roleName)
        {
            RoleRepository role = new RoleRepository();
            role.mtUnlockUserInRole(username, roleName);
        }
        
        //****************** E N D **********************
        #endregion

        #region Публичные методы для работы с капчей
        // **********************************************
        // Публичные методы для работы с капчей
        // **********************************************

        /// <summary>Хешированное значение капчи
        /// </summary>
        /// <param name="captcha">значение капчи</param>
        /// <returns>возвращает string</returns>
        public static string HashCaptcha(string captcha)
        {
            string c = FormsAuthentication
                .HashPasswordForStoringInConfigFile((CoreSetting.applicationName() + captcha), "SHA1");
            return c;
        }

        //****************** E N D **********************
        #endregion

        #region Публичные методы для работы с кешем пользователей
        // **********************************************
        // Публичные методы для работы с кешем пользователей
        // **********************************************

        /// <summary>Проверка текущего пользователя на принадлежность к роли
        /// </summary>
        /// <param name="roleName">Имя роли</param>
        /// <returns>возвращает bool</returns>
        public static bool UserInRole(string roleName)
        {
            bool result = false;
            string userName = HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                IEnumerable<ListUsers> cacherole = MytripCacheRole("mtm_cacherole", null, 36000, CacheItemPriority.High) as IEnumerable<ListUsers>;
                string roles = cacherole.FirstOrDefault(x => x._userName == userName)._userRole;
                if (!String.IsNullOrEmpty(roles) && roles.Contains(string.Format("[{0}]", roleName)))
                    result = true;
            }
            return result;
        }

        /// <summary>Получение Email для текущего пользователя
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string UserEmail()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                IEnumerable<ListUsers> cacherole = MytripCacheRole("mtm_cacherole", null, 36000, CacheItemPriority.High) as IEnumerable<ListUsers>;
                return cacherole.Where(x => x._userName == userName).Select(x => x._userEmail).FirstOrDefault();
            }
            return string.Empty;
        }

        /// <summary>Получение Email для пользователя по его имени
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <returns>возвращает string</returns>
        public static string UserEmail(string userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                IEnumerable<ListUsers> cacherole = MytripCacheRole("mtm_cacherole", null, 36000, CacheItemPriority.High) as IEnumerable<ListUsers>;
                return cacherole.Where(x => x._userName == userName).Select(x => x._userEmail).FirstOrDefault();
            }
            return string.Empty;
        }
        /// <summary>Получение списка пользователей принадлежащих к определенной роли
        /// </summary>
        /// <param name="role">имя роли</param>
        /// <returns></returns>
        public static IEnumerable<ListUsers> GetUsers(string role)
        {
            IEnumerable<ListUsers> users = MytripCacheRole("mtm_cacherole", null, 36000, CacheItemPriority.High) as IEnumerable<ListUsers>;
            return users.Where(x => x._userRole.Contains(role)).OrderBy(x => x._userName);
        }
        //****************** E N D **********************
        #endregion

        #region Приватные методы для работы с кешем пользователей
        // **********************************************
        // Приватные методы для работы с кешем пользователей
        // **********************************************

        /// <summary>
        /// Коллекция пользователей для кеша
        /// </summary>
        /// <returns>возвращает IEnumerable[ListUsers]</returns>
        private static IEnumerable<ListUsers> GetAllUsers()
        {
            MembershipRepository db = new MembershipRepository();
            return db._mtGetAllUsers().OrderBy(x => x._userName);
        }

        /// <summary>
        /// Кеш коллекции пользователей
        /// </summary>
        /// <param name="key">имя кеша</param>
        /// <param name="absolutSek">абсолютные секунды</param>
        /// <param name="spanSek">скользящие секунды</param>
        /// <param name="priority">приоритет кеширования</param>
        /// <returns>возвращает object</returns>
        private static object MytripCacheRole(string key, int? absolutSek, int? spanSek, CacheItemPriority priority)
        {
            if (HttpContext.Current.Cache[key] == null)
            {
                TimeSpan _spanSek = spanSek == null ? TimeSpan.Zero : TimeSpan.FromSeconds((int)spanSek);
                DateTime _absolutSek = absolutSek == null ? DateTime.MaxValue : DateTime.Now.AddSeconds((int)absolutSek);
                HttpContext.Current.Cache.Insert(key, GetAllUsers(), null, _absolutSek, _spanSek, priority, null);
            }
            return HttpContext.Current.Cache[key];
        }

        //****************** E N D **********************
        #endregion
    }

    /// <summary>
    /// Класс для коллекции пользователей
    /// </summary>
    public class ListUsers
    {
        #region Класс для коллекции пользователей
        // **********************************************
        // Класс для коллекции пользователей
        // **********************************************

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string userName; 

        /// <summary>
        /// Email пользователя
        /// </summary>
        private string userEmail;
 
        /// <summary>
        /// Список ролей пользователя в одну строчку
        /// </summary>
        private string userRole;

        /// <summary>
        /// экземпляр пользователя
        /// </summary>
        public ListUsers(){ }

        /// <summary>
        /// Внесение данных в экземпляр пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userEmail">Email пользователя</param>
        /// <param name="userRole">Список ролей пользователя в одну строчку</param>
        public ListUsers(string userName, string userEmail, string userRole)
        {
            _userName = userName; _userEmail = userEmail; _userRole = userRole;
        }

        /// <summary>
        /// Список ролей пользователя в одну строчку
        /// </summary>
        public string _userRole { get { return userRole; } set { userRole = value; } }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string _userEmail { get { return userEmail; } set { userEmail = value; } }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string _userName { get { return userName; } set { userName = value; } }

        //****************** E N D **********************
        #endregion
    }
}