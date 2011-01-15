/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;
using System.Web;
using mtm.Core.Repository.DataEntities;
using System.Linq;

namespace mtm.Core.Models
{
    #region Модель для страницы /Views/Home/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/Index.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/Index.cshtml
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// Титл страницы
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Включение/отключение режима показа лицензионного соглашения
        /// </summary>
        public bool developer { get; set; }
    }

    //****************** E N D **********************
    #endregion
    #region Модель для страницы /Views/Home/Search.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/Search.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/Index.cshtml
    /// </summary>
    public class HomeSearchModel
    {
        /// <summary>
        /// Контент страницы
        /// </summary>
        public int page { get; set; }
        public int count { get; set; }
        public string search { get; set; }
        public bool _total { get; set; }
        public int defsize { get; set; }
        public int total { get; set; }
        public string title { get; set; }
    }

    //****************** E N D **********************
    #endregion
    #region Модель для страницы /Views/Home/About.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/About.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/About.cshtml
    /// </summary>
    public class AboutModel
    {
        /// <summary>
        /// Данные из файла About.xml
        /// </summary>
        public HtmlString body { get; set; }

        /// <summary>
        /// Имя пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        public string name { get; set; }

        /// <summary>
        /// Настройка отправки Email с сайта (true - настроена)
        /// </summary>
        public bool approvedemail { get; set; }

        /// <summary>
        /// Сообщение пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "message_null")]
        public string messege { get; set; }

        /// <summary>
        /// Email пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string email { get; set; }

        /// <summary>
        /// Титл страницы
        /// </summary>
        public string title { get; set; }

    }

    //****************** E N D **********************
    #endregion
    #region Модель для страницы /Views/Home/Page.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/Page.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/Page.cshtml
    /// </summary>
    public class CorePageModel
    {
        public string title{get;set;}
        public HtmlString body{get;set;}
        public bool sideBar{get;set;}
        public int id { get; set; }
        public mytrip_corepages pages { get; set; }
        /// <summary>
        /// Имя пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        public string name { get; set; }

        /// <summary>
        /// Настройка отправки Email с сайта (true - настроена)
        /// </summary>
        public bool approvedemail { get; set; }

        /// <summary>
        /// Сообщение пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "message_null")]
        public string messege { get; set; }

        /// <summary>
        /// Email пользователя для отправки Email в администрацию сайта
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string email { get; set; }

    }

    //****************** E N D **********************
    #endregion
    #region Модель для страницы /Views/Home/Statistic.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/Statistic.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/Statistic.cshtml
    /// </summary>
    public class StatisticModel
    {
        public string title { get; set; }
        public IQueryable<mytrip_corestatistic> body { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string bodyonload { get; set; }

    }

    //****************** E N D **********************
    #endregion
   
}
