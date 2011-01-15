using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Install;
using mtm.Tourism.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Web;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Tourism
{
    public static class InstallAndUninstall
    {
        #region Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
       public static string moduleName = "mtm.Tourism";
        static string moduleVersion = "1.0.52.0";
        static string moduleStatus = "Beta";
        static string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();

        /// <summary>
        /// 
        /// </summary>
        public static void CreateModuleConfiguration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            XElement artmodule = new XElement("add",
                new XAttribute("name", moduleName),
                new XAttribute("version", moduleVersion),
                new XAttribute("status", moduleStatus));
            _doc.Root.Element("installModules").Add(artmodule);
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            XElement article = new XElement(moduleName,
                new XElement("add", new XAttribute("name", "unlockTours"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paginalTours"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewDescription"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "columnTours"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "styleTours"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "widthImgTours"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "MoneyProcent"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "closeTour"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "roleChiefTourManager"), new XAttribute("value", "chief_tour_manager")),
                new XElement("add", new XAttribute("name", "roleTourManager"), new XAttribute("value", "tour_manager")),
                new XElement("add", new XAttribute("name", "nameTours"),
                    new XElement("add", new XAttribute("name", "Tours"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Туры"), new XAttribute("value", "ru-ru"))),
                    new XElement("add", new XAttribute("name", "Money"),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("name", "$"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("name", "руб."), new XAttribute("value", "ru-ru")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("name", "€"), new XAttribute("value", "null"))),
                new XElement("add", new XAttribute("name", "Course"),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("to", "USD"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "RUB"), new XAttribute("to", "EUR"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("to", "RUB"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "USD"), new XAttribute("to", "EUR"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("to", "RUB"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")),
                    new XElement("add", new XAttribute("key", "EUR"), new XAttribute("to", "USD"), new XAttribute("rate", "0"), new XAttribute("date", "22-10-2009")))
                    );
            _doc.Root.Add(article);
            _doc.Save(_absolutDirectory);
        }
        //****************** E N D **********************
        #endregion
        public static void CreateAndDeleteDataBase(bool e)
        {
            CreateDataBase.CreateDataBaseMSSQL(ScriptSql.ScriptMSSQL, ScriptSql.deleteScriptMSSQL, e);
            CreateDataBase.CreateDataBaseMYSQL(ScriptSql.ScriptMySql, ScriptSql.deleteScriptMysql, e);
        }
        public static void CreateAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.Tourism.resources.dll", Localisation.ru_ru_mtm_Tourism_resources);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Scripts/Page/Tour.EditorCategory.js", JScripts.Tour_EditorCategory);
            EditePageRepository.CreatePage("/Scripts/Page/Tour.EditorTour.js", JScripts.Tour_EditorTour);
            EditePageRepository.CreatePage("/Scripts/Page/Tour.OrderTour.js", JScripts.Tour_OrderTour);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Tours");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Tours/EditorCategory.cshtml", WebPages.EditorCategory);
            EditePageRepository.CreatePage("/Views/Tours/EditorTour.cshtml", WebPages.EditorTour);
            EditePageRepository.CreatePage("/Views/Tours/Index.cshtml", WebPages.Index);
            EditePageRepository.CreatePage("/Views/Tours/OrderTour.cshtml", WebPages.OrderTour);
            EditePageRepository.CreatePage("/Views/Tours/View.cshtml", WebPages.View);
            EditePageRepository.CreatePage("/Views/Tours/Web.config", WebPages.Web);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru/mtm.Tourism.resources.dll");
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Tour.EditorCategory.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Tour.EditorTour.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Tour.OrderTour.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Tours");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        }
    }
}
