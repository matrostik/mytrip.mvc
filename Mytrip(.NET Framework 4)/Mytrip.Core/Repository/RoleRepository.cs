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
            if (UsersSetting.membership == "MSSQL")
            {
                mssqlUser.mssqlRemoveUsersFromRoles(usernames, roleNames);
            }
            else if (UsersSetting.membership == "XML")
            {
                xmlUser.xmlRemoveUsersFromRoles(usernames, roleNames);
            }
        }
        public override bool RoleExists(string roleName)
        {
            bool result = false;
            if (UsersSetting.membership == "MSSQL")
            {
                result = mssqlUser.mssqlRoleExists(roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                result = xmlUser.xmlRoleExists(roleName);
            }
            return result;
        }
        /*--------------------------------------------------------------------------------------------*/
        public void mtUnlockUserInRole(string username, string roleName)
        {
            if (UsersSetting.membership == "MSSQL")
            {
                mssqlUser.mssqlUnlockUserInRole(username, roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                xmlUser.xmlUnlockUserInRole(username, roleName);
            }
        }
        public void mtDeleteUserInRole(string username, string roleName)
        {
            if (UsersSetting.membership == "MSSQL")
            {
                mssqlUser.mssqlDeleteUserInRole(username, roleName);
            }
            else if (UsersSetting.membership == "XML")
            {
                xmlUser.xmlDeleteUserInRole(username, roleName);
            }
        }
        public IDictionary<string, string> mtGetAllRolesDictionary()
        {
            IDictionary<string, string> Roles =
                 new Dictionary<string, string>();
            if (UsersSetting.membership == "MSSQL")
            {
              Roles=mssqlUser.mssqlGetAllRolesDictionary();
            }
            else if (UsersSetting.membership == "XML")
            {
                Roles = xmlUser.xmlGetAllRolesDictionary();
            }
            return Roles;
        }
        public string[] mtGetRolesForUser(string username)
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
    }
}