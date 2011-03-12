/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace mtm.Core.Models
{

    #region Модель для страницы /Views/Core/InstallModules.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/InstallModules.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Core/InstallModules.cshtml
    /// </summary>
    public class ModulesModel
    {
        /// <summary>Список модулей для установки
        /// </summary>
        public IDictionary<string, List<string>> install { get; set; }

        /// <summary>Список модулей для удаления
        /// </summary>
        public IDictionary<string, List<string>> uninstall { get; set; }
        public string[] bread { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Core/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/Index.cshtml
    // **********************************************
    public class SeoModel {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameHome_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameHome_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seokeywords_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seodescription_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seokeywords_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seodescription_en_us { get; set; }
        /// <summary>Титл сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seoTitle_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string seoTitle_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameTitle_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameLogo_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameTitle_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameLogo_ru_ru { get; set; }
        public string[] bread { get; set; }
        public string view_ru_ru { get; set; }
        public string view_en_us { get; set; }        
    }
    /// <summary>Модель для страницы /Views/Core/Index.cshtml
    /// </summary>
    public class CoreModel
    {


        /// <summary>Культура по умолчанию
        /// </summary>
        public string defaultCulture { get; set; }

        /// <summary>Выбор культур (true - разрешен)
        /// </summary>
        public bool unlockAllCulture { get; set; }

        /// <summary>Тема по умолчанию
        /// </summary>
        public string defaultTheme { get; set; }

        /// <summary>Список тем сайта
        /// </summary>
        public SelectList AllTheme { get; set; }

        /// <summary>Список культур сайта
        /// </summary>
        public SelectList AllCulture { get; set; }

        /// <summary>Выбор тем (true - разрешен)
        /// </summary>
        public bool unlockTheme { get; set; }

        /// <summary>Gravatar (true - включен)
        /// </summary>
        public bool unlockGravatar { get; set; }

        /// <summary>Название главной страницы
        /// </summary>
        
        /// <summary>Показ панели управления в сайдбаре (true - включен)
        /// </summary>
        public int CountLatestUsers { get; set; }

        public string SideBarPosition { get; set; }
        public SelectList SideBarPositionSelect { get; set; }
        public string MenuPosition { get; set; }
        public SelectList MenuPositionSelect { get; set; }
        public string[] bread { get; set; }
        public string twitterLogin { get; set; }
        public int twitterWidth { get; set; }
        public int twitterCount { get; set; }

    }
    public class CoreStatisticModel
    {
        public bool unlockStatistic { get; set; }
        public bool Anonym { get; set; }
        public bool User { get; set; }
        public string[] bread { get; set; }
    }
    public class CoreGeoModel
    {
        public bool unlockGeo { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Url { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string keyBingMap { get; set; }
        public string[] bread { get; set; }
    }
    public class CoreLogonModel
    {
        /// <summary>Минимальная длина пароля
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int minRequiredPasswordLength { get; set; }

        /// <summary>Максимальное число неудачных попыток входа
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int maxInvalidPasswordAttempts { get; set; }

        /// <summary>Уникальные Email пользователей (true - включены)
        /// </summary>
        public bool requiresUniqueEmail { get; set; }

        /// <summary>Капча (true - включена)
        /// </summary>
        public bool unlockCaptcha { get; set; }

        /// <summary>Роль администратора сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleAdmin { get; set; }

        /// <summary>Роль главного редактора сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_null")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "rolename_max_length")]
        public string roleChiefEditor { get; set; }

        /// <summary>Регистрация (true - разрешена)
        /// </summary>
        public bool unlockRegistration { get; set; }

        /// <summary>Показ кнопки вход на сайте (true - включен)
        /// </summary>
        public bool unlockVisibleLogon { get; set; }

        /// <summary>Подтверждение пользователя по Email (true - включено)
        /// </summary>
        public bool unlockApprovedEmail { get; set; }

        /// <summary>Вход OpenId (true - разрешен)
        /// </summary>
        public bool unlockOpenId { get; set; }

        /// <summary>Вход через провайдера сайта (true - разрешен)
        /// </summary>
        public bool unlockSiteId { get; set; }
        public bool EmailasUsername { get; set; }
        public bool AutoGenerateUserName { get; set; }
        public string[] bread { get; set; }

    }
    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Core/Email.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/Email.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Core/Email.cshtml
    /// </summary>
    public class EmailModel
    {
        /// <summary>
        /// Email адрес для отправки с сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string FromEmail { get; set; }

        /// <summary>
        /// Smtp Email адреса для отправки с сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Smtp { get; set; }

        /// <summary>
        /// Port Email адреса для отправки с сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int Port { get; set; }

        /// <summary>
        /// Ssl Email адреса для отправки с сайта (true - включен)
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// Имя пользователя Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        public string LoginEmail { get; set; }
        /// <summary>
        /// Пароль пользователя Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        public string PasswordEmail { get; set; }
        public string[] bread { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Core/CreateBase.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/CreateBase.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Core/CreateBase.cshtml
    /// </summary>
    public class CreateBaseModel
    {
        /// <summary>
        ///  Интегрированная система аутентификации SQL (true - включена)
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        /// <summary>
        ///  Адрес сервера SQL
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_server_null")]
        public string Server { get; set; }

        /// <summary>
        /// Провайдер SQL
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_database_null")]
        public string DataBase { get; set; }

        /// <summary>
        /// Имя пользователя SQL сервера
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Пароль SQL сервера
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Список провайдеров SQL
        /// </summary>
        public SelectList AllProvider { get; set; }

        public string pageTitle { get; set; }
        public string[] bread { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Core/HomePage.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/HomePage.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Core/HomePage.cshtml
    /// </summary>
    public class HomePageModel
    {
        /// <summary>
        /// Текущая тема
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Добавляемые модули
        /// </summary>
        public IQueryable<HomePageItem> NewHomeItems { get; set; }

        /// <summary>
        /// Существующие модули
        /// </summary>
        public IQueryable<HomePageItem> HomeItems { get; set; }
        public string[] bread { get; set; }
    }

    /// <summary>
    /// Экземпляр модуля HomePageItem
    /// </summary>
    public class HomePageItem
    {
        /// <summary>
        /// Пространство имен
        /// </summary>
        private string assembly;

        /// <summary>
        /// Имя модуля
        /// </summary>
        private string name;

        /// <summary>
        /// Культура модуля
        /// </summary>
        private string culture;

        /// <summary>
        /// Индентификатор модуля
        /// </summary>
        private int id;

        /// <summary>
        /// Индекс модуля
        /// </summary>
        private int index;

        /// <summary>
        /// Количество строк в модуле
        /// </summary>
        private int rows;

        /// <summary>
        /// Количество столбцов в модуле
        /// </summary>
        private int columns;

        /// <summary>
        /// Длина контента в модуле
        /// </summary>
        private int content;

        /// <summary>
        /// Ширина картинки в модуле
        /// </summary>
        private int image;

        /// <summary>
        /// Показ титла модуля
        /// </summary>
        private bool showtitle;

        /// <summary>
        /// Номер стиля модуля
        /// </summary>
        private int style;

        /// <summary>
        /// Экземпляр модуля
        /// </summary>
        public HomePageItem() { }

        /// <summary>
        /// Заполнение нового экземпляра модуля
        /// </summary>
        /// <param name="index">Индекс модуля</param>
        /// <param name="name">Имя модуля</param>
        /// <param name="assembly">Пространство имен</param>
        /// <param name="other">Культура модуля</param>
        public HomePageItem(int index, string name, string assembly, string other)
        {
            Assembly = assembly; Name = name; Culture = other; Id = 0; Index = index;
            Rows = 0; Columns = 0; Content = 0; Image = 0; Style = 0; Showtitle = false;
        }
        /// <summary>
        /// Заполнение существующего экземпляра модуля
        /// </summary>
        /// <param name="assembly">Пространство имен</param>
        /// <param name="name">Имя модуля</param>
        /// <param name="culture">Культура модуля</param>
        /// <param name="id">Индентификатор модуля</param>
        /// <param name="index">Индекс модуля</param>
        /// <param name="rows">Количество строк в модуле</param>
        /// <param name="columns">Количество столбцов в модуле</param>
        /// <param name="content">Длина контента в модуле</param>
        /// <param name="image">Ширина картинки в модуле</param>
        /// <param name="style">Номер стиля модуля</param>
        /// <param name="showtitle">Показ титла модуля</param>
        public HomePageItem(string assembly, string name, string culture, int id, int index, int rows, int columns, int content, int image, int style, bool showtitle)
        {
            Assembly = assembly; Name = name; Culture = culture; Id = id; Index = index;
            Rows = rows; Columns = columns; Content = content; Image = image;
            Style = style; Showtitle = showtitle;
        }

        /// <summary>
        /// Пространство имен
        /// </summary>
        public string Assembly { get { return assembly; } set { assembly = value; } }

        /// <summary>
        /// Имя модуля
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Культура модуля
        /// </summary>
        public string Culture { get { return culture; } set { culture = value; } }

        /// <summary>
        /// Индентификатор модуля
        /// </summary>
        public int Id { get { return id; } set { id = value; } }

        /// <summary>
        /// Индекс модуля
        /// </summary>
        public int Index { get { return index; } set { index = value; } }

        /// <summary>
        /// Количество строк в модуле
        /// </summary>
        public int Rows { get { return rows; } set { rows = value; } }

        /// <summary>
        /// Количество столбцов в модуле
        /// </summary>
        public int Columns { get { return columns; } set { columns = value; } }

        /// <summary>
        /// Длина контента в модуле
        /// </summary>
        public int Content { get { return content; } set { content = value; } }

        /// <summary>
        /// Ширина картинки в модуле
        /// </summary>
        public int Image { get { return image; } set { image = value; } }

        /// <summary>
        /// Номер стиля модуля
        /// </summary>
        public int Style { get { return style; } set { style = value; } }

        /// <summary>
        /// Показ титла модуля
        /// </summary>
        public bool Showtitle { get { return showtitle; } set { showtitle = value; } }
    }

    //****************** E N D **********************
    #endregion

    public class EditorPageModel
    {
        public string pagetitle { get; set; }
        public string title { get; set; }
        public string seotitle { get; set; }
        public string path { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        [AllowHtml]
        public string body { get; set; }
        public bool sideBar { get; set; }
        public bool emailForm { get; set; }
        public bool allCulture { get; set; }
        public bool addMenu { get; set; }
        public bool addHomePage { get; set; }
        public bool viewOnlyHomePage { get; set; }
        public string submit { get; set; }
        public string[] bread { get; set; }
        public int id { get; set; }
        public string id2 { get; set; }
    }
    public class RewriteModel
    {
        public HtmlString AllRewrite { get; set; }
        public string message { get; set; }
        public string[] bread { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ControlPanelModel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool viewAdmin { get; set; }
    }
    #region Модель для страницы /Views/Core/Footer.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/Footer.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Home/Footer.cshtml
    /// </summary>
    public class FooterModel
    {
        /// <summary>Редактируемый Footer
        /// </summary>
        [AllowHtml]
        public string Content { get; set; }
        public string[] bread { get; set; }
    }

    //****************** E N D **********************
    #endregion
}
