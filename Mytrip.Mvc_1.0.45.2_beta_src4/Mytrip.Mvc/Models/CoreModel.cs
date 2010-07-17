using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Models
{
    [MetadataType(typeof(ModulesModel))]
    public class ModulesModel
    {
        public IDictionary<string, bool> modules { get; set; }
        public IDictionary<int, string> uninstall { get; set; }
    }
    [MetadataType(typeof(CoreModel))]
    public class CoreModel
    {
        public bool IntegratedSecurity { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_server_null")]
        public string Server { get; set; }
        public string Provider { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_database_null")]
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int minRequiredPasswordLength { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int maxInvalidPasswordAttempts { get; set; }
        public bool requiresUniqueEmail { get; set; }
        public bool unlockCaptcha { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleAdmin { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(15, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleChiefEditor { get; set; }
        public bool unlockRegistration { get; set; }
        public bool unlockVisibleLogon { get; set; }
        public string defaultCulture { get; set; }
        public bool unlockAllCulture { get; set; }
        public string defaultTheme { get; set; }
        public SelectList AllTheme { get; set; }
        public SelectList AllCulture { get; set; }
        public SelectList AllProvider { get; set; }
        public bool unlockTheme { get; set; }
        public bool unlockGravatar { get; set; }
        public bool unlockApprovedEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameHome { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameAbout { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameTitle { get; set; }
    }
    [MetadataType(typeof(EmailModel))]
    public class EmailModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string FromEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Smtp { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int Port { get; set; }
        public bool Ssl { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        public string LoginEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        public string PasswordEmail { get; set; }
    }
    [MetadataType(typeof(CreateBaseModel))]
    public class CreateBaseModel
    {
        public bool IntegratedSecurity { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_server_null")]
        public string Server { get; set; }
        public string Provider { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_database_null")]
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }        
        public SelectList AllProvider { get; set; }
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
        UsersSetting userset = new UsersSetting();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!db.IsUserInRoleOnline(userset.roleAdmin()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
    public class CoreStartSettingAttribute : ActionFilterAttribute
    {
        CoreSetting core = new CoreSetting();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!core.Development())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Home", action = "Index"}));
            }
        }
    }
    [MetadataType(typeof(HomePageModel))]
    public class HomePageModel
    {
        public string Theme { get; set; }
        public IQueryable<HomePageItem> NewHomeItems { get; set; }
        public IQueryable<HomePageItem> HomeItems { get; set; }
    }
   
    public class HomePageItem
    {
        private string assembly;
        private string name;
        private string culture;
        private int id;
        private int index;
        private int rows;
        private int columns;
        private int content;
        private int image;
        private bool showtitle;
        private int style;

        public HomePageItem()
        { }
        public HomePageItem(int index, string name, string assembly, string other)
        {
            Assembly = assembly;
            Name = name;
            Culture = other;
            Id = 0;
            Index = index;
            Rows = 0;
            Columns = 0;
            Content = 0;
            Image = 0;
            Style = 0;
            Showtitle = false;
        }
        public HomePageItem(string assembly, string name, string culture, int id, int index, int rows, int columns, int content, int image,int style, bool showtitle)
        {
            Assembly = assembly;
            Name = name;
            Culture = culture;
            Id = id;
            Index = index;
            Rows = rows;
            Columns = columns;
            Content = content;
            Image = image;
            Style = style;
            Showtitle = showtitle;
        }
        public string Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Culture
        {
            get { return culture; }
            set { culture = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        public int Content
        {
            get { return content; }
            set { content = value; }
        }
        public int Image
        {
            get { return image; }
            set { image = value; }
        }
        public int Style
        {
            get { return style; }
            set { style = value; }
        }
        public bool Showtitle
        {
            get { return showtitle; }
            set { showtitle = value; }
        }
    }
}
