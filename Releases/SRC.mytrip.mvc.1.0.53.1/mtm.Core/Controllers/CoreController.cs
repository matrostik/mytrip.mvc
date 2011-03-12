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
            model.twitterCount = ProfileSetting.twitterCount();
            model.twitterLogin = ProfileSetting.twitterLogin();
            model.twitterWidth = ProfileSetting.twitterWidth();
            model.CountLatestUsers = CoreSetting.CountLatestUsers();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Core_Settings };
            model.bread = a;
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
                var profile = _doc.Root.Elements("profile").Elements("add");
                profile.FirstOrDefault(x => x.Attribute("name").Value == "unlockGravatar")
                    .SetAttributeValue("value", model.unlockGravatar.ToString());
                var twitter = _doc.Root.Elements("twitter").Elements("add");
                twitter.FirstOrDefault(x => x.Attribute("name").Value == "login")
                    .SetAttributeValue("value", model.twitterLogin.ToString());
                twitter.FirstOrDefault(x => x.Attribute("name").Value == "width")
                    .SetAttributeValue("value", model.twitterWidth.ToString());
                twitter.FirstOrDefault(x => x.Attribute("name").Value == "count")
                    .SetAttributeValue("value", model.twitterCount.ToString());
                _doc.Save(_absolutDirectory);
                #endregion
                #region Очистка кеша
                    GeneralMethods.MytripCacheRemove("mtm_cs_countlatestusers");
                    GeneralMethods.MytripCacheRemove("ls_defaultculture");
                    GeneralMethods.MytripCacheRemove("ls_unlockallculture");
                    GeneralMethods.MytripCacheRemove("mtm_ts_defaulttheme");
                    GeneralMethods.MytripCacheRemove("mtm_ts_unlocktheme");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockgravatar");
                    GeneralMethods.MytripCacheRemove("mtm_us_unlockvisibleprofiletosidebar");
                    GeneralMethods.MytripCacheRemove("mtm_cs_sidebarposition");
                    GeneralMethods.MytripCacheRemove("mtm_cs_menuposition");
                    #endregion
                
                LayoutHelper.PositionSideBar(model.SideBarPosition);
                LayoutHelper.PositionMenu(model.MenuPosition);
                RedirectToRouteResult a = RedirectToAction("ControlPanel");
                return a;
            }
            model.SideBarPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.SideBarPosition());
            model.MenuPositionSelect = new SelectList(LayoutHelper.PositionHtmlElements(), "Key", "Value", ProfileSetting.MenuPosition());
            model.AllCulture = new SelectList(LocalisationSetting.allCultureDictionary(), "Value", "Value", LocalisationSetting.defaultCulture());
            model.AllTheme = new SelectList(ThemeSetting.allThemeDictionary(), "Value", "Value", ThemeSetting.defaultTheme());
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Core_Settings };
            model.bread = _a;
            return View(model);
        }
        /// <summary>GET: /Core/Index
        /// Основные настройки сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdminAndEditor]
        public ActionResult Seo()
        {
            SeoModel model = new SeoModel();
            model.nameHome_ru_ru = CoreSetting.NameHomePage("ru-ru");
            model.nameHome_en_us = CoreSetting.NameHomePage("en-us");
            model.seodescription_en_us = CoreSetting.HomePageDesc("en-us");
            model.seodescription_ru_ru = CoreSetting.HomePageDesc("ru-ru");
            model.seokeywords_en_us = CoreSetting.HomePageKeyWords("en-us");
            model.seokeywords_ru_ru = CoreSetting.HomePageKeyWords("ru-ru");
            model.nameTitle_ru_ru = CoreSetting.NameTitlePage("ru-ru");
            model.nameTitle_en_us = CoreSetting.NameTitlePage("en-us");
            model.seoTitle_ru_ru = CoreSetting.HomePageTitle("ru-ru");
            model.seoTitle_en_us = CoreSetting.HomePageTitle("en-us");
            model.nameLogo_ru_ru = CoreSetting.nameLogo("ru-ru");
            model.nameLogo_en_us = CoreSetting.nameLogo("en-us");
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.seo_setting };
            model.bread = a;
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            return View(model);
        }

        /// <summary>POST: /Core/Index
        /// Обработка данных основных настроек сайта
        /// </summary>
        /// <param name="model">CoreModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Seo(SeoModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var pagename = _doc.Root.Elements("core").Elements("add");
                var content = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameHome").Elements("add");
                content.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameHome_ru_ru);
                content.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.seoTitle_ru_ru);
                content.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.seokeywords_ru_ru);
                content.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.seodescription_ru_ru);
                content.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameHome_en_us);
                content.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.seoTitle_en_us);
                content.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.seokeywords_en_us);
                content.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.seodescription_en_us);
                var titlepage = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameTitle").Elements("add");
                titlepage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameTitle_ru_ru);
                titlepage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameTitle_en_us);
                var namelogo = pagename.FirstOrDefault(x => x.Attribute("name").Value == "nameLogo").Elements("add");
                namelogo.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameLogo_ru_ru);
                namelogo.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameLogo_en_us);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("mtm_cs_namehomepage", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_nametitlepage", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_namelogo", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_namehomepage", "en-us");
                GeneralMethods.MytripCacheRemove("mtm_cs_nametitlepage", "en-us");
                GeneralMethods.MytripCacheRemove("mtm_cs_namelogo", "en-us");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagetitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagetitle", "en-us");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagedescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagedescription", "en-us");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagekeywords", "ru-ru");
                GeneralMethods.MytripCacheRemove("mtm_cs_homepagekeywords", "en-us");

                #endregion

                string _id;
                if (HttpContext.Request.Cookies["mtProductComparison"] == null)
                    _id = string.Format("[{0}]", "id");
                else
                {
                    if (HttpContext.Request.Cookies["mtProductComparison"].Value.Contains(string.Format("[{0}]", "id")))
                        _id = HttpContext.Request.Cookies["mtProductComparison"].Value.Replace(string.Format("[{0}]", "id"), "");
                    else
                        _id = string.Format("{0}[{1}]", HttpContext.Request.Cookies["mtProductComparison"].Value, "id");
                }





                RedirectToRouteResult a = RedirectToAction("ControlPanel");
                return a;
            }
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.seo_setting };
            model.bread = _a;
            return View(model);
        }
        /// <summary>GET: /Core/Logon
        /// Настройка входа и регистрации пользователей
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
            model.AutoGenerateUserName = UsersSetting.AutoGenerateUserName();
            model.EmailasUsername = UsersSetting.EmailasUsername();
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Membership_Settings };
            model.bread = _a;
            return View(model);
        }

        /// <summary>POST: /Core/Logon
        /// Обработка настроек входа и регистрации пользователей
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
                membership.FirstOrDefault(x => x.Attribute("name").Value == "AutoGenerateUserName")
                    .SetAttributeValue("value", model.AutoGenerateUserName.ToString());
                membership.FirstOrDefault(x => x.Attribute("name").Value == "EmailasUsername")
                    .SetAttributeValue("value", model.EmailasUsername.ToString());
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
                GeneralMethods.MytripCacheRemove("mtm_us_autogenerateusername");
                GeneralMethods.MytripCacheRemove("mtm_us_emailasusername");
                #endregion
                return RedirectToAction("ControlPanel");
            }
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Membership_Settings };
            model.bread = _a;
            return View(model);
        }
        /// <summary>GET: /Core/Statistic
        /// Настройка статистики
        /// </summary>
        /// <returns>ActionResult</returns>
        [RoleAdmin]
        public ActionResult Statistic()
        {
            CoreStatisticModel model = new CoreStatisticModel();
            model.unlockStatistic = StatisticSetting.unlockStatistic();
            model.Anonym = StatisticSetting.statisticAnonym();
            model.User = StatisticSetting.statisticUser();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.statisticSetting };
            model.bread = a;
            return View(model);
        }

        /// <summary>POST: /Core/Statistic
        /// Обработка настройки статистики
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.statisticSetting };
            model.bread = a;
            return View(model);
        }
        /// <summary>GET: /Core/Geo
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.geoSetting };
            model.bread = a;
            return View(model);
        }

        /// <summary>POST: /Core/Geo
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.geoSetting };
            model.bread = a;
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
            coreRepo.installModuleRepo.CreateSiteMap(LocalisationSetting.culture());
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.InstallModules };
            model.bread = a;
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Email_setting };
            model.bread = a;
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.Email_setting };
            model.bread = a;
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
            string[] page = EditePageRepository.WritePage("/Views/Home/Partial/_homePage.cshtml");
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
                    else if (assembly == "mytrip.mvc")
                        newItems.Add(new HomePageItem(assembly, name, cult, id, 0, 0, 0, 1000, 0, 0, false));
                    else
                        newItems.Add(new HomePageItem(assembly, name, cult, id, 0, 1, 1, 100, 100, 2, true));

                }
            }
            HomePageModel model = new HomePageModel();
            model.Theme = ThemeSetting.theme();
            model.HomeItems = exists.AsQueryable().OrderBy(x => x.Index);
            model.NewHomeItems = newItems.AsQueryable().OrderBy(x => x.Name);
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.homepage_setting };
            model.bread = _a;
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
                string path = "/Views/Home/Partial/_homePage.cshtml";
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
                    if (assembly == "mytrip.mvc")
                        newLine += "Html.HomePageAnonce(" + id + "," + content + ")";
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
            string[] page = EditePageRepository.WritePage("/Views/Shared/Partial/_sideBar.cshtml");
            int index = 1;
            foreach (string line in page)
            {
                if (line.Contains("Html."))
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
                string ns = "Html.mtmCore";
                if (assembly.Contains("Export"))
                    ns = assembly.Remove(assembly.IndexOf("Export"));
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
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.sidebar_setting };
            model.bread = _a;
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
                string path = "/Views/Shared/Partial/_sideBar.cshtml";
                string[] page = EditePageRepository.WritePage(path);
                StringBuilder result = new StringBuilder();
                foreach (string x in page)
                {
                    result.AppendLine(x);
                    if (x.Contains("}"))
                        break;
                }
                bool sidebar = (ids == null || ids.Trim().Length == 0) ? false : true;
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
            string[] page = EditePageRepository.WritePage("/Views/Shared/Partial/_menu.cshtml");
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
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.topmenu_setting };
            model.bread = _a;
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
                string path = "/Views/Shared/Partial/_menu.cshtml";
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
            string[] page = EditePageRepository.WritePage("/Views/Shared/Partial/_announce.cshtml");
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
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.announce };
            model.bread = _a;
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Announce(string ids)
        {
            try
            {
                string path = "/Views/Shared/Partial/_announce.cshtml";
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
            string path = "/Views/Shared/Partial/_menu.cshtml";
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
            model.id = id;
            model.id2 = id2;
            if (id2 == "CreatePage")
            {

                model.pagetitle = id == 0 ? CoreLanguage.CreatePage : CoreLanguage.CreateSubPage;
                model.submit = CoreLanguage.create;
                model.seodescription = CoreSetting.HomePageDesc();
                model.seokeywords = CoreSetting.HomePageKeyWords();
                if (id == 0)
                {
                    string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.CreatePage };
                    model.bread = a;
                }
                else
                {
                    var x = coreRepo.corePageRepo.GetPages(id);
                    string[] a = { "<a href='/Home/Page/" + id + "/" + x.Path + "'>" + x.Title + "</a>", CoreLanguage.CreateSubPage };
                    model.bread = a;
                }
            }
            else if (id2 == "EditPage")
            {
                var x = coreRepo.corePageRepo.GetPages(id);
                model.pagetitle = string.Format(CoreLanguage.EditPage, x.Title);
                model.submit = CoreLanguage.edit;
                model.addHomePage = x.AddHomePage;
                model.seotitle = x.SeoTitle;
                model.addMenu = x.AddMenu;
                model.allCulture = x.AllCulture;
                model.body = x.Body;
                model.emailForm = x.EmailForm;
                model.sideBar = x.SideBar;
                model.title = x.Title;
                model.viewOnlyHomePage = x.ViewOnlyHomePage;
                model.seodescription = x.SeoDescription;
                model.seokeywords = x.SeoKeyword;
                model.path = x.Path;
                if (x.SubPagesId == 0)
                {
                    string[] a = { "<a href='/Home/Page/" + id + "/" + x.Path + "'>" + x.Title + "</a>", model.pagetitle };
                    model.bread = a;
                }
                else
                {
                    string[] a = { "<a href='/Home/Page/" + x.SubPagesId + "/" + x.mytrip_corepages2.Path + "'>" + x.mytrip_corepages2.Title + "</a>",
                                     "<a href='/Home/Page/" + id + "/" + x.Path + "'>" + x.Title + "</a>",
                                     model.pagetitle };
                    model.bread = a;
                }
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
        public ActionResult EditorPage(EditorPageModel model)
        {
            if (ModelState.IsValid && model.id2 == "CreatePage")
            {
                var x = coreRepo.corePageRepo.CreatePage(model);
                return RedirectToAction("Page", "Home", new { id = x.PageId, id2 = x.Path });
            }
            else if (ModelState.IsValid && model.id2 == "EditPage")
            {
                var x = coreRepo.corePageRepo.EditPage(model);
                if (!x.ViewOnlyHomePage)
                    return RedirectToAction("Page", "Home", new { id = x.PageId, id2 = x.Path });
                else
                    return RedirectToAction("Index", "Home");
            }
            if (model.id2 == "CreatePage")
            {
                if (model.id == 0)
                {
                    string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.CreatePage };
                    model.bread = a;
                }
                else
                {
                    var x = coreRepo.corePageRepo.GetPages(model.id);
                    string[] a = { "<a href='/Home/Page/" + model.id + "/" + x.Path + "'>" + x.Title + "</a>", CoreLanguage.CreateSubPage };
                    model.bread = a;
                }
            }
            else if (model.id2 == "EditPage")
            {
                var x = coreRepo.corePageRepo.GetPages(model.id);
                if (x.SubPagesId == 0)
                {
                    string[] a = { "<a href='/Home/Page/" + model.id + "/" + x.Path + "'>" + x.Title + "</a>", model.pagetitle };
                    model.bread = a;
                }
                else
                {
                    string[] a = { "<a href='/Home/Page/" + x.SubPagesId + "/" + x.mytrip_corepages2.Path + "'>" + x.mytrip_corepages2.Title + "</a>",
                                     "<a href='/Home/Page/" + model.id + "/" + x.Path + "'>" + x.Title + "</a>",
                                     model.pagetitle };
                    model.bread = a;
                }
            }
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult EditorRewrite()
        {
            RewriteModel model = new RewriteModel();
            model.AllRewrite = RewriteHelper.GetAllRewrite();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.rewritetitle };
            model.bread = a;
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.rewritetitle };
            model.bread = a;
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
            if (restart)
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
            string[] page = EditePageRepository.WritePage("/Views/Shared/Partial/_footer.cshtml");
            StringBuilder result = new StringBuilder();
            foreach (var x in page)
            { result.AppendLine(x); }
            model.Content = result.ToString();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.edit_footer };
            model.bread = a;
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
                EditePageRepository.CreatePage("/Views/Shared/Partial/_footer.cshtml", model.Content);
                return RedirectToAction("ControlPanel", "Core");
            }
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.edit_footer };
            model.bread = a;
            model.Content = EditePageRepository.WritePage("/Views/Shared/Partial/_footer.cshtml").ToString();
            return View(model);

        }
        [RoleAdminAndEditor]
        public ActionResult GoogleAnalytics()
        {
            FooterModel model = new FooterModel();
            string[] page = EditePageRepository.WritePage("/Views/Shared/Partial/_googleAnalytics.cshtml");
            StringBuilder result = new StringBuilder();
            foreach (var x in page)
            { result.AppendLine(x); }
            model.Content = result.ToString();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.googleAnalytics };
            model.bread = a;
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
                EditePageRepository.CreatePage("/Views/Shared/Partial/_googleAnalytics.cshtml", model.Content);
                return RedirectToAction("ControlPanel", "Core");
            }
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.googleAnalytics };
            model.bread = a;
            model.Content = EditePageRepository.WritePage("/Views/Shared/Partial/_googleAnalytics.cshtml").ToString();
            return View(model);

        }
        //****************** E N D **********************
        #endregion

        #region Первое включение и настройка базы
        // **********************************************
        // Первое включение и настройка базы
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
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.database_connection };
            model.bread = a;
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
                if (CoreSetting.Development())
                    return RedirectToAction("CreateBase2", new { id = model.Provider, id2 = 0 });
                else
                    return RedirectToAction("ControlPanel", "Core");
            } if (CoreSetting.Development())
                model.pageTitle = CoreLanguage.CreateBase;
            else
                model.pageTitle = CoreLanguage.database_connection;
            model.AllProvider = new SelectList(CoreSetting.allProviderDictionary(), "Value", "Value", CoreSetting.Provider());
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", CoreLanguage.database_connection };
            model.bread = a;
            return View(model);
        }
        public ActionResult CreateBase2(string id, int id2)
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
            catch
            {
                count++;
                if (count > 5)
                    return RedirectToAction("CreateBase");
                else
                    return RedirectToAction("CreateBase2", new { id, id2 = count });
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