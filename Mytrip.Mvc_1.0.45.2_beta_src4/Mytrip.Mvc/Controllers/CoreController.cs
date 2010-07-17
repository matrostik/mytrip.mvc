using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using System.Configuration;
using System.Web.Configuration;
using System.Collections;
using System.Text;
using Mytrip.Mvc.Repository;
using System.Xml.Linq;
using Mytrip.Mvc.StartUpSettings;
using System.Web.Security;

namespace Mytrip.Mvc.Controllers
{
    [HandleError]
    [Localization]
    public class CoreController : Controller
    {
        #region Properties
        CoreSetting _core;
        public CoreSetting __core
        {
            get
            {
                if (_core == null)
                    _core = new CoreSetting();
                return _core;
            }
        }
        CreateDataBase _createDB;
        public CreateDataBase createDB
        {
            get
            {
                if (_createDB == null)
                    _createDB = new CreateDataBase();
                return _createDB;
            }
        }
        ICoreRepository _coreRepo;
        public ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
            }
        }
        LocalisationSetting _loc;
        public LocalisationSetting loc
        {
            get
            {
                if (_loc == null)
                    _loc = new LocalisationSetting();
                return _loc;
            }
        }
        ThemeSetting _theme;
        public ThemeSetting theme
        {
            get
            {
                if (_theme == null)
                    _theme = new ThemeSetting();
                return _theme;
            }
        }
        UsersSetting _userset;
        public UsersSetting userset
        {
            get
            {
                if (_userset == null)
                    _userset = new UsersSetting();
                return _userset;
            }
        }
        HelperRepository _hpRepo;
        public HelperRepository hpRepo
        {
            get
            {
                if (_hpRepo == null)
                    _hpRepo = new HelperRepository();
                return _hpRepo;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        #endregion
        //
        // GET: /Admin/
        [CoreSqlSetting]
        public ActionResult Index()
        {
            CoreModel model = new CoreModel();
            model.DataBase = __core.DataBase();
            model.IntegratedSecurity = __core.IntegratedSecurity();
            model.Password = __core.Password();
            model.Server = __core.Server();
            model.User = __core.User();
            model.minRequiredPasswordLength = userset.minRequiredPasswordLength();
            model.maxInvalidPasswordAttempts = userset.maxInvalidPasswordAttempts();
            model.requiresUniqueEmail = userset.requiresUniqueEmail();
            model.unlockCaptcha = userset.unlockCaptcha();
            model.roleAdmin = userset.roleAdmin();
            model.roleChiefEditor = userset.roleChiefEditor();
            model.unlockRegistration = userset.unlockRegistration();
            model.unlockVisibleLogon = userset.unlockVisibleLogon();
            model.defaultCulture = loc.defaultCulture();
            model.unlockAllCulture = loc.unlockAllCulture();
            model.defaultTheme = theme.defaultTheme();
            model.unlockTheme = theme.unlockTheme();
            model.unlockGravatar = userset.unlockGravatar();
            model.Provider = __core.Provider();
            model.AllProvider = new SelectList(__core.allProviderDictionary(), "Key", "Value", __core.Provider());
            model.AllCulture = new SelectList(loc.allCultureDictionary(), "Key", "Value", loc.defaultCulture());
            model.AllTheme = new SelectList(theme.allThemeDictionary(), "Key", "Value", theme.defaultTheme());
            model.unlockApprovedEmail = userset.unlockApprovedEmail();
            model.nameHome = __core.NameHomePage();
            model.nameAbout = __core.NameAboutPage();
            model.nameTitle = __core.NameTitlePage();
            return View(model);
        }
        [HttpPost]
        [CoreSqlSetting]
        public ActionResult Index(CoreModel model)
        {
            CreateBaseModel _model = new CreateBaseModel();
            _model.DataBase = model.DataBase;
            _model.IntegratedSecurity = model.IntegratedSecurity;
            _model.Password = model.Password;
            _model.Server = model.Server;
            _model.User = model.User;
            _model.Provider = model.Provider;
            if (ModelState.IsValid & ValidCreateBase(_model))
            {
                coreRepo.roleRepo.RenameRole(userset.roleAdmin(), model.roleAdmin);
                coreRepo.roleRepo.RenameRole(userset.roleChiefEditor(), model.roleChiefEditor);
                string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
                XDocument _doc = XDocument.Load(_absolutDirectory);

                var connection = _doc.Root.Elements("connection").Elements("add");
                connection.FirstOrDefault(x => x.Attribute("name").Value == "IntegratedSecurity")
                    .SetAttributeValue("value", model.IntegratedSecurity.ToString());
                connection.FirstOrDefault(x => x.Attribute("name").Value == "DataBase")
                    .SetAttributeValue("value", model.DataBase);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                    .SetAttributeValue("value", model.Password);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Server")
                    .SetAttributeValue("value", model.Server);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "User")
                    .SetAttributeValue("value", model.User);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Provider")
                    .SetAttributeValue("value", model.Provider);

                var membership = _doc.Root.Elements("membership").Elements("add");
                membership.FirstOrDefault(x => x.Attribute("name").Value == "minRequiredPasswordLength")
                    .SetAttributeValue("value", model.minRequiredPasswordLength.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "maxInvalidPasswordAttempts")
                    .SetAttributeValue("value", model.maxInvalidPasswordAttempts.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "requiresUniqueEmail")
                    .SetAttributeValue("value", model.requiresUniqueEmail.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockCaptcha")
                    .SetAttributeValue("value", model.unlockCaptcha.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "roleAdmin")
                    .SetAttributeValue("value", model.roleAdmin);
                membership.FirstOrDefault(x => x.Attribute("name").Value == "roleChiefEditor")
                    .SetAttributeValue("value", model.roleChiefEditor);
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockRegistration")
                    .SetAttributeValue("value", model.unlockRegistration.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockVisibleLogon")
                    .SetAttributeValue("value", model.unlockVisibleLogon.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockApprovedEmail")
                    .SetAttributeValue("value", model.unlockApprovedEmail.ToString());

                var language = _doc.Root.Elements("language").Elements("add");
                language.FirstOrDefault(x => x.Attribute("name").Value == "defaultCulture")
                    .SetAttributeValue("value", model.defaultCulture);
                language.FirstOrDefault(x => x.Attribute("name").Value == "unlockAllCulture")
                    .SetAttributeValue("value", model.unlockAllCulture.ToString());

                var theme1 = _doc.Root.Elements("theme").Elements("add");
                theme1.FirstOrDefault(x => x.Attribute("name").Value == "defaultTheme")
                    .SetAttributeValue("value", model.defaultTheme);
                theme1.FirstOrDefault(x => x.Attribute("name").Value == "unlockTheme")
                    .SetAttributeValue("value", model.unlockTheme.ToString());

                var profile = _doc.Root.Elements("profile").Elements("add");
                profile.FirstOrDefault(x => x.Attribute("name").Value == "unlockGravatar")
                    .SetAttributeValue("value", model.unlockGravatar.ToString());
                var pagename = _doc.Root.Elements("core").Elements("add");
                var homepage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameHome").Elements("add");
                homepage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameHome);
                var aboutpage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameAbout").Elements("add");
                aboutpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameAbout);
                var titlepage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameTitle").Elements("add");
                titlepage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameTitle);
                _doc.Save(_absolutDirectory);
                return RedirectToAction("Index", "Home");
            }
            model.AllProvider = new SelectList(__core.allProviderDictionary(), "Key", "Value", __core.Provider());
            model.AllCulture = new SelectList(loc.allCultureDictionary(), "Key", "Value", loc.defaultCulture());
            model.AllTheme = new SelectList(theme.allThemeDictionary(), "Key", "Value", theme.defaultTheme());
            return View(model);
        }
        [CoreStartSetting]
        public ActionResult CreateBase()
        {
            CreateBaseModel model = new CreateBaseModel();
            model.DataBase = __core.DataBase();
            model.IntegratedSecurity = __core.IntegratedSecurity();
            model.Password = __core.Password();
            model.Server = __core.Server();
            model.User = __core.User();
            model.Provider = __core.Provider();
            model.AllProvider = new SelectList(__core.allProviderDictionary(), "Key", "Value", __core.Provider());
            return View(model);
        }
        [HttpPost]
        [CoreStartSetting]
        public ActionResult CreateBase(CreateBaseModel model)
        {
            if (ModelState.IsValid & ValidCreateBase(model))
            {
                if (model.Provider == "MYSQL")
                    createDB.CreateDataBaseMYSQL(model.Server, model.DataBase, model.User, model.Password);
                else if (model.Provider == "MSSQL")
                    createDB.CreateDataBaseMSSQL(model.Server, model.DataBase, model.User, model.Password, model.IntegratedSecurity);
                if (model.User == null)
                    model.User = "user";
                if (model.Password == null)
                    model.Password = "password";
                string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var connection = _doc.Root.Elements("connection").Elements("add");
                connection.FirstOrDefault(x => x.Attribute("name").Value == "IntegratedSecurity")
                    .SetAttributeValue("value", model.IntegratedSecurity.ToString());
                connection.FirstOrDefault(x => x.Attribute("name").Value == "DataBase")
                    .SetAttributeValue("value", model.DataBase);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                    .SetAttributeValue("value", model.Password);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Server")
                    .SetAttributeValue("value", model.Server);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "User")
                    .SetAttributeValue("value", model.User);
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Provider")
                    .SetAttributeValue("value", model.Provider);
                _doc.Save(_absolutDirectory);
                return RedirectToAction("Register", "Account");
            }
            model.AllProvider = new SelectList(__core.allProviderDictionary(), "Key", "Value", __core.Provider());
            return View(model);
        }
        [CoreStartSetting]
        public ActionResult CreateAdmin()
        {
            Roles.CreateRole(userset.roleAdmin());
            coreRepo.roleRepo.mtUnlockUserInRole(HttpContext.User.Identity.Name, userset.roleAdmin());
            Roles.CreateRole(userset.roleChiefEditor());
            coreRepo.roleRepo.mtUnlockUserInRole(HttpContext.User.Identity.Name, userset.roleChiefEditor());
            string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add");
            core.FirstOrDefault(x => x.Attribute("name").Value == "Development")
                .SetAttributeValue("value", false.ToString());
            _doc.Save(_absolutDirectory);
            return RedirectToAction("InstallModules");
        }
        [CoreSqlSetting]
        public ActionResult UninstallModule(string id)
        {
            UninstallModules model = new UninstallModules();
            model.UninstallModule(id);
            return RedirectToAction("UninstallModule", id.Replace(".", "_") + "Export");
        }
        [CoreSqlSetting]
        public ActionResult InstallModules()
        {
            ModulesModel model = new ModulesModel();
            model.modules = hpRepo.alldllMassiveForModules();
            model.uninstall = hpRepo.alldllMassiveForUninstall();
            return View(model);
        }
        [HttpPost]
        [CoreSqlSetting]
        public ActionResult InstallModules(ModulesModel model)
        {
            var ts = hpRepo.alldllMassiveForModules();
            foreach (var tag in ts)
            {
                if (Boolean.Parse(Request.Form.GetValues(tag.Key)[0]))
                {
                    hpRepo.CreateDataBase(tag.Key, culture);
                }
            }
            hpRepo.CreateSideBar();
            hpRepo.CreateSideBarExport(culture);
            hpRepo.CreateMenu(culture);
            hpRepo.CreateProfileSmall();
            hpRepo.CreateProfile(culture);
            return RedirectToAction("Index", "Home");
        }
        private bool ValidCreateBase(CreateBaseModel model)
        {
            if (!model.IntegratedSecurity && model.User == null)
                ModelState.AddModelError("User", CoreLanguage.account_error_username);
            if (!model.IntegratedSecurity && model.Password == null)
                ModelState.AddModelError("Password", CoreLanguage.account_error_password);
            if (model.Provider == "MYSQL" && model.User == null)
                ModelState.AddModelError("User", CoreLanguage.account_error_username);
            if (model.Provider == "MYSQL" && model.Password == null)
                ModelState.AddModelError("Password", CoreLanguage.account_error_password);
            if (model.Provider == "MSSQL" && !createDB.TestConnectMSSQL(model))
                ModelState.AddModelError("Provider", CoreLanguage.provider_error);
            if (model.Provider == "MYSQL" && !createDB.TestConnectMYSQL(model))
                ModelState.AddModelError("Provider", CoreLanguage.provider_error);
            return ModelState.IsValid;
        }
        [CoreSqlSetting]
        public ActionResult Email()
        {
            EmailModel model = new EmailModel();
            model.FromEmail = coreRepo.emailRepo.from_email();
            model.Port = coreRepo.emailRepo.port();
            model.Smtp = coreRepo.emailRepo.smtp();
            model.Ssl = coreRepo.emailRepo.ssl();
            model.LoginEmail = coreRepo.emailRepo.login_email();
            model.PasswordEmail = coreRepo.emailRepo.pass_email();
            return View(model);

        }
        [HttpPost]
        [CoreSqlSetting]
        public ActionResult Email(EmailModel model)
        {
            if (ModelState.IsValid & approvedSendEmail(model))
            {
                string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var core = _doc.Root.Elements("email").Elements("add");
                core.FirstOrDefault(x => x.Attribute("name").Value == "unlockSendEmail")
                    .SetAttributeValue("value", true.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "EmailAdress")
                    .SetAttributeValue("value", model.FromEmail);
                core.FirstOrDefault(x => x.Attribute("name").Value == "Smtp")
                    .SetAttributeValue("value", model.Smtp);
                core.FirstOrDefault(x => x.Attribute("name").Value == "Port")
                    .SetAttributeValue("value", model.Port.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "Ssl")
                    .SetAttributeValue("value", model.Ssl.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "Login")
                    .SetAttributeValue("value", model.LoginEmail);
                core.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                    .SetAttributeValue("value", model.PasswordEmail);
                _doc.Save(_absolutDirectory);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [CoreSqlSetting]
        public ActionResult About()
        {
            EditAboutModel model = new EditAboutModel();
            model.body = coreRepo.aboutRepo.GetAbout(Session["culture"].ToString());
            return View(model);

        }
        [HttpPost]
        [CoreSqlSetting]
        [ValidateInput(false)]
        public ActionResult About(EditAboutModel model)
        {
            if (ModelState.IsValid)
            {
                coreRepo.aboutRepo.EditAbout(model.body, Session["culture"].ToString());
                return RedirectToAction("About", "Home");
            }
            model.body = coreRepo.aboutRepo.GetAbout(Session["culture"].ToString());
            return View(model);

        }
        private bool approvedSendEmail(EmailModel model)
        {
            if (!coreRepo.emailRepo.approvedSendEmail(model))
                ModelState.AddModelError("FromEmail", CoreLanguage.Email_send_error);
            return ModelState.IsValid;
        }
        public ActionResult HomePage()
        {
            // Articles id=-1 , Blogs id=-2
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Home/HomePage.ascx");
            int index = 1;
            foreach (string x in page)
            {
                if (x.Contains("Export.HomePage"))
                {
                    string[] first = x.Remove(x.LastIndexOf(")")).Replace("<%=", "").Split('(');
                    string assembly = first[0].Replace(".Export.HomePage", "");
                    string[] vars = first[1].Split(',');
                    int id = int.Parse(vars[0]);
                    int rows = int.Parse(vars[1]);
                    int columns = int.Parse(vars[2]);
                    int content = int.Parse(vars[3]);
                    int image = int.Parse(vars[4]);
                    int style = int.Parse(vars[5]);
                    bool showtitle = Boolean.Parse(vars[6]);
                    exists.Add(new HomePageItem(assembly, "", "", id, index, rows, columns, content, image, style, showtitle));
                    index++;
                }
            }
            List<HomePageItem> newItems = new List<HomePageItem>();
            foreach (var item in hpRepo.HomePage(culture))
            {
                string assembly = item.Key.Remove(0, item.Key.IndexOf('_') + 1);
                string name = "";
                string cult = "";
                int id = 0;
                if (!String.IsNullOrEmpty(item.Value))
                {
                    string[] opts = item.Value.Split('_');
                    id = int.Parse(opts[0]);
                    cult = opts[1];
                    name = opts[2];
                    var existitem = exists.Where(a => a.Assembly == assembly).FirstOrDefault(x => x.Id == id);
                    if (existitem != null)
                    {
                        existitem.Name = name;
                        existitem.Culture = cult;
                    }
                    else
                        newItems.Add(new HomePageItem(assembly, name, cult, id, 0, 1, 1, 100, 100, 2, true));
                }
            }
            HomePageModel model = new HomePageModel();
            model.Theme = new ThemeSetting().theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult HomePage(string ids)
        {
            try
            {
                string path = "/Views/Home/HomePage.ascx";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                foreach (string x in page)
                {
                    if (!x.Contains("Export.HomePage"))
                        result.AppendLine(x);
                }
                foreach (var item in ids.Split('|'))
                {
                    if (string.IsNullOrEmpty(item))
                        break;
                    string newLine = "<%=";
                    string[] dataArr = item.Split('_');
                    string assembly = dataArr[0];
                    int id = int.Parse(dataArr[1]);
                    int rows = int.Parse(dataArr[2]);
                    int columns = int.Parse(dataArr[3]);
                    int content = int.Parse(dataArr[4]);
                    int image = int.Parse(dataArr[5]);
                    int style = int.Parse(dataArr[6]);
                    bool showtitle = Boolean.Parse(dataArr[7]);
                    newLine += assembly + ".Export.HomePage(" + id + "," + rows + "," + columns + "," + content + ","
                        + image + "," + style + "," + showtitle.ToString().ToLower() + ")%>";
                    result.AppendLine(newLine);
                }
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content(CoreLanguage.save_error);
            }
        }
        public ActionResult SideBar()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Shared/SideBarExport.ascx");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains(".Export."))
                {
                    string assembly = line.Remove(line.LastIndexOf("%")).Replace("<%=", "");
                    exists.Add(new HomePageItem(index, "", assembly, ""));
                    index++;
                }
            }
            List<HomePageItem> newItems = new List<HomePageItem>();
            //int id = 1;
            foreach (var item in hpRepo.SideBarPage(culture))
            {
                string assembly = item.Key;
                string ns = assembly.Remove(assembly.IndexOf(".Export."));
                string name = item.Value;
                var existitem = exists.FirstOrDefault(a => a.Assembly == assembly);
                if (existitem != null)
                {
                    existitem.Name = name;
                    existitem.Culture = ns;
                }
                else
                    newItems.Add(new HomePageItem(index, name, assembly, ns));
                index++;
            }
            HomePageModel model = new HomePageModel();
            model.Theme = new ThemeSetting().theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult SideBar(string ids)
        {
            try
            {
                string path = "/Views/Shared/SideBarExport.ascx";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                foreach (string x in page)
                {
                    if (!x.Contains(".Export."))
                        result.AppendLine(x);
                }
                foreach (var item in ids.Split('|'))
                {
                    if (string.IsNullOrEmpty(item))
                        break;
                    string newLine = "<%=" + item + "%>";
                    result.AppendLine(newLine);
                }
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content(CoreLanguage.save_error);
            }
        }
        public ActionResult TopMenu()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Shared/Menu.ascx");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains(".Export."))
                {
                    string assembly = line.Remove(0, line.IndexOf("=") + 1);
                    if (assembly.EndsWith(","))
                        assembly = assembly.Replace(",", "");
                    exists.Add(new HomePageItem(index, "", assembly, ""));
                    index++;
                }
            }
            List<HomePageItem> newItems = new List<HomePageItem>();
            //int id = 1;
            foreach (var item in hpRepo.MenuPage(culture))
            {
                string assembly = item.Key;
                string ns = assembly.Remove(assembly.IndexOf(".Export."));
                string name = item.Value;
                var existitem = exists.FirstOrDefault(a => a.Assembly == assembly);
                if (existitem != null)
                {
                    existitem.Name = name;
                    existitem.Culture = ns;
                }
                else
                    newItems.Add(new HomePageItem(index, name, assembly, ns));
                index++;
            }
            HomePageModel model = new HomePageModel();
            model.Theme = new ThemeSetting().theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult TopMenu(string ids)
        {
            try
            {
                string path = "/Views/Shared/Menu.ascx";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                bool isOk = false;
                foreach (string x in page)
                {
                    if (!isOk)
                        result.AppendLine(x);
                    if (x.Contains("//StartMenu"))
                        isOk = true;

                    //if (x.Contains("//EndMenu"))
                    //{
                    //    isOk = false;
                    //    result.AppendLine(x);
                    //}
                }
                int ctr = 1;
                foreach (var item in ids.Split('|'))
                {
                    if (string.IsNullOrEmpty(item))
                        break;
                    string newLine = "i" + ctr + "=" + item + ",";
                    result.AppendLine(newLine);
                    ctr++;
                }
                result.AppendLine("//EndMenu (Don't remove this line)");
                result.AppendLine("})%>");
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content(CoreLanguage.save_error);
            }
        }
    }
}