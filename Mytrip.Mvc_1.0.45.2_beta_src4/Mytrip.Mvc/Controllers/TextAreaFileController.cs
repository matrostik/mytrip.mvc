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
using Mytrip.Mvc.Models;
using Mytrip.Mvc;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// File Controller
    /// </summary>
    [HandleError]
    [Localization]
    public class TextAreaFileController : Controller
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
        UsersSetting _artset;
        public UsersSetting artset
        {
            get
            {
                if (_artset == null)
                    _artset = new UsersSetting();
                return _artset;
            }
        }
        /// <summary>
        /// URL: /File/Index/directory?
        /// </summary>
        /// <param name="id">directory</param>
        /// <returns>ActionResult</returns>
        [RoleArea]
        public ActionResult Index(string id,string id2)
        {
            IndexFilesModels model = new IndexFilesModels();
            if (!String.IsNullOrEmpty(id) && id.IndexOf("()file_null") != -1)
            {
                id = id.Replace("()file_null", "");
                ModelState.AddModelError("Error", CoreLanguage.file_null_file);
            }
            if (!String.IsNullOrEmpty(id) && id.IndexOf("()file_dublicate") != -1)
            {
                id = id.Replace("()file_dublicate", "");
                ModelState.AddModelError("Error", CoreLanguage.file_dublicate_file);
            }
                
                    if (User.IsInRole(artset.roleChiefEditor()) && id.IndexOf("()Content()Articles") == -1)
                        id = "()Content()Articles";
                    else if (!User.IsInRole(artset.roleChiefEditor()) && id.IndexOf("()Content()Articles()" + User.Identity.Name) == -1)
                        id = "()Content()Articles()" + User.Identity.Name;
               
           
            model.Folders = fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = fileRepo.GetAllFilesFromDirectory(id);
            model.Directory = id;
            model.Param = id2;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Index/directory?
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleArea]
        public ActionResult Index(IndexFilesModels model, string id, string id2)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, id))
            {
                string name = id + "()" + model.Name;
                fileRepo.CreateFolder(name);
                RedirectToAction("Index", new { id=id, id2=id2 });
            }
            model.Folders = fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = fileRepo.GetAllFilesFromDirectory(id);
            model.Directory = id;
            model.Param = id2;
            return View(model);
        }
        /// <summary>
        /// URL: /File/AddFile/directory/file
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id3">HttpPostedFileBase</param>
        /// <returns>ActionResult</returns>
        [RoleArea]
        public ActionResult AddFile(string id,string id2, HttpPostedFileBase id3)
        {
            if (id3 != null)
            {
                bool result = true;
                string name = Path.GetFileName(id3.FileName);
                var files = fileRepo.GetAllFilesFromDirectory(id);
                foreach (System.IO.FileInfo x in files)
                {
                    if (name == x.Name)
                        result = false;
                }
                if (result)
                {
                    fileRepo.UploadFile(id, id3);
                }
                else
                {
                    id = id + "()file_dublicate";
                    RedirectToAction("Index", new { id = id,id2 = id2 });
                }
            }
            else
            {
                id = id + "()file_null";
                RedirectToAction("Index", new { id = id, id2 = id2 });
            }
                return RedirectToAction("Index", new { id = id, id2 = id2 });

        }
        /// <summary>
        /// URL: /File/Delete/directory/param
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id2">param</param>
        /// <returns>ActionResult</returns>
        [RoleArea]
        public ActionResult Delete(string id, string id2,string id3)
        {
            string _directory = id.Remove(id.LastIndexOf("()"));
            if (id3 == "file")
                fileRepo.DeleteFile(id);
            else
                fileRepo.DeleteFolder(id);
            id = _directory;
            return RedirectToAction("Index", new { id = id, id2 = id2 });
        }
        /// <summary>
        /// URL: /File/Rename/directory/param
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id2">param</param>
        /// <returns></returns>
        [RoleArea]
        public ActionResult Rename(string id, string id2,string id3)
        {
            RenameModels model = new RenameModels();
            model.Directory = id;
            model.Name = id.Remove(0, id.LastIndexOf("()") + 2);
            model.Back = id.Remove(id.LastIndexOf("()"));
            model.Param2 = id3;
            model.Param = id2;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Rename/directory/param
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [RoleArea]
        public ActionResult Rename(RenameModels model, string id, string id2, string id3)
        {
            model.Back = id.Remove(id.LastIndexOf("()"));
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                if (id3 == "folder")
                    fileRepo.RenameFolder(model.Name, id);
                else if (id3 == "file")
                    fileRepo.RenameFile(model.Name, id);
                
                    return RedirectToAction("Index", new { id = model.Back, id2=id2 });
            }
            model.Directory = id;
            model.Param2 = id3;
            model.Param = id2;
            return View(model);
        }
        /// <summary>
        /// Validate Rename
        /// </summary>
        /// <param name="id">name</param>
        /// <param name="id2">directory</param>
        /// <returns></returns>
        private bool ValidateRename(string id, string id2)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(id))
            {
                result = true;

                var folder = fileRepo.GetAllFoldersFromDirectory(id2);
                foreach (System.IO.DirectoryInfo x in folder)
                {
                    if (id == x.Name)
                        result = false;
                }
                var file = fileRepo.GetAllFilesFromDirectory(id2);
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
        public ActionResult Smile(string id)
        {
            IndexFilesModels model = new IndexFilesModels();          
            model.Files = fileRepo.GetAllFilesFromDirectory("()Content()smiles");
            model.Directory = id;
            return View(model);
        }
    }
}

