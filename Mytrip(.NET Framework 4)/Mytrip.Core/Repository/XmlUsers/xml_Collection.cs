using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Core.Repository.XmlUsers
{
   public class xml_Users
   {
       public string UserId { get; set; }
       public string UserName { get; set; }
       public string Password { get; set; }
       public string PasswordSalt { get; set; }
       public DateTime LastActivityDate { get; set; }
       public string Email { get; set; }
       public bool IsApproved { get; set; }
       public DateTime CreationDate { get; set; }
       public DateTime LastLockoutDate { get; set; }
       public DateTime LastLoginDate { get; set; }
       public DateTime LastPasswordChangedDate { get; set; }
       public string UserIP { get; set; }
    }
   public class xml_Roles
   {
       public string RoleName { get; set; }
   }
   public class xml_UsersInRoles
   {
       public string RoleName { get; set; }
       public string UserName { get; set; }
   }
}
