using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Repository;
using System.Xml.Linq;
using Mytrip.Mvc;
using System.Web;

namespace Mytrip.Store
{
   public class StoreSettings
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
        string moduleName = "Mytrip.Store";
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
                new XElement("add", new XAttribute("name", "unlockStore"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "columnDepartment"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgDepartment"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleDepartment"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "columnProduct"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgProduct"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleProduct"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "nameStore"),
                    new XElement("add", new XAttribute("name", "Store"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Магазин"), new XAttribute("value", "ru-ru"))));
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
        }//<add name="widthImgProduct" value="100" />
      //<add name="styleProduct" value="2" />
        //<add name="columnProduct" value="3" />
        public string nameStore()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameStore");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public bool unlockStore()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockStore");
            return bool.Parse(core.Attribute("value").Value);
        }
        public int columnDepartment()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "columnDepartment");
            return int.Parse(core.Attribute("value").Value);
        }
        public int widthImgDepartment()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "widthImgDepartment");
            return int.Parse(core.Attribute("value").Value);
        }
        public int styleDepartment()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "styleDepartment");
            return int.Parse(core.Attribute("value").Value);
        }
        public int columnProduct()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "columnProduct");
            return int.Parse(core.Attribute("value").Value);
        }
        public int widthImgProduct()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "widthImgProduct");
            return int.Parse(core.Attribute("value").Value);
        }
        public int styleProduct()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(moduleName).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "styleProduct");
            return int.Parse(core.Attribute("value").Value);
        }
        public string connectionString()
        {
            string result = string.Empty;
            if (core.Provider() == "MSSQL")
            {
                result = core.connectionStringMSSQL("Repository.DataEntities.MssqlStore");
            }
            else if (core.Provider() == "MYSQL")
            {
                result = core.connectionStringMYSQL("Repository.DataEntities.MysqlStore");
            }
            return result;
        }
    }
}
