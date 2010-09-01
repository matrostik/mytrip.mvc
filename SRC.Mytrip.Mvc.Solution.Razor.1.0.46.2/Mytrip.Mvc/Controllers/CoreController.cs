/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Mytrip.Mvc.Install;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>Контроллер настройки сайта
    /// </summary>
    public class CoreController : ICoreController
    {
        #region Панель управления
        // **********************************************
        // Панель управления
        // **********************************************

        /// <summary>GET: /Core/Index
        /// Основные настройки сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult Index()
        {
            CoreModel model = new CoreModel();
            model.DataBase = CoreSetting.DataBase();
            model.IntegratedSecurity = CoreSetting.IntegratedSecurity();
            model.Password = CoreSetting.Password();
            model.Server = CoreSetting.Server();
            model.User = CoreSetting.User();
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
            model.maxInvalidPasswordAttempts = UsersSetting.maxInvalidPasswordAttempts();
            model.requiresUniqueEmail = UsersSetting.requiresUniqueEmail();
            model.unlockCaptcha = UsersSetting.unlockCaptcha();
            model.roleAdmin = UsersSetting.roleAdmin();
            model.roleChiefEditor = UsersSetting.roleChiefEditor();
            model.unlockRegistration = UsersSetting.unlockRegistration();
            model.unlockVisibleLogon = UsersSetting.unlockVisibleLogon();
            model.defaultCulture = LocalisationSetting.defaultCulture();
            model.unlockAllCulture = LocalisationSetting.unlockAllCulture();
            model.defaultTheme = ThemeSetting.defaultTheme();
            model.unlockTheme = ThemeSetting.unlockTheme();
            model.unlockGravatar = UsersSetting.unlockGravatar();
            model.Provider = CoreSetting.Provider();
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Value", "Value", CoreSetting.Provider());
            model.AllCulture = new SelectList(LocalisationSetting.allCultureDictionary(), "Value", "Value", LocalisationSetting.defaultCulture());
            model.AllTheme = new SelectList(ThemeSetting.allThemeDictionary(), "Value", "Value", ThemeSetting.defaultTheme());
            model.unlockApprovedEmail = UsersSetting.unlockApprovedEmail();
            model.nameHome = CoreSetting.NameHomePage();
            model.nameAbout = CoreSetting.NameAboutPage();
            model.nameTitle = CoreSetting.NameTitlePage();
            model.ControlPanelSideBare = CoreSetting.ControlPanelSideBare();
            model.DonateSideBare = CoreSetting.DonateSideBare();
            model.unlockOpenId = UsersSetting.unlockOpenId();
            model.unlockSiteId = UsersSetting.unlockSiteId();
            return View(model);
        }

        /// <summary>POST: /Core/Index
        /// Обработка данных основных настроек сайта
        /// </summary>
        /// <param name="model">CoreModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
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
                coreRepo.roleRepo.RenameRole(UsersSetting.roleAdmin(), model.roleAdmin);
                coreRepo.roleRepo.RenameRole(UsersSetting.roleChiefEditor(), model.roleChiefEditor);

                #region Сохранение данных в MytripConfiguration.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var core = _doc.Root.Elements("core").Elements("add");
                core.FirstOrDefault(x => x.Attribute("name").Value == "ControlPanelSideBare")
                    .SetAttributeValue("value", model.ControlPanelSideBare.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "DonateSideBare")
                    .SetAttributeValue("value", model.DonateSideBare.ToString());
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
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockOpenId")
                    .SetAttributeValue("value", model.unlockOpenId.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockSiteId")
                    .SetAttributeValue("value", model.unlockSiteId.ToString());
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
                var content = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameHome").Elements("add");
                content.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameHome);
                var aboutpage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameAbout").Elements("add");
                aboutpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameAbout);
                var titlepage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameTitle").Elements("add");
                titlepage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameTitle);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("cs_controlpanelsidebare");
                GeneralMethods.MytripCacheRemove("cs_donatesidesare");
                GeneralMethods.MytripCacheRemove("cs_namehomepage", true);
                GeneralMethods.MytripCacheRemove("cs_nameaboutpage", true);
                GeneralMethods.MytripCacheRemove("cs_nametitlepage", true);
                GeneralMethods.MytripCacheRemove("cs_server");
                GeneralMethods.MytripCacheRemove("cs_database");
                GeneralMethods.MytripCacheRemove("cs_user");
                GeneralMethods.MytripCacheRemove("cs_password");
                GeneralMethods.MytripCacheRemove("cs_integratedsecurity");
                GeneralMethods.MytripCacheRemove("cs_provider");
                GeneralMethods.MytripCacheRemove("ls_defaultculture");
                GeneralMethods.MytripCacheRemove("ls_unlockallculture");
                GeneralMethods.MytripCacheRemove("ts_defaulttheme");
                GeneralMethods.MytripCacheRemove("ts_unlocktheme");
                GeneralMethods.MytripCacheRemove("us_minrequiredpasswordlength");
                GeneralMethods.MytripCacheRemove("us_maxinvalidpasswordattempts");
                GeneralMethods.MytripCacheRemove("us_requiresuniqueemail");
                GeneralMethods.MytripCacheRemove("us_unlockopenid");
                GeneralMethods.MytripCacheRemove("us_unlocksiteid");
                GeneralMethods.MytripCacheRemove("us_unlockcaptcha");
                GeneralMethods.MytripCacheRemove("us_roleadmin");
                GeneralMethods.MytripCacheRemove("us_unlockgravatar");
                GeneralMethods.MytripCacheRemove("us_unlockregistration");
                GeneralMethods.MytripCacheRemove("us_unlockvisiblelogon");
                GeneralMethods.MytripCacheRemove("us_unlockapprovedemail");
                GeneralMethods.MytripCacheRemove("us_rolechiefeditor");
                #endregion

                return RedirectToAction("ControlPanel");
            }
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Key", "Value", CoreSetting.Provider());
            model.AllCulture = new SelectList(LocalisationSetting.allCultureDictionary(), "Key", "Value", LocalisationSetting.defaultCulture());
            model.AllTheme = new SelectList(ThemeSetting.allThemeDictionary(), "Key", "Value", ThemeSetting.defaultTheme());
            return View(model);
        }

        /// <summary>GET: /Core/UninstallModule
        /// Удаление модуля
        /// </summary>
        /// <param name="id">имя модуля</param>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult UninstallModule(string id)
        {
            UninstallModules model = new UninstallModules();
            model.UninstallModule(id);
            return RedirectToAction("UninstallModule", id.Replace(".", "_") + "Export");
        }

        /// <summary>GET: /Core/InstallModule
        /// Установка модуля
        /// </summary>
        /// <param name="id">имя модуля</param>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult InstallModule(string id)
        {
            return RedirectToAction("CreateDataBaseAndXml", id.Replace(".", "_") + "Export");
        }

        /// <summary>GET: /Core/SaveModule
        /// Сохранение записей на страницах сайта о
        /// установленном модуле
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult SaveModule()
        {
            coreRepo.installModuleRepo.CreateSideBar();
            coreRepo.installModuleRepo.CreateMenu(LocalisationSetting.culture());
            coreRepo.installModuleRepo.CreateProfileSmall();
            coreRepo.installModuleRepo.CreateProfile(LocalisationSetting.culture());
            coreRepo.installModuleRepo.CreateControlPanel();

            string _absolutDirectory = HttpContext.Server.MapPath("/Web.config");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("appSettings").Elements("add");
            core.FirstOrDefault(x => x.Attribute("key").Value == "restart")
                .SetAttributeValue("value", Guid.NewGuid());
            _doc.Save(_absolutDirectory);
            return RedirectToAction("InstallModules");
        }

        /// <summary>GET: /Core/InstallModules
        /// Страница установки и удаления модулей
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult InstallModules()
        {
            ModulesModel model = new ModulesModel();
            model.modules = coreRepo.installModuleRepo.alldllMassiveForModules();
            model.uninstall = coreRepo.installModuleRepo.alldllMassiveForUninstall();
            return View(model);
        }

        /// <summary>GET: /Core/Email
        /// Настройка Email
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult Email()
        {
            EmailModel model = new EmailModel();
            model.FromEmail = EmailSetting.from_email();
            model.Port = EmailSetting.port();
            model.Smtp = EmailSetting.smtp();
            model.Ssl = EmailSetting.ssl();
            model.LoginEmail = EmailSetting.login_email();
            model.PasswordEmail = EmailSetting.pass_email();
            return View(model);

        }

        /// <summary>POST: /Core/Email
        /// Обработка данных настройки Email
        /// </summary>
        /// <param name="model">EmailModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdmin]
        public ActionResult Email(EmailModel model)
        {
            if (ModelState.IsValid & approvedSendEmail(model))
            {
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
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
                GeneralMethods.MytripCacheRemove("es_unlocksendemail");
                GeneralMethods.MytripCacheRemove("es_from_email");
                GeneralMethods.MytripCacheRemove("es_smtp");
                GeneralMethods.MytripCacheRemove("es_port");
                GeneralMethods.MytripCacheRemove("es_ssl");
                GeneralMethods.MytripCacheRemove("es_login_email");
                GeneralMethods.MytripCacheRemove("es_pass_email");
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        /// <summary>GET: /Core/About
        /// Редактирование страницы About
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult About()
        {
            EditAboutModel model = new EditAboutModel();
            model.body = coreRepo.aboutRepo.GetAbout(Session["culture"].ToString());
            return View(model);

        }

        /// <summary>POST: /Core/About
        /// Сохранение измененных данных
        /// </summary>
        /// <param name="model">EditAboutModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
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

        /// <summary>GET: /Core/HomePage
        /// Настройка главной страницы (добавление и удаление модулей)
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult HomePage()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Home/_homePage.cshtml");
            int index = 1;
            foreach (string x in page)
            {
                if (x.Contains("Export.HomePage"))
                {
                    string[] first = x.Remove(x.LastIndexOf(")")).Replace("@", "").Split('(');
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
            foreach (var item in coreRepo.installModuleRepo.HomePage(LocalisationSetting.culture()))
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
            model.Theme = ThemeSetting.theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }

        /// <summary>POST: /Core/HomePage
        /// Сохранение главной страницы с учетом
        /// добавленных и удаленных модулей
        /// </summary>
        /// <param name="ids">данные по модулю</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult HomePage(string ids)
        {
            try
            {
                string path = "/Views/Home/_homePage.cshtml";
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
                    string newLine = "@";
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
                        + image + "," + style + "," + showtitle.ToString().ToLower() + ")";
                    result.AppendLine(newLine);
                }
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content("<span class='error'>" + CoreLanguage.save_error + "</span>");
            }
        }

        /// <summary>GET: /Core/CorSideBar
        /// Настройка сайдбара (добавление и удаление модулей)
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult CorSideBar()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Shared/_sideBarExport.cshtml");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains(".Export."))
                {
                    string assembly = line.Replace("@", "");
                    exists.Add(new HomePageItem(index, "", assembly, ""));
                    index++;
                }
            }
            List<HomePageItem> newItems = new List<HomePageItem>();
            foreach (var item in coreRepo.installModuleRepo.SideBarPage(LocalisationSetting.culture()))
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
            model.Theme = ThemeSetting.theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }

        /// <summary>POST: /Core/CorSideBar
        /// Сохранение сайдбара с учетом
        /// добавленных и удаленных модулей
        /// </summary>
        /// <param name="ids">данные по модулю</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult CorSideBar(string ids)
        {
            try
            {
                string path = "/Views/Shared/_sideBarExport.cshtml";
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
                    string newLine = "@" + item;
                    result.AppendLine(newLine);
                }
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content("<span class='error'>" + CoreLanguage.save_error + "</span>");
            }
        }

        /// <summary>GET: /Core/TopMenu
        /// Настройка меню (добавление и удаление пунктов)
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult TopMenu()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Shared/_menu.cshtml");
            int index = 1;
            bool apprstart = false;
            bool apprend = false;
            foreach (string line in page)
            {
                if (line.Contains("/*--StartMenu--*/"))
                    apprstart = true;
                else if (line.Contains("/*--EndMenu--*/"))
                    apprend = true;
                else if (line.Contains(".Export."))
                {
                    string assembly = line.Remove(0, line.IndexOf("=") + 1);
                    if (assembly.EndsWith(","))
                        assembly = assembly.Replace(",", "");
                    exists.Add(new HomePageItem(index, "", assembly, ""));
                    index++;
                }
            }
            if (!apprstart || !apprend)
                return RedirectToAction("CreateTopMenu");
            List<HomePageItem> newItems = new List<HomePageItem>();
            foreach (var item in coreRepo.installModuleRepo.MenuPage(LocalisationSetting.culture()))
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
            model.Theme = ThemeSetting.theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            return View(model);
        }

        /// <summary>POST: /Core/TopMenu
        /// Сохранение меню с учетом
        /// добавленных и удаленных пунктов
        /// </summary>
        /// <param name="ids">данные по пункту</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult TopMenu(string ids)
        {
            try
            {
                string path = "/Views/Shared/_menu.cshtml";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                bool isOk = false;
                foreach (string x in page)
                {
                    if (!isOk)
                        result.AppendLine(x);
                    if (x.Contains("/*--StartMenu--*/"))
                        isOk = true;
                }
                int ctr = 1;
                IDictionary<int, string> _ids = new Dictionary<int, string>();

                foreach (var item in ids.Split('|'))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        _ids.Add(ctr, item);
                        ctr++;
                    }
                }
                int _ctr = _ids.Count();
                foreach (var item in _ids)
                {
                    string end = (_ctr == item.Key) ? string.Empty : ",";
                    string newLine =string.Concat("i",item.Key,"=",item.Value,end);
                    result.AppendLine(newLine);
                }
                result.AppendLine("/*--EndMenu--*/");
                result.AppendLine("})");
                EditePageRepository.CreatePage(path, result.ToString());
                return Content(CoreLanguage.save_success);
            }
            catch
            {
                return Content("<span class='error'>" + CoreLanguage.save_error + "</span>");
            }
        }

        /// <summary>GET: /Core/ControlPanel
        /// Страница панели управления
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult ControlPanel()
        {
            return View();
        }

        /// <summary>GET: /Core/CreateTopMenu
        /// Восстановление меню при изменении параметров
        /// автогенерации
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult CreateTopMenu()
        {
            string path = "/Views/Shared/_menu.cshtml";
            StringBuilder result = new StringBuilder();
            result.AppendLine("@inherits System.Web.Mvc.WebViewPage");
            result.AppendLine("@{");
            result.AppendLine("    /* WARNING AUTO GENERATE CODE */");
            result.AppendLine("");
            result.AppendLine("    /* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich");
            result.AppendLine("       To learn more about Mytrip.Mvc visit");
            result.AppendLine("       http://mytripmvc.net  http://mytripmvc.codeplex.com");
            result.AppendLine("       mytripmvc@gmail.com");
            result.AppendLine("       license: Microsoft Public License (Ms-PL) */");
            result.AppendLine("");
            result.AppendLine("    /* WARNING AUTO GENERATE CODE */");
            result.AppendLine("}");
            result.AppendLine("@Html.MytripMenu(new {");
            result.AppendLine("/*--StartMenu--*/");
            result.AppendLine("/*--EndMenu--*/");
            result.AppendLine("})");
            EditePageRepository.CreatePage(path, result.ToString());
            return RedirectToAction("TopMenu");
        }

        //****************** E N D **********************
        #endregion

        #region Первое включение
        // **********************************************
        // Первое включение
        // **********************************************


        /// <summary>GET: /Core/CreateBase
        /// Создание таблиц в базе данных необходимых
        /// для работы ядра
        /// </summary>
        /// <returns>ActionResult</returns>
        [StartSetting]
        public ActionResult CreateBase()
        {
            CreateBaseModel model = new CreateBaseModel();
            model.DataBase = CoreSetting.DataBase();
            model.IntegratedSecurity = CoreSetting.IntegratedSecurity();
            model.Password = CoreSetting.Password();
            model.Server = CoreSetting.Server();
            model.User = CoreSetting.User();
            model.Provider = CoreSetting.Provider();
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Value", "Value", CoreSetting.Provider());
            return View(model);
        }

        /// <summary>POST: /Core/CreateBase
        /// Обработка данных создания таблиц в базе данных необходимых
        /// для работы ядра
        /// </summary>
        /// <param name="model">CreateBaseModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [StartSetting]
        public ActionResult CreateBase(CreateBaseModel model)
        {
            if (ModelState.IsValid & ValidCreateBase(model))
            {

                if (model.User == null)
                    model.User = "user";
                if (model.Password == null)
                    model.Password = "password";
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
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
                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("cs_server");
                GeneralMethods.MytripCacheRemove("cs_database");
                GeneralMethods.MytripCacheRemove("cs_user");
                GeneralMethods.MytripCacheRemove("cs_password");
                GeneralMethods.MytripCacheRemove("cs_integratedsecurity");
                GeneralMethods.MytripCacheRemove("cs_provider");
                if (model.Provider == "MySql")
                    CreateDataBase.CreateDataBaseMYSQL();
                else if (model.Provider == "MSSQL")
                    CreateDataBase.CreateDataBaseMSSQL();
                #endregion
                return RedirectToAction("Register", "Account");
            }
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Value", "Value", CoreSetting.Provider());
            return View(model);
        }

        /// <summary>GET: /Core/CreateAdmin
        /// Создание ролей сайта и регистрация пользователя
        /// как администратора сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [StartSetting]
        public ActionResult CreateAdmin()
        {
            Roles.CreateRole(UsersSetting.roleAdmin());
            coreRepo.roleRepo.mtUnlockUserInRole(HttpContext.User.Identity.Name, UsersSetting.roleAdmin());
            Roles.CreateRole(UsersSetting.roleChiefEditor());
            coreRepo.roleRepo.mtUnlockUserInRole(HttpContext.User.Identity.Name, UsersSetting.roleChiefEditor());
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add");
            core.FirstOrDefault(x => x.Attribute("name").Value == "Development")
                .SetAttributeValue("value", false.ToString());
            _doc.Save(_absolutDirectory);
            #region Очистка кеша
            GeneralMethods.MytripCacheRemove("cs_development");
            GeneralMethods.MytripCacheRemove("cacherole");
            #endregion
            return RedirectToAction("InstallModules");
        }

        //****************** E N D **********************
        #endregion

        #region Валидация данных
        // **********************************************
        // Валидация данных
        // **********************************************

        /// <summary>Проверка подключения к базе данных
        /// </summary>
        /// <param name="model">CreateBaseModel</param>
        /// <returns>возвращает bool</returns>
        private bool ValidCreateBase(CreateBaseModel model)
        {
            if (!model.IntegratedSecurity && model.User == null)
                ModelState.AddModelError("User", CoreLanguage.account_error_username);
            if (!model.IntegratedSecurity && model.Password == null)
                ModelState.AddModelError("Password", CoreLanguage.account_error_password);
            if (model.Provider == "MySql" && model.User == null)
                ModelState.AddModelError("User", CoreLanguage.account_error_username);
            if (model.Provider == "MySql" && model.Password == null)
                ModelState.AddModelError("Password", CoreLanguage.account_error_password);
            if (model.Provider == "MSSQL" && !CreateDataBase.TestConnectMSSQL(model))
                ModelState.AddModelError("Provider", CoreLanguage.provider_error);
            if (model.Provider == "MySql" && !CreateDataBase.TestConnectMYSQL(model))
                ModelState.AddModelError("Provider", CoreLanguage.provider_error);
            return ModelState.IsValid;
        }

        /// <summary>Проверка на корректность введенных данных
        /// при настройке Email
        /// </summary>
        /// <param name="model">EmailModel</param>
        /// <returns>возвращает bool</returns>
        private bool approvedSendEmail(EmailModel model)
        {
            if (!coreRepo.emailRepo.approvedSendEmail(model))
                ModelState.AddModelError("FromEmail", CoreLanguage.Email_send_error);
            return ModelState.IsValid;
        }

        //****************** E N D **********************
        #endregion
    }
}