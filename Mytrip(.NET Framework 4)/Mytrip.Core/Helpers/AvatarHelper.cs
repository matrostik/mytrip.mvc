using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Mytrip.Core.Helpers
{
   public static class AvatarHelper
    {
        public static string Avatar(this HtmlHelper html, string email)
       {
           if (UsersSetting.unlockGravatar)
           {
               return GetImageTag(GetGravatar(email));
           }
           else { return string.Empty; }
        }
        public static string Avatar(this HtmlHelper html, string email,  object htmlAttributes)
        {if (UsersSetting.unlockGravatar)
           {
               return GetImageTag(GetGravatar(email), htmlAttributes);
           }
        else { return string.Empty; }
        }
       /*PRIVATE*/
        private static string GetImageTag(string source)
        {
            return GetImageTag(source, null);
        }
        private static string GetImageTag(string source, object htmlAttributes)
        {

            IDictionary<string, object> attributes =
                (htmlAttributes == null
                    ? new RouteValueDictionary()
                    : new RouteValueDictionary(htmlAttributes));

            string returnVal = "<img src=\"{0}\"";

            foreach (string key in attributes.Keys)
            {
                returnVal += string.Format("{0}=\"{1}\"", key, attributes[key]);
            }
            returnVal += " />";
            return string.Format(returnVal, source);
        }
        private static string GetGravatar(string email)
        {
            return string.Format("http://www.gravatar.com/avatar/{0}", EncryptMD5(email));
        }
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
            else { return string.Empty;}
        }
    }
}


