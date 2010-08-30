﻿/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml.Linq;
using Mytrip.Mvc.StylesTable;

namespace Mytrip.Mvc.Settings
{
    /// <summary>Вспомогательные методы для работы как ядра так и модулей
    /// </summary>
     public class GeneralMethods
    {
        #region Сброс кеша
        // **********************************************
        // Сброс кеша
        // **********************************************

        /// <summary>Удаление кеша
        /// </summary>
        /// <param name="key">ключ кеша</param>
        public static void MytripCacheRemove(string key)
        {
            if (HttpContext.Current.Cache[key] != null) HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>Удаление кеша
        /// </summary>
        /// <param name="key">ключ кеша</param>
        /// <param name="culture">проверка на культуру (true проверка включена)</param>
        public static void MytripCacheRemove(string key, bool culture)
        {
            string _culture = culture ? LocalisationSetting.culture() : string.Empty;
            if (HttpContext.Current.Cache[key + _culture] != null)
                HttpContext.Current.Cache.Remove(key + _culture);
        }

        //****************** E N D **********************
        #endregion

        #region Методы для работы с MytripConfiguration.xml
        // **********************************************
        // Методы для работы с MytripConfiguration.xml
        // **********************************************

        /// <summary>Абсолютная дирректория для файла MytripConfiguration.xml в папке Configuration
        /// /Configuration/MytripConfiguration.xml</summary>
        /// <returns>возвращает string</returns>
        public static string MytripConfigurationDirectory()
        {
            return HttpContext.Current.Server.MapPath("/Configuration/MytripConfiguration.xml");
        }

        /// <summary>Абсолютная дирректория для файлов .xml в папке Configuration
        /// /Configuration/{имя файла}.xml</summary>
        /// <param name="filename">имя файла (/Configuration/{имя файла}.xml)</param>
        /// <returns>возвращает string</returns>
        public static string MytripConfigurationDirectory(string filename)
        {
            return HttpContext.Current.Server.MapPath("/Configuration/" + filename + ".xml");
        }

        /// <summary>Метод для кеширования данных из MytripConfiguration.xml
        /// </summary>
        /// <param name="key">ключ кеша</param>
        /// <param name="element">имя элемента</param>
        /// <param name="attribute">имя атрибута</param>
        /// <param name="culture">проверка на культуру (true проверка включена)</param>
        /// <param name="absolutSek">абсолютные секунды (для отключения введите null)</param>
        /// <param name="spanSek">скользящие секунды (для отключения введите null)</param>
        /// <param name="priority">приоритет кеша</param>
        /// <returns>возвращает object</returns>
        public static object MytripCache(string key, string element, string attribute, bool culture, int? absolutSek, int? spanSek, CacheItemPriority priority)
        {
            string _culture = culture ? LocalisationSetting.culture() : string.Empty;
            if (HttpContext.Current.Cache[key + _culture] == null)
            {
                TimeSpan _spanSek = spanSek == null ? TimeSpan.Zero : TimeSpan.FromSeconds((int)spanSek);
                DateTime _absolutSek = absolutSek == null ? DateTime.MaxValue : DateTime.Now.AddSeconds((int)absolutSek);
                HttpContext.Current.Cache.Insert(key + _culture, MytripConfigurationValue(element, attribute, culture),
                null, _absolutSek, _spanSek, priority, null);
            }
            return HttpContext.Current.Cache[key + _culture];
        }

        /// <summary>Метод получения значения из MytripConfiguration.xml
        /// </summary>
        /// <param name="element">имя элемента</param>
        /// <param name="attribute">имя атрибута</param>
        /// <returns>возвращает string</returns>
        public static string MytripConfigurationValue(string element, string attribute)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
            return core.Attribute("value").Value;
        }

