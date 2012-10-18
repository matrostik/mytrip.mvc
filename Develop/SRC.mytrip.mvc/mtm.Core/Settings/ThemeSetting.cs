/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace mtm.Core.Settings
{
    /// <summary>Данные из mtm.Config.xml, Списки тем и текущая тема
    /// </summary>
     public static class ThemeSetting
    {
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        /// <summary>Тема по умолчанию
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_ts_defaulttheme")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string defaultTheme()
        {
            return GeneralMethods.MytripCache("mtm_ts_defaulttheme", "theme", "defaultTheme", false, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Выбор тем из списка (true = разрешен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_ts_unlocktheme")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockTheme()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_ts_unlocktheme", "theme", "unlockTheme", false, null, 36000, CacheItemPriority.High).ToString());
        }//<add name="unlockWrap" value="False"/>
        internal static bool unlockWrap()
        {
            string absolutDirectory = GeneralMethods.GetPath("Theme", theme(), "wrap");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                return false;
            else
            return bool.Parse(GeneralMethods.MytripCache("mtm_ts_unlockwrap", "theme", "unlockWrap", false, null, 36000, CacheItemPriority.High).ToString());
        }
        internal static bool _unlockWrap()
        {
                return bool.Parse(GeneralMethods.MytripCache("mtm_ts_unlockwrap", "theme", "unlockWrap", false, null, 36000, CacheItemPriority.High).ToString());
        }
        //****************** E N D **********************
        #endregion

        #region Списки тем и текущая тема
        // **********************************************
        // Списки тем и текущая тема
        // **********************************************

        /// <summary>Текущая тема
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string theme()
        {
            string theme = defaultTheme();
            if (unlockTheme())
            {
                theme = (HttpContext.Current.Request.Cookies["mtTheme"] != null)
                    ? HttpContext.Current.Request.Cookies["mtTheme"].Value
                    :((HttpContext.Current.Session["theme"] != null)
                    ? HttpContext.Current.Session["theme"].ToString()
                    : defaultTheme());
                if (theme!=defaultTheme() && allThemeMassive().FirstOrDefault(x=>x==theme)==null)
                    theme = defaultTheme();
            }
            return theme;
        }
        public static string wrap()
        {
            if(unlockWrap())
                return "mtm.Core.Wrap.YesWrapRes";
            else
                return "mtm.Core.Wrap.NoWrapRes";
        }
        /// <summary>Список тем в массиве
        /// </summary>
        /// <returns>возвращает string[]</returns>
        internal static string[] allThemeMassive()
        {
            string absolutDirectory = GeneralMethods.GetPath("Theme");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            DirectoryInfo[] result = _absolutDirectory.GetDirectories();
            int count = result.Count();
            int namber = 0;
            string[] _result = new string[count];
            foreach (DirectoryInfo x in result)
            {
                _result[namber] = x.Name;
                namber++;
            }
            return _result;
        }

        /// <summary>Список тем в словаре
        /// </summary>
        /// <returns>возвращает IDictionary &lt; string, string &gt;</returns>
        internal static IDictionary<int, string> allThemeDictionary()
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            int key = 1;
            foreach (string x in allThemeMassive()) {
                result.Add(key, x); key++;
            }
            return result;
        }

        //****************** E N D **********************
        #endregion
    }
}
