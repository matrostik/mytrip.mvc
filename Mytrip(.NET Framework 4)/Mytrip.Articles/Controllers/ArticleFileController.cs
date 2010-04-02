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
using System.IO;
using Mytrip.Core.Models;
using Mytrip.Core;
using Mytrip.Articles.Repository;
using Mytrip.Core.Repository;

namespace Mytrip.Articles.Controllers
{
    /// <summary>
    /// File Controller
    /// </summary>
    [HandleError]
    [Localization]    
    public class ArticleFileController : Controller
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
        [RoleArticle]
        public ActionResult Index(string directory,string param)
        {
            IndexFilesModels model = new IndexFilesModels();
            if (!String.IsNullOrEmpty(directory) && directory.IndexOf("()file_null") != -1)
            {
                directory = directory.Replace("()file_null", "");
                ModelState.AddModelError("Error", CoreLanguage.file_null_file);
            }
            if (!String.IsNullOrEmpty(directory) && directory.IndexOf("()file_dublicate") != -1)
            {
                directory = directory.Replace("()file_dublicate", "");
                ModelState.AddModelError("Error", CoreLanguage.file_dublicate_file);
            }
                
                    if (User.IsInRole(ArticlesSetting.roleChiefEditor) && directory.IndexOf("()Content()Articles") == -1)
                        directory = "()Content()Articles";
                    else if (!User.IsInRole(ArticlesSetting.roleChiefEditor) && directory.IndexOf("()Content()Articles()" + User.Identity.Name) == -1)
                        directory = "()Content()Articles()" + User.Identity.Name;
               
           
            model.Folders = fileRepo.GetAllFoldersFromDirectory(directory);
            model.Files = fileRepo.GetAllFilesFromDirectory(directory);
            model.Directory = directory;
            model.Param = param;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Index/directory?
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleArticle]
        public ActionResult Index(IndexFilesModels model)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Directory))
            {
                string name = model.Directory + "()" + model.Name;
                fileRepo.CreateFolder(name);
                RedirectToAction("Index", new { directory=model.Directory, param=model.Param });
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
        [RoleArticle]
        public ActionResult AddFile(string directory,string param, HttpPostedFileBase file)
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
                    RedirectToAction("Index", new { directory,param });
                }
            }
            else
            {
                directory = directory + "()file_null";
                RedirectToAction("Index", new { directory, param });
            }
                return RedirectToAction("Index", new { directory, param });

        }
        /// <summary>
        /// URL: /File/Delete/directory/param
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="param">param</param>
        /// <returns>ActionResult</returns>
        [RoleArticle]
        public ActionResult Delete(string directory, string param,string param2)
        {
            string _directory = directory.Remove(directory.LastIndexOf("()"));
            if (param2 == "file")
                fileRepo.DeleteFile(directory);
            else
                fileRepo.DeleteFolder(directory);
            directory = _directory;
            return RedirectToAction("Index", new { directory, param });
        }
        /// <summary>
        /// URL: /File/Rename/directory/param
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        [RoleArticle]
        public ActionResult Rename(string directory, string param,string param2)
        {
            RenameModels model = new RenameModels();
            model.Directory = directory;
            model.Name = directory.Remove(0, directory.LastIndexOf("()") + 2);
            model.Back = directory.Remove(directory.LastIndexOf("()"));
            model.Param2 = param2;
            model.Param = param;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Rename/directory/param
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleArticle]
        public ActionResult Rename(RenameModels model)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                if (model.Param2 == "folder")
                    fileRepo.RenameFolder(model.Name, model.Directory);
                else if (model.Param2 == "file")
                    fileRepo.RenameFile(model.Name, model.Directory);
                    return RedirectToAction("Index", new { directory = model.Back, param=model.Param });
            }
            return View(model);
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

                var folder = fileRepo.GetAllFoldersFromDirectory(directory);
                foreach (System.IO.DirectoryInfo x in folder)
                {
                    if (name == x.Name)
                        result = false;
                }
                var file = fileRepo.GetAllFilesFromDirectory(directory);
                foreach (System.IO.FileInfo x in file)
                {
                    if (name == x.Name)
                        result = false;
                }

            }
            if (!result)
                ModelState.AddModelError("Name", CoreLanguage.file_dublicate_name);
            return ModelState.IsValid;
        }
        public ActionResult Smile(string directory)
        {
            IndexFilesModels model = new IndexFilesModels();          
            model.Files = fileRepo.GetAllFilesFromDirectory("()Content()smiles");
            model.Directory = directory;
            return View(model);
        }
    }
}

