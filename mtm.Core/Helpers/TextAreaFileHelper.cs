/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Хелпер по работе с файловой системой сайта из текстового редактора
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
                string img=GeneralMethods.Image(string.Concat(path,"/",x.Name),x.Name);
                _result += MytripHtmlHelper.MytripImgInput(html, string.Concat(path, "/", x.Name), img, "smile");
            }
            return new HtmlString(_result);
        }

        /// <summary>Возвращает в TextArea разметку для отображения выбранного файла
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="value">ссылка на файл</param>
        /// <param name="name">имя файла</param>
        /// <param name="extension">расширение файла</param>
        /// <param name="_class">css класс</param>
        /// <param name="param">индентификатор текстового редактора</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripAddTextArea(this HtmlHelper html,string value, string name, string extension,string _class,string param)
        {
            string _value = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
                _value = string.Format("<img src='{0}' alt='{1}' style='border:0px;'/>", value, name);
            else if (extension == ".wmv")
                _value = string.Format("[SilverWmvStart]{0}[SilverWmvEnd]", value);
            else if (extension == ".mp4"){}
            else if (extension == ".swf"){}
            else
                _value = string.Format("<a href='{0}'><img src='/Theme/"+ThemeSetting.theme()+"/images/download.png' alt='download' style='border:0px; width:20px'/></a>  <a href='{0}'>{1}</a>", value, name);
            StringBuilder result = new StringBuilder();
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "image");
            input.AddCssClass(_class);
            input.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/addtextarea.png");
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
                    if (item.Contains("."))
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
                            else if (id == 1 && !HttpContext.Current.User.IsInRole(UsersSetting.roleChiefEditor())) { _path += "()" + item; }
                            else if (id > 1)
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
