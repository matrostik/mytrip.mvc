using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Install;
using mtm.Store.Files;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Web;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Store
{
    public static class InstallAndUninstall
    {
        #region Module installation
        // **********************************************
        // Данные по модулю
        // **********************************************
        public static string moduleName = "mtm.Store";
        static string moduleVersion = "1.0.52.1";
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
                new XElement("add", new XAttribute("name", "unlockStore"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "columnDepartment"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgDepartment"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleDepartment"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "columnProduct"), new XAttribute("value", "3")),
                new XElement("add", new XAttribute("name", "widthImgProduct"), new XAttribute("value", "100")),
                new XElement("add", new XAttribute("name", "styleProduct"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "roleChiefStoreManager"), new XAttribute("value", "chief_store_manager")),
                new XElement("add", new XAttribute("name", "roleStoreManager"), new XAttribute("value", "store_manager")),
                new XElement("add", new XAttribute("name", "onlineBuy"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "MoneyProcent"), new XAttribute("value", "2")),
                new XElement("add", new XAttribute("name", "organizationBuy"), new XAttribute("value", "False")),
                new XElement("add", new XAttribute("name", "privatepersonBuy"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "viewProduktTable"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paypal"), new XAttribute("value", "True")),
                new XElement("add", new XAttribute("name", "paypalseller"), new XAttribute("value", "***")),
                new XElement("add", new XAttribute("name", "nameStore"),
                    new XElement("add", new XAttribute("name", "Store"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Магазин"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameProducer"),
                    new XElement("add", new XAttribute("name", "Producers"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Производители"), new XAttribute("value", "ru-ru"))),
                new XElement("add", new XAttribute("name", "nameSearch"),
                    new XElement("add", new XAttribute("name", "Search"), new XAttribute("value", "en-us")),
                    new XElement("add", new XAttribute("name", "Поиск"), new XAttribute("value", "ru-ru"))),
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
        public static void CreateAndDeleteDataBase(bool create)
        {
            CreateDataBase.CreateDataBaseMSSQL(ScriptSql.ScriptMSSQL, ScriptSql.deleteScriptMSSQL, create);
            CreateDataBase.CreateDataBaseMYSQL(ScriptSql.ScriptMySql, ScriptSql.deleteScriptMysql, create);
        }
        public static void CreateAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/bin/ru-ru/mtm.Store.resources.dll", Localisation.ru_ru_mtm_Store_resources);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Scripts/Page/Store.Cart.js", JScripts.Store_Cart);
            EditePageRepository.CreatePage("/Scripts/Page/Store.CreateProductXml.js", JScripts.Store_CreateProductXml);
            EditePageRepository.CreatePage("/Scripts/Page/Store.CreateSale.js", JScripts.Store_CreateSale);
            EditePageRepository.CreatePage("/Scripts/Page/Store.EditorCategory.js", JScripts.Store_EditorCategory);
            EditePageRepository.CreatePage("/Scripts/Page/Store.EditorProduct.js", JScripts.Store_EditorProduct);
            EditePageRepository.CreatePage("/Scripts/Page/Store.Index.js", JScripts.Store_Index);
            EditePageRepository.CreatePage("/Scripts/Page/Store.ManagerOrders.js", JScripts.Store_ManagerOrders);
            EditePageRepository.CreatePage("/Scripts/Page/Store.OrdersDetails.js", JScripts.Store_OrdersDetails);
            EditePageRepository.CreatePage("/Scripts/Page/Store.View.js", JScripts.Store_View);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Store");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            EditePageRepository.CreatePage("/Views/Store/Cart.cshtml", WebPages.Cart);
            EditePageRepository.CreatePage("/Views/Store/CreateProductXml.cshtml", WebPages.CreateProductXml);
            EditePageRepository.CreatePage("/Views/Store/CreateSale.cshtml", WebPages.CreateSale);
            EditePageRepository.CreatePage("/Views/Store/EditorCategory.cshtml", WebPages.EditorCategory);
            EditePageRepository.CreatePage("/Views/Store/EditorProduct.cshtml", WebPages.EditorProduct);
            EditePageRepository.CreatePage("/Views/Store/Index.cshtml", WebPages.Index);
            EditePageRepository.CreatePage("/Views/Store/Manager.cshtml", WebPages.Manager);
            EditePageRepository.CreatePage("/Views/Store/ManagerOrders.cshtml", WebPages.ManagerOrders);
            EditePageRepository.CreatePage("/Views/Store/OrderDetails.cshtml", WebPages.OrderDetails);
            EditePageRepository.CreatePage("/Views/Store/Setting.cshtml", WebPages.Setting);
            EditePageRepository.CreatePage("/Views/Store/View.cshtml", WebPages.View);
            EditePageRepository.CreatePage("/Views/Store/Web.config", WebPages.Web);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Product");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Department");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Producer");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/Content/Store/Catalog/Empty.zip", Content.Empty);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog/Empty");
            _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                _absolutDirectory.Create();
            CreateFileRepository.CreateFile("/Content/Store/Catalog/Empty/mytrip_products.xls", Content.mytrip_products);
            CreateFileRepository.CreateFile("/Content/Store/Catalog/Empty/mytrip_products.xlsx", Content.mytrip_products1);
        }
        public static void DeleteAllFiles()
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin/ru-ru/mtm.Store.resources.dll");
            FileInfo file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.Cart.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.CreateProductXml.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.CreateSale.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.EditorCategory.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.EditorProduct.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.Index.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.ManagerOrders.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.OrdersDetails.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Scripts/Page/Store.View.js");
            file = new FileInfo(absolutDirectory);
            file.Delete();
            absolutDirectory = HttpContext.Current.Server.MapPath("/Views/Store");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
            absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store");
            folder = new DirectoryInfo(absolutDirectory);
            folder.Delete(true);
        }
    }
}
