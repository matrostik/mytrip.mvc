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
        internal string UploadFileProduct(string param, HttpPostedFileBase file)
        {
            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0, name.LastIndexOf("."));
            name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store/Product";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            if (param.Contains("CreateProduct"))
            {
                directory += "/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
            }
            else if (param.Contains("EditProduct"))
            {
                string[] _param = param.Split('_');
                directory += "/"+_param[0];
            }
            string filename = "product";
            absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains(filename))
                {
                    x.Delete();
                    break;
                }
            }
            filename += extension.ToLower();
            string _absolutDirectory = Path.Combine(absolutDirectory, filename);
            file.SaveAs(_absolutDirectory);
            return directory + "/" + filename;
        }
        internal string GetFileProduct(string param)
        {
            string name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store/Product";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            if (param.Contains("CreateProduct"))
            {
                directory += "/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
            }
            else if (param.Contains("EditProduct"))
            {
                string[] _param = param.Split('_');
                directory += "/" + _param[0];
            }
            string filename = "product";
            absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            bool file = false;
            string _file = "";
            foreach (var x in result)
            {
                if (x.Name.Contains(filename))
                {
                    _file = x.Name;
                    file = true;
                }
            }
            if (file)
                return "<img src='" + directory + "/" + _file + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                        + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile/" + directory.Replace("/", "()") + "()" + _file, "deleteImg", 14);
            else
                return null;
        }
        internal string[] UploadFileProductOption(string param, HttpPostedFileBase file)
        {
            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0, name.LastIndexOf("."));
            name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store/Product";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            if (param.Contains("CreateProduct"))
            {
                directory += "/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
            }
            else if (param.Contains("EditProduct"))
            {
                string[] _param = param.Split('_');
                directory += "/" + _param[0];
            }
            int _filename=0;
            string _file = "";
            absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains("_" + _filename+"."))
                {
                    _filename++;
                }
                if (!x.Name.Contains("product"))
                {
                    _file += "[" + directory + "/" + x.Name + "]";
                }
            }
            string filename = "_" + _filename;
            filename += extension.ToLower();
            string _absolutDirectory = Path.Combine(absolutDirectory, filename);
            file.SaveAs(_absolutDirectory);
            _file += "[" + directory + "/" + filename + "]";
            _file = _file.Replace("][", "|").Replace("[", "").Replace("]", "");
            return _file.Split('|');
        }
        internal string[] GetFileProductOption(string param)
        {
            
            string name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store/Product";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            if (param.Contains("CreateProduct"))
            {
                directory += "/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
            }
            else if (param.Contains("EditProduct"))
            {
                string[] _param = param.Split('_');
                directory += "/" + _param[0];
            }
            string _file = "";
            absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (!x.Name.Contains("product"))
                {
                    _file += "[" + directory + "/" + x.Name + "]";
                }
            }
            _file = _file.Replace("][", "|").Replace("[", "").Replace("]", "");
            return _file.Split('|');
        }
        internal void RenameFolder(int id)
        {
            string oldName = HttpContext.Current.Server.MapPath("/Content/Store/Product/temp" + HttpContext.Current.User.Identity.Name);
            string newName = HttpContext.Current.Server.MapPath("/Content/Store/Product/" + id);
            Directory.Move(oldName, newName);
        }
        internal void DeleteFolder(int id)
        {
            string _directory = HttpContext.Current.Server.MapPath("/Content/Store/Product/" + id);
           DirectoryInfo folder = new DirectoryInfo(_directory);
           folder.Delete(true);
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
