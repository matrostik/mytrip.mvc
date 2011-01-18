using System.Web;
using System.IO;
using System;
using mtm.Core.Settings;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mtm.Store.Repository
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

            name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store";
            string absolutDirectory = "";
            int width = ModuleSetting.widthImgProduct();
            if (param.Contains("Department"))
                directory += "/Department";
            else if (param.Contains("Producer"))
                directory += "/Producer";
            else if (param.Contains("CreateProduct"))
            {
                directory += "/Product/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
                name = "product";
            }
            else if (param.Contains("EditProduct"))
            {
                string[] _param = param.Split('_');
                directory += "/Product/" + _param[0];
                name = "product";
            }
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
            name +=extension.ToLower();
            if (!param.Contains("Product"))
            {
                width= ModuleSetting.widthImgDepartment();
                Random random = new Random(DateTime.Now.Millisecond);
                name = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))) + name;
            }
            string _absolutDirectory = Path.Combine(absolutDirectory, name);
            //file.SaveAs(_absolutDirectory);
            ResizeSaveImage(file, width*2,_absolutDirectory);
            return directory + "/" + name;
        }

        internal HtmlString GetFile(int id, string path)
        {
            string name = "temp" + HttpContext.Current.User.Identity.Name;
            string directory = "/Content/Store/";
            string absolutDirectory = "";
            if (path=="CreateProduct")
            {
                directory += "Product/" + name;
                absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
                if (!folder.Exists)
                    folder.Create();
                name = "product";
            }
            else if (path=="EditProduct")
            {
                directory += "Product/" + id;
                name = "product";
            }
            else if (path.Contains("Department"))
            {
                directory += "Department";
                if (path=="EditDepartment")
                    name = "_" + id + ".";
            }
            else if (path.Contains("Producer"))
            {
                directory += "Producer";
                if (path=="EditProducer")
                    name = "_" + id + ".";
            }
            else
                return new HtmlString("");
            absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory2.GetFiles();
            string _file = "";
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    _file = "<span class='forimg'><img src='" + directory + "/" + x.Name + "?" + x.Length +  "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                        + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile/" + directory.Replace("/", "()") + "()" + x.Name, "deleteImg", 14) + "</span>";
                    break;
                }
            }
            return new HtmlString(_file);

        }
        
        internal string[] UploadFiles(string param, HttpPostedFileBase file)
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
            ResizeSaveImage(file,  ModuleSetting.widthImgDepartment() * 2, _absolutDirectory);
            //file.SaveAs(_absolutDirectory);
            _file += "[" + directory + "/" + filename + "]";
            _file = _file.Replace("][", "|").Replace("[", "").Replace("]", "");
            return _file.Split('|');
        }

        internal string[] GetFiles(string param)
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

        internal bool ResizeSaveImage(HttpPostedFileBase file,int width,string fullPath)
        {
            try
            {
                //Image resize
                Size size = new Size(width, width);
                Image imgToResize = Image.FromStream(file.InputStream, true, true);
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(imgToResize, new Rectangle(0, 0, destWidth, destHeight), 0, 0, sourceWidth, sourceHeight, GraphicsUnit.Pixel);
                //g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();
                //fullPath=Path.ChangeExtension(fullPath, "jpg");
                b.Save(fullPath, imgToResize.RawFormat);
                //b.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //b.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
