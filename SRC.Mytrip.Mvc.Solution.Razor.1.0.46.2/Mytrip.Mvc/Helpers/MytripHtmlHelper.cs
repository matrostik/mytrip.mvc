/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>Основной набор хелперов для работы
    /// </summary>
    public static class MytripHtmlHelper
    {
        /// <summary>Файл изображения для краткого описания
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="image">ссылка на изображение</param>
        /// <param name="width">ширина изображения</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImageForAbstract(this HtmlHelper html, string image, int width)
        {
                return new HtmlString(GeneralMethods.ImageForAbstract(image,width));
        }

        /// <summary>Создание html разметки [label for='{target}']{text}[/label]
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="target">label for='{target}'</param>
        /// <param name="text">отоброжаемый текст</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripLabelFor(this HtmlHelper html, string target, string text)
        {
            string result= string.Format("<label for='{0}'>{1}</label>", target, text);
            HtmlString htmlresult = new HtmlString(result);
            return htmlresult;
        }

        /// <summary>Титл страницы 
        /// CoreSetting.NameTitlePage() = "CMS Mytrip.Mvc ({0})";
        /// UsersSetting.applicationName() = "www.mysite.com";
        /// _char = "/";
        /// CMS Mytrip.Mvc (www.mysite.com)/{title}
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="title">Титл страницы{_char}{title}</param>
        /// <param name="_char">Титл страницы{_char}{title}</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripPageTitle(this HtmlHelper html, string title, string _char)
        {
            string domain = UsersSetting.applicationName().Replace("www.", "");
            string _result = string.Format(CoreSetting.NameTitlePage(), domain) + _char + title;
            return new HtmlString(_result);
        }

        /// <summary>Оформление ссылки как кнопки
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="link">ссылка</param>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripButton(this HtmlHelper html, string link, string value, bool warning, string _float)
        {
            return new HtmlString(GeneralMethods.Button(link, value, warning, _float));
        }
        
        /// <summary>Оформление [input type="submit" value="value"/]
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripButton(this HtmlHelper html, string value, bool warning, string _float)
        {
            return new HtmlString(GeneralMethods.Button(value, warning, _float));
        }
        
        /// <summary>Оформление [input type="submit" id="id"/]
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="id">индентификатор</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripButton(this HtmlHelper html, string value, bool warning, string id, string _float)
        {
            return new HtmlString(GeneralMethods.Button(value, warning,id,_float));
        }
        /// <summary>Оформление [input type="image" value="value" class="_class"/]
        /// если ссылка не содержит "Content" перенаправление в
        /// папку текущей темы
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="value">значение</param>
        /// <param name="_class">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImgInput(this HtmlHelper html, string src, string value, string _class)
        {
            return new HtmlString(GeneralMethods.ImgInput(src, value, _class));
        }
        
        /// <summary>Оформление [input type="image" value="value" name="name" class="_class"/]
        /// если ссылка не содержит "Content" перенаправление в
        /// папку текущей темы
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="value">значение</param>
        /// <param name="name">name</param>
        /// <param name="_class">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImgInput(this HtmlHelper html, string src, string value, string name, string _class)
        {
            return new HtmlString(GeneralMethods.ImgInput(src, value,name, _class));
        }

        /// <summary>Оформление изображения как ссылки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">ссылка</param>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="border">бордюр</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImageLink(this HtmlHelper html, string UrlAction, string src, string alt, int width, int height, int border)
        {

            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = GeneralMethods.Image(src, width, height, alt, border);
            result.Append(_result.ToString());
            return new HtmlString(result.ToString());
        }

        /// <summary>Оформление изображения как ссылки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">ссылка</param>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="cssClass">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImageLink(this HtmlHelper html, string UrlAction, string src, string alt, string cssClass)
        {

            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            if (!String.IsNullOrEmpty(cssClass))
            {
                _result.InnerHtml = GeneralMethods.Image(src, alt, cssClass);
            }
            else { _result.InnerHtml = GeneralMethods.Image(src, alt); }
            result.Append(_result.ToString());
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }

        /// <summary>Оформление изображения как ссылки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="id">индентификатор ссылки</param>
        /// <param name="UrlAction">ссылка</param>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="cssClass">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImageLink(this HtmlHelper html, string id, string UrlAction, string src, string alt, string cssClass)
        {

            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.MergeAttribute("id", id);
            if (!String.IsNullOrEmpty(cssClass))
            {
                _result.InnerHtml = GeneralMethods.Image(src, alt, cssClass);
            }
            else { _result.InnerHtml = GeneralMethods.Image(src, alt); }
            result.Append(_result.ToString());
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }

        /// <summary>ХТМЛ разметка для изображения
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="url">ссылка на изображение</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="cssClass">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImage(this HtmlHelper html, string url, string alt, string cssClass)
        {

            string result = string.Empty;
            if (!String.IsNullOrEmpty(cssClass))
            {
                result = GeneralMethods.Image(url, alt, cssClass);
            }
            else { result = GeneralMethods.Image(url, alt); }
            HtmlString htmlresult = new HtmlString(result.ToString());
            return htmlresult;
        }

        /// <summary>ХТМЛ разметка для изображения
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="url">ссылка на изображение</param>
        /// <param name="alt">альтернативное название</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="border">бордюр</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImage(this HtmlHelper html, string url, string alt, int width, int height, int border)
        {
            return new HtmlString(GeneralMethods.Image(url, width, height, alt, border));
        }

        /// <summary>ХТМЛ разметка для ссылки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">ссылка</param>
        /// <param name="name">имя</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripActionLink(this HtmlHelper html, string UrlAction, string name)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = name;
            result.Append(_result.ToString());
            return new HtmlString(result.ToString());
        }
               
    }
}
