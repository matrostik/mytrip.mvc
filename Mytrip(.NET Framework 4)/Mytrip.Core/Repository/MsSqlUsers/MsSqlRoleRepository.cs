﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mytrip.Core.Repository.MsSqlUsers
{
    public class MsSqlRoleRepository
    {
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
        public bool mssqlIsUserInRoleOnline(string roleName)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                var role = mssqlGetRoleByName(roleName);
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
        public bool mssqlIsUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = mssqlGetRoleByName(roleName);
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
        public string[] mssqlGetUsersInRole(string roleName)
        {
            string[] result = new string[0];
            var role = mssqlGetRoleByName(roleName);
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
        public string[] mssqlGetRolesForUser(string username)
        {
            string[] result = new string[0];
            var user = mssqlGetUserByName(username);
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
        public string[] mssqlGetAllRoles()
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
        public string[] mssqlFindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] result = new string[0];
            var role = mssqlGetRoleByName(roleName);
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
        public bool mssqlDeleteRole(string roleName)
        {
            bool result = false;
            var role = mssqlGetRoleByName(roleName);
            if (role != null)
            {
                mssqlRemoveUsersFromRole(roleName);
                mytrip_Roles x = role;
                entities.DeleteObject(x);
                entities.SaveChanges();
                result = true;
            }
            return result;
        }
        private void mssqlRemoveUsersFromRole(string rolename)
        {
            var role = mssqlGetRoleByName(rolename);
            if (role.mytrip_Users != null)
            {
                foreach (mytrip_Users x in role.mytrip_Users.ToList())
                {
                    var user = mssqlGetUserByName(x.UserName);
                    role.mytrip_Users.Remove(user);
                    entities.SaveChanges();
                }
            }
        }
        public void mssqlAddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = mssqlGetUserByName(username);
                    if (user != null)
                    {
                        var role = mssqlGetAndCreateRole(roleName);
                        bool addRole = true;
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
        private mytrip_Users mssqlGetUserByName(string username)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserName == username);
        }
        private mytrip_Roles mssqlGetAndCreateRole(string roleName)
        {
            var role = mssqlGetRoleByName(roleName);
            if (role == null)
            {
                mytrip_Roles x = new mytrip_Roles
                {
                    RoleId = mssqlCreateUniqueRoleId(),
                    RoleName = roleName
                };
                entities.AddTomytrip_Roles(x);
                entities.SaveChanges();
                role = x;
            }
            return role;
        }
        public void mssqlCreateRole(string roleName)
        {
            var role = mssqlGetRoleByName(roleName);
            if (role == null)
            {
                mytrip_Roles x = new mytrip_Roles
                {
                    RoleId = mssqlCreateUniqueRoleId(),
                    RoleName = roleName
                };
                entities.AddTomytrip_Roles(x);
                entities.SaveChanges();
            }
        }
        public mytrip_Roles mssqlGetRoleByName(string rolename)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleName == rolename);
        }
        private int mssqlCreateUniqueRoleId()
        {
            int result;
            int roleId;
            for (roleId = 1; mssqlGetRoleByRoleId(roleId) != null; roleId++) ;
            result = roleId;
            return result;
        }
        private mytrip_Roles mssqlGetRoleByRoleId(int roleid)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleId == roleid);
        }
    }
}