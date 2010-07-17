using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Xml.Linq;
using System.Web;
using Mytrip.Mvc.Repository;

namespace Mytrip.Gismeteo
{
   public class GismeteoSettings
   {
       CoreSetting _core;
       public CoreSetting core
       {
           get
           {
               if (_core == null)
                   _core = new CoreSetting();
               return _core;
           }
       }
       string moduleName = "Mytrip.Gismeteo";
       string moduleVersion = "1.0.0.0";
       string moduleStatus = "Alfa";
       string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
       string culture = HttpContext.Current.Session["culture"].ToString();
       public void CreateGismeteoConfiguration()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var modules = _doc.Root.Elements("installModules");
           try
           {
               modules.Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
           }
           catch
           {
           }
           var module = _doc.Root.Element("installModules");
           XElement artmodule = new XElement("add",
               new XAttribute("name", moduleName),
               new XAttribute("version", moduleVersion),
               new XAttribute("status", moduleStatus));
           module.Add(artmodule);
           try
           {
               _doc.Root.Elements(moduleName).Remove();
           }
           catch
           {
           }
           XElement article = new XElement(moduleName,
               new XElement("add", new XAttribute("name", "unlockGismeteo"), new XAttribute("value", "True")),
               new XElement("add", new XAttribute("name", "nameGismeteo"),
                   new XElement("add", new XAttribute("name", "Weather"), new XAttribute("value", "en-us")),
                   new XElement("add", new XAttribute("name", "Погода"), new XAttribute("value", "ru-ru"))));
           _doc.Root.Add(article);
           _doc.Save(_absolutDirectory);
       }
       public string NameGismeteoPage()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameGismeteo");
           var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
           return _core.Attribute("name").Value;
       }
       public bool unlockGismeteo()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockGismeteo");
           return bool.Parse(core.Attribute("value").Value);
       }
       public string connectionString()
       {
           string result = string.Empty;
           if (core.Provider() == "MSSQL")
           {
               result = core.connectionStringMSSQL("Repository.DataEntities.MssqlGismeteo");
           }
           else if (core.Provider() == "MYSQL")
           {
               result = core.connectionStringMYSQL("Repository.DataEntities.MysqlGismeteo");
           }
           return result;
       }
    }
}
