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
using System.Web;
using System.IO;

namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// File Repository
    /// </summary>
    public class FileRepository
    {
        /// <summary>
        /// Get All Folders from Directory
        /// </summary>
        /// <param name="directory">directory</param>
        /// <returns>DirectoryInfo[]</returns>
        public DirectoryInfo[] GetAllFoldersFromDirectory(string directory)
        {
            if (String.IsNullOrEmpty(directory))
                directory = "/";
            directory = directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
            {
                _absolutDirectory.Create();
            }
            DirectoryInfo[] result = _absolutDirectory.GetDirectories();
            return result;
        }
        /// <summary>
        /// Get All Files from Directory
        /// </summary>
        /// <param name="directory">directory</param>
        /// <returns>FileInfo[]</returns>
        public FileInfo[] GetAllFilesFromDirectory(string directory)
        {
            if (String.IsNullOrEmpty(directory))
                directory = "/";
            directory = directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory.GetFiles();
            return result;
        }
        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="directory">directory</param>
        /// <param name="file">HttpPostedFileBase</param>
        public void UploadFile(string directory, HttpPostedFileBase file)
        {
            if (String.IsNullOrEmpty(directory))
                directory = "/";
            else
                directory = directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            string name = Path.GetFileName(file.FileName);
            name = DecodingString(name);
            string _absolutDirectory = Path.Combine(absolutDirectory, name);
            file.SaveAs(_absolutDirectory);
        }
        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="directory">directory</param>
        public void DeleteFile(string directory)
        {
            string _directory = directory;
            _directory = _directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(_directory);
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();

        }
        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="directory">directory</param>
        public void CreateFolder(string directory)
        {
            string _directory = DecodingString(directory);
            _directory = _directory.Replace("()", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath(_directory);
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Create();

        }
        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="directory">directory</param>
        public void DeleteFolder(string directory)
        {
            directory = directory.Replace("()", "/");
            string _directory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo folder = new DirectoryInfo(_directory);
            folder.Delete(true);

        }
        /// <summary>
        /// Rename Folder
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="directory">directory</param>
        public void RenameFolder(string name, string directory)
        {
            string oldDirectory = directory.Replace("()", "/");
            string newDirectory = directory.Remove(directory.LastIndexOf("()")).Replace("()", "/") + "/" + DecodingString(name);

            string oldName = HttpContext.Current.Server.MapPath(oldDirectory);
            string newName = HttpContext.Current.Server.MapPath(newDirectory);
            if (oldName != newName)
                Directory.Move(oldName, newName);
        }
        /// <summary>
        /// Rename File
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="directory">directory</param>
        public void RenameFile(string name, string directory)
        {
            string oldDirectory = directory.Replace("()", "/");
            string newDirectory = directory.Remove(directory.LastIndexOf("()")).Replace("()", "/") + "/" + DecodingString(name);

            string oldName = HttpContext.Current.Server.MapPath(oldDirectory);
            string newName = HttpContext.Current.Server.MapPath(newDirectory);
            if (oldName != newName)
                File.Move(oldName, newName);
        }
        /// <summary>
        /// Decoding String
        /// </summary>
        /// <param name="a">string</param>
        /// <returns>string</returns>
        public string DecodingString(string a)
        {
            a = a.Trim();
            a = a.Replace(" ", "_");
            a = a.Replace("А", "A");
            a = a.Replace("а", "a");
            a = a.Replace("Б", "B");
            a = a.Replace("б", "b");
            a = a.Replace("В", "V");
            a = a.Replace("в", "v");
            a = a.Replace("Г", "G");
            a = a.Replace("г", "g");
            a = a.Replace("Д", "D");
            a = a.Replace("д", "d");
            a = a.Replace("Е", "E");
            a = a.Replace("е", "e");
            a = a.Replace("Ё", "E");
            a = a.Replace("ё", "e");
            a = a.Replace("Ж", "J");
            a = a.Replace("ж", "j");
            a = a.Replace("З", "Z");
            a = a.Replace("з", "z");
            a = a.Replace("И", "I");
            a = a.Replace("и", "i");
            a = a.Replace("Й", "Y");
            a = a.Replace("й", "y");
            a = a.Replace("К", "K");
            a = a.Replace("к", "k");
            a = a.Replace("Л", "L");
            a = a.Replace("л", "l");
            a = a.Replace("М", "M");
            a = a.Replace("м", "m");
            a = a.Replace("Н", "N");
            a = a.Replace("н", "n");
            a = a.Replace("О", "O");
            a = a.Replace("о", "o");
            a = a.Replace("П", "P");
            a = a.Replace("п", "p");
            a = a.Replace("Р", "R");
            a = a.Replace("р", "r");
            a = a.Replace("С", "S");
            a = a.Replace("с", "s");
            a = a.Replace("Т", "T");
            a = a.Replace("т", "t");
            a = a.Replace("У", "U");
            a = a.Replace("у", "u");
            a = a.Replace("Ф", "F");
            a = a.Replace("ф", "f");
            a = a.Replace("Х", "H");
            a = a.Replace("х", "h");
            a = a.Replace("Ц", "C");
            a = a.Replace("ц", "c");
            a = a.Replace("Ч", "Ch");
            a = a.Replace("ч", "ch");
            a = a.Replace("Ш", "Sh");
            a = a.Replace("ш", "sh");
            a = a.Replace("Щ", "Sh");
            a = a.Replace("щ", "sh");
            a = a.Replace("Ъ", "");
            a = a.Replace("ъ", "");
            a = a.Replace("Ы", "Y");
            a = a.Replace("ы", "y");
            a = a.Replace("Ь", "");
            a = a.Replace("ь", "");
            a = a.Replace("Э", "E");
            a = a.Replace("э", "e");
            a = a.Replace("Ю", "Yu");
            a = a.Replace("ю", "yu");
            a = a.Replace("Я", "Ya");
            a = a.Replace("я", "ya");
            a = a.Replace("!", "");
            a = a.Replace(",", "");
            return a;
        }
    }
}
