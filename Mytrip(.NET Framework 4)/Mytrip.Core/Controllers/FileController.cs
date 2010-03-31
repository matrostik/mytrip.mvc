//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Core.Models;
using System.IO;
using Mytrip.Core.Repository;

namespace Mytrip.Core.Controllers
{
    /// <summary>
    /// File Controller
    /// </summary>
    [HandleError]
    [Localization]
    [RoleAdmin]
    public class FileController : Controller
    {
        FileRepository _fileRepo;
        public FileRepository fileRepo
        {
            get
            {
                if (_fileRepo == null)
                    _fileRepo = new FileRepository();
                return _fileRepo;
            }
        }
        /// <summary>
        /// URL: /File/Index/directory?
        /// </summary>
        /// <param name="directory">directory</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string directory)
        {
            IndexFilesModels model = new IndexFilesModels();
            if (!String.IsNullOrEmpty(directory) && directory.IndexOf("()file_null") != -1)
            {
                directory = directory.Replace("()file_null","");
                ModelState.AddModelError("Error", CoreLanguage.file_null_file);
            }
            if (!String.IsNullOrEmpty(directory) && directory.IndexOf("()file_dublicate") != -1)
            {
                directory = directory.Replace("()file_dublicate", "");
                ModelState.AddModelError("Error", CoreLanguage.file_dublicate_file);
            }
            model.Folders = fileRepo.GetAllFoldersFromDirectory(directory);
            model.Files = fileRepo.GetAllFilesFromDirectory(directory);          
            model.Directory = directory;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Index/directory?
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(IndexFilesModels model)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Directory))
            {
                string name = model.Directory + "()" + model.Name;
                fileRepo.CreateFolder(name);
            }
            model.Folders = fileRepo.GetAllFoldersFromDirectory(model.Directory);
            model.Files = fileRepo.GetAllFilesFromDirectory(model.Directory);
            return View(model);
        }
        /// <summary>
        /// URL: /File/AddFile/directory/file
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="file">HttpPostedFileBase</param>
        /// <returns>ActionResult</returns>
        public ActionResult AddFile(string directory, HttpPostedFileBase file)
        {
            if (file != null)
            {
                bool result = true;
                string name = Path.GetFileName(file.FileName);
                var files = fileRepo.GetAllFilesFromDirectory(directory);
                foreach (System.IO.FileInfo x in files)
                {
                    if (name == x.Name)
                        result = false;
                }
                if (result)
                {
                    fileRepo.UploadFile(directory, file);
                }
                else
                {
                    directory = directory + "()file_dublicate";
                    RedirectToAction("Index", new { directory });
                }
            }
            else
            {
                directory =directory + "()file_null";
                RedirectToAction("Index", new { directory}); 
            }
            
            if (!String.IsNullOrEmpty(directory))
                return RedirectToAction("Index", new { directory });
            return RedirectToAction("Index");
            
        }
        /// <summary>
        /// URL: /File/Delete/directory/param
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="param">param</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string directory, string param)
        {
            string _directory = directory.Remove(directory.LastIndexOf("()"));
            if (param == "file")
                fileRepo.DeleteFile(directory);
            else
                fileRepo.DeleteFolder(directory);
            directory = _directory;
            if (!String.IsNullOrEmpty(directory))
                return RedirectToAction("Index", new { directory });
            return RedirectToAction("Index");
        }
        /// <summary>
        /// URL: /File/Rename/directory/param
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        public ActionResult Rename(string directory, string param)
        {
            RenameModels model = new RenameModels();
            model.Directory = directory;
            model.Name = directory.Remove(0, directory.LastIndexOf("()") + 2);
            model.Back = directory.Remove(directory.LastIndexOf("()"));
            model.Param = param;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Rename/directory/param
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Rename(RenameModels model)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                if (model.Param == "folder")
                    fileRepo.RenameFolder(model.Name, model.Directory);
                else if (model.Param == "file")
                    fileRepo.RenameFile(model.Name, model.Directory);
                if (!String.IsNullOrEmpty(model.Back))
                    return RedirectToAction("Index", new { directory = model.Back });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Page(string directory)
        {
            directory = directory.Replace("()", "/");
            string[] a = EditePageRepository.WritePage(directory);
            StringBuilder result = new StringBuilder();
            string absol = string.Empty;
            foreach (string x in a)
            {
                string priv = string.Empty;
                int aa = x.IndexOf("<");
                if (aa > 0)
                {
                    int aaaa = 0;
                    while (aaaa < aa)
                    {
                        priv += "&nbsp;";
                        aaaa++;
                    }
                    absol = priv;
                }
                else
                {
                    priv = absol;
                }
                result.AppendLine(priv + EditePageRepository.HtmlDecoding(x) + "<br/>");
            }
            PageModel model = new PageModel();
            model.page = result.ToString();
            model.directory = directory.Replace("/", "()");
            return View(model);
        }
        public ActionResult EditePage(string directory)
        {
            directory = directory.Replace("()", "/");
            string[] a = EditePageRepository.WritePage(directory);
            StringBuilder result = new StringBuilder();
            foreach (string x in a)
            {
                result.AppendLine(x);
            }
            PageModel model = new PageModel();
            model.page = result.ToString();
            model.directory = directory.Replace("/", "()");
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditePage(PageModel model, string directory)
        {
            string id1 = directory.Replace("()", "/");
            EditePageRepository.CreatePage(id1, model.page);
            return RedirectToAction("EditPage", new { directory });
        }
        /// <summary>
        /// Validate Rename
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="directory">directory</param>
        /// <returns></returns>
        private bool ValidateRename(string name, string directory)
        {
            bool result = false;
          if (!String.IsNullOrEmpty(name))
          {
              result = true;           
              
                    var folder=fileRepo.GetAllFoldersFromDirectory(directory);
                    foreach (System.IO.DirectoryInfo x in folder)
                    {
                        if (name == x.Name)
                            result=false;
                    }               
                    var file = fileRepo.GetAllFilesFromDirectory(directory);
                    foreach (System.IO.FileInfo x in file)
                    {
                        if (name == x.Name)
                            result=false;
                    }
                      
            }
          if (!result)
              ModelState.AddModelError("Name", CoreLanguage.file_dublicate_name); 
            return ModelState.IsValid;
        }
    }
}
