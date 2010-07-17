using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.IO;
using Mytrip.Mvc.Models;
using System.Xml.Linq;

namespace Mytrip.Mvc
{
    public class CoreSetting
    {
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        string culture = HttpContext.Current.Session["culture"].ToString();
        public bool Development()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Development");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string NameHomePage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameHome");
            var _core= core.Elements("add").FirstOrDefault(x=>x.Attribute("value").Value==culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string NameAboutPage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameAbout");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string NameTitlePage()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("core").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "nameTitle");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower());
            return _core.Attribute("name").Value;
        }
        public string Server()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Server");
            return core.Attribute("value").Value;
        }
        public string DataBase()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "DataBase");
            return core.Attribute("value").Value;
        }
        public string User()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "User");
            return core.Attribute("value").Value;
        }
        public string Password()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Password");
            return core.Attribute("value").Value;
        }
        public bool IntegratedSecurity()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "IntegratedSecurity");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string Provider()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("connection").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Provider");
            return core.Attribute("value").Value;
        }
        public IDictionary<string, string> allProviderDictionary()
        {
            IDictionary<string, string> result =
              new Dictionary<string, string>();
            result.Add("MSSQL", "MSSQL");
            result.Add("MYSQL", "MYSQL");
            return result;
        }
        public string connectionString()
        {
            string result = string.Empty;
            if (Provider() == "MSSQL")
            {
                result = connectionStringMSSQL("Repository.DataEntities.MssqlModel");
            }
            else if (Provider() == "MYSQL")
            {
                result = connectionStringMYSQL("Repository.DataEntities.MysqlModel");
            }
            return result;

        }
        public string connectionStringMSSQL(string entitiesName)
        {
            string result = string.Empty;
            if (Server().IndexOf("SQLEXPRESS") != -1)
            {
                result = "Data Source=" + Server() + @"; AttachDbFilename=|DataDirectory|\"
                    + DataBase() + ";Integrated Security=True;User Instance=True;";
            }
            else
            {
                if (IntegratedSecurity())
                { result = "Data Source=" + Server() + "; Initial Catalog=" + DataBase() + "; Integrated Security=True;"; }
                else
                {

                    result = "Data Source=" + Server() + "; Initial Catalog=" + DataBase()
                        + "; Persist Security Info=True; User ID=" + User() + "; Password="
                        + Password() + ";";
                }

            }
            string connectionString = "metadata=res://*/" + entitiesName +
                ".csdl|res://*/" + entitiesName +
                ".ssdl|res://*/" + entitiesName +
                ".msl;provider=System.Data.SqlClient;provider connection string='"
                + result + " MultipleActiveResultSets=True'";
            return connectionString;

        }
        public string connectionStringMYSQL(string entitiesName)
        {
            string result = "server=" + Server() + "; User Id=" + User()
                        + "; password=" + Password() + "; Persist Security Info=True;database="
                        + DataBase() + ";";
            string connectionString = "metadata=res://*/" + entitiesName +
                ".csdl|res://*/" + entitiesName +
                ".ssdl|res://*/" + entitiesName +
                ".msl;provider=MySql.Data.MySqlClient;provider connection string='"
                + result + "'";
            return connectionString;

        }

    }
}
