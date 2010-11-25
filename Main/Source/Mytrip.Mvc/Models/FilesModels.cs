//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Mytrip.Mvc.Models
{
    #region Модель для страницы /Views/File/EditPage.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/EditPage.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Core/EditPage.cshtml
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// Содержание текстового файла
        /// </summary>
        [SkipRequestValidation]
        public HtmlString page { get; set; }

        /// <summary>
        /// Адрес файла
        /// </summary>
        public string directory { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/File/Rename.cshtml
    // **********************************************
    // Модель для страницы /Views/Core/Rename.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/Core/Rename.cshtml
    /// </summary>
    public class RenameModels
    {
        /// <summary>
        /// Новое имя файла или папки
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_null_name")]
        [StringLength(30, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_max_name")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес файла или папки
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Адрес для возврата к списку файлов
        /// </summary>
        public string Back { get; set; }

        /// <summary>
        /// Параметр для определения кем является
        /// редактируемый объект файлом или папкой
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// Параметр для текстового редактора 
        /// определяет в какой экземпляр редактора
        /// вносить файл
        /// </summary>
        public string Param2 { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/File/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/File/Index.cshtml
    // **********************************************

    /// <summary>
    /// Модель для страницы /Views/File/Index.cshtml
    /// </summary>
    public class IndexFilesModels
    {
        /// <summary>
        /// Имя новой папки
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_null_name")]
        [StringLength(30, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_max_name")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес папки из которой зачитываются файлы
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Список папок
        /// </summary>
        public DirectoryInfo[] Folders { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        public FileInfo[] Files { get; set; }

        /// <summary>
        /// Ошибка загрузки файла
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Параметр для текстового редактора 
        /// определяет в какой экземпляр редактора
        /// вносить файл
        /// </summary>
        public string Param { get; set; }
    }

    //****************** E N D **********************
    #endregion
}