        /// <summary>Метод получения значения из MytripConfiguration.xml
        /// </summary>
        /// <param name="element">имя элемента</param>
        /// <param name="attribute">имя атрибута</param>
        /// <param name="culture">проверка на культуру (true проверка включена)</param>
        /// <returns>возвращает string</returns>
        public static string MytripConfigurationValue(string element, string attribute, bool culture)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            if (culture)
            {
                string localization = LocalisationSetting.culture();
                var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
                var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == localization.ToLower());
                return _core.Attribute("name").Value;
            }
            else
            {
                var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
                return core.Attribute("value").Value;
            }
        }

        //****************** E N D **********************
        #endregion

        #region Методы для HtmlHelpers
        // **********************************************
        // Методы для HtmlHelpers
        // **********************************************

        /// <summary>ХТМЛ разметка для акардиона
        /// </summary>
        /// <param name="title">заголовок</param>
        /// <param name="content">контент</param>
        /// <returns></returns>
        public static string Accordion(string title, string content)
        {
            return string.Format("<div class='acc'><div class='accT'>{0}</div><div class='accVC'>{1}</div></div>",title,content);
        }

        /// <summary>ХТМЛ разметка для акардиона
        /// </summary>
        /// <param name="accordion">наличие контента</param>
        /// <param name="title">заголовок</param>
        /// <param name="content">контент</param>
        /// <returns></returns>
        public static string Accordion(bool accordion, string title, string content)
        {
            return accordion
                ? string.Format("<div class='acc'><div class='accT'>{0}</div><div class='accVC'>{1}</div></div>", title, content)
                : string.Format("<div class='acc'><div class='noaccT'>{0}</div></div>", title);
        
        }

        /// <summary>Стиль таблицы
        /// </summary>
        /// <param name="column">количество столбцов</param>
        /// <param name="style"></param>
        /// <param name="tr"></param>
        /// <param name="width"></param>
        /// <param name="content"></param>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <param name="line"></param>
        /// <param name="line2"></param>
        /// <param name="outtr"></param>
        /// <param name="outline"></param>
        /// <param name="finaltr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="styletable">номер стиля</param>
        /// <returns></returns>
        public static string StyleTable(int column, int style, int tr, int width, string content, int count, int count2, int line, int line2, out int outtr, out int outline, out string finaltr, out string start, out string end, out string styletable)
        {
            outtr = 0; outline = 0; finaltr = string.Empty; start = string.Empty; end = string.Empty; styletable = string.Empty; string result = string.Empty;
            if (style == 1)
                result = StyleTableClass.StyleTable1(column, tr, width, content, count, count2, line, line2, out outtr, out outline, out finaltr, out start, out end, out styletable);
            else if (style == 2)
                result = StyleTableClass.StyleTable2(column, width, content, count, count2, line, line2, out outline, out styletable);
            return result;
        }

        /// <summary>Рейтинг
        /// </summary>
        /// <param name="approvedvotes">показ голосования (true включен)</param>
        /// <param name="active">активность ссылок (true включен)</param>
        /// <param name="totalvote">текущая оценка</param>
        /// <param name="votescount">количество голосов</param>
        /// <returns></returns>
        public static string CoreRating(bool approvedvotes, bool active, double totalvote, int votescount)
        {
            StringBuilder result = new StringBuilder();
            if (approvedvotes)
            {
                if (votescount != -1)
                    result.AppendLine(string.Format(CoreLanguage.score_votes, totalvote.ToString("N2"), votescount.ToString()));

                for (int rate = 0; rate < 5; rate++)
                {
                    double rate_125 = rate + 0.125;
                    double rate_375 = rate + 0.375;
                    double rate_625 = rate + 0.625;
                    double rate_875 = rate + 0.875;
                    string num = string.Empty;
                    if (totalvote > rate_125 && totalvote < rate_375)
                        num = "25";
                    else if (totalvote > rate_375 && totalvote < rate_625)
                        num = "50";
                    else if (totalvote > rate_625 && totalvote < rate_875)
                        num = "75";
                    else if (totalvote < rate_875)
                        num = "100";
                    if (active)
                    {   
                        TagBuilder input = new TagBuilder("input");
                        input.MergeAttribute("type", "submit");
                        input.MergeAttribute("value", (rate + 1).ToString());
                        input.MergeAttribute("name", "vote");
                        input.MergeAttribute("id", string.Format("vote{0}", (rate + 1)));                        
                        input.MergeAttribute("style", string.Format("background:url('/Theme/{0}/images/star{1}.png')", ThemeSetting.theme(), num));
                        input.AddCssClass("rating");
                        input.MergeAttribute("title", (rate + 1).ToString());
                        if (!HttpContext.Current.User.Identity.IsAuthenticated)
                            input.MergeAttribute("onclick", string.Format("location.href('/Account/LogOn?returnUrl={0}')", HttpContext.Current.Request.Url.AbsolutePath));
                        result.AppendLine(input.ToString());
                    }
                    else
                    {
                        TagBuilder input = new TagBuilder("img");
                        input.AddCssClass("rating");
                        input.MergeAttribute("src", string.Format("/Theme/{0}/images/star{1}.png", ThemeSetting.theme(),num));
                        result.AppendLine(input.ToString());
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>ХТМЛ разметка для Меню
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="visible"></param>
        /// <param name="warning"></param>
        /// <param name="menu"></param>
        /// <param name="drop"></param>
        /// <returns></returns>
        public static string Menu(string title, IDictionary<int, string> content, bool visible, bool warning, bool menu, bool drop)
        {
            string _drop = drop ? "D" : string.Empty;
            string _menu = menu ? "menu" : "logon";
            string _visible = (warning && !visible) ? "W" : string.Empty;
            if (visible)
                _visible = "V";
            StringBuilder _result = new StringBuilder();
            bool count = false;
            if (content != null)
            {
                foreach (var x in content)
                {
                    _result.Append(string.Format("<div>{0}</div>", x.Value));
                    count = true;
                }
            }
            string _content = string.Empty;
            if (count)
                _content = string.Format("<div class=\"{0}UL\">{1}</div>", _menu, _result);
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("<div class=\"{0}I\">", _menu));
            result.Append(string.Format("<div class=\"{0}C{1}{2}\">", _menu, _visible, _drop));
            result.Append(title);
            result.Append(string.Format("</div>{0}</div>", _content));
            return result.ToString();
        }

        /// <summary>Файл изображения для краткого описания
        /// </summary>
        /// <param name="image">ссылка на изображение</param>
        /// <param name="width">ширина картинки</param>
        /// <returns>возвращает string</returns>
        public static string ImageForAbstract(string image, int width)
        {
            if (image != null && image.Contains("src"))
            {
                image = image.Remove(0, image.IndexOf("src"));
                image = image.Remove(0, (image.IndexOf("\"") + 1));
                image = image.Remove(image.IndexOf("\""));
                string title = image.Remove(0, (image.LastIndexOf("/") + 1));
                title = title.Remove(title.LastIndexOf("."));
                return string.Format("<img src='{0}' alt='{1}' title='{1}' class='imgabstract' style='width:{2}px;'/>", image, title, width);
             }
            else
                return string.Empty;
        }

        /// <summary>Преобразование URL в массив
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public static string[] UrlDictionary(string urlPath)
        {
            string[] _urlPath = urlPath.Split('/');
            return _urlPath;

        }

        /// <summary>Изображение
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="alt"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        public static string Image(string url, int width, int height, string alt, int border)
        {
            if (!url.Contains("Content"))
            {
                url =string.Concat("/Theme/",ThemeSetting.theme(),url);
            }
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = string.Format("border-width: {0}px; width: {1}px; height: {2}px;", border, width, height);
            if (width == 0 && height > 0)
                style = string.Format("border-width: {0}px; height: {1}px;", border, height);
            if (width > 0 && height == 0)
                style = string.Format("border-width: {0}px; width: {1}px;", border, width);
            if (width == 0 && height == 0)
                style = string.Format("border-width: {0}px;", border);
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("style", style);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }

        /// <summary>Изображение
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="alt"></param>
        /// <param name="border"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static string Image(string url, int width, int height, string alt, int border,bool theme)
        {
            if (theme && !url.Contains("Content"))
            {
                url = string.Concat("/Theme/", ThemeSetting.theme(), url);
            }
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = string.Format("border-width: {0}px; width: {1}px; height: {2}px;", border, width, height);
            if (width == 0 && height > 0)
                style = string.Format("border-width: {0}px; height: {1}px;", border, height);
            if (width > 0 && height == 0)
                style = string.Format("border-width: {0}px; width: {1}px;", border, width);
            if (width == 0 && height == 0)
                style = string.Format("border-width: {0}px;", border);
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("style", style);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }

        /// <summary>Изображение
        /// </summary>
        /// <param name="url"></param>
        /// <param name="alt"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static string Image(string url, string alt, string cssClass)
        {
            if (!url.Contains("Content"))
            {
                url = string.Concat("/Theme/", ThemeSetting.theme(), url);
            }
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            _result.AddCssClass(cssClass);
            result.Append(_result.ToString());
            return result.ToString();
        }

        /// <summary>Изображение
        /// </summary>
        /// <param name="url"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static string Image(string url, string alt)
        {
            if (!url.Contains("Content"))
            {
                url = string.Concat("/Theme/", ThemeSetting.theme(), url);
            }
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }

        /// <summary>Изображение флага
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string Flag(string culture)
        {
            culture = culture.ToLower();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", string.Format("/Theme/{0}/images/{1}.png",ThemeSetting.theme(), culture));
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", culture);
            img.MergeAttribute("title", culture);
            return img.ToString();
        }

        /// <summary>Изображение глобуса
        /// </summary>
        /// <param name="show"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string Globe(bool show, string title)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", string.Format("/Theme/{0}/images/globe.png",ThemeSetting.theme()));
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", "all languages");
            img.MergeAttribute("title", title);
            return img.ToString();
        }

        /// <summary>Изображение ключа
        /// </summary>
        /// <param name="show"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string Keys(bool show, string title)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", string.Format("/Theme/{0}/images/Keys.png", ThemeSetting.theme()));
            img.MergeAttribute("style", "border-width:0px;width:20px");
            img.MergeAttribute("alt", "for registered users");
            img.MergeAttribute("title", title);
            return img.ToString();
        }

        /// <summary>Отображение статического текста для пейджинга
        /// </summary>
        /// <param name="Text">статический текст</param>
        /// <returns></returns>
        public static string pagerStaticText(string Text)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class=\"pagerStatic\">");
            TagBuilder _result = new TagBuilder("a");
            _result.AddCssClass("pagerStaticText");
            _result.InnerHtml = Text;
            result.AppendLine(_result.ToString());
            result.AppendLine("</div>");
            return result.ToString();
        }

        /// <summary>Оформление ссылки как кнопки
        /// </summary>
        /// <param name="link">ссылка</param>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает string</returns>
        public static string Button(string link, string value, bool warning, string _float)
        {
            string _warning = warning ? "W" : string.Empty;
            string _float2 = (!String.IsNullOrEmpty(_float) && _float == "right") ? "R" : "L";
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"buttonA" + _warning + _float2 + "\">");
            TagBuilder _result = new TagBuilder("a");
            if (!String.IsNullOrEmpty(link))
                _result.MergeAttribute("href", link);
            else
                _result.MergeAttribute("style", "cursor:default;");
            _result.InnerHtml = value;
            result.Append(_result.ToString());
            result.Append("</div>");
            return result.ToString();
        }

        /// <summary>Оформление [input type="submit"/]
        /// </summary>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает string</returns>
        public static string Button(string value, bool warning, string _float)
        {
            string _warning = warning ? "W" : string.Empty;
            string _float2 = (!String.IsNullOrEmpty(_float) && _float == "right") ? "R" : "L";
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"buttonI" + _warning + _float2 + "\">");
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass("button");
            _result.MergeAttribute("type", "submit");
            _result.MergeAttribute("value", value);
            result.Append(_result.ToString());
            result.Append("</div>");
            return result.ToString();
        }

        /// <summary>Оформление [input type="submit" id="id"/]
        /// </summary>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="id">индентификатор</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает string</returns>
        public static string Button(string value, bool warning, string id, string _float)
        {
            string _warning = warning ? "W" : string.Empty;
            string _float2 = (!String.IsNullOrEmpty(_float) && _float == "right") ? "R" : "L";
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"buttonI" + _warning + _float2 + "\">");
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass("button");
            _result.MergeAttribute("type", "submit");
            _result.MergeAttribute("value", value);
            _result.MergeAttribute("id", id);
            result.Append(_result.ToString());
            result.Append("</div>");
            return result.ToString();
        }

        /// <summary>Оформление [input type="submit" id="id" name="name"/]
        /// </summary>
        /// <param name="value">текст</param>
        /// <param name="warning">выделение цветом (true - выделен)</param>
        /// <param name="id">индентификатор</param>
        /// <param name="name">имя</param>
        /// <param name="_float">положение "left" "right"</param>
        /// <returns>возвращает string</returns>
        public static string Button(string value, bool warning, string id, string name, string _float)
        {
            string _warning = warning ? "W" : string.Empty;
            string _float2 = (!String.IsNullOrEmpty(_float) && _float == "right") ? "R" : "L";
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"buttonI" + _warning + _float2 + "\">");
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass("button");
            _result.MergeAttribute("type", "submit");
            _result.MergeAttribute("value", value);
            _result.MergeAttribute("id", id);
            _result.MergeAttribute("name", name);
            result.Append(_result.ToString());
            result.Append("</div>");
            return result.ToString();
        }

        /// <summary>Оформление [input type="image" value="value" class="_class"/]
        /// если ссылка не содержит "Content" перенаправление в
        /// папку текущей темы
        /// </summary>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="value">значение</param>
        /// <param name="_class">css класс</param>
        /// <returns>возвращает string</returns>
        public static string ImgInput(string src, string value, string _class)
        {
            if (!src.Contains("Content"))
            {
                src = "/Theme/" + ThemeSetting.theme() + src;
            }
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass(_class);
            _result.MergeAttribute("src", src);
            _result.MergeAttribute("type", "image");
            _result.MergeAttribute("value", value);
             return _result.ToString();
        }
        /// <summary>Оформление [input type="image" value="value" class="_class"/]
        /// если ссылка не содержит "Content" перенаправление в
        /// папку текущей темы
        /// </summary>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="value">значение</param>
        /// <param name="_class">css класс</param>
        /// <param name="height">height</param>
        /// <returns>возвращает string</returns>
        public static string ImgInput(string src, string value, string _class, int height)
        {
            if (!src.Contains("Content"))
            {
                src = "/Theme/" + ThemeSetting.theme() + src;
            }
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass(_class);
            _result.MergeAttribute("src", src);
            _result.MergeAttribute("type", "image");
            _result.MergeAttribute("value", value);
            _result.MergeAttribute("style", "height:" + height + "px;");
            return _result.ToString();
        }
        /// <summary>Оформление [input type="image" value="value" name="name" class="_class"/]
        /// если ссылка не содержит "Content" перенаправление в
        /// папку текущей темы
        /// </summary>
        /// <param name="src">ссылка на изображение</param>
        /// <param name="value">значение</param>
        /// <param name="name">name</param>
        /// <param name="_class">css класс</param>
        /// <returns>возвращает HtmlString</returns>
        public static string ImgInput(string src, string value, string name, string _class)
        {
            if (!src.Contains("Content"))
            {
                src = "/Theme/" + ThemeSetting.theme() + src;
            }
            TagBuilder _result = new TagBuilder("input");
            _result.AddCssClass(_class);
            _result.MergeAttribute("type", "image");
            _result.MergeAttribute("src", src);
            _result.MergeAttribute("value", value);
            _result.MergeAttribute("name", name);
            return _result.ToString();
        }

        //****************** E N D **********************
        #endregion

        #region Методы для работы со стоками
        // **********************************************
        // Методы для работы со стоками
        // **********************************************

        /// <summary>Перевод знаков на латиницу
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
        public static string DecodingString(string a)
        {
            a = a.Trim();
            #region знаки препинания

            a = a.Replace(" ", "_").Replace("!", "_").Replace(",", "_").Replace(".", "_").Replace("?", "_")
                 .Replace(":", "_").Replace("/", "_").Replace("|", "_").Replace("#", "_").Replace("%", "_")
                 .Replace("<", "_").Replace(">", "_").Replace("$", "_").Replace("&", "_").Replace("*", "_")
                 .Replace(">", "_").Replace("-", "_").Replace("=", "_").Replace("+", "_").Replace("`", "_")
                 .Replace("~", "_").Replace("@", "_").Replace("^", "_").Replace("{", "_").Replace("}", "_")
                 .Replace("}", "_").Replace("?", "_").Replace("*", "_").Replace("%", "_").Replace(@"\", "_")
                 .Replace("\"", "_");

            #endregion

            #region иврит

            a = a.Replace("א", "a").Replace("ב", "b").Replace("ג", "g").Replace("ד", "d").Replace("ה", "a")
                 .Replace("ו", "v").Replace("ז", "z").Replace("ח", "h").Replace("ט", "t").Replace("י", "i")
                 .Replace("כ", "k").Replace("ך", "k").Replace("ל", "l").Replace("מ", "m").Replace("ם", "m")
                 .Replace("נ", "n").Replace("ן", "n").Replace("ס", "s").Replace("ע", "e").Replace("פ", "p")
                 .Replace("ף", "f").Replace("צ", "c").Replace("ץ", "c").Replace("ק", "k").Replace("ר", "r")
                 .Replace("ש", "sh").Replace("ת", "t");

            #endregion

            #region русский

            a = a.Replace("А", "A").Replace("а", "a").Replace("Б", "B").Replace("б", "b").Replace("В", "V")
                 .Replace("в", "v").Replace("Г", "G").Replace("г", "g").Replace("Д", "D").Replace("д", "d")
                 .Replace("Е", "E").Replace("е", "e").Replace("Ё", "E").Replace("ё", "e").Replace("Ж", "J")
                 .Replace("ж", "j").Replace("З", "Z").Replace("з", "z").Replace("И", "I").Replace("и", "i")
                 .Replace("Й", "Y").Replace("й", "y").Replace("К", "K").Replace("к", "k").Replace("Л", "L")
                 .Replace("л", "l").Replace("М", "M").Replace("м", "m").Replace("Н", "N").Replace("н", "n")
                 .Replace("О", "O").Replace("о", "o").Replace("П", "P").Replace("п", "p").Replace("Р", "R")
                 .Replace("р", "r").Replace("С", "S").Replace("с", "s").Replace("Т", "T").Replace("т", "t")
                 .Replace("У", "U").Replace("у", "u").Replace("Ф", "F").Replace("ф", "f").Replace("Х", "H")
                 .Replace("х", "h").Replace("Ц", "C").Replace("ц", "c").Replace("Ч", "Ch").Replace("ч", "ch")
                 .Replace("Ш", "Sh").Replace("ш", "sh").Replace("Щ", "Sh").Replace("щ", "sh").Replace("Ъ", "")
                 .Replace("ъ", "").Replace("Ы", "Y").Replace("ы", "y").Replace("Ь", "").Replace("ь", "")
                 .Replace("Э", "E").Replace("э", "e").Replace("Ю", "Yu").Replace("ю", "yu").Replace("Я", "Ya")
                 .Replace("я", "ya");

            #endregion

            return a;

        }

        /// <summary>Подсветка результатов поиска
        /// </summary>
        /// <param name="original"></param>
        /// <param name="findText"></param>
        /// <returns></returns>
        public static string ReplaceString(string original, string findText)
        {
            string insert1 = "<span class='replasesearch'>"; string insert2 = "</span>";
            int at1 = 0;
            while (true)
            {
                at1 = original.IndexOf(findText, at1, original.Length - at1, StringComparison.OrdinalIgnoreCase);
                if (at1 == -1)
                    break;
                original = original.Insert(at1, insert1).Insert(at1 + findText.Length + insert1.Length, insert2);
                at1 += insert1.Length + insert2.Length;
            }
            return original;
        }

        /// <summary>Кодировка спец символов
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string DecodingSearch(string a)
        {
            return a.Replace("?", "[x_1_x]").Replace("<", "[x_2_x]").Replace(">", "[x_3_x]").Replace("*", "[x_4_x]")
                    .Replace("%", "[x_5_x]").Replace("&", "[x_6_x]").Replace(@"\", "[x_7_x]").Replace("|", "[x_8_x]")
                    .Replace(":", "[x_9_x]").Replace("\"", "[x_10_x]").Replace(".", "[x_11_x]").Replace("/", "[x_12_x]");
        }

        /// <summary>Расшифровка спец символов
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string UndecodingSearch(string a)
        {
            return a.Replace("[x_1_x]", "?").Replace("[x_2_x]", "<").Replace("[x_3_x]", ">")
            .Replace("[x_4_x]", "*").Replace("[x_5_x]", "%").Replace("[x_6_x]", "&")
            .Replace("[x_7_x]", @"\").Replace("[x_8_x]", "|").Replace("[x_9_x]", ":")
            .Replace("[x_10_x]", "\"").Replace("[x_11_x]", ".").Replace("[x_12_x]", "/");
        }

        /// <summary>Чистка HTML
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CleanHtmlCode(string code)
        {
            while (code.StartsWith("<P>&nbsp;</P>")) { code = code.Remove(0, 13); }
            while (code.EndsWith("<P>&nbsp;</P>")) { code = code.Remove(code.Length - 13, 13); }
            code = code.Replace("<P>&nbsp;</P>", "<br/>").Replace("<P>", "").Replace("</P>", "<br/>");
            while (code.StartsWith("<p>&nbsp;</p>")) { code = code.Remove(0, 13); }
            while (code.EndsWith("<p>&nbsp;</p>")) { code = code.Remove(code.Length - 13, 13); }
            code = code.Replace("<p>&nbsp;</p>", "<br/>").Replace("<p>", "").Replace("</p>", "<br/>");
            return code;
        }

        //****************** E N D **********************
        #endregion

        #region Парсинг по протоколу HTTP
        // **********************************************
        // Парсинг по протоколу HTTP
        // **********************************************

        /// <summary>Парсинг по протоколу HTTP GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string HttpGetParsing(string url, string culture)
        {
            try
            {
                string retVal = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                request.Headers.Add("Accept-Language:" + culture);
                request.Headers.Add(HttpRequestHeader.Cookie, "myTripCulture=" + culture.ToLower());
                request.Headers.Add("Accept-Encoding:gzip, deflate");
                request.Accept = "*/*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    retVal = streamReader.ReadToEnd();
                }
                response.Close();
                return retVal;
            }
            catch { return string.Empty; }
        }

        //****************** E N D **********************
        #endregion
    }

}
