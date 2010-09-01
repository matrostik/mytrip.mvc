/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Linq;
using System.Xml.Linq;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// Методы для работы с файлом About.xml
    /// </summary>
   internal class AboutRepository
   {
       #region Методы для работы с файлом About.xml
       // **********************************************
       // Методы для работы с файлом About.xml
       // **********************************************

       /// <summary>Вывод текста из файла About.xml
       /// </summary>
       /// <param name="culture">текущая культура</param>
       /// <returns>возвращает string</returns>
       internal string GetAbout(string culture)
       {
           XDocument doc = XDocument.Load(GeneralMethods.MytripConfigurationDirectory("About"));
           var core = doc.Root.Elements("About").FirstOrDefault(x => x.Attribute("culture").Value == culture.ToLower());
           return core.Element("Body").Value.Replace("[_", "<").Replace("_]", ">");
       }
       /// <summary>Сохранение информации в файл About.xml
       /// </summary>
       /// <param name="body">текст</param>
       /// <param name="culture">текущая культура</param>
       internal void EditAbout(string body, string culture)
       {
           string absolutDirectory = GeneralMethods.MytripConfigurationDirectory("About");
           body = body.Replace("<","[_").Replace(">","_]");
           XDocument doc = XDocument.Load(absolutDirectory);
           var core = doc.Root.Elements("About").FirstOrDefault(x => x.Attribute("culture").Value == culture.ToLower());
           core.SetElementValue("Body", body);
           doc.Save(absolutDirectory);
       }

       //****************** E N D **********************
       #endregion
    }
}
