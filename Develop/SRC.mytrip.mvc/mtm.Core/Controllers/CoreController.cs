/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
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
using mtm.Core.Install;
using mtm.Core.Models;
using mtm.Core.Repository;
using mtm.Core.Settings;
using mtm.Core.Helpers;

namespace mtm.Core.Controllers
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
            model.SideBarPosition = ProfileSetting.SideBarPosition();
            model.SideBarPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.SideBarPosition());
            model.MenuPosition = ProfileSetting.MenuPosition();
            model.MenuPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.MenuPosition());
            model.defaultCulture = LocalisationSetting.defaultCulture();
            model.unlockAllCulture = LocalisationSetting.unlockAllCulture();
            model.defaultTheme = ThemeSetting.defaultTheme();
            model.unlockTheme = ThemeSetting.unlockTheme();
            model.unlockGravatar = ProfileSetting.unlockGravatar();
            model.AllCulture = new SelectList(LocalisationSetting.allCultureDictionary(), "Value", "Value", LocalisationSetting.defaultCulture());
            model.AllTheme = new SelectList(ThemeSetting.allThemeDictionary(), "Value", "Value", ThemeSetting.defaultTheme());
            model.nameHome = CoreSetting.NameHomePage();
            model.nameTitle = CoreSetting.NameTitlePage();
            model.CountLatestUsers = CoreSetting.CountLatestUsers();
            model.unlockWrap = ThemeSetting._unlockWrap();
            model.nameLogo = CoreSetting.nameLogo();
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
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
               
                var core = _doc.Root.Elements("core").Elements("add");
                core.FirstOrDefault(x => x.Attribute("name").Value == "CountLatestUsers")
                    .SetAttributeValue("value", model.CountLatestUsers.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "SideBarPosition")
                    .SetAttributeValue("value", model.SideBarPosition);
                core.FirstOrDefault(x => x.Attribute("name").Value == "MenuPosition")
                    .SetAttributeValue("value", model.MenuPosition);
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
                theme1.FirstOrDefault(x => x.Attribute("name").Value == "unlockWrap")
                    .SetAttributeValue("value", model.unlockWrap.ToString());
                var profile = _doc.Root.Elements("profile").Elements("add");
                profile.FirstOrDefault(x => x.Attribute("name").Value == "unlockGravatar")
                    .SetAttributeValue("value", model.unlockGravatar.ToString());
                var pagename = _doc.Root.Elements("core").Elements("add");
                var content = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameHome").Elements("add");
                content.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameHome);
                var titlepage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameTitle").Elements("add");
                titlepage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameTitle);
                _doc.Save(_absolutDirectory);
                #endregion
                if (ThemeSetting.unlockWrap() != model.unlockWrap)
                    GeneralMethods.RestartApplication();
                else
                {
                    #region Очистка кеша
                    GeneralMethods.MytripCacheRemove("mtm_cs_countlatestusers");
                    GeneralMethods.MytripCacheRemove("mtm_cs_namehomepage", true);
                    GeneralMethods.MytripCacheRemove("mtm_cs_nametitlepage", true);
                    GeneralMethods.MytripCacheRemove("mtm_cs_namelogo", true);
                    GeneralMethods.MytripCacheRemove("ls_defaultculture");
                    GeneralMethods.MytripCacheRemove("ls_unlockallculture");
                    GeneralMethods.MytripCacheRemove("mtm_ts_defaulttheme");
                    GeneralMethods.MytripCacheRemove("mtm_ts_unlocktheme");                   
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockgravatar");                    
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockvisibleprofiletosidebar");
                    GeneralMethods.MytripCacheRemove("mtm_cs_sidebarposition");
                    GeneralMethods.MytripCacheRemove("mtm_cs_menuposition");
                    #endregion
                }
                LayoutHelper.PositionSideBar(model.SideBarPosition);
                LayoutHelper.PositionMenu(model.MenuPosition);
                RedirectToRouteResult a = RedirectToAction("ControlPanel");
                return a;
            }
           model.SideBarPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.SideBarPosition());
            model.MenuPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.MenuPosition());
            model.AllCulture = new SelectList(LocalisationSetting.allCultureDictionary(), "Value", "Value", LocalisationSetting.defaultCulture());
            model.AllTheme = new SelectList(ThemeSetting.allThemeDictionary(), "Value", "Value", ThemeSetting.defaultTheme());
            return View(model);
        }
        /// <summary>GET: /Core/Index
        /// Основные настройки сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult Logon()
        {
            CoreLogonModel model = new CoreLogonModel();
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
            model.maxInvalidPasswordAttempts = UsersSetting.maxInvalidPasswordAttempts();
            model.requiresUniqueEmail = UsersSetting.requiresUniqueEmail();
            model.unlockCaptcha = UsersSetting.unlockCaptcha();
            model.roleAdmin = UsersSetting.roleAdmin();
            model.roleChiefEditor = UsersSetting.roleChiefEditor();
            model.unlockRegistration = UsersSetting.unlockRegistration();
            model.unlockVisibleLogon = UsersSetting.unlockVisibleLogon();
            model.unlockApprovedEmail = UsersSetting.unlockApprovedEmail();
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
        [RoleAdmin]
        public ActionResult Logon(CoreLogonModel model)
        {
            if (ModelState.IsValid)
            {
               coreRepo.roleRepo.RenameRole(UsersSetting.roleAdmin(), model.roleAdmin);
               coreRepo.roleRepo.RenameRole(UsersSetting.roleChiefEditor(), model.roleChiefEditor);
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
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
                _doc.Save(_absolutDirectory);
                #endregion
               
                    #region Очистка кеша
                    GeneralMethods.MytripCacheRemove("mtm_us_minrequiredpasswordlength");
                    GeneralMethods.MytripCacheRemove("mtm_us_maxinvalidpasswordattempts");
                    GeneralMethods.MytripCacheRemove("mtm_us_requiresuniqueemail");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockopenid");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlocksiteid");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockcaptcha");
                    GeneralMethods.MytripCacheRemove("mtm_us_roleadmin");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockregistration");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockvisiblelogon");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockapprovedemail");
                    GeneralMethods.MytripCacheRemove("mtm_us_rolechiefeditor");
                    #endregion
                return RedirectToAction("ControlPanel");
            }
            return View(model);
        }
        /// <summary>GET: /Core/Index
        /// Основные настройки сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult Statistic()
        {
            CoreStatisticModel model = new CoreStatisticModel();
            model.unlockStatistic = StatisticSetting.unlockStatistic();
            model.Anonym = StatisticSetting.statisticAnonym();
            model.User = StatisticSetting.statisticUser();
            return View(model);
        }

        /// <summary>POST: /Core/Index
        /// Обработка данных основных настроек сайта
        /// </summary>
        /// <param name="model">CoreModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdmin]
        public ActionResult Statistic(CoreStatisticModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var membership = _doc.Root.Elements("statistic").Elements("add");
                membership.FirstOrDefault(x => x.Attribute("name").Value == "Anonym")
                    .SetAttributeValue("value", model.Anonym.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockStatistic")
                    .SetAttributeValue("value", model.unlockStatistic.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "User")
                    .SetAttributeValue("value", model.User.ToString());                
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_unlockstatistic");
                GeneralMethods.MytripCacheRemove("mtm_cs_statisticuser");
                GeneralMethods.MytripCacheRemove("mtm_cs_statisticanonym");
                #endregion
                return RedirectToAction("ControlPanel");
            }
            return View(model);
        }
        /// <summary>GET: /Core/Index
        /// Основные настройки сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult Geo()
        {
            CoreGeoModel model = new CoreGeoModel();
            model.unlockGeo = GeoSetting.unlockGeo();
            model.Url = GeoSetting.UrlGeo();
            model.keyBingMap = GeoSetting.keyBingMap();
            return View(model);
        }

        /// <summary>POST: /Core/Index
        /// Обработка данных основных настроек сайта
        /// </summary>
        /// <param name="model">CoreModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdmin]
        public ActionResult Geo(CoreGeoModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var membership = _doc.Root.Elements("geo").Elements("add");
                membership.FirstOrDefault(x => x.Attribute("name").Value == "keyBingMap")
                    .SetAttributeValue("value", model.keyBingMap.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "unlockGeo")
                    .SetAttributeValue("value", model.unlockGeo.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "Url")
                    .SetAttributeValue("value", model.Url.ToString());
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_unlockgeo");
                GeneralMethods.MytripCacheRemove("mtm_cs_keybingmap");
                #endregion
                return RedirectToAction("ControlPanel");
            }
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
            return RedirectToAction("InstallModule", id.Replace(".", "_") + "Export");
        }

        /// <summary>GET: /Core/SaveModule
        /// Сохранение записей на страницах сайта о
        /// установленном модуле
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult SaveModule()
        {
            coreRepo.installModuleRepo.CreateSideBarProfile();
            coreRepo.installModuleRepo.CreateMenu(LocalisationSetting.culture());
            coreRepo.installModuleRepo.CreateProfileSmall();
            coreRepo.installModuleRepo.CreateProfile(LocalisationSetting.culture());
            coreRepo.installModuleRepo.CreateControlPanel();
            coreRepo.installModuleRepo.CreateSearch();
            coreRepo.installModuleRepo.CreateRss();
            GeneralMethods.RestartApplication();
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
            model.install = coreRepo.installModuleRepo.alldllMassiveForModules();
            model.uninstall = coreRepo.installModuleRepo.alldllMassiveForUninstall();
            if (CoreSetting.Development())
            {
                if (model.install.Count() < 1 && model.uninstall.Count() < 1)
                    return RedirectToAction("ControlPanel");
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var core = _doc.Root.Elements("core").Elements("add");
                core.FirstOrDefault(x => x.Attribute("name").Value == "Development")
                    .SetAttributeValue("value", false.ToString());
                _doc.Save(_absolutDirectory);
                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_development");
                #endregion
            }
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
                    .SetAttributeValue("value", Crypto.Encrypt(model.FromEmail));
                core.FirstOrDefault(x => x.Attribute("name").Value == "Smtp")
                    .SetAttributeValue("value", Crypto.Encrypt(model.Smtp));
                core.FirstOrDefault(x => x.Attribute("name").Value == "Port")
                    .SetAttributeValue("value", model.Port.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "Ssl")
                    .SetAttributeValue("value", model.Ssl.ToString());
                core.FirstOrDefault(x => x.Attribute("name").Value == "Login")
                    .SetAttributeValue("value", Crypto.Encrypt(model.LoginEmail));
                core.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                    .SetAttributeValue("value", Crypto.Encrypt(model.PasswordEmail));
                _doc.Save(_absolutDirectory);
                GeneralMethods.MytripCacheRemove("mtm_es_unlocksendemail");
                GeneralMethods.MytripCacheRemove("mtm_es_fromemail");
                GeneralMethods.MytripCacheRemove("mtm_es_smtp");
                GeneralMethods.MytripCacheRemove("mtm_es_port");
                GeneralMethods.MytripCacheRemove("mtm_es_ssl");
                GeneralMethods.MytripCacheRemove("mtm_es_user");
                GeneralMethods.MytripCacheRemove("mtm_es_password");
                if (CoreSetting.Development())
                    return RedirectToAction("InstallModules");
                else
                    return RedirectToAction("ControlPanel");
            }
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
                else if (x.Contains("Html.HomePageAnonce"))
                {
                    string[] first = x.Remove(x.LastIndexOf(")")).Replace("@", "").Split('(');
                    string assembly = "mytrip.mvc";
                    string[] vars = first[1].Split(',');
                    int id = int.Parse(vars[0]);
                    int rows = 0;
                    int columns = 0;
                    int content = int.Parse(vars[1]);
                    int image = 0;
                    int style = 0;
                    bool showtitle = false;
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
                    int.TryParse(opts[0], out id);
                    cult = opts[1];
                    name = opts[2];
                    var existitem = exists.Where(a => a.Assembly == assembly).FirstOrDefault(x => x.Id == id);
                    if (existitem != null)
                    {
                        existitem.Name = name;
                        existitem.Culture = cult;
                    }
                    else if(assembly=="mytrip.mvc")
                        newItems.Add(new HomePageItem(assembly, name, cult, id, 0, 0, 0, 1000, 0, 0, false));
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
                    if (!x.Contains("Export.HomePage") && !x.Contains("HomePageAnonce"))
                        result.AppendLine(x);
                }
                int anonce = 0;
                //result.AppendLine("@Html.HomePageAnonce(" + anonce + ")");
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
                    if(assembly=="mytrip.mvc")
                        newLine += "Html.HomePageAnonce(" + id + "," + content +")";
                    else
                    newLine += assembly + ".Export.HomePage(" + id + "," + rows + "," + columns + "," + content + ","
                        + image + "," + style + "," + showtitle.ToString().ToLower() + ")";
                    result.AppendLine(newLine);
                    anonce++;
                    //result.AppendLine("@Html.HomePageAnonce(" + anonce + ")");
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
            string[] page = EditePageRepository.WritePage("/Views/Shared/_sideBar.cshtml");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains(".Export.") || line.Contains("Html."))
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
                string ns ="mytrip.mvc";
                if (assembly.Contains(".Export."))
                 ns = assembly.Remove(assembly.IndexOf(".Export."));
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
                string path = "/Views/Shared/_sideBar.cshtml";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                foreach (string x in page)
                {
                    result.AppendLine(x);
                    if (x.Contains("}"))
                    break;
                }
                bool sidebar =(ids==null||ids.Trim().Length==0)?false:true;
                LayoutHelper.PositionSideBar(sidebar, ProfileSetting.SideBarPosition());
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
                    string newLine = string.Concat("i", item.Key, "=", item.Value, end);
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
        [RoleAdminAndEditor]
        public ActionResult Announce()
        {
            List<HomePageItem> exists = new List<HomePageItem>();
            string[] page = EditePageRepository.WritePage("/Views/Shared/_announce.cshtml");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains(".Export.") || line.Contains("Html."))
                {
                    string assembly = line.Replace("@", "");
                    exists.Add(new HomePageItem(index, "", assembly, ""));
                    index++;
                }
            }
            List<HomePageItem> newItems = new List<HomePageItem>();
            foreach (var item in coreRepo.installModuleRepo.AnoncePage(LocalisationSetting.culture()))
            {
                string assembly = item.Key;
                string ns = "mytrip.mvc";
                if (assembly.Contains(".Export."))
                    ns = assembly.Remove(assembly.IndexOf(".Export."));
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
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Announce(string ids)
        {
            try
            {
                string path = "/Views/Shared/_announce.cshtml";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                foreach (string x in page)
                {
                    result.AppendLine(x);
                    if (x.Contains("}"))
                        break;
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
        /// <summary>GET: /Core/ControlPanel
        /// Страница панели управления
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult ControlPanel()
        {
            if (CoreSetting.Development())
            {
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var core = _doc.Root.Elements("core").Elements("add");
                core.FirstOrDefault(x => x.Attribute("name").Value == "Development")
                    .SetAttributeValue("value", false.ToString());
                _doc.Save(_absolutDirectory);
                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_development");
                #endregion
            }
            ControlPanelModel model = new ControlPanelModel();
            model.viewAdmin = MytripUser.UserInRole(UsersSetting.roleAdmin()) ? true : false;
            return View(model);
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
            result.AppendLine("       To learn more about mytrip.mvc visit");
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
        [RoleAdminAndEditor]
        public ActionResult EditorPage(int id, string id2)
        {
            EditorPageModel model = new EditorPageModel();
            if (id2 == "CreatePage")
            {

                model.pagetitle = id == 0 ? CoreLanguage.CreatePage : CoreLanguage.CreateSubPage;
                model.submit = CoreLanguage.create;
            }
            else if (id2 == "EditPage")
            {
                var x = coreRepo.corePageRepo.GetPages(id);
                model.pagetitle = string.Format(CoreLanguage.EditPage, x.Title);
                model.submit = CoreLanguage.edit;
                model.addHomePage = x.AddHomePage;
                model.addMenu = x.AddMenu;
                model.allCulture = x.AllCulture;
                model.body = x.Body;
                model.emailForm = x.EmailForm;
                model.sideBar = x.SideBar;
                model.title = x.Title;
                model.viewOnlyHomePage = x.ViewOnlyHomePage;
            }
            else if (id2 == "DeletePage")
            {
                var x = coreRepo.corePageRepo.GetPages(id);
                if (x.SubPagesId > 0)
                {
                    int _id = x.SubPagesId;
                    string path = x.Path;
                    coreRepo.corePageRepo.DeletePage(id);
                    return RedirectToAction("Page", "Home", new { id = _id, id2 = path });
                }
                else
                {
                    coreRepo.corePageRepo.DeletePage(id);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult EditorPage(int id, string id2, EditorPageModel model)
        {
            if (ModelState.IsValid && id2 == "CreatePage")
            {
                var x = coreRepo.corePageRepo.CreatePage(model.title, model.body, LocalisationSetting.culture(),
                    id, model.sideBar, model.emailForm, model.allCulture, model.addMenu, model.addHomePage, model.viewOnlyHomePage);
                return RedirectToAction("Page", "Home", new { id = x.PageId, id2 = x.Path });
            }
            else if (ModelState.IsValid && id2 == "EditPage")
            {
                var x = coreRepo.corePageRepo.EditPage(model.title, model.body,
                                   id, model.sideBar, model.emailForm, model.allCulture, model.addMenu, model.addHomePage, model.viewOnlyHomePage);
                if (!x.ViewOnlyHomePage)
                    return RedirectToAction("Page", "Home", new { id = x.PageId, id2 = x.Path });
                else
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult EditorRewrite()
        {
            RewriteModel model = new RewriteModel();
            model.AllRewrite = RewriteHelper.GetAllRewrite();
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult EditorRewrite(string url, string rewrite)
        {
            if (url != null && rewrite != null)
            {
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory("mtm.Rewrite");
                XDocument _doc = XDocument.Load(_absolutDirectory);
                bool restart = false;
                try
                {
                    var z = _doc.Root.Elements("add")
                         .FirstOrDefault(x => x.Attribute("url").Value == url);
                    if (z != null)
                    {
                        z.SetAttributeValue("url", url);
                        z.SetAttributeValue("rewrite", rewrite);
                    }
                    else
                    {
                        XElement newrewrite = new XElement("add",
                        new XAttribute("url", url),
                        new XAttribute("rewrite", rewrite));
                        _doc.Root.Add(newrewrite);
                    }
                    restart = true;
                }
                catch { }
                _doc.Save(_absolutDirectory);
                if (restart)
                    GeneralMethods.RestartApplication();
            }
            RewriteModel model = new RewriteModel();
            model.AllRewrite = RewriteHelper.GetAllRewrite();
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult DleteRewrite(string id)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory("mtm.Rewrite");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            bool restart = false;
            try
            {
                _doc.Root.Elements("add")
                    .FirstOrDefault(x => x.Attribute("url").Value == id.Replace("()", "/"))
                    .Remove();
                restart = true;
            }
            catch { }
            _doc.Save(_absolutDirectory);
            if(restart)
                GeneralMethods.RestartApplication();
            return RedirectToAction("EditorRewrite");
        }
        /// <summary>GET: /Core/Footer
        /// Редактирование нижнего колонтитула
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult Footer()
        {
            FooterModel model = new FooterModel();
            //string[] page = EditePageRepository.WritePage("/Views/Shared/_footer.cshtml");
            string[] page = EditePageRepository.WritePage("/Views/Shared/_footer.cshtml");
            StringBuilder result = new StringBuilder();
            foreach (var x in page)
            { result.AppendLine(x); }
            model.Content = result.ToString();// page.ToString();
            return View(model);

        }

        /// <summary>POST: /Core/Footer
        /// Сохранение измененных данных нижнего колонтитула
        /// </summary>
        /// <param name="model">FooterModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Footer(FooterModel model)
        {
            if (ModelState.IsValid)
            {
                EditePageRepository.CreatePage("/Views/Shared/_footer.cshtml", model.Content);
                return RedirectToAction("ControlPanel", "Core");
            }
            model.Content = EditePageRepository.WritePage("/Views/Shared/_footer.cshtml").ToString();
            return View(model);

        }
        [RoleAdminAndEditor]
        public ActionResult GoogleAnalytics()
        {
            FooterModel model = new FooterModel();
            //string[] page = EditePageRepository.WritePage("/Views/Shared/_footer.cshtml");
            string[] page = EditePageRepository.WritePage("/Views/Shared/_googleAnalytics.cshtml");
            StringBuilder result = new StringBuilder();
            foreach (var x in page)
            { result.AppendLine(x); }
            model.Content = result.ToString();// page.ToString();
            return View(model);

        }

        /// <summary>POST: /Core/Footer
        /// Сохранение измененных данных нижнего колонтитула
        /// </summary>
        /// <param name="model">FooterModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult GoogleAnalytics(FooterModel model)
        {
            if (ModelState.IsValid)
            {
                EditePageRepository.CreatePage("/Views/Shared/_googleAnalytics.cshtml", model.Content);
                return RedirectToAction("ControlPanel", "Core");
            }
            model.Content = EditePageRepository.WritePage("/Views/Shared/_googleAnalytics.cshtml").ToString();
            return View(model);

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
            if (CoreSetting.Development())
                model.pageTitle = CoreLanguage.CreateBase;
            else
                model.pageTitle = CoreLanguage.database_connection;
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
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var connection = _doc.Root.Elements("connection").Elements("add");
                connection.FirstOrDefault(x => x.Attribute("name").Value == "IntegratedSecurity")
                    .SetAttributeValue("value", model.IntegratedSecurity.ToString());
                connection.FirstOrDefault(x => x.Attribute("name").Value == "DataBase")
                    .SetAttributeValue("value", Crypto.Encrypt(model.DataBase));
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                    .SetAttributeValue("value", Crypto.Encrypt(model.Password));
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Server")
                    .SetAttributeValue("value", Crypto.Encrypt(model.Server));
                connection.FirstOrDefault(x => x.Attribute("name").Value == "User")
                    .SetAttributeValue("value", Crypto.Encrypt(model.User));
                connection.FirstOrDefault(x => x.Attribute("name").Value == "Provider")
                    .SetAttributeValue("value", model.Provider);
                _doc.Save(_absolutDirectory);
                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_server");
                GeneralMethods.MytripCacheRemove("mtm_cs_database");
                GeneralMethods.MytripCacheRemove("mtm_cs_user");
                GeneralMethods.MytripCacheRemove("mtm_cs_password");
                GeneralMethods.MytripCacheRemove("mtm_cs_integratedsecurity");
                GeneralMethods.MytripCacheRemove("mtm_cs_provider");
                #endregion
                if(CoreSetting.Development())
                    return RedirectToAction("CreateBase2", new { id = model.Provider,id2=0 });
                else
                    return RedirectToAction("ControlPanel", "Core");
            } if (CoreSetting.Development())
                model.pageTitle = CoreLanguage.CreateBase;
            else
                model.pageTitle = CoreLanguage.database_connection;
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Value", "Value", CoreSetting.Provider());
            return View(model);
        }
        public ActionResult CreateBase2(string id,int id2)
        {
            int count = id2;
            try
            {
                if (id == "MySql")
                    CreateDataBase.CreateDataBaseMYSQL();
                else if (id == "MSSQL")
                    CreateDataBase.CreateDataBaseMSSQL();
                return RedirectToAction("Register", "Account");
            }
            catch {
                count++;
                if(count>5)
                    return RedirectToAction("CreateBase");
                else
                    return RedirectToAction("CreateBase2", new { id,id2=count}); 
            }
        
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
            #region Очистка кеша
            GeneralMethods.MytripCacheRemove("mtm_cacherole");
            #endregion
            return RedirectToAction("Email");
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