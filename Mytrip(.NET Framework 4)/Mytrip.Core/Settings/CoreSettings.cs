using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.IO;
using Mytrip.Core.Models;

namespace Mytrip.Core
{
    public class CoreSetting
    {
        public static bool Development = bool.Parse(ConfigurationManager.AppSettings["Development"]);
        public static string MSSQLServer = ConfigurationManager.AppSettings["MSSQLServer"];
        public static string MSSQLDataBase = ConfigurationManager.AppSettings["MSSQLDataBase"];
        public static string MSSQLUser = ConfigurationManager.AppSettings["MSSQLUser"];
        public static string MSSQLPassword = ConfigurationManager.AppSettings["MSSQLPassword"];
        public static bool MSSQLIntegratedSecurity = bool.Parse(ConfigurationManager.AppSettings["MSSQLIntegratedSecurity"]);
        public static string connectionStringSQL(string entitiesName)
        {
            string result = string.Empty;            
            if (MSSQLServer.IndexOf("SQLEXPRESS") != -1)
            {
                result = "Data Source=" + MSSQLServer + @"; AttachDbFilename=|DataDirectory|\" 
                    + MSSQLDataBase + ";Integrated Security=True;User Instance=True;";
            }
            else
            {                
                if (MSSQLIntegratedSecurity)
                { result = "Data Source=" + MSSQLServer + "; Initial Catalog=" + MSSQLDataBase + "; Integrated Security=True;"; }
                else
                {
                    
                    result = "Data Source=" + MSSQLServer + "; Initial Catalog=" + MSSQLDataBase 
                        + "; Persist Security Info=True; User ID=" + MSSQLUser + "; Password=" 
                        + EncryptedString.EncryptedValue(MSSQLPassword) + ";";
                        //+ MSSQLPassword + ";";
                }

            }
            string connectionString = "metadata=res://*/" + entitiesName +
                ".csdl|res://*/" + entitiesName +
                ".ssdl|res://*/" + entitiesName +
                ".msl;provider=System.Data.SqlClient;provider connection string='" 
                + result + " MultipleActiveResultSets=True'";
            return connectionString;

        }
        public static string connectionStringSQL()
        {
            string result = string.Empty;
            string MSSQLServer = ConfigurationManager.AppSettings["MSSQLServer"];
            string MSSQLDataBase = ConfigurationManager.AppSettings["MSSQLDataBase"];
            if (MSSQLServer.IndexOf("SQLEXPRESS") != -1)
            {
                result = "Data Source=" + MSSQLServer + @"; AttachDbFilename=|DataDirectory|\"
                    + MSSQLDataBase + ";Integrated Security=SSPI;User Instance=True;";
            }
            else
            {
                bool MSSQLIntegratedSecurity = bool.Parse(ConfigurationManager.AppSettings["MSSQLIntegratedSecurity"]);
                if (MSSQLIntegratedSecurity)
                { result = "Data Source=" + MSSQLServer + "; Initial Catalog=" + MSSQLDataBase + "; Integrated Security=True;"; }
                else
                {
                    string MSSQLUser = ConfigurationManager.AppSettings["MSSQLUser"];
                    string MSSQLPassword = ConfigurationManager.AppSettings["MSSQLPassword"];
                    result = "Data Source=" + MSSQLServer + "; Initial Catalog=" + MSSQLDataBase
                        + "; Persist Security Info=True; User ID=" + MSSQLUser + "; Password="
                        + EncryptedString.EncryptedValue(MSSQLPassword) + ";";
                        //+ MSSQLPassword + ";";
                }

            }
            
            return result;

        }
    }
}
