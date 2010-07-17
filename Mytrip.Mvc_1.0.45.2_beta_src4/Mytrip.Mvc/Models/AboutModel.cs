using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Mytrip.Mvc.Repository;
using System.Web;
using System.Web.Routing;

namespace Mytrip.Mvc.Models
{
    [MetadataType(typeof(AboutModel))]
    public class HomeModel
    {
        public string title { get; set; }
        public bool developer { get; set; }
    }
    [MetadataType(typeof(AboutModel))]
    public class AboutModel
    {
        public string body { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        public string name { get; set; }
        public bool approvedemail { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "message_null")]
        public string messege { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string email { get; set; }
        public string title { get; set; }

    }
    [MetadataType(typeof(EditAboutModel))]
    public class EditAboutModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string body { get; set; }

    }
    public class RoleAreaAttribute : ActionFilterAttribute
    {
        RoleRepository db = new RoleRepository();
        UsersSetting artset = new UsersSetting();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (db.IsUserInRoleOnline(artset.roleChiefEditor()) || db.IsUserInRoleOnline(artset.roleAdmin()))
            {

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
}
