﻿/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Mytrip.Mvc.Settings
{
    /// <summary>
    /// Данные из MytripConfiguration.xml, Списки культур
    /// </summary>
     public static class LocalisationSetting
    {
        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        /// <summary>Культура по умолчанию
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ls_defaultculture")
        /// </summary>
        /// <returns>возвращает string</returns>
         internal static string defaultCulture()
        {
            return GeneralMethods.MytripCache("ls_defaultculture", "language", "defaultCulture", false,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Выбор культур из списка (true = разрешен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "ls_unlockallculture")
        /// </summary>
        /// <returns>возвращает bool</returns>
         public static bool unlockAllCulture()
        {
            return bool.Parse(GeneralMethods.MytripCache("ls_unlockallculture", "language", "unlockAllCulture", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        //****************** E N D **********************
        #endregion

        #region Списки культур
        // **********************************************
        // Списки культур
        // **********************************************

         /// <summary>текущая культура 
         /// </summary>
         /// <returns>возвращает string</returns>
         public static string culture() {
            string culture = defaultCulture();
            if (!unlockAllCulture())
                culture = defaultCulture();
            else if (HttpContext.Current.Request.Cookies["myTripCulture"] != null)
                culture = HttpContext.Current.Request.Cookies["myTripCulture"].Value;
            else if (HttpContext.Current.Session["culture"] != null)
                culture = HttpContext.Current.Session["culture"].ToString();
            else if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages[0] != null)
                culture = HttpContext.Current.Request.UserLanguages[0];
            if (culture!=defaultCulture() && !allCulture().Contains(culture))
                culture = defaultCulture();
            return culture;
        
        }
        /// <summary>Список всех культур в одну строчку
        /// </summary>
        /// <returns>возвращает string</returns>
         internal static string allCulture()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            DirectoryInfo[] result = _absolutDirectory.GetDirectories();
            string _result = string.Empty;
            bool namber = true;
            foreach (DirectoryInfo x in result)
            {
                string start = namber ? string.Empty : "|";
                if (!x.Name.Contains("bin"))
                {
                    _result += start + x.Name;
                    if (namber)
                        namber = false;
                }
            }
            return _result;
        }

        /// <summary>Список всех культур в массиве
        /// </summary>
        /// <returns>возвращает string[]</returns>
         public static string[] allCultureMassive()
        {
            return allCulture().Split('|');
        }

        /// <summary>Список всех культур в словаре
        /// </summary>
        /// <returns>возвращает IDictionary[string, string]</returns>
         internal static IDictionary<int, string> allCultureDictionary()
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            int key = 1;
            foreach (string x in allCultureMassive())
            {
                result.Add(key, x); key++;
            }
            return result;
        }

        //****************** E N D **********************
        #endregion
    }
}
