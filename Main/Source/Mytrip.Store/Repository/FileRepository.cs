using System.Web;
using System.IO;
using System;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class FileRepository
    {
        internal string UploadFile(string param, HttpPostedFileBase file)
        {
            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0, name.LastIndexOf("."));

            Random random = new Random(DateTime.Now.Millisecond);
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            if (param == "CreateDepartment" || param == "EditDepartment")
            {
                directory = "/Content/Store/Department";
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
                FileInfo[] result = _absolutDirectory2.GetFiles();
                foreach (var x in result)
                {
                    if (x.Name.Contains(name))
                    {
                        x.Delete();
                        break;
                    }
                }
            }
            else if (param == "CreateProducer" || param == "EditProducer")
            {
                directory = "/Content/Store/Producer";
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
                FileInfo[] result = _absolutDirectory2.GetFiles();
                foreach (var x in result)
                {
                    if (x.Name.Contains(name))
                    {
                        x.Delete();
                        break;
                    }
                }
            }
            name += extension.ToLower();
            string _absolutDirectory = Path.Combine(absolutDirectory, ch + name);
            file.SaveAs(_absolutDirectory);
            return directory + "/" + ch + name;
        }
        internal void RenameFile(int id, string directory)
        {
            string name = "temp" + HttpContext.Current.User.Identity.Name + ".";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            string _name = "";
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            bool _result = false;
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    _result = true; 
                    break;
                }
            }
            if (_result)
            {
                foreach (var x in result)
                {
                    if (x.Name.Contains("_" + id + "."))
                    {
                        x.Delete();
                        break;
                    }
                }
                foreach (var x in result)
                {
                    if (x.Name.Contains(name))
                    {
                        _name = x.FullName.Replace(x.Name, "_" + id + x.Extension);
                        File.Move(x.FullName, _name);
                        break;
                    }
                }
            }
        }
        internal void DeleteFile(int id, string directory)
        {
            string name = "_" + id + ".";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);

            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    x.Delete();
                    break;
                }
            }
        }
        internal void DeleteFile(string directory)
        {
            
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
        }
        internal HtmlString GetFileForEdit(string directory, int id)
        {
            string name = "temp" + HttpContext.Current.User.Identity.Name + ".";
            if (id > 0)
                name = "_" + id + ".";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            string _directory = "";
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            bool a = false;
            string val = directory.Replace("/", "()");
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    
                    _directory =
                        "<img src='" + directory + "/" + x.Name + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                        + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile/"+val+"()"+x.Name, "deleteImg", 14);
                    a = true;
                    break;
                }
            }
            if (id > 0 && !a)
            {
                name = "temp" + HttpContext.Current.User.Identity.Name + ".";
                foreach (var x in result)
                {
                    if (x.Name.Contains(name))
                    {
                        _directory = "<img src='" + directory + "/" + x.Name + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                            + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile/"+val+"()"+x.Name, "deleteImg", 14);
                        break;
                    }
                }
            }
            return new HtmlString(_directory);
        }
    }
}
