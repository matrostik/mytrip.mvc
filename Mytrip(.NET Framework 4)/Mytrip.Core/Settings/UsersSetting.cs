using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace Mytrip.Core
{
    public class UsersSetting
    {
        public static string membership = ConfigurationManager.AppSettings["Membership"];
        public static string connectionString = CoreSetting.connectionStringSQL("UsersEntities");
        public static int minRequiredPasswordLength = int.Parse(ConfigurationManager.AppSettings["minRequiredPasswordLength"]);
        public static int maxInvalidPasswordAttempts = int.Parse(ConfigurationManager.AppSettings["maxInvalidPasswordAttempts"]);
        public static string applicationName = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        public static bool requiresUniqueEmail = bool.Parse(ConfigurationManager.AppSettings["requiresUniqueEmail"]);
        public static bool unlockCaptcha = bool.Parse(ConfigurationManager.AppSettings["unlockCaptcha"]);
        public static string roleAdmin = ConfigurationManager.AppSettings["roleAdmin"];
        public static bool unlockGravatar = bool.Parse(ConfigurationManager.AppSettings["unlockGravatar"]);
        public static bool unlockRegistration = bool.Parse(ConfigurationManager.AppSettings["unlockRegistration"]);
        public static bool unlockVisibleLogon = bool.Parse(ConfigurationManager.AppSettings["unlockVisibleLogon"]);

    }
}
