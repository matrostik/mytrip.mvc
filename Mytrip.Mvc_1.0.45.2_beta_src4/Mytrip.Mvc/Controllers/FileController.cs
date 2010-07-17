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
using Mytrip.Mvc.Models;
using System.IO;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
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
        /// <param name="id">directory</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string id)
        {
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
            model.Folders = fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = fileRepo.GetAllFilesFromDirectory(id);          
            model.Directory = id;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Index/directory?
        /// </summary>
        /// <param name="model">IndexFilesModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(IndexFilesModels model, string id)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, id))
            {
                string name = id + "()" + model.Name;
                fileRepo.CreateFolder(name);
            }
            model.Folders = fileRepo.GetAllFoldersFromDirectory(id);
            model.Files = fileRepo.GetAllFilesFromDirectory(id);
            model.Directory = id;
            return View(model);
        }
        /// <summary>
        /// URL: /File/AddFile/directory/file
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id2">HttpPostedFileBase</param>
        /// <returns>ActionResult</returns>
        public ActionResult AddFile(string id, HttpPostedFileBase id2)
        {
            if (id2 != null)
            {
                bool result = true;
                string name = Path.GetFileName(id2.FileName);
                var files = fileRepo.GetAllFilesFromDirectory(id);
                foreach (System.IO.FileInfo x in files)
                {
                    if (name == x.Name)
                        result = false;
                }
                if (result)
                {
                    fileRepo.UploadFile(id, id2);
                }
                else
                {
                    id = id + "()file_dublicate";
                    RedirectToAction("Index", new { id = id });
                }
            }
            else
            {
                id =id + "()file_null";
                RedirectToAction("Index", new { id = id}); 
            }
            
            if (!String.IsNullOrEmpty(id))
                return RedirectToAction("Index", new { id = id });
            return RedirectToAction("Index");
            
        }
        /// <summary>
        /// URL: /File/Delete/directory/param
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id2">param</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(string id, string id2)
        {
            string _directory = id.Remove(id.LastIndexOf("()"));
            if (id2 == "file")
                fileRepo.DeleteFile(id);
            else
                fileRepo.DeleteFolder(id);
            id = _directory;
            if (!String.IsNullOrEmpty(id))
                return RedirectToAction("Index", new { id = id });
            return RedirectToAction("Index");
        }
        /// <summary>
        /// URL: /File/Rename/directory/param
        /// </summary>
        /// <param name="id">directory</param>
        /// <param name="id2">param</param>
        /// <returns></returns>
        public ActionResult Rename(string id, string id2)
        {
            RenameModels model = new RenameModels();
            model.Directory = id;
            model.Name = id.Remove(0, id.LastIndexOf("()") + 2);
            model.Back = id.Remove(id.LastIndexOf("()"));
            model.Param = id2;
            return View(model);
        }
        /// <summary>
        /// POST: /File/Rename/directory/param
        /// </summary>
        /// <param name="model">RenameModels</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Rename(RenameModels model, string id, string id2)
        {
            if (ModelState.IsValid & ValidateRename(model.Name, model.Back))
            {
                if (id2 == "folder")
                    fileRepo.RenameFolder(model.Name, id);
                else if (id2 == "file")
                    fileRepo.RenameFile(model.Name, id);
                model.Back = id.Remove(id.LastIndexOf("()"));
                if (!String.IsNullOrEmpty(model.Back))
                    return RedirectToAction("Index", new { id = model.Back });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Page(string id)
        {
            string _directory = id.Replace("()", "/");
            _directory = _directory.Replace("(x)", ".");
            string[] a = EditePageRepository.WritePage(_directory);
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
            model.directory = id;
            return View(model);
        }
        public ActionResult EditPage(string id)
        {
            string _directory = id.Replace("()", "/");
            _directory = _directory.Replace("(x)", ".");
            string[] a = EditePageRepository.WritePage(_directory);
            StringBuilder result = new StringBuilder();
            foreach (string x in a)
            {
                result.AppendLine(x);
            }
            PageModel model = new PageModel();
            model.page = result.ToString();
            model.directory = id;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPage(PageModel model, string id)
        {
            string _directory = id.Replace("()", "/");
            _directory = _directory.Replace("(x)", ".");
            EditePageRepository.CreatePage(_directory, model.page);
            return RedirectToAction("EditPage", new { id = id });
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
              
                    var folder=fileRepo.GetAllFoldersFromDirectory(id2);
                    foreach (System.IO.DirectoryInfo x in folder)
                    {
                        if (id == x.Name)
                            result=false;
                    }               
                    var file = fileRepo.GetAllFilesFromDirectory(id2);
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
