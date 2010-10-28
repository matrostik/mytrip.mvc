/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.IO;
using System.Web;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Repository
{
    /// <summary>File Repository
    /// </summary>
    internal class FileRepository
    {
        /// <summary>Get All Folders from Directory
        /// </summary>
        /// <param name="directory">directory</param>
        /// <returns>DirectoryInfo[]</returns>
        internal DirectoryInfo[] GetAllFoldersFromDirectory(string directory)
        {
            directory = String.IsNullOrEmpty(directory) ? "/" : directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists) 
                _absolutDirectory.Create();
            DirectoryInfo[] result = _absolutDirectory.GetDirectories();
            return result;
        }
        /// <summary>Get All Files from Directory
        /// </summary>
        /// <param name="directory">directory</param>
        /// <returns>FileInfo[]</returns>
        internal FileInfo[] GetAllFilesFromDirectory(string directory)
        {
            directory = String.IsNullOrEmpty(directory) ? "/" : directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            FileInfo[] result = _absolutDirectory.GetFiles();
            return result;
        }
        /// <summary>Upload File
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="file">HttpPostedFileBase</param>
        internal void UploadFile(string directory, HttpPostedFileBase file)
        {
            directory = String.IsNullOrEmpty(directory) ? "/" : directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0,name.LastIndexOf("."));
            string extension2 = extension.Replace(".", "_");
            name = GeneralMethods.DecodingString(name).Replace(extension2, extension.ToLower());
            string _absolutDirectory = Path.Combine(absolutDirectory, name);
            file.SaveAs(_absolutDirectory);
        }
        /// <summary>Delete File
        /// </summary>
        /// <param name="directory">directory</param>
        internal void DeleteFile(string directory)
        {
            string _directory = directory;
            _directory = _directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(_directory);
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();

        }
        /// <summary>Create Folder
        /// </summary>
        /// <param name="directory">directory</param>
        internal void CreateFolder(string directory)
        {
            string _directory = GeneralMethods.DecodingString(directory);
            _directory = _directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(_directory);
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Create();

        }
        /// <summary>Delete Folder
        /// </summary>
        /// <param name="directory">directory</param>
        internal void DeleteFolder(string directory)
        {
            directory = directory.Replace("()", "/");
            string _directory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo folder = new DirectoryInfo(_directory);
            folder.Delete(true);

        }
        /// <summary>Rename Folder
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="directory">directory</param>
        internal void RenameFolder(string name, string directory)
        {
            string oldDirectory = directory.Replace("()", "/");
            string newDirectory = string.Concat(directory.Remove(directory.LastIndexOf("()")).Replace("()", "/"),"/",GeneralMethods.DecodingString(name));
            string oldName = HttpContext.Current.Server.MapPath(oldDirectory);
            string newName = HttpContext.Current.Server.MapPath(newDirectory);
            if (oldName != newName)
                Directory.Move(oldName, newName);
        }
        /// <summary>Rename File
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="directory">directory</param>
        internal void RenameFile(string name, string directory)
        {
            string oldDirectory = directory.Replace("()", "/");
            string newDirectory = string.Concat(directory.Remove(directory.LastIndexOf("()")).Replace("()", "/"),"/",GeneralMethods.DecodingString(name));
            string oldName = HttpContext.Current.Server.MapPath(oldDirectory);
            string newName = HttpContext.Current.Server.MapPath(newDirectory);
            if (oldName != newName)
                File.Move(oldName, newName);
        }
    }
}
