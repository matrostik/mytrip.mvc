/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net.Mail;
namespace Mytrip.Mvc.Model.Linq2sql.Files
{
    public class FileRepository
    {
        static string dir = HttpContext.Current.Server.MapPath("/");
        DirectoryInfo _df = new DirectoryInfo(dir);
        /// <summary>
        /// Получить все папки в дирректории сайта
        /// </summary>
        /// <returns></returns>
        public DirectoryInfo[] Get_folder_site()
        {
            DirectoryInfo[] a = _df.GetDirectories();
            return a;
        }
        /// <summary>
        /// Получить все файлы в дирректории сайта
        /// </summary>
        /// <returns></returns>
        public FileInfo[] Get_file_site()
        {
            FileInfo[] a = _df.GetFiles();
            return a;
        }
        /// <summary>
        /// Получить все папки из под дирректории 
        /// </summary>
        /// <returns></returns>
        public DirectoryInfo[] Get_folder_folder(string a)
        {
            string b=a;
            b = b.Replace("()", "/");
            string c = HttpContext.Current.Server.MapPath(b);
            c = DecodingString(c);
            DirectoryInfo d = new DirectoryInfo(c);
            bool g = d.Exists;
            if (g == false) {               
                d.Create();
            }
            DirectoryInfo[] e = d.GetDirectories();
            return e;
        }
        /// <summary>
        /// Получить все файлы из под дирректории 
        /// </summary>
        /// <returns></returns>
        public FileInfo[] Get_file_folder(string a)
        {
            string b = a;
            b = b.Replace("()", "/");
            string c = HttpContext.Current.Server.MapPath(b);
            DirectoryInfo d = new DirectoryInfo(c);
            FileInfo[] e = d.GetFiles();
            return e;
        }

        /// <summary>
        /// Получить все файлы из под дирректории 
        /// </summary>
        /// <returns></returns>
        public void Upload_file(HttpPostedFileBase a)
        {
            string b = HttpContext.Current.Server.MapPath("/");
            string c=Path.GetFileName(a.FileName);
            c = DecodingString(c);
            string d = Path.Combine(b, c);
            a.SaveAs(d);
        }
        /// <summary>
        /// Получить все файлы из под дирректории 
        /// </summary>
        /// <returns></returns>
        public void Upload_file2(string a, HttpPostedFileBase b)
        {
            string c = a;
            c = c.Replace("()", "/");
            string d = HttpContext.Current.Server.MapPath(c);
            string name = Path.GetFileName(b.FileName);
            name = DecodingString(name);
            string e = Path.Combine(d, name);
            b.SaveAs(e);
        }
        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <returns></returns>
        public void Delete_file(string a)
        {
            string b = a;
            b = b.Replace("()", "/");
            string c = HttpContext.Current.Server.MapPath(b);
            FileInfo d = new FileInfo(c);
            d.Delete();
            
        }
        /// <summary>
        /// Создать папку
        /// </summary>
        /// <returns></returns>
        public void Create_folder(string a)
        {
            string b = a;
            b = b.Replace("()", "/");
            b = DecodingString(b);
            string c = HttpContext.Current.Server.MapPath(b);
            DirectoryInfo d = new DirectoryInfo(c);
            d.Create();

        }
        /// <summary>
        /// Удалить папку
        /// </summary>
        /// <returns></returns>
        public void Delete_folder(string a)
        {
            string b = a;
            b = b.Replace("()", "/");
            string c = HttpContext.Current.Server.MapPath(b);
            DirectoryInfo d = new DirectoryInfo(c);
            d.Delete(true);

        }
        #region Перевод из русского на латиницу
        /// <summary>
        /// Перевод из русского на латиницу
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
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
        #endregion
    }
}
