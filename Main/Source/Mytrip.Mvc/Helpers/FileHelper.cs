/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>ХТМЛ Хелперы для работы в файловом менеджере
    /// </summary>
    public static class FileHelper
    {
        /// <summary>Вывод ссылок на родителей текущих папок и файлов
       /// </summary>
        /// <param name="html">HtmlHelper</param>
       /// <param name="path">дирректория</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripDirectory(this HtmlHelper html, string path)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(path))
            {
                string[] directory = path.Remove(0, 2).Replace("()", "/").Split('/');
                string _path = string.Empty;
                foreach (string item in directory)
                {
                    if (item == directory[directory.Length - 1])
                    {
                        result += " / " + item;
                    }
                    else
                    {
                        _path += "()" + item;
                        TagBuilder _result = new TagBuilder("a");
                        _result.MergeAttribute("href", "/File/Index/" + _path);
                        _result.InnerHtml = item;
                        result += " / " + _result;
                    }
                }
            }
            return new HtmlString(result);
        }

        /// <summary>Определение пути файла в зависимости от его расширения
        /// </summary>
        /// <param name="directory">дирректория</param>
        /// <param name="name">имя файла</param>
        /// <param name="extension">расширение файла</param>
        /// <returns>возвращает string</returns>
        public static string MytripMim(string directory, string name, string extension)
        {
            string result = (directory).Replace("()", "/");
            if (extension != ".ico" && extension != ".png" && extension != ".jpg" && extension != ".gif")
             result = "/Content/files/" + extension + ".png";
            return result;
        }

        /// <summary> Изображение файла в зависимости от его расширения
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высотта</param>
        /// <param name="border">бордюр</param>
        /// <param name="directory">дирректория</param>
        /// <param name="name">имя файла</param>
        /// <param name="extension">расширение файла</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString FileImageLink(this HtmlHelper html,string alt, int width, int height, int border,string directory, string name, string extension)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            string UrlAction = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
            {
                UrlAction = directory.Replace("()", "/");
                _result.MergeAttribute("href", UrlAction);
            }
            else if (extension == ".Master" || extension == ".master" || extension == ".cs" || extension == ".cshtml" || extension == ".css" || extension == ".aspx" || extension == ".xml"
                || extension == ".txt" || extension == ".config" || extension == ".ascx" || extension == ".js" || extension == ".resx")
            {
                directory = directory.Replace("/", "()");
                directory = directory.Replace(".", "(x)");
                UrlAction = "/File/EditPage/" + directory;
                _result.MergeAttribute("href", UrlAction);
            } 
            _result.InnerHtml = GeneralMethods.Image(MytripMim(directory,name,extension), width, height, alt, border,false);
            result.Append(_result.ToString());
            return new HtmlString(result.ToString());
        }
        
        /// <summary>Ссылка для работы с файлом в зависимости от его расширения
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="directory">дирректория</param>
        /// <param name="name">имя файла</param>
        /// <param name="extension">расширение файла</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString FileActionLink(this HtmlHelper html, string directory, string name, string extension)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            string UrlAction = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
            { UrlAction = directory.Replace("()", "/");
            _result.MergeAttribute("href", UrlAction);
            }
            else if (extension == ".Master" || extension == ".master" || extension == ".cs" || extension == ".cshtml" || extension == ".css" || extension == ".aspx" || extension == ".xml"
                || extension == ".txt" || extension == ".config" || extension == ".ascx" || extension == ".js" || extension == ".resx")
            {
                directory = directory.Replace("/", "()");
                directory = directory.Replace(".", "(x)");
                UrlAction = "/File/EditPage/" + directory;
                _result.MergeAttribute("href", UrlAction);
            }            
            _result.InnerHtml = name;
            result.Append(_result.ToString());
            return new HtmlString(result.ToString());
        }
        
        /// <summary>Оформление загрузчика файлов
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="_type">тип инпута</param>
        /// <param name="name">имя</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripInputFile(this HtmlHelper html, string _type, string name)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class=\"file\">");
            TagBuilder _result = new TagBuilder("input");
            _result.MergeAttribute("type", _type);
            _result.MergeAttribute("name", name);
            result.Append(_result.ToString());
            result.AppendLine("</div>");
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }
    }
}
