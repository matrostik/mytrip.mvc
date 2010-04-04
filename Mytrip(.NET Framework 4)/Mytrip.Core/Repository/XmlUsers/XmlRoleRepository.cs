using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.IO;

namespace Mytrip.Core.Repository.XmlUsers
{
    public class XmlRoleRepository
    {
        public IEnumerable<XElement> xmlGetAllRolesPaginal(int pageIndex, int pageSize, string sorting, out int total)
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
            XDocument doc = XDocument.Load(absolutDirectory);
            total = doc.Root.Elements("Role").OrderBy(x => x.Attribute("RoleName").Value).Count();
            var roles = doc.Root.Elements("Role").OrderBy(x => x.Attribute("RoleName").Value)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (sorting == "RoleName")
                roles = doc.Root.Elements("Role").OrderBy(x => x.Attribute("RoleName").Value).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sorting == "_RoleName")
                roles = doc.Root.Elements("Role").OrderByDescending(x => x.Attribute("RoleName").Value).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return roles;
        }
        public IDictionary<string, string> xmlGetAllRolesDictionary()
        {
            IDictionary<string, string> Roles =
              new Dictionary<string, string>();
            if (xmlLoadFile("xmlRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var roles = doc.Root.Elements("Role").OrderBy(x => x.Attribute("RoleName").Value);
                if (roles != null)
                {
                    foreach (XElement x in roles)
                    {
                        Roles.Add(x.Attribute("RoleName").Value, x.Attribute("RoleName").Value);
                    }
                }
            }
            return Roles;
        }
        public void xmlDeleteUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName != null)
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                    .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                if (_user != null)
                    result = true;
            }
            if (result)
            {
                xmlRemoveUserFromRole(username, roleName);
            }
        }
        public void xmlUnlockUserInRole(string username, string roleName)
        {
            bool result = false;
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName != null)
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                        .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                    if (_user != null)
                        result = true;
                }               
            }
            if (result)
            {
                xmlRemoveUserFromRole(username, roleName);
            }
            else { xmlAddUserToRole(username, roleName); }
        }
        private void xmlRemoveUserFromRole(string username, string rolename)
        {
            if (xmlLoadFile("xmlUsersInRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == rolename)
                    .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                if (_user != null)
                {
                    _user.Remove();
                    doc.Save(absolutDirectory);
                }
            }
        }
        public void xmlAddUserToRole(string username, string rolename)
        {
            var role = xmlGetRoleByName(rolename);
            var user = xmlGetUserByName(username);
            if (role.RoleName != null && user.UserName != null) {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");

                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var userInRole = doc.Root.Elements("UserInRole")
                        .Where(x => x.Attribute("RoleName").Value == rolename)
                     .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                    if (userInRole == null)
                    {
                        XElement _role = new XElement("UserInRole",
                          new XAttribute("RoleName", rolename),
                          new XAttribute("UserName", username));
                        doc.Root.Add(_role);
                        doc.Save(absolutDirectory);
                    }
                }
                else
                {
                    XDocument doc = new XDocument(
                  new XElement("mytrip_UsersInRoles",
                      new XElement("UserInRole",
                          new XAttribute("RoleName", rolename),
                          new XAttribute("UserName", username))
                          ));
                    doc.Save(absolutDirectory);
                }
            
            
            }

        }
        public bool xmlIsUserInRoleOnline(string roleName)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var _role = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                        .FirstOrDefault(x => x.Attribute("UserName").Value == HttpContext.Current.User.Identity.Name);
                    if (_role != null)
                        result = true;
                }
            }
            return result;
        }
        public bool xmlIsUserInRole(string username, string roleName)
        {
            bool result = false;
            if (xmlLoadFile("xmlUsersInRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _role = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                    .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                if (_role != null)
                    result = true;
            }
            return result;
        }
        public string[] xmlGetUsersInRole(string roleName)
        {
            string[] result = new string[0];
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName != null)
            {
                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var _role = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName);
                    int namberUser = 0;
                    int countUser = _role.Count();
                    result = new string[countUser];
                    foreach (XElement x in _role)
                    {
                        result[namberUser] = x.Attribute("UserName").Value;
                        namberUser++;

                    }
                }
            }
            return result;
        }
        public string[] xmlGetRolesForUser(string username)
        {
            string[] result = new string[0];
            var user = xmlGetUserByName(username);
            if (user.UserName != null)
            {
                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var _role = doc.Root.Elements("UserInRole").Where(x => x.Attribute("UserName").Value == username);
                    int namberUser = 0;
                    int countUser = _role.Count();
                    result = new string[countUser];
                    foreach (XElement x in _role)
                    {
                        result[namberUser] = x.Attribute("RoleName").Value;
                        namberUser++;

                    }
                }
            }
            return result;
        }
        public string[] xmlGetAllRoles()
        {
            string[] result = new string[0];
            if (xmlLoadFile("xmlRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var roles = doc.Root.Elements("Role").OrderBy(x => x.Attribute("RoleName").Value);
                if (roles != null)
                {
                    int namberRole = 0;
                    int countRoles = roles.Count();
                    result = new string[countRoles];
                    foreach (XElement x in roles)
                    {
                        result[namberRole] = x.Attribute("RoleName").Value;
                        namberRole++;
                    }
                }
            }
            return result;
        }
        public string[] xmlFindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] result = new string[0];
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName != null)
            {
                if (xmlLoadFile("xmlUsersInRoles.xml"))
                {
                    string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                    XDocument doc = XDocument.Load(absolutDirectory);
                    var _role = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                        .Where(x=>x.Attribute("UserName").Value==usernameToMatch);
                    int namberUser = 0;
                    int countUser = _role.Count();
                    result = new string[countUser];
                    foreach (XElement x in _role)
                    {
                            result[namberUser] = x.Attribute("UserName").Value;
                            namberUser++;
                        
                    }
                }
            }
            return result;
        }
        public bool xmlDeleteRole(string roleName)
        {
            bool result = false;
            if (xmlLoadFile("xmlRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var role = doc.Root.Elements("Role").FirstOrDefault(x => x.Attribute("RoleName").Value == roleName);
                if (role != null)
                {
                    role.Remove();
                    doc.Save(absolutDirectory);
                    xmlRemoveUsersFromRole(roleName);
                    result = true;
                }
            }
            return result;
        }
        public bool xmlRoleExists(string roleName)
        {
            if (xmlGetRoleByName(roleName).RoleName != null)
                return true;
            else
                return false;
        }
        public void xmlRemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    if (xmlLoadFile("xmlUsersInRoles.xml"))
                    {
                        string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                        XDocument doc = XDocument.Load(absolutDirectory);
                        var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == roleName)
                            .FirstOrDefault(x => x.Attribute("UserName").Value == username);
                        if (_user != null)
                        {
                            _user.Remove();
                            doc.Save(absolutDirectory);
                        }
                    }
                }
            }
        }
        private void xmlRemoveUsersFromRole(string rolename)
        {
            if (xmlLoadFile("xmlUsersInRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("RoleName").Value == rolename);
                if (_user != null)
                {
                    _user.Remove();
                    doc.Save(absolutDirectory);
                }
            }
        }
        public void xmlAddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    var user = xmlGetUserByName(username);
                    if (user.UserName != null)
                    {
                        var role = xmlGetAndCreateRole(roleName);

                        string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");

                        if (xmlLoadFile("xmlUsersInRoles.xml"))
                        {
                            XDocument doc = XDocument.Load(absolutDirectory);
                            var userInRole = doc.Root.Elements("UserInRole")
                                .Where(x=>x.Attribute("RoleName").Value==roleName)
                             .FirstOrDefault(x => x.Element("UserName").Value == username);
                            if (userInRole == null)
                            {
                                XElement _role = new XElement("UserInRole",
                                  new XAttribute("RoleName", roleName),
                                  new XAttribute("UserName", username));
                                doc.Root.Add(_role);
                                doc.Save(absolutDirectory);
                            }
                        }
                        else
                        {
                            XDocument doc = new XDocument(
                          new XElement("mytrip_UsersInRoles",
                              new XElement("UserInRole",
                                  new XAttribute("RoleName", roleName),
                                  new XAttribute("UserName", username))
                                  ));
                            doc.Save(absolutDirectory);
                        }

                    }
                }
            }
        }
        public xml_Users xmlGetUserByName(string Username)
        {
            xml_Users user = new xml_Users();
            if (xmlLoadFile("xmlUsers.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _user = doc.Root.Elements("User")
                    .FirstOrDefault(x => x.Element("UserName").Value == Username);
                if (_user != null)
                {
                    user.UserId = _user.Attribute("UserId").Value;
                    user.UserName = _user.Element("UserName").Value;
                    user.Password = _user.Element("Password").Value;
                    user.PasswordSalt = _user.Element("PasswordSalt").Value;
                    user.LastActivityDate = DateTime.Parse(_user.Element("LastActivityDate").Value);
                    user.Email = _user.Element("Email").Value;
                    user.IsApproved = bool.Parse(_user.Element("IsApproved").Value);
                    user.CreationDate = DateTime.Parse(_user.Element("CreationDate").Value);
                    user.LastLockoutDate = DateTime.Parse(_user.Element("LastLockoutDate").Value);
                    user.LastLoginDate = DateTime.Parse(_user.Element("LastLoginDate").Value);
                    user.LastPasswordChangedDate = DateTime.Parse(_user.Element("LastPasswordChangedDate").Value);
                    user.UserIP = _user.Element("UserIP").Value;
                }
            }
            return user;
        }
        private xml_Roles xmlGetAndCreateRole(string roleName)
        {
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName == null)
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                if (xmlLoadFile("xmlRoles.xml"))
                {
                    XDocument doc = XDocument.Load(absolutDirectory);
                    XElement _role = new XElement("Role",
                       new XAttribute("RoleName", roleName));
                    doc.Root.Add(_role);
                    doc.Save(absolutDirectory);
                }
                else
                {
                    XDocument doc = new XDocument(
                  new XElement("mytrip_Roles",
                      new XElement("Role",
                       new XAttribute("RoleName", roleName))
                          ));
                    doc.Save(absolutDirectory);
                }
                role = new xml_Roles();
                role.RoleName = roleName;
            }
            return role;
        }
        public void xmlCreateRole(string roleName)
        {
            var role = xmlGetRoleByName(roleName);
            if (role.RoleName == null)
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                if (xmlLoadFile("xmlRoles.xml"))
                {
                    XDocument doc = XDocument.Load(absolutDirectory);
                    XElement _role = new XElement("Role",
                       new XAttribute("RoleName", roleName));
                    doc.Root.Add(_role);
                    doc.Save(absolutDirectory);
                }
                else
                {
                    XDocument doc = new XDocument(
                  new XElement("mytrip_Roles",
                      new XElement("Role",
                       new XAttribute("RoleName", roleName))
                          ));
                    doc.Save(absolutDirectory);
                }
            }
        }
        public xml_Roles xmlGetRoleByName(string rolename)
        {
            xml_Roles role = new xml_Roles();
            if (xmlLoadFile("xmlRoles.xml"))
            {
                string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlRoles.xml");
                XDocument doc = XDocument.Load(absolutDirectory);
                var _role = doc.Root.Elements("Role")
                    .FirstOrDefault(x => x.Attribute("RoleName").Value == rolename);
                if (_role != null)
                {
                    role.RoleName = _role.Attribute("RoleName").Value;
                }
            }
            return role;
        }
        private bool xmlLoadFile(string fileName)
        {
            bool _result = false;
            string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory.GetFiles();
            foreach (FileInfo x in result)
            {
                if (fileName == x.Name)
                    _result = true;
            }
            return _result;
        }
    }
}
