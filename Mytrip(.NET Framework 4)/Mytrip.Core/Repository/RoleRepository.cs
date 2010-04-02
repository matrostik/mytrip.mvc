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
using Mytrip.Core.Repository.XmlUsers;
using Mytrip.Core.Repository.MsSqlUsers;

namespace Mytrip.Core.Repository
{
    public class RoleRepository : RoleProvider
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
        #region подключаемые провайдеры Role
        MsSqlRoleRepository _mssqlUser;
        public MsSqlRoleRepository mssqlUser
        {
            get
            {
                if (_mssqlUser == null)
                    _mssqlUser = new MsSqlRoleRepository();
                return _mssqlUser;
            }
        }
        XmlRoleRepository _xmlUser;
        public XmlRoleRepository xmlUser
        {
            get
            {
                if (_xmlUser == null)
                    _xmlUser = new XmlRoleRepository();
                return _xmlUser;
            }
        }
        #endregion
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (UsersSetting.membership == "MSSQL")
            {
                mssqlUser.mssqlAddUsersToRoles(usernames, roleNames);
            }
            else if (UsersSetting.membership == "XML")
            {
                xmlUser.xmlAddUsersToRoles(usernames, roleNames);
            }
        }
        public override string ApplicationName
        {
            get
            {
                return UsersSetting.applicationName;
            }
            set { UsersSetting.applicationName = value; }
        }
        public override void CreateRole(string roleName)
        {
            if (UsersSetting.membership == "MSSQL")
            {
                mssqlUser.mssqlCreateRole(roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                xmlUser.xmlCreateRole(roleName);
            }
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlDeleteRole(roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlDeleteRole(roleName);
            }
            return result;
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] result = new string[0];
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlFindUsersInRole(roleName, usernameToMatch);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlFindUsersInRole(roleName, usernameToMatch);
            }
            return result;
        }
        public override string[] GetAllRoles()
        {
            string[] result = new string[0];
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetAllRoles();
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetAllRoles();
            }
            return result;
        }
        public override string[] GetRolesForUser(string username)
        {
            string[] result = new string[0];
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetRolesForUser(username);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetRolesForUser(username);
            }
            return result;
        }
        public override string[] GetUsersInRole(string roleName)
        {
            string[] result = new string[0];
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlGetUsersInRole(roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlGetUsersInRole(roleName);
            }
            return result;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlIsUserInRole(username, roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlIsUserInRole(username, roleName);
            }
            return result;
        }
        public bool IsUserInRoleOnline(string roleName)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlIsUserInRoleOnline(roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlIsUserInRoleOnline(roleName);
            }
            return result;
        }
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
        public override bool RoleExists(string roleName)
        {
            if (mtGetRoleByName(roleName) != null)
                return true;
            else
                return false;
        }
        /*--------------------------------------------------------------------------------------------*/
        private int mtCreateUniqueRoleId()
        {
            int result;
            int roleId;
            for (roleId = 1; mtGetRoleByRoleId(roleId) != null; roleId++) ;
            result = roleId;
            return result;
        }
        /*--------------------------------------------------------------------------------------------*/
        private mytrip_Users mtGetUserByName(string username)
        {
            return entities.mytrip_Users.FirstOrDefault(x => x.UserName == username);
        }
        private mytrip_Roles mtGetRoleByRoleId(int roleid)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleId == roleid);
        }
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
        public mytrip_Roles mtGetRoleByName(string rolename)
        {
            return entities.mytrip_Roles.FirstOrDefault(x => x.RoleName == rolename);
        }
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
        public void mtAddUserToRole(string username, string rolename)
        {
            var role = mtGetRoleByName(rolename);
            var user = mtGetUserByName(username);
            role.mytrip_Users.Add(user);
            entities.SaveChanges();
             
        }
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
        public IQueryable<mytrip_Roles> mtGetAllRoles()
        {
            return entities.mytrip_Roles.OrderBy(x => x.RoleName);
        }
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