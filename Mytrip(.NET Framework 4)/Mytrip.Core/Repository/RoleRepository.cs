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
using System.Web.Security;
using System.Web;

namespace Mytrip.Core.Repository
{
    /// <summary>
    /// Role Repository : Role Provider
    /// </summary>
    public class RoleRepository : RoleProvider
    {
        /// <summary>
        /// Connecting to ORM
        /// </summary>
        usersEntities _entities;
        public usersEntities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new usersEntities(UsersSetting.connectionString);
                return _entities;
            }
        }
        /// <summary>
        /// Add Users to Roles
        /// </summary>
        /// <param name="usernames">Users Names</param>
        /// <param name="roleNames">Roles Names</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = mtGetUserByName(username);
                    if (user != null)
                    {
                        if (mtGetRoleByName(roleName) == null)
                            mtCreateRole(roleName);
                        bool addRole = true;
                        var role = mtGetRoleByName(roleName);
                        foreach (mytrip_Roles x in user.mytrip_Roles.ToList())
                        {
                            if (roleName == x.RoleName)
                                addRole = false;
                        }
                        if (addRole)
                        {
                            mytrip_Users x = user;
                            x.mytrip_Roles.Add(role);
                            entities.SaveChanges();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Name Http Host
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return UsersSetting.applicationName;
            }
            set { UsersSetting.applicationName = value; }
        }
        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="roleName">Role Name</param>
        public override void CreateRole(string roleName)
        {
            mtCreateRole(roleName);
        }
        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="throwOnPopulatedRole"></param>
        /// <returns>bool</returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                mtRemoveUsersFromRole(roleName);
                mytrip_Roles x = role;
                entities.DeleteObject(x);
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Find Users In Role
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="usernameToMatch">User Name to Match</param>
        /// <returns>string[]</returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] result = new string[0];
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                int namberUser = 0;
                int countUser = role.mytrip_Users.Count();
                result = new string[countUser];
                foreach (mytrip_Users x in role.mytrip_Users)
                {
                    if (x.UserName.IndexOf(usernameToMatch) != -1)
                    {
                        result[namberUser] = x.UserName;
                        namberUser++;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns>string[]</returns>
        public override string[] GetAllRoles()
        {
            string[] result = new string[0];
            var roles = entities.mytrip_Roles.OrderBy(x => x.RoleName);
            if (roles != null)
            {
                int namberRole = 0;
                int countRoles = roles.Count();
                result = new string[countRoles];
                foreach (mytrip_Roles x in roles)
                {
                    result[namberRole] = x.RoleName;
                    namberRole++;
                }
            }
            return result;
        }
        /// <summary>
        /// Get Roles For User
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>string[]</returns>
        public override string[] GetRolesForUser(string username)
        {
            string[] result = new string[0];
            var user = mtGetUserByName(username);
            if (user != null)
            {
                int namberRole = 0;
                int countRoles = user.mytrip_Roles.Count();
                result = new string[countRoles];
                foreach (mytrip_Roles x in user.mytrip_Roles)
                {
                    result[namberRole] = x.RoleName;
                    namberRole++;
                }
            }
            return result;
        }
        /// <summary>
        /// Get Users In Role
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>string[]</returns>
        public override string[] GetUsersInRole(string roleName)
        {
            string[] result = new string[0];
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                int namberUser = 0;
                int countUser = role.mytrip_Users.Count();
                result = new string[countUser];
                foreach (mytrip_Users x in role.mytrip_Users)
                {
                    result[namberUser] = x.UserName;
                    namberUser++;
                }
            }
            return result;
        }
        /// <summary>
        /// Is User In Role
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>bool</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_Users x in role.mytrip_Users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            return result;
        }
        public bool IsUserInRoleOnline(string roleName)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                var role = mtGetRoleByName(roleName);
                if (role != null)
                {
                    foreach (mytrip_Users x in role.mytrip_Users)
                    {
                        if (x.UserName == HttpContext.Current.User.Identity.Name)
                            result = true;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Remove Users From Roles
        /// </summary>
        /// <param name="usernames">Users Names</param>
        /// <param name="roleNames">Roles Names</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = mtGetUserByName(username);
                    var role = mtGetRoleByName(roleName);
                    if (user != null && role != null)
                    {
                        bool removeRole = true;
                        foreach (mytrip_Roles x in user.mytrip_Roles)
                        {
                            if (roleName != x.RoleName)
                                removeRole = false;
                        }
                        if (removeRole)
                        {
                            mytrip_Users x = user;
                            x.mytrip_Roles.Remove(role);
                            entities.SaveChanges();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Role Exists
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>bool</returns>
        public override bool RoleExists(string roleName)
        {
            if (mtGetRoleByName(roleName) != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Create Unique RoleId
        /// </summary>
        /// <returns>int</returns>
        private int mtCreateUniqueRoleId()
        {
            int result;
            int roleId;
            for (roleId = 1; mtGetRoleByRoleId(roleId) != null; roleId++) ;
            result = roleId;
            return result;
        }
        /// <summary>
        /// Get User by Name
        /// </summary>
        /// <param name="username">User Name</param>
        /// <returns>mytrip_Users</returns>
        private mytrip_Users mtGetUserByName(string username)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserName == username);
        }
        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleid">RoleId</param>
        /// <returns>mytrip_Roles</returns>
        private mytrip_Roles mtGetRoleByRoleId(int roleid)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleId == roleid);
        }
        /// <summary>
        /// Remove Users From Role
        /// </summary>
        /// <param name="rolename">Role Name</param>
        private void mtRemoveUsersFromRole(string rolename)
        {
            var role = mtGetRoleByName(rolename);
            if (role.mytrip_Users != null)
            {
                foreach (mytrip_Users x in role.mytrip_Users.ToList())
                {
                    var user = mtGetUserByName(x.UserName);
                    role.mytrip_Users.Remove(user);
                    entities.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="roleName">Role Name</param>
        private void mtCreateRole(string roleName)
        {
            var role = mtGetRoleByName(roleName);
            if (role == null)
            {
                mytrip_Roles x = new mytrip_Roles
                {
                    RoleId = mtCreateUniqueRoleId(),
                    RoleName = roleName
                };
                entities.AddTomytrip_Roles(x);
                entities.SaveChanges();
            }
        }
        /// <summary>
        /// Get Role By Name
        /// </summary>
        /// <param name="rolename">Role Name</param>
        /// <returns>mytrip_Roles</returns>
        public mytrip_Roles mtGetRoleByName(string rolename)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleName == rolename);
        }
        /// <summary>
        /// Remove User From Role
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="rolename">Role Name</param>
        private void mtRemoveUserFromRole(string username, string rolename)
        {
            var role = mtGetRoleByName(rolename);
                foreach (mytrip_Users x in role.mytrip_Users.ToList())
                {
                    if (username == x.UserName)
                    {
                        var user = mtGetUserByName(x.UserName);
                        role.mytrip_Users.Remove(user);
                        entities.SaveChanges();
                    }
               
            }
        }
        /// <summary>
        /// Add User to Role
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="rolename">Role Name</param>
        public void mtAddUserToRole(string username, string rolename)
        {
            var role = mtGetRoleByName(rolename);
            var user = mtGetUserByName(username);
            role.mytrip_Users.Add(user);
            entities.SaveChanges();
             
        }
        /// <summary>
        /// Get All Roles Paginal
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sorting">Sorting</param>
        /// <param name="total">Total</param>
        /// <returns>IQueryable(mytrip_Roles)</returns>
        public IQueryable<mytrip_Roles> mtGetAllRolesPaginal(int pageIndex, int pageSize, string sorting, out int total)
        {
            total = entities.mytrip_Roles.OrderBy(x => x.RoleName).Count();
            var roles = entities.mytrip_Roles.OrderBy(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (sorting == "RoleName")
                roles = entities.mytrip_Roles.OrderBy(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_RoleName")
                roles = entities.mytrip_Roles.OrderByDescending(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "UserCount")
                roles = entities.mytrip_Roles.OrderBy(x => x.mytrip_Users.Count()).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserCount")
                roles = entities.mytrip_Roles.OrderByDescending(x => x.mytrip_Users.Count()).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (!String.IsNullOrEmpty(sorting))
            {
                total = entities.mytrip_Roles.Where(x => x.RoleName.IndexOf(sorting) != -1).Count();
                roles = entities.mytrip_Roles.Where(x => x.RoleName.IndexOf(sorting) != -1).OrderBy(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return roles;
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns>IQueryable(mytrip_Roles)</returns>
        public IQueryable<mytrip_Roles> mtGetAllRoles()
        {
            return entities.mytrip_Roles.OrderBy(x => x.RoleName);
        }
        /// <summary>
        /// Unlock User In Role
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="roleName">Role Name</param>
        public void mtUnlockUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_Users x in role.mytrip_Users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            if (result) {
            mtRemoveUserFromRole(username, roleName);
            }
            else { mtAddUserToRole(username, roleName); }
        }        
        public void mtDeleteUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_Users x in role.mytrip_Users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            if (result)
            {
                mtRemoveUserFromRole(username, roleName);
            }
        }
    }
}