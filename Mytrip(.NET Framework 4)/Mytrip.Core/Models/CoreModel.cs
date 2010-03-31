using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Models
{
    [MetadataType(typeof(CoreModel))]
    public class CoreModel
    {
        public bool Development { get; set; }
        public bool MSSQLIntegratedSecurity { get; set; }
        public string MSSQLServer { get; set; }
        public string MSSQLDataBase { get; set; }
        public string MSSQLUser { get; set; }
        public string MSSQLPassword { get; set; }
        public string SQLError { get; set; }
        public int minRequiredPasswordLength { get; set; }
        public int maxInvalidPasswordAttempts { get; set; }
        public bool requiresUniqueEmail { get; set; }
        public bool unlockCaptcha { get; set; }
        public string roleAdmin { get; set; }
        public bool unlockRegistration { get; set; }
        public bool unlockVisibleLogon { get; set; }
        public string defaultCulture { get; set; }
        public bool unlockAllCulture { get; set; }
        public string defaultTheme { get; set; }
        public bool unlockTheme { get; set; }
        public bool unlockGravatar { get; set; }
    }
    [MetadataType(typeof(PageModel))]
    public class PageModel
    {
        public string page { get; set; }
        public string directory { get; set; }
    }
    public class CoreSqlSettingAttribute : ActionFilterAttribute
    {
        RoleRepository db = new RoleRepository();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CoreSetting.Development || db.IsUserInRoleOnline(UsersSetting.roleAdmin))
            { }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
}
