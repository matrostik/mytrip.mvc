using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Xml.Linq;
using System.Web;
using Mytrip.Mvc.Repository;

namespace Mytrip.Rssparser
{
    public class RssparserSetting
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
        string moduleName = "Mytrip.Rssparser";
        string moduleVersion = "1.0.44.2";
        string moduleStatus = "Beta";
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        string culture = HttpContext.Current.Session["culture"].ToString();
        public void CreateRssparserConfiguration()
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
                new XElement("add", new XAttribute("name", "unlockRssparser"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "nameRssparser"),
                    new XElement("add", new XAttribute("name", "News (rss)"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Новости (rss)"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
            /*<rssparser>
    <add name="unlockRssparser" value="True" />
    <add name="nameRssparser">
      <add name="News (rss)" value="en-us" />
      <add name="Новости (rss)" value="ru-ru" />
    </add>
  </rssparser>*/
        }
        public string nameRssparser()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameRssparser");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public bool unlockRssparser()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockRssparser");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string connectionString()
        {
            string result = string.Empty;
            if (core.Provider() == "MSSQL")
            {
                result = core.connectionStringMSSQL("Repository.DataEntities.MssqlRssparser");
            }
            else if (core.Provider() == "MYSQL")
            {
                result = core.connectionStringMYSQL("Repository.DataEntities.MysqlRssparser");
            }
            return result;
        }
    }
}
