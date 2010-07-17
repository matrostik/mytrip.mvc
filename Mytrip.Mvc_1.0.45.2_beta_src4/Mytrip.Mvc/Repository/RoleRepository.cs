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
using Mytrip.Mvc.Repository.DataEntities;
namespace Mytrip.Mvc.Repository
{
    public class RoleRepository : RoleProvider
    {
        #region Entities

        CoreSetting _core;
        public CoreSetting core
        {
            get
            {
                if (_core == null)
                    _core = new CoreSetting();
                return _core;
            }
        }
        UsersSetting _userset;
        public UsersSetting userset
        {
            get
            {
                if (_userset == null)
                    _userset = new UsersSetting();
                return _userset;
            }
        }
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(core.connectionString());
                return _entities;
            }
        }

        #endregion

        #region RoleProvider public override

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
        public override string ApplicationName
        {
            get
            {
                return userset.applicationName();
            }
            set
            { string applicationName = value; }
        }
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
        public override bool RoleExists(string roleName)
        {
            if (mtGetRoleByName(roleName) != null)
                return true;
            else
                return false;
        }

        #endregion

        #region RoleProvider public

        public bool IsUserInRoleOnline(string roleName)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                var role = mtGetRoleByName(roleName);
                if (role != null)
                {
                    foreach (mytrip_users x in role.mytrip_users)
                    {
                        if (x.UserName == HttpContext.Current.User.Identity.Name)
                            result = true;
                    }
                }
            }
            return result;
        }
        public IQueryable<mytrip_usersroles> mtGetAllRolesPaginal(int pageIndex, int pageSize, string sorting, out int total)
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
        public void mtUnlockUserInRole(string username, string roleName)
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
        public void mtDeleteUserInRole(string username, string roleName)
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
        public IDictionary<string, string> mtGetAllRolesDictionary()
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
        public string[] mtGetRolesForUser(string username)
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
        public void RenameRole(string oldName, string newName)
        {
            if (oldName != newName)
            {
                var role = entities.mytrip_usersroles.FirstOrDefault(x => x.RoleName == oldName);
                role.RoleName = newName;
                entities.SaveChanges();
            }        
        }
        #endregion

        #region RoleProvider private

        private mytrip_users mtGetUserByNameRole(string username)
        {
            return entities.mytrip_users.Include("mytrip_usersroles").FirstOrDefault(x => x.UserName == username);
        }
        private mytrip_users mtGetUserByName(string username)
        {
            return entities.mytrip_users.Include("mytrip_usersroles").FirstOrDefault(x => x.UserName == username);
        }
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
        private mytrip_usersroles mtGetRoleByName(string rolename)
        {
            return entities.mytrip_usersroles.Include("mytrip_users").FirstOrDefault(x => x.RoleName == rolename);
        }
        private int mtCreateUniqueRoleId()
        {
            int result;
            int roleId;
            for (roleId = 1; mtGetRoleByRoleId(roleId) != null; roleId++) ;
            result = roleId;
            return result;
        }
        private mytrip_usersroles mtGetRoleByRoleId(int roleid)
        {
            return entities.mytrip_usersroles.FirstOrDefault(x => x.RoleId == roleid);
        }
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
        private void mtAddUserToRole(string username, mytrip_usersroles role)
        {
            var user = mtGetUserByName(username);
            role.mytrip_users.Add(user);
            entities.SaveChanges();
        }

        #endregion
    }
}