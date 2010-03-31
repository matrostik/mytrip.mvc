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
           var user = xmlGetMembershipByUserName(username);
           result = user.Email;
           return result;
       }
       public bool xmlCheckOldPassword(string OldPassword)
       {
           bool result = false;
           var user = xmlGetMembershipByUserName(HttpContext.Current.User.Identity.Name);
           if (user != null && xmlHashPassword(OldPassword, user.PasswordSalt) == user.Password)
           {
               result = true;
           }
           return result;
       }
       public bool xmlUnlockUser(string userName)
       {
           bool result = false;
           var user = xmlGetUserByUserName(userName);
           if (user != null)
           {
               string _absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument _doc = XDocument.Load(_absolutDirectory);
               var membership = _doc.Root.Elements("Membership").FirstOrDefault(x => x.Attribute("UserId").Value == user.UserId);
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
           return result;
       }
       public string xmlGetUserNameByEmail(string email)
       {
           string result = string.Empty;
           string _absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var membership = _doc.Root.Elements("Membership").FirstOrDefault(x => x.Element("Email").Value == email);
           string UserId = membership.Attribute("UserId").Value;
           var user = xmlGetUserByUserId(UserId);
           result = user.UserName;
           return result;
       }
       public string xmlGetUser(object providerUserKey, bool userIsOnline)
       {
           var user = xmlGetUserByUserId(providerUserKey.ToString());
           if (user != null)
           {
               if (userIsOnline)
               {
                   xmlLastActivityDate(user.UserName);
               }
               return user.UserName;
           }
           else
           {
               return string.Empty;
           }
       }
       public string xmlGetUser(string username, bool userIsOnline)
       {
           var user = xmlGetUserByUserName(username);
           if (user != null)
           {
               if (userIsOnline)
               {
                   xmlLastActivityDate(username);
               }
               return username;
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
           var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Attribute("UserName").Value == username);
           _user.SetAttributeValue("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
           doc.Save(absolutDirectory);
       }
       public int xmlGetNumberOfUsersOnline()
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
           XDocument doc = XDocument.Load(absolutDirectory);
           var users = doc.Root.Elements("User").OrderByDescending(x => x.Attribute("LastActivityDate").Value);
           int result = 0;
           foreach (XElement user in users)
           {
               DateTime date = DateTime.Parse(user.Attribute("LastActivityDate").Value);
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
           var user = xmlGetUserByUserName(username);
           if (user != null)
           {
               xmlRemoveRolesFromUser(username);
               xmlDeleteMembership(username);
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("User").FirstOrDefault(x => x.Attribute("UserId").Value == user.UserId);
               _user.Remove();
               doc.Save(absolutDirectory);
               result = true;
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
               var _user = doc.Root.Elements("User").OrderBy(x => x.Attribute("LastActivityDate").Value)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
               totalRecords = doc.Root.Elements("User").OrderBy(x => x.Attribute("LastActivityDate").Value).Count();
               if (emailToMatch != null)
               {
                   string _absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
                   XDocument _doc = XDocument.Load(_absolutDirectory);
                   var membership = _doc.Root.Elements("Membership").Where(x => x.Element("Email").Value == emailToMatch)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                   var __user = doc.Root.Elements("User").Where(x => x.Attribute("UserId").Value=="");
                   string[] userId = new string[pageSize];
                   foreach (XElement y in membership)
                   {
                       var user = doc.Root.Elements("User").Where(x => x.Attribute("UserId").Value == y.Attribute("UserId").Value);
                       __user = __user.Union(user);
                   }
                   _user=__user.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                   totalRecords = __user.Count();
               }
               else if (usernameToMatch != null)
               {
                   _user = doc.Root.Elements("User").Where(x => x.Attribute("UserName").Value == usernameToMatch).OrderBy(x => x.Attribute("LastActivityDate").Value)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                   totalRecords = doc.Root.Elements("User").Where(x => x.Attribute("UserName").Value == usernameToMatch).Count();

               }
               int index = 0;
               foreach (XElement y in _user)
               {
                   result[index] = y.Attribute("UserName").Value;
                   index++;
               }
           }
           return result;
       }
       private void xmlDeleteMembership(string username)
       {
           var user = xmlGetUserByUserName(username);
           if (user != null)
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("Membership").FirstOrDefault(x => x.Attribute("UserId").Value == user.UserId);
               _user.Remove();
               doc.Save(absolutDirectory);
           }
       }
       private void xmlRemoveRolesFromUser(string username)
       {
           var user = xmlGetUserByUserName(username);
           if (user != null && xmlLoadFile("xmlUsersInRoles.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsersInRoles.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("UsersInRoles").Where(x => x.Attribute("UserId").Value == user.UserId);
               _user.Remove();
               doc.Save(absolutDirectory);
           }
       }
       public bool xmlValidateUser(string username, string password)
       {
           bool result = false;
           if (xmlCheckUserName(username))
           {
               var user = xmlGetMembershipByUserName(username);
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
           var user = xmlGetMembershipByUserName(username);
           if (users != null)
           {
               bool _users = user.IsApproved;
               if (_users)
                   result = true;
           }
           return result;
       }
       private void xmlUpdateUserIP(string username)
       {
           var user = xmlGetMembershipByUserName(username);
           if (user != null)
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("Membership").FirstOrDefault(x => x.Attribute("UserId").Value == user.UserId);
               _user.SetElementValue("UserIP", HttpContext.Current.Request.UserHostAddress);
               doc.Save(absolutDirectory);
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
               xmlCreateUser(userId, username);
               xmlCreateMembership(userId, password, email, isApproved);
               status = MembershipCreateStatus.Success;
           }
           return status;

       }
       public MembershipUser xmlCreateMembershipFromInternalUser(string Username, string baseName)
       {
           var user = xmlGetUserByUserName(Username);
           var membership=xmlGetMembershipByUserName(Username);
           MembershipUser muser = new MembershipUser(baseName, user.UserName, user.UserId,
               membership.Email, string.Empty, string.Empty, membership.IsApproved, false,
               membership.CreationDate, membership.LastLoginDate, user.LastActivityDate, membership.LastPasswordChangedDate,
               membership.LastLockoutDate);
           return muser;
       }
       public bool xmlChangePassword(string username, string oldPassword, string newPassword)
       {
           bool result = false;
           var user = xmlGetMembershipByUserName(username);
           if (user != null && xmlHashPassword(oldPassword, user.PasswordSalt) == user.Password)
           {
               string newGuid = Guid.NewGuid().ToString();
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var _user = doc.Root.Elements("Membership").FirstOrDefault(x => x.Attribute("UserId").Value == user.UserId);
               _user.SetAttributeValue("Password", xmlHashPassword(newPassword, newGuid));
               _user.SetAttributeValue("PasswordSalt", newGuid);
               _user.SetAttributeValue("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now));
               doc.Save(absolutDirectory);
               result = true;
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
                   .FirstOrDefault(x => x.Attribute("UserName").Value == Username);
               if (_user != null) {
                   user.UserName = _user.Attribute("UserName").Value;
                   user.UserId = _user.Attribute("UserId").Value;
                   user.LastActivityDate = DateTime.Parse(_user.Attribute("LastActivityDate").Value);
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
                   user.UserName = _user.Attribute("UserName").Value;
                   user.UserId = _user.Attribute("UserId").Value;
                   user.LastActivityDate = DateTime.Parse(_user.Attribute("LastActivityDate").Value);
               }
           }
           return user;
       }
       public xml_Membership xmlGetMembershipByUserName(string username)
       {    xml_Membership model=new xml_Membership();
           string UserId = "null";
           if (xmlLoadFile("xmlUsers.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               UserId = doc.Root.Elements("User")
                   .FirstOrDefault(x => x.Attribute("UserName").Value == username).Attribute("UserId").Value;
           }
           if (UserId != "null" && xmlLoadFile("xmlMembership.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var User=doc.Root.Elements("Membership").FirstOrDefault(x => x.Attribute("UserId").Value == UserId);
               model.UserId = User.Attribute("UserId").Value;
               model.Password = User.Attribute("Password").Value;
               model.PasswordSalt = User.Attribute("PasswordSalt").Value;
               model.Email = User.Element("Email").Value;
               model.IsApproved = bool.Parse(User.Element("IsApproved").Value);
               model.CreationDate = DateTime.Parse(User.Element("CreationDate").Value);
               model.LastLockoutDate = DateTime.Parse(User.Element("LastLockoutDate").Value);
               model.LastLoginDate = DateTime.Parse(User.Element("LastLoginDate").Value);
               model.LastPasswordChangedDate = DateTime.Parse(User.Element("LastPasswordChangedDate").Value);
               model.UserIP = User.Element("UserIP").Value;
           }
           return model;
       
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
           if (xmlLoadFile("xmlMembership.xml"))
           {
               string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
               XDocument doc = XDocument.Load(absolutDirectory);
               var user = doc.Root.Elements("Membership").FirstOrDefault(x => x.Element("Email").Value == email);               
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
               var user = doc.Root.Elements("User").FirstOrDefault(x => x.Attribute("UserName").Value == username);
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
       public void xmlCreateMembership(string userId, string password, string email, bool isApproved)
       {
           string newGuid = Guid.NewGuid().ToString();
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlMembership.xml");
           if (xmlLoadFile("xmlMembership.xml"))
           {
               XDocument doc = XDocument.Load(absolutDirectory);
               XElement user = new XElement("Membership",
                  new XAttribute("UserId", userId),
                  new XAttribute("Password", xmlHashPassword(password, newGuid)),
                  new XAttribute("PasswordSalt", newGuid),
                  new XElement("Email",email),
                  new XElement("IsApproved", isApproved.ToString()),
                  new XElement("CreationDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLockoutDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLoginDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("UserIP", HttpContext.Current.Request.UserHostAddress)
                  );
               doc.Root.Add(user);
               doc.Save(absolutDirectory);
           }
           else
           {
               XDocument doc = new XDocument(
             new XElement("mytrip_Membership",
                 new XElement("Membership",
                     new XAttribute("UserId", userId),
                     new XAttribute("Password", xmlHashPassword(password, newGuid)),
                     new XAttribute("PasswordSalt", newGuid),
                  new XElement("Email", email),
                  new XElement("IsApproved", isApproved.ToString()),
                  new XElement("CreationDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLockoutDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastLoginDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("LastPasswordChangedDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                  new XElement("UserIP", HttpContext.Current.Request.UserHostAddress)
                     
                     
                     )));
               doc.Save(absolutDirectory);
           }
       }
       public void xmlCreateUser(string userId,string username) {
           string absolutDirectory = HttpContext.Current.Server.MapPath("/App_Data/xmlUsers.xml");
           if (xmlLoadFile("xmlUsers.xml"))
           {
               XDocument doc = XDocument.Load(absolutDirectory);
               XElement user = new XElement("User",
                  new XAttribute("UserId", userId),
                  new XAttribute("UserName", username),
                  new XAttribute("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)));
               doc.Root.Add(user);
               doc.Save(absolutDirectory);
           }
           else
           {
             XDocument doc = new XDocument(
           new XElement("mytrip_Users",
               new XElement("User",
                   new XAttribute("UserId", userId),
                   new XAttribute("UserName", username),
                   new XAttribute("LastActivityDate", String.Format("{0:yyyy-MM-dd}", DateTime.Now)))));
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
