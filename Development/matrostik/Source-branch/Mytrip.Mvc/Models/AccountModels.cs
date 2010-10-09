/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;

namespace Mytrip.Mvc.Models
{
    #region Модель для страницы /Views/Account/ChangePassword.cshtml
    // **********************************************
    // Модель для страницы /Views/Account/ChangePassword.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Account/ChangePassword.cshtml
    /// </summary>
    [MetadataType(typeof(ChangePasswordModel))]
    public class ChangePasswordModel
    {
        /// <summary>
        /// Старый пароль (текущий)
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [OldPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "logon_error_password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Повтор нового пароля
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int minRequiredPasswordLength { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Account/ChangeEmail.cshtml
    /// <summary>
    /// Модель для страницы /Views/Account/ChangeEmail.cshtml
    /// </summary>
    public class ChangeEmailModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [EmailRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Account/LogOn.cshtml
    // **********************************************
    // Модель для страницы /Views/Account/LogOn.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Account/LogOn.cshtml
    /// </summary>
    [MetadataType(typeof(LogOnModel))]
    public class LogOnModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_username")]
        [UserNameNullLogon(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_username")]
        [UserNameLogon(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_block_username")]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить меня?
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Регистрация на сайте (true - разрешена)
        /// </summary>
        public bool unlockRegistration { get; set; }

        /// <summary>
        /// Вход через OpenId (true - разрешен)
        /// </summary>
        public bool unlockOpenId { get; set; }

        /// <summary>
        /// Вход через провайдера сайта (true - разрешен)
        /// </summary>
        public bool unlockSiteId { get; set; }

        /// <summary>
        /// Ошибка входа через OpenId
        /// </summary>
        public string errorOpenId { get; set; }

        /// <summary>
        /// Url для переадресации пользователя после входа
        /// </summary>
        public string returnUrl { get; set; }

        /// <summary>
        /// Ширина таблицы в зависимости от разрешенных
        /// провайдеров
        /// </summary>
        public string tableWidth { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Account/Register.cshtml
    // **********************************************
    // Модель для страницы /Views/Account/Register.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Account/Register.cshtml
    /// </summary>
    [MetadataType(typeof(RegisterModel))]
    public class RegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_username")]
        [UserNameRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_username")]
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [EmailRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [MinPassword(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Повтор пароля
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_password")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Капча
        /// </summary>
        [CaptchaNullString(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_captcha")]
        [CaptchaError(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_error_captcha")]
        public string Captcha { get; set; }

        /// <summary>
        /// Проверка через капчу (true - включена)
        /// </summary>
        public bool unlockCaptcha { get; set; }

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int minRequiredPasswordLength { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Account/OpenIdRegister.cshtml
    // **********************************************
    // Модель для страницы /Views/Account/OpenIdRegister.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Account/OpenIdRegister.cshtml
    /// </summary>
    [MetadataType(typeof(OpenIdRegisterModel))]
    public class OpenIdRegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_username")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_username")]
        [UserNameRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_username")]
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [EmailRegister(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_duplicate_email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Показ поля ввода Email
        /// </summary>
        public string display { get; set; }

    }

    //****************** E N D **********************
    #endregion
}
