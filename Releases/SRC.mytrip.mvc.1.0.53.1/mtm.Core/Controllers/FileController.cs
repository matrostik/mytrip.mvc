/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core.Models;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Core.Controllers
{
    /// <summary>Контроллер по работе с файловой системой сайта
    /// </summary>
    [RoleAdmin]
    public class FileController : ICoreController
    {

        /// <summary>GET: /File/Index
        /// Страница отображения файлов и папок в 
        /// запрашиваемой дирректории
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string id)
        {
            id = id.Replace("()()", "()");
            IndexFilesModels model = new IndexFilesModels();
            if (!String.IsNullOrEmpty(id) && id.IndexOf("()file_null") != -1)
            {
                id = id.Replace("()file_null","");
                ModelState.AddModelError("Error", CoreLanguage.file_null_file);
            }
            if (!String.IsNullOrEmpty(id) && id.IndexOf("()file_dublicate") != -1)
            {
                id = id.Replace("()file_dublicate", "");
                ModelState.AddModelError("Error", CoreLanguage.file_dublicate_file);
            }
            model.Folders = coreRepo.fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);          
            model.Directory = id;
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 CoreLanguage.file_manager };
            model.bread = a;
            return View(model);
        }

        /// <summary>POST: /File/Index
        /// Создание папки в текущей дирректории
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <param name="id">файловая дирректория</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(IndexFilesModels model, string id)
        {
            
            if (ModelState.IsValid & ValidateRename(model.Name, id))
            {
                string name = id + "()" + model.Name;
                name = name.Replace("()()", "()");
                coreRepo.fileRepo.CreateFolder(name);
            }
            model.Folders = coreRepo.fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);
            model.Directory = id;
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                 CoreLanguage.file_manager };
            model.bread = a;
            return View(model);
        }

        /// <summary>GET: /File/AddFile
        /// Загрузка файла в текущую дирректорию
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">загружаемый файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult AddFile(string id, HttpPostedFileBase id2)
        {
            id = id.Replace("()()", "()");
            if (id2 != null)
            {
                bool result = true;
                string name = Path.GetFileName(id2.FileName);
                var files = coreRepo.fileRepo.GetAllFilesFromDirectory(id);
                foreach (System.IO.FileInfo x in files)
                {
                    if (name == x.Name)
                        result = false;
                }
                if (result)
                {
                    coreRepo.fileRepo.UploadFile(id, id2);
                }
                else
                {
                    id = id + "()file_dublicate";
                    id = id.Replace("()()", "()");
                }
            }
            else
            {
                id =id + "()file_null";
                id = id.Replace("()()", "()");
            }
            
            if (!String.IsNullOrEmpty(id))
                return RedirectToAction("Index", new { id = id });
            return RedirectToAction("Index", new { id = "()" });
            
        }

        /// <summary>GET: /File/Delete
        /// Удаление файла или папки
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string id, string id2)
        {
            id = id.Replace("()()", "()");
            string _directory = id.Remove(id.LastIndexOf("()"));
            if (id2 == "file")
                coreRepo.fileRepo.DeleteFile(id);
            else
                coreRepo.fileRepo.DeleteFolder(id);
            id = _directory;
            if (!String.IsNullOrEmpty(id))
                return RedirectToAction("Index", new { id = id });
            return RedirectToAction("Index", new { id = "()" });
        }

        /// <summary>GET: /File/Rename
        /// Страница по переименованию файла или папки
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        public ActionResult Rename(string id, string id2)
        {
            id = id.Replace("()()", "()");
            RenameModels model = new RenameModels();
            model.Directory = id;
            model.Name = id.Remove(0, id.LastIndexOf("()") + 2);
            if(id.LastIndexOf("()")>2)
            model.Back = id.Remove(id.LastIndexOf("()"));
            else
                model.Back = "()";
            model.Param = id2;
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                            "<a href='/File/Index/()'>" + CoreLanguage.file_manager + "</a>",
                            CoreLanguage.rename_file_or_folder };
            model.bread = a;
            return View(model);
        }

        /// <summary>POST: /File/Rename
        /// Сохранение файла или папки с новым именем
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <param name="id">файловая дирректория</param>
        /// <param name="id2">параметр идентифицирующий объект как папку или файл</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Rename(RenameModels model, string id, string id2)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                id = id.Replace("()()", "()");
                if (id2 == "folder")
                    coreRepo.fileRepo.RenameFolder(model.Name, id);
                else if (id2 == "file")
                    coreRepo.fileRepo.RenameFile(model.Name, id);
                model.Back = id.Remove(id.LastIndexOf("()"));
                if (!String.IsNullOrEmpty(model.Back))
                    return RedirectToAction("Index", new { id = model.Back });
                return RedirectToAction("Index", new { id = "()" });
            }
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                            "<a href='/File/Index/()'>" + CoreLanguage.file_manager + "</a>",
                            CoreLanguage.rename_file_or_folder };
            model.bread = a;
            return View(model);
        }

        /// <summary>GET: /File/EditPage
        /// Страница для редактирования текстового файла
        /// </summary>
        /// <param name="id">файловая дирректория</param>
        /// <returns>ActionResult</returns>
        public ActionResult EditPage(string id)
        {
            id = id.Replace("()()", "()");
            string _directory = id.Replace("()", "/").Replace("(x)", ".");
            string[] a = EditePageRepository.WritePage(_directory);
            StringBuilder result = new StringBuilder();
            foreach (string x in a)
            {
                result.AppendLine(x);
            }
            PageModel model = new PageModel();
            model.page = result.ToString();
            model.directory = id;
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                            "<a href='/File/Index/()'>" + CoreLanguage.file_manager + "</a>",
                            CoreLanguage.Edit_page };
            model.bread = _a;
            return View(model);
        }

        /// <summary>POST: /File/EditPage
        /// Сохранение изменений в редактируемом файле
        /// </summary>
        /// <param name="model">PageModel</param>
        /// <param name="id">файловая дирректория</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult EditPage(PageModel model, string id)
        {
            id = id.Replace("()()", "()");
            string _directory = id.Replace("()", "/").Replace("(x)", ".");
            EditePageRepository.CreatePage(_directory, model.page.ToString());
            return RedirectToAction("EditPage", new { id = id });
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
                            result=false;
                    }
                    var file = coreRepo.fileRepo.GetAllFilesFromDirectory(id2);
                    foreach (System.IO.FileInfo x in file)
                    {
                        if (id == x.Name)
                            result=false;
                    }
                      
            }
          if (!result)
              ModelState.AddModelError("Name", CoreLanguage.file_dublicate_name); 
            return ModelState.IsValid;
        }
    }
}
