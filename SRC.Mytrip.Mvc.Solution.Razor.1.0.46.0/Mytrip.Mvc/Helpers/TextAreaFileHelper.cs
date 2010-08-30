/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>Контроллер по работе с файловой системой сайта из текстового редактора
    /// </summary>
    public static class TextAreaFileHelper
    {
        /// <summary>Формирует список из смайликов для модального окна 
        /// предназначен только для работы с текстовым редактором
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="path">дирректория</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripSmiles(this HtmlHelper html, string path)
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath(path);
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory.GetFiles();
            string _result = string.Empty;
            foreach (FileInfo x in result)
            {
                string img=GeneralMethods.Image(path +"/"+ x.Name,x.Name);
                _result += MytripHtmlHelper.MytripImgInput(html, path +"/"+ x.Name,img , "smile");
            }
            return new HtmlString(_result);
        }

        /// <summary>Возвращает в TextArea разметку для отображения выбранного файла
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="value">ссылка на файл</param>
        /// <param name="name">имя файла</param>
        /// <param name="extension">расширение файла</param>
        /// <param name="_class">цсс класс</param>
        /// <param name="param">индентификатор текстового редактора</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripAddTextArea(this HtmlHelper html,string value, string name, string extension,string _class,string param)
        {
            string _value = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
                _value = "<img src='" + value + "' alt='" + name + "' style='border:0px;'/>";
            else if (extension == ".wmv")
                _value = "[SilverWmvStart]" + value + "[SilverWmvEnd]";
            else if (extension == ".mp4"){}
            else if (extension == ".swf"){}
            else
                _value = "<a href='" + value + "'><img src='/content/images/download.png' alt='download' style='border:0px; width:20px'/></a>" +
                    "  <a href='" + value + "'>" + name + "</a>";
            StringBuilder result = new StringBuilder();
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "image");
            input.AddCssClass(_class);
            input.MergeAttribute("src", "/content/images/addtextarea.png");
            input.MergeAttribute("style", "border:0px; width:20px");
            input.MergeAttribute("value", _value);
            input.MergeAttribute("name", param);
            input.MergeAttribute("onclick", "window.close();");
            result.AppendLine(input.ToString());
            return new HtmlString(result.ToString());
        }

        /// <summary>Вывод ссылок на родителей текущих папок и файлов
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="path">дирректория</param>
        /// <param name="controller">текущий контроллкр</param>
        /// <param name="param">индентификатор текстового редактора</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripDirectory(this HtmlHelper html, string path, string controller, string param)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(path))
            {
                string[] directory = path.Remove(0, 2).Replace("()", "/").Split('/');
                string _path = string.Empty;
                int id = 0;
                foreach (string item in directory)
                {
                    if (item.IndexOf(".") != -1)
                    {
                        result += " / " + item;
                    }
                    else
                    {
                        if (id != 0 && item != "Content")
                        {
                            if (id == 1 && HttpContext.Current.User.IsInRole(UsersSetting.roleChiefEditor()))
                            {
                                _path += "()" + item;
                                TagBuilder _result = new TagBuilder("a");
                                _result.MergeAttribute("href", "/" + controller + "/Index/" + _path + "/" + param);
                                _result.InnerHtml = item;
                                result += " / " + _result;
                            }
                            if (id == 1 && !HttpContext.Current.User.IsInRole(UsersSetting.roleChiefEditor())) { _path += "()" + item; }
                            if (id > 1)
                            {
                                _path += "()" + item;
                                TagBuilder _result = new TagBuilder("a");
                                _result.MergeAttribute("href", "/" + controller + "/Index/" + _path + "/" + param);
                                _result.InnerHtml = item;
                                result += " / " + _result;
                            }
                        }
                        else { _path += "()" + item; }
                        id++;
                    }
                }
            }
            return new HtmlString(result);
        }
    }
}
