/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Mytrip.Mvc.Repository.DataEntities;
using Mytrip.Mvc.Settings;
namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// 
    /// </summary>
    internal class RoleRepository : RoleProvider
    {
        #region Entities
        Entities _entities;

        /// <summary>
        /// 
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(CoreSetting.connectionString());
                return _entities;
            }
        }

        #endregion

        #region RoleProvider public override

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = mtGetUserByNameRole(username);
                    if (user != null)
                    {
                        var role = mtGetAndCreateRole(roleName);
                        bool addRole = true;
                        foreach (mytrip_usersroles x in user.mytrip_usersroles.ToList())
                        {
                            if (roleName == x.RoleName)
                                addRole = false;
                        }
                        if (addRole)
                        {
                            mytrip_users x = user;
                            x.mytrip_usersroles.Add(role);
                            entities.SaveChanges();
                        }
                    }
                }
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return UsersSetting.applicationName();
            }
            set
            { string applicationName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        public override void CreateRole(string roleName)
        {
            var role = mtGetRoleByName(roleName);
            if (role == null)
            {
                mytrip_usersroles x = new mytrip_usersroles
                {
                    RoleId = mtCreateUniqueRoleId(),
                    RoleName = roleName
                };
                entities.mytrip_usersroles.AddObject(x);
                entities.SaveChanges();
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="throwOnPopulatedRole"></param>
        /// <returns></returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                mtRemoveUsersFromRole(role);
                mytrip_usersroles x = role;
                entities.DeleteObject(x);
                entities.SaveChanges();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="usernameToMatch"></param>
        /// <returns></returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] result = new string[0];
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                int namberUser = 0;
                int countUser = role.mytrip_users.Count();
                result = new string[countUser];
                foreach (mytrip_users x in role.mytrip_users)
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            string[] result = new string[0];
            var roles = entities.mytrip_usersroles.OrderBy(x => x.RoleName);
            if (roles != null)
            {
                int namberRole = 0;
                int countRoles = roles.Count();
                result = new string[countRoles];
                foreach (mytrip_usersroles x in roles)
                {
                    result[namberRole] = x.RoleName;
                    namberRole++;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            string[] result = new string[0];
            var user = mtGetUserByName(username);
            if (user != null)
            {
                int namberRole = 0;
                int countRoles = user.mytrip_usersroles.Count();
                result = new string[countRoles];
                foreach (mytrip_usersroles x in user.mytrip_usersroles)
                {
                    result[namberRole] = x.RoleName;
                    namberRole++;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override string[] GetUsersInRole(string roleName)
        {
            string[] result = new string[0];
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                int namberUser = 0;
                int countUser = role.mytrip_users.Count();
                result = new string[countUser];
                foreach (mytrip_users x in role.mytrip_users)
                {
                    result[namberUser] = x.UserName;
                    namberUser++;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_users x in role.mytrip_users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            return result;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {

            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = mtGetUserByNameRole(username);
                    var role = mtGetRoleByName(roleName);
                    if (user != null && role != null)
                    {
                        bool removeRole = true;
                        foreach (mytrip_usersroles x in user.mytrip_usersroles)
                        {
                            if (roleName != x.RoleName)
                                removeRole = false;
                        }
                        if (removeRole)
                        {
                            mytrip_users x = user;
                            x.mytrip_usersroles.Remove(role);
                            entities.SaveChanges();
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool RoleExists(string roleName)
        {
            if (mtGetRoleByName(roleName) != null)
                return true;
            else
                return false;
        }

        #endregion

        #region RoleProvider internal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        internal IQueryable<mytrip_usersroles> mtGetAllRolesPaginal(int pageIndex, int pageSize, string sorting, out int total)
        {
            total = entities.mytrip_usersroles.OrderBy(x => x.RoleName).Count();
            IQueryable<mytrip_usersroles> roles = null;
            if (sorting == "RoleName")
                roles = entities.mytrip_usersroles.Include("mytrip_users").OrderBy(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_RoleName")
                roles = entities.mytrip_usersroles.Include("mytrip_users").OrderByDescending(x => x.RoleName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "UserCount")
                roles = entities.mytrip_usersroles.Include("mytrip_users").OrderBy(x => x.mytrip_users.Count()).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_UserCount")
                roles = entities.mytrip_usersroles.Include("mytrip_users").OrderByDescending(x => x.mytrip_users.Count()).Skip((pageIndex - 1) * pageSize).Take(pageSize);
           
            return roles;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        internal void mtUnlockUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_users x in role.mytrip_users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            if (result)
            {
                mtRemoveUserFromRole(username, role);
            }
            else { mtAddUserToRole(username, role); }
          
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        internal void mtDeleteUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mtGetRoleByName(roleName);
            if (role != null)
            {
                foreach (mytrip_users x in role.mytrip_users)
                {
                    if (x.UserName == username)
                        result = true;
                }
            }
            if (result)
            {
                mtRemoveUserFromRole(username, role);
            }
           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IDictionary<string, string> mtGetAllRolesDictionary()
        {
            IDictionary<string, string> Roles =
             new Dictionary<string, string>();
            var roles = entities.mytrip_usersroles.OrderBy(x => x.RoleName);
            foreach (var x in roles)
            {
                Roles.Add(x.RoleName, x.RoleName);
            }
            return Roles;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal string[] mtGetRolesForUser(string username)
        {
            string[] result = new string[0];
            var user = mtGetUserByNameRole(username);
            if (user != null)
            {
                int namberRole = 0;
                int countRoles = user.mytrip_usersroles.Count();
                result = new string[countRoles];
                foreach (mytrip_usersroles x in user.mytrip_usersroles)
                {
                    result[namberRole] = x.RoleName;
                    namberRole++;
                }
            }
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        internal void RenameRole(string oldName, string newName)
        {
            if (oldName != newName)
            {
                var role = entities.mytrip_usersroles.FirstOrDefault(x => x.RoleName == oldName);
                role.RoleName = newName;
                entities.SaveChanges();
                GeneralMethods.MytripCacheRemove("cacherole");
            }        
        }

        #endregion

        #region RoleProvider private
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByNameRole(string username)
        {
            return entities.mytrip_users.Include("mytrip_usersroles").FirstOrDefault(x => x.UserName == username);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private mytrip_users mtGetUserByName(string username)
        {
            return entities.mytrip_users.Include("mytrip_usersroles").FirstOrDefault(x => x.UserName == username);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private mytrip_usersroles mtGetAndCreateRole(string roleName)
        {
            var role = mtGetRoleByName(roleName);
            if (role == null)
            {
                mytrip_usersroles x = new mytrip_usersroles
                {
                    RoleId = mtCreateUniqueRoleId(),
                    RoleName = roleName
                };
                entities.mytrip_usersroles.AddObject(x);
                entities.SaveChanges();
                role = x;
            }
            return role;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns></returns>
        private mytrip_usersroles mtGetRoleByName(string rolename)
        {
            return entities.mytrip_usersroles.Include("mytrip_users").FirstOrDefault(x => x.RoleName == rolename);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int mtCreateUniqueRoleId()
        {
            int result;
            int roleId;
            for (roleId = 1; entities.mytrip_usersroles.Count(x => x.RoleId == roleId) != 0; roleId++) ;
            result = roleId;
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        private mytrip_usersroles mtGetRoleByRoleId(int roleid)
        {
            return entities.mytrip_usersroles.FirstOrDefault(x => x.RoleId == roleid);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        private void mtRemoveUsersFromRole(mytrip_usersroles role)
        {
            if (role.mytrip_users != null)
            {
                foreach (mytrip_users x in role.mytrip_users.ToList())
                {
                    var user = mtGetUserByName(x.UserName);
                    role.mytrip_users.Remove(user);
                    entities.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        private void mtRemoveUserFromRole(string username, mytrip_usersroles role)
        {
            foreach (mytrip_users x in role.mytrip_users.ToList())
            {
                if (username == x.UserName)
                {
                    var user = mtGetUserByName(x.UserName);
                    role.mytrip_users.Remove(user);
                    entities.SaveChanges();
                }

            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        private void mtAddUserToRole(string username, mytrip_usersroles role)
        {
            var user = mtGetUserByName(username);
            role.mytrip_users.Add(user);
            entities.SaveChanges();
        }

        #endregion
    }
    
}