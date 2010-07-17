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
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Configuration;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace Mytrip.Mvc.Models
{
    /// <summary>
    /// Rename Models
    /// </summary>
    [MetadataType(typeof(RenameModels))]
    public class RenameModels
    {
        /// <summary>
        /// string Name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_null_name")]
        [StringLength(30, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_max_name")]
        public string Name { get; set; }
        /// <summary>
        /// string Directory
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// string Back
        /// </summary>
        public string Back { get; set; }
        /// <summary>
        /// string Param
        /// </summary>
        public string Param { get; set; }
        public string Param2 { get; set; }
    }
    /// <summary>
    /// Index Files Models
    /// </summary>
    [MetadataType(typeof(IndexFilesModels))]
    public class IndexFilesModels
    {
        /// <summary>
        /// string Name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_null_name")]
        [StringLength(30, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "file_max_name")]
        public string Name { get; set; }
        /// <summary>
        /// string Directory
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// DirectoryInfo[] Folders
        /// </summary>
        public DirectoryInfo[] Folders { get; set; }
        /// <summary>
        /// FileInfo[] Files
        /// </summary>
        public FileInfo[] Files { get; set; }
        /// <summary>
        /// bool Error
        /// </summary>
        public bool Error { get; set; }
        public string Param { get; set; }
    }
   
}
