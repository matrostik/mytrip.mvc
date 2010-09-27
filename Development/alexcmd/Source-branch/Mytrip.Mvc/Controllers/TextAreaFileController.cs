/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web.Mvc;
using System.Web;
using System.IO;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>Контроллер по работе с файловой системой сайта из текстового редактора
    /// </summary>
    [Authorize]
    public class TextAreaFileController : ICoreController
    {

        /// <summary>GET: /File/Index
        /// Страница отображения файлов и папок в 
        /// запрашиваемой дирректории
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string id, string id2)
        {
            IndexFilesModels model = new IndexFilesModels();
            if (!String.IsNullOrEmpty(id) && id.Contains("()file_null"))
            {
                id = id.Replace("()file_null", "");
                ModelState.AddModelError("Error", CoreLanguage.file_null_file);
            }
            if (!String.IsNullOrEmpty(id) && id.Contains("()file_dublicate"))
            {
                id = id.Replace("()file_dublicate", "");
                ModelState.AddModelError("Error", CoreLanguage.file_dublicate_file);
            }
            if (User.IsInRole(UsersSetting.roleChiefEditor()) && !id.Contains("()Content()Articles"))
                id = "()Content()Articles";
            else if (!User.IsInRole(UsersSetting.roleChiefEditor()) && !id.Contains("()Content()Articles()" + User.Identity.Name))
                id = "()Content()Articles()" + User.Identity.Name;
            model.Folders = coreRepo.fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);
            model.Directory = id;
            model.Param = id2;
            return View(model);
        }

        /// <summary>POST: /File/Index
        /// Создание папки в текущей дирректории
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(IndexFilesModels model, string id, string id2)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, id))
            {
                string name = id + "()" + model.Name;
                coreRepo.fileRepo.CreateFolder(name);
                return RedirectToAction("Index", new { id = id, id2 = id2 });
            }
            model.Folders = coreRepo.fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);
            return View(model);
        }

        /// <summary>GET: /File/Upload
        /// Загрузка файла в текущую дирректорию
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <param name="id3">загружаемый файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult Upload(string id, string id2, HttpPostedFileBase id3)
        {
            if (id3 != null)
            {
                bool result = true;
                string name = Path.GetFileName(id3.FileName);
                var files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);
                foreach (System.IO.FileInfo x in files)
                {
                    if (name == x.Name)
                        result = false;
                }
                if (result)
                {
                    coreRepo.fileRepo.UploadFile(id, id3);
                }
                else
                {
                    id = id + "()file_dublicate";
                    RedirectToAction("Index", new { id = id, id2 = id2 });
                }
            }
            else
            {
                id = id + "()file_null";
                RedirectToAction("Index", new { id = id, id2 = id2 });
            }
            return RedirectToAction("Index", new { id = id, id2 = id2 });

        }

        /// <summary>GET: /File/Delete
        /// Удаление файла или папки
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <param name="id3">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string id, string id2, string id3)
        {
            string _directory = id.Remove(id.LastIndexOf("()"));
            if (id3 == "file")
                coreRepo.fileRepo.DeleteFile(id);
            else
                coreRepo.fileRepo.DeleteFolder(id);
            id = _directory;
            return RedirectToAction("Index", new { id = id, id2 = id2 });
        }

        /// <summary>GET: /File/Rename
        /// Страница по переименованию файла или папки
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <param name="id3">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult Rename(string id, string id2, string id3)
        {
            RenameModels model = new RenameModels();
            model.Directory = id;
            model.Name = id.Remove(0, id.LastIndexOf("()") + 2);
            model.Back = id.Remove(id.LastIndexOf("()"));
            model.Param2 = id3;
            model.Param = id2;
            return View(model);
        }

        /// <summary>POST: /File/Rename
        /// Сохранение файла или папки с новым именем
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">индентификатор текстового редактора</param>
        /// <param name="id3">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Rename(RenameModels model, string id, string id2, string id3)
        {
            model.Back = id.Remove(id.LastIndexOf("()"));
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                if (id3 == "folder")
                    coreRepo.fileRepo.RenameFolder(model.Name, id);
                else if (id3 == "file")
                    coreRepo.fileRepo.RenameFile(model.Name, id);

                return RedirectToAction("Index", new { id = model.Back, id2 = id2 });
            }
            model.Directory = id;
            model.Param2 = id3;
            model.Param = id2;
            return View(model);
        }

        /// <summary>Проверка имени паки или файла на уникальность
        /// </summary>
        /// <param name="id">имя файла или папки</param>
        /// <param name="id2">файловая дирректория</param>
        /// <returns>возвращает bool</returns>
        private bool ValidateRename(string id, string id2)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(id))
            {
                result = true;

                var folder = coreRepo.fileRepo.GetAllFoldersFromDirectory(id2);
                foreach (System.IO.DirectoryInfo x in folder)
                {
                    if (id == x.Name)
                        result = false;
                }
                var file = coreRepo.fileRepo.GetAllFilesFromDirectory(id2);
                foreach (System.IO.FileInfo x in file)
                {
                    if (id == x.Name)
                        result = false;
                }

            }
            if (!result)
                ModelState.AddModelError("Name", CoreLanguage.file_dublicate_name);
            return ModelState.IsValid;
        }
    }
}

