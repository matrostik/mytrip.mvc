using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Web;
using System.Xml.Linq;
using Mytrip.Mvc.Repository;

namespace Mytrip.Votes
{
    public class VotesSetting
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
        string moduleName = "Mytrip.Votes";
        string moduleVersion = "1.0.44.2";
        string moduleStatus = "Beta";
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        string culture = HttpContext.Current.Session["culture"].ToString();
        public void CreateVotesConfiguration()
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
                new XElement("add", new XAttribute("name", "unlockVotes"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "nameVotes"),
                    new XElement("add", new XAttribute("name", "Our Vote"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Наш Опрос"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
            /*<votes>
    <add name="unlockVotes" value="True" />
    <add name="nameVotes">
      <add name="Our Vote" value="en-us" />
      <add name="Наш Опрос" value="ru-ru" />
    </add>
  </votes>*/
        }
        public string NameVotesPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameVotes");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public bool unlockVotes()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockVotes");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string connectionString()
        {
            string result = string.Empty;
            if (core.Provider() == "MSSQL")
            {
                result = core.connectionStringMSSQL("Repository.DataEntities.MssqlVotes");
            }
            else if (core.Provider() == "MYSQL")
            {
                result = core.connectionStringMYSQL("Repository.DataEntities.MysqlVotes");
            }
            return result;
        }
    }
}
