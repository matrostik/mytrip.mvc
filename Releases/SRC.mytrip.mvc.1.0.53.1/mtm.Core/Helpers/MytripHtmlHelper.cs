/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    /// <summary>Основной набор хелперов для работы
    /// </summary>
    public static class MytripHtmlHelper
    {
        #region MytripImageForAbstract   ??????
        /// <summary>Файл изображения для краткого описания
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="image">ссылка на изображение</param>
        /// <param name="width">ширина изображения</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripImageForAbstract(this HtmlHelper html, string image, int width)
        {
            return new HtmlString(GeneralMethods.ImageForAbstract(image, width));
        }
        public static HtmlString MytripImageForAbstract2(this HtmlHelper html, string image, int width)
        {
            return new HtmlString(GeneralMethods.ImageForAbstract2(image, width));
        }
        #endregion

        #region LabelFor
        /// <summary>Создание html разметки &lt; label for='{target}' &gt; {text} &lt; /label &gt;
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="target">label for='{target}'</param>
        /// <param name="text">отоброжаемый текст</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString LabelFor(this HtmlHelper html, string target, string text)
        {
            string result = string.Format("<label for='{0}'>{1}</label>", target, text);
            return new HtmlString(result);
        }
        #endregion

        #region MytripPageTitle
        /// <summary>Титл страницы 
        /// CoreSetting.NameTitlePage() = "CMS mytrip.mvc ({0})";
        /// UsersSetting.applicationName() = "www.mysite.com";
        /// _char = "/";
        /// CMS mytrip.mvc (www.mysite.com)/{title}
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="title">Титл страницы{_char}{title}</param>
        /// <param name="_char">Титл страницы{_char}{title}</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripPageTitle(this HtmlHelper html, string title, string _char)
        {
            string domain = CoreSetting.applicationName();
            string[] _domain = CoreSetting.applicationName().Split('.');
            if (_domain.Length == 3)
                domain = _domain[1] + "." + _domain[2];
            else if (_domain.Length == 3)
                domain = _domain[0] + "." + _domain[1];
            string _result = string.Format(CoreSetting.NameTitlePage(), domain) + _char + title;
            return new HtmlString(_result);
        }
        public static HtmlString MytripPageTitle(this HtmlHelper html, string title)
        {
            string domain = CoreSetting.applicationName();
            string[] _domain = CoreSetting.applicationName().Split('.');
            if(_domain.Length==3)
            domain = _domain[1]+"."+_domain[2];
            else if (_domain.Length == 3)
                domain = _domain[0] + "." + _domain[1];
            string _result = string.Format(CoreSetting.NameTitlePage(), domain) + title;
            return new HtmlString(_result);
        }
        #endregion

        #region MytripButton
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

        /// <summary>Оформление &lt; input type="submit" value="value"/ &gt;
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

        /// <summary>Оформление &lt; input type="submit" id="id"/ &gt;
        /// </summary>
        /// <param name="html">this HtmlHelper</param>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="id">индентификатор</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripButton(this HtmlHelper html, string value, bool warning, string id, string _float)
        {
            return new HtmlString(GeneralMethods.Button(value, warning, id, _float));
        }
        #endregion

        #region MytripImgInput
        /// <summary>Оформление &lt;input type="image" value="value" class="_class"/ &gt;
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

        /// <summary>Оформление &lt; input type="image" value="value" name="name" class="_class"/ &gt;
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
            return new HtmlString(GeneralMethods.ImgInput(src, value, name, _class));
        }
        #endregion

        #region MytripImageLink
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
            TagBuilder result = new TagBuilder("a");
            result.MergeAttribute("href", UrlAction);
            result.InnerHtml = GeneralMethods.Image(src, width, height, alt, border);
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
            TagBuilder result = new TagBuilder("a");
            result.MergeAttribute("href", UrlAction);
            if (!String.IsNullOrEmpty(cssClass))
                result.InnerHtml = GeneralMethods.Image(src, alt, cssClass);
            else
                result.InnerHtml = GeneralMethods.Image(src, alt);
            return new HtmlString(result.ToString());
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
            TagBuilder result = new TagBuilder("a");
            result.MergeAttribute("href", UrlAction);
            result.MergeAttribute("id", id);
            if (!String.IsNullOrEmpty(cssClass))
                result.InnerHtml = GeneralMethods.Image(src, alt, cssClass);
            else 
                result.InnerHtml = GeneralMethods.Image(src, alt); 
            return new HtmlString(result.ToString()); ;
        }
        #endregion

        #region MytripImage
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
                result = GeneralMethods.Image(url, alt, cssClass);
            else 
                result = GeneralMethods.Image(url, alt);
            return new HtmlString(result.ToString());
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
        #endregion

        #region MytripActionLink
        /// <summary>ХТМЛ разметка для ссылки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">ссылка</param>
        /// <param name="name">имя</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripActionLink(this HtmlHelper html, string UrlAction, string name)
        {
            TagBuilder result = new TagBuilder("a");
            result.MergeAttribute("href", UrlAction);
            result.InnerHtml = name;
            return new HtmlString(result.ToString());
        }
        #endregion
    }
}
