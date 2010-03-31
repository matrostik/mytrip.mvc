using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Core.Models;
using Mytrip.Core;
using System.Configuration;
using System.Web.Configuration;
using System.Collections;
using System.Text;
using Mytrip.Core.Repository;

namespace Mytrip.Mvc.Controllers
{
    [HandleError]
    [Localization]
    [CoreSqlSetting]
    public class CoreController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            CoreModel model = new CoreModel();
            model.SQLError = mtSQLError.ShowSqlException(CoreSetting.connectionStringSQL());
            model.Development = CoreSetting.Development;
            model.MSSQLDataBase = CoreSetting.MSSQLDataBase;
            model.MSSQLIntegratedSecurity = CoreSetting.MSSQLIntegratedSecurity;
            model.MSSQLPassword = CoreSetting.MSSQLPassword;//EncryptedString.EncryptedValue(CoreSetting.MSSQLPassword);
            model.MSSQLServer = CoreSetting.MSSQLServer;
            model.MSSQLUser = CoreSetting.MSSQLUser;
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength;
            model.maxInvalidPasswordAttempts = UsersSetting.maxInvalidPasswordAttempts;
            model.requiresUniqueEmail = UsersSetting.requiresUniqueEmail;
            model.unlockCaptcha = UsersSetting.unlockCaptcha;
            model.roleAdmin = UsersSetting.roleAdmin;
            model.unlockRegistration = UsersSetting.unlockRegistration;
            model.unlockVisibleLogon = UsersSetting.unlockVisibleLogon;
            model.defaultCulture = LocalisationSetting.defaultCulture;
            model.unlockAllCulture = LocalisationSetting.unlockAllCulture;
            model.defaultTheme = ThemeSetting.defaultTheme;
            model.unlockTheme = ThemeSetting.unlockTheme;
            model.unlockGravatar = UsersSetting.unlockGravatar;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CoreModel model)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            config.AppSettings.Settings["Development"].Value = model.Development.ToString();
            config.AppSettings.Settings["MSSQLIntegratedSecurity"].Value = model.MSSQLIntegratedSecurity.ToString();
            config.AppSettings.Settings["MSSQLDataBase"].Value = model.MSSQLDataBase;
            config.AppSettings.Settings["MSSQLPassword"].Value =EncryptedString.DescriptedValue(model.MSSQLPassword);
            config.AppSettings.Settings["MSSQLServer"].Value = model.MSSQLServer;
            config.AppSettings.Settings["MSSQLUser"].Value = model.MSSQLUser;
            config.AppSettings.Settings["minRequiredPasswordLength"].Value = model.minRequiredPasswordLength.ToString();
            config.AppSettings.Settings["maxInvalidPasswordAttempts"].Value = model.maxInvalidPasswordAttempts.ToString();
            config.AppSettings.Settings["requiresUniqueEmail"].Value = model.requiresUniqueEmail.ToString();
            config.AppSettings.Settings["unlockCaptcha"].Value = model.unlockCaptcha.ToString();
            config.AppSettings.Settings["roleAdmin"].Value = model.roleAdmin;
            config.AppSettings.Settings["unlockRegistration"].Value = model.unlockRegistration.ToString();
            config.AppSettings.Settings["unlockVisibleLogon"].Value = model.unlockVisibleLogon.ToString();
            config.AppSettings.Settings["defaultCulture"].Value = model.defaultCulture;
            config.AppSettings.Settings["unlockAllCulture"].Value = model.unlockAllCulture.ToString();
            config.AppSettings.Settings["defaultTheme"].Value = model.defaultTheme;
            config.AppSettings.Settings["unlockTheme"].Value = model.unlockTheme.ToString();
            config.AppSettings.Settings["unlockGravatar"].Value = model.unlockGravatar.ToString();
            config.Save();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
