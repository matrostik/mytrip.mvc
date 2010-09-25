/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mytrip.Mvc.Settings
{
    /// <summary>
    /// Пространства имен для интеграции через глобал в RAZOR
    /// </summary>
     internal static class CustomNamespace
    {
        #region Словарь пространств имен для интеграции через глобал в RAZOR из MytripConfiguration.xml
        // **********************************************
        // Словарь пространств имен для интеграции через 
        // глобал в RAZOR из MytripConfiguration.xml
        // **********************************************

        /// <summary>Словарь пространств имен для интеграции через глобал в RAZOR из MytripConfiguration.xml
        /// </summary>
        /// <returns>возвращает IDictionary &lt; int, string &gt;</returns>
        internal static IDictionary<int, string> Namespace()
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            IDictionary<int, string> result = new Dictionary<int, string>();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("namespace").Elements("add");
            int count = 1;
            foreach (var x in core)
            {
                result.Add(count, x.Attribute("name").Value);
                count++;
            }
            return result;
        }

        //****************** E N D **********************
        #endregion
    }
}