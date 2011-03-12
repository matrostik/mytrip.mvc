/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Разметка для аватара с использованием
    /// глобальных аватаров от gravatar
    /// </summary>
    public static class AvatarHelper
    {
        /// <summary>Изображение Аватара пользователя
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="email">Email пользователя</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString Avatar(this HtmlHelper html, string email)
        {
            if (ProfileSetting.unlockGravatar())
            {
                return GetImageTag(GetGravatar(email));
            }
            else { return new HtmlString(""); }
        }

        /// <summary>Изображение Аватара пользователя
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="htmlAttributes">хтмл аттрибуты</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString Avatar(this HtmlHelper html, string email, object htmlAttributes)
        {
            if (ProfileSetting.unlockGravatar())
            {
                return GetImageTag(GetGravatar(email), htmlAttributes);
            }
            else { return new HtmlString(""); }
        }

        /// <summary>Изображение Аватара пользователя
        /// </summary>
        /// <param name="source">ссылка gravatar</param>
        /// <returns>возвращает HtmlString</returns>
        private static HtmlString GetImageTag(string source)
        {
            return GetImageTag(source, null);
        }

        /// <summary>Изображение Аватара пользователя
        /// </summary>
        /// <param name="source">ссылка gravatar</param>
        /// <param name="htmlAttributes">хтмл аттрибуты</param>
        /// <returns>возвращает HtmlString</returns>
        private static HtmlString GetImageTag(string source, object htmlAttributes)
        {

            IDictionary<string, object> attributes =
                (htmlAttributes == null
                    ? new RouteValueDictionary()
                    : new RouteValueDictionary(htmlAttributes));

            string returnVal = "<img src=\"{0}\" alt=\"gravatar\" class=\"avatar\" ";
            foreach (string key in attributes.Keys)
            {
                returnVal += string.Format("{0}=\"{1}\" ", key, attributes[key]);
            }
            returnVal += " />";
            return new HtmlString(string.Format(returnVal, source));
        }

        /// <summary>ссылка gravatar
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <returns>возвращает string</returns>
        private static string GetGravatar(string email)
        {
            return string.Format("http://www.gravatar.com/avatar/{0}?d=identicon", EncryptMD5(email));
        }

        /// <summary>Зашифровка Email пользователя
        /// </summary>
        /// <param name="Value">Email пользователя</param>
        /// <returns>возвращает string</returns>
        private static string EncryptMD5(string Value)
        {
            if (!String.IsNullOrEmpty(Value))
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] valueArray = System.Text.Encoding.ASCII.GetBytes(Value);
                valueArray = md5.ComputeHash(valueArray);
                string encrypted = "";
                for (int i = 0; i < valueArray.Length; i++)
                    encrypted += valueArray[i].ToString("x2").ToLower();
                return encrypted;
            }
            else { return string.Empty; }
        }
    }
}


