/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mytrip.Mvc.Models
{
    #region Модель для страницы /Views/Home/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/Index.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/Index.cshtml
    /// </summary>
    [MetadataType(typeof(HomeModel))]
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

    #region Модель для страницы /Views/Home/About.cshtml
    // **********************************************
    // Модель для страницы /Views/Home/About.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Home/About.cshtml
    /// </summary>
    [MetadataType(typeof(AboutModel))]
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
}
