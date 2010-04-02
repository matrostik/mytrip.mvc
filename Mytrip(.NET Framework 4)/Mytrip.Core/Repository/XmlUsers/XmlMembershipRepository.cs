using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Web;
using System.IO;

namespace Mytrip.Core.Repository.XmlUsers
{

   public class XmlMembershipRepository
   {
       public string xmlGetUserEmail(string username)
       {
           string result = string.Empty;
           var user = xmlGetUserByUserName(username);
           result = user.Email;
           return result;
       }
       public bool xmlCheckOldPassword(string OldPassword)
       {
           bool result = false;
           var user = xmlGetUserByUserName(HttpContext.Current.User.Identity.Name);
           if (user != null && xmlHashPassword(OldPassword, user.PasswordSalt) == user.Password)
           {
               result = true;
           }
           return result;
       }
       public bool xmlUnlockUser(string userName)
       {
           bool result = false;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string _absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument _doc = XDocument.Load(_absolutDirectory);
               var membership = _doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == userName);
               if (membership != null)
               {
                   bool IsApproved = bool.Parse(membership.Element("IsApproved").Value);
                   if (IsApproved)
                   {
                       membership.SetElementValue("IsApproved", false.ToString());
                       _doc.Save(_absolutDirectory);
                   }
                   else
                   {
                       membership.SetElementValue("IsApproved", true.ToString());
                       _doc.Save(_absolutDirectory);
                   }
                   result = true;
               }
           }
           return result;
       }
       public string xmlGetUserNameByEmail(string email)
       {
           string result = string.Empty;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string _absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument _doc = XDocument.Load(_absolutDirectory);
               var membership = _doc.Root.Elements("User").FirstOrDefault(x => x.Element("Email").Value == email);
               if (membership != null)
               {
                   result = membership.Element("UserName").Value;
               }
           }
           return result;
       }
       public string xmlGetUser(object providerUserKey, bool userIsOnline)
       {
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("User")
                   .FirstOrDefault(x => x.Attribute("UserId").Value == providerUserKey.ToString());
               if (user != null)
               {
                   string username = user.Element("UserName").Value;
                   if (userIsOnline)
                   {
                       user.SetElementValue("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
                       doc.Save(absolutDirectory);
                   }
                   return username;
               }
               else
               {
                   return string.Empty;
               }
           }
           else
           {
               return string.Empty;
           }
       }
       public string xmlGetUser(string username, bool userIsOnline)
       {
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("User")
                   .FirstOrDefault(x => x.Element("UserName").Value == username);
               if (user != null)
               {
                   if (userIsOnline)
                   {
                       user.SetElementValue("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
                       doc.Save(absolutDirectory);
                   }
                   return username;
               }
               else
               {
                   return string.Empty;
               }
           }
           else
           {
               return string.Empty;
           }
       }
       public void xmlLastActivityDate(string username)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
           XDocument doc = XDocument.Load(absolutDirectory);
           var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == username);
           _user.SetElementValue("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
           doc.Save(absolutDirectory);
       }
       public int xmlGetNumberOfUsersOnline()
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
           XDocument doc = XDocument.Load(absolutDirectory);
           var users = doc.Root.Elements("User").OrderByDescending(x => x.Element("LastActivityDate").Value);
           int result = 0;
           foreach (XElement user in users)
           {
               DateTime date = DateTime.Parse(user.Element("LastActivityDate").Value);
               if (date.AddDays(Membership.UserIsOnlineTimeWindow) >= DateTime.Now)
               {
                   result++;
               }
           }
           return result;
       }
       public bool xmlDeleteUser(string username)
       {
           bool result = false;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == username);
               if (_user != null)
               {
                   xmlRemoveRolesFromUser(username);
                   _user.Remove();
                   doc.Save(absolutDirectory);
                   result = true;
               }
           }
           return result;
       }
       public string[] xmlCreateMembershipCollectionFromInternalList(string emailToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
       {
           totalRecords = 0;
           string[] result = new string[pageSize];
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User").OrderBy(x => x.Element("LastActivityDate").Value)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
               totalRecords = doc.Root.Elements("User").OrderBy(x => x.Element("LastActivityDate").Value).Count();
               if (emailToMatch != null)
               {
                   _user = doc.Root.Elements("User").Where(x => x.Element("Email").Value == usernameToMatch)
                       .OrderBy(x => x.Element("LastActivityDate").Value)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                   totalRecords = doc.Root.Elements("User").Where(x => x.Element("Email").Value == usernameToMatch).Count();
               }
               else if (usernameToMatch != null)
               {
                   _user = doc.Root.Elements("User").Where(x => x.Element("UserName").Value == usernameToMatch)
                       .OrderBy(x => x.Element("LastActivityDate").Value)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                   totalRecords = doc.Root.Elements("User").Where(x => x.Element("UserName").Value == usernameToMatch).Count();

               }
               int index = 0;
               foreach (XElement y in _user)
               {
                   result[index] = y.Element("UserName").Value;
                   index++;
               }
           }
           return result;
       }
       
       private void xmlRemoveRolesFromUser(string username)
       {
           if (xmlLoadFile("xmlUsersInRoles.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("UserInRole").Where(x => x.Attribute("UserName").Value == username);
               _user.Remove();
               doc.Save(absolutDirectory);
           }
       }
       public bool xmlValidateUser(string username, string password)
       {
           bool result = false;
           if (xmlCheckUserName(username))
           {
               var user = xmlGetUserByUserName(username);
               if (user != null && xmlHashPassword(password, user.PasswordSalt) == user.Password)
               {
                   xmlUpdateUserIP(username);
                   result = true;
               }
           }
           return result;
       }
       public bool xmlCheckUserName(string username)
       {
           bool result = false;
           var users = xmlGetUserByUserName(username);
           if (users != null)
           {
               bool _users = users.IsApproved;
               if (_users)
                   result = true;
           }
           return result;
       }
       private void xmlUpdateUserIP(string username)
       {
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == username);
               if (_user != null)
               {
                   _user.SetElementValue("UserIP", HttpContext.Current.Request.UserHostAddress);
                   doc.Save(absolutDirectory);
               }
           }
       }
       public MembershipCreateStatus xmlCreateUser(string username, string password, string email, bool isApproved)
       {
           MembershipCreateStatus status;
           if (!xmlAccessibleUserName(username))
               status = MembershipCreateStatus.DuplicateUserName;
           else if (String.IsNullOrEmpty(username))
               status = MembershipCreateStatus.InvalidUserName;
           else if (String.IsNullOrEmpty(password))
               status = MembershipCreateStatus.InvalidPassword;
           else if (password.Length < UsersSetting.minRequiredPasswordLength)
               status = MembershipCreateStatus.InvalidPassword;
           else if (UsersSetting.requiresUniqueEmail && !xmlAccessibleEmail(email))
               status = MembershipCreateStatus.DuplicateEmail;
           else if (String.IsNullOrEmpty(email))
               status = MembershipCreateStatus.InvalidEmail;
           else if (!Regex.IsMatch(email, "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$"))
               status = MembershipCreateStatus.InvalidEmail;
           else
           {
               string userId = xmlCreateUserByUserId();
               xmlCreateUser(userId, username, password, email, isApproved);
               status = MembershipCreateStatus.Success;
           }
           return status;

       }
       public MembershipUser xmlCreateMembershipFromInternalUser(string Username, string baseName)
       {
           var user = xmlGetUserByUserName(Username);
           MembershipUser muser = new MembershipUser(baseName, user.UserName, user.UserId,
               user.Email, string.Empty, string.Empty, user.IsApproved, false,
               user.CreationDate, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate,
               user.LastLockoutDate);
           return muser;
       }
       public bool xmlChangePassword(string username, string oldPassword, string newPassword)
       {
           bool result = false;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == username);
               string userPasswordSalt = _user.Element("PasswordSalt").Value;
               string userPassword = _user.Element("Password").Value;
               if (xmlHashPassword(oldPassword, userPasswordSalt) == userPassword)
               {
                   string newGuid = Guid.NewGuid().ToString();
                   _user.SetElementValue("Password", xmlHashPassword(newPassword, newGuid));
                   _user.SetElementValue("PasswordSalt", newGuid);
                   _user.SetElementValue("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
                   doc.Save(absolutDirectory);
                   result = true;
               }
           }
           return result;
       }
       public xml_Users xmlGetUserByUserName(string Username)
       {
           xml_Users user = new xml_Users();
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User")
                   .FirstOrDefault(x => x.Element("UserName").Value == Username);
               if (_user != null) {
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
       public xml_Users xmlGetUserByUserId(string UserId)
       {
           xml_Users user = new xml_Users();
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User")
                   .FirstOrDefault(x => x.Attribute("UserId").Value == UserId);
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
       private string xmlCreateUserByUserId()
       {
           string result = Guid.NewGuid().ToString();
           while (!xmlValidUserId(result))
           {
               result = Guid.NewGuid().ToString();
           }
           return result;
       }
       public bool xmlAccessibleEmail(string email)
       {
           bool result = true;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("Email").Value == email);               
                   if (user!=null)
                   {
                       result = false;
                   }
               
           }
           else { result = true; }
           return result;
       }
       public bool xmlAccessibleUserName(string username)
       {
           bool result = true;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("User").FirstOrDefault(x => x.Element("UserName").Value == username);
                if (user!=null)
                   {
                       result = false;
                   }

           }
           else { result = true; }
           return result;
       }
       private bool xmlValidUserId(string userid)
       {
           bool result = true;
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("User").FirstOrDefault(x => x.Attribute("UserId").Value == userid);
              
                   if (user!=null) {
                       result = false;
                   }
               
           }
           return result;
       }
       public void xmlCreateUser(string userId, string username, string password, string email, bool isApproved)
       {
           string newGuid = Guid.NewGuid().ToString();
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
           if (xmlLoadFile("xmlUsers.xml"))
           {
               XDocument doc = XDocument.Load(absolutDirectory);
               XElement user = new XElement("User",
                  new XAttribute("UserId", userId),
                  new XElement("UserName", username),
                  new XElement("Password", xmlHashPassword(password, newGuid)),
                  new XElement("PasswordSalt", newGuid),
                  new XElement("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("Email", email),
                  new XElement("IsApproved", isApproved.ToString()),
                  new XElement("CreationDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLockoutDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLoginDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("UserIP", HttpContext.Current.Request.UserHostAddress));
               doc.Root.Add(user);
               doc.Save(absolutDirectory);
           }
           else
           {
             XDocument doc = new XDocument(
           new XElement("mytrip_Users",
               new XElement("User",
                  new XAttribute("UserId", userId),
                  new XElement("UserName", username),
                  new XElement("Password", xmlHashPassword(password, newGuid)),
                  new XElement("PasswordSalt", newGuid),
                  new XElement("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("Email", email),
                  new XElement("IsApproved", isApproved.ToString()),
                  new XElement("CreationDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLockoutDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLoginDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("UserIP", HttpContext.Current.Request.UserHostAddress))
                   ));
               doc.Save(absolutDirectory);
           }
       }
       private string xmlHashPassword(string password, string passwordsalt)
       {
           string hash = FormsAuthentication
               .HashPasswordForStoringInConfigFile((passwordsalt + password), "SHA1");
           return hash;
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
