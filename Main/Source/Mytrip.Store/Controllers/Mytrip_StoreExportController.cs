using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using System.Web.Security;

namespace Mytrip.Store.Controllers
{
    public class Mytrip_StoreExportController : Controller
    {
        StoreCreateDataBase _createdb;
        public StoreCreateDataBase createdb
        {
            get
            {
                if (_createdb == null)
                    _createdb = new StoreCreateDataBase();
                return _createdb;
            }
        }
        public string HomePage()
        { StringBuilder result = new StringBuilder();
            result.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            result.AppendLine("<root_el>");
            result.AppendLine("<first_el>1_globe_" + ModuleSetting.nameStore());
            result.AppendLine("</first_el>");
            result.AppendLine("</root_el>");
            return result.ToString();
        }
        public string MenuPage()
        {
            string result = "MenuStore_" + ModuleSetting.nameStore();
            return result;
        }
        public string SideBarPage()
        {
            string result = "AccordionSearch_" + ModuleSetting.NameSearchPage() +
                            "|AccordionStore_" + ModuleSetting.nameStore() +
                            "|AccordionProducer_" + ModuleSetting.nameProducer()+
                            "|AccordionCart_" + StoreLanguage.mycarttitle;
            return result;
        }
        public string AnoncePage()
        {
            string result = "AnnounceCart_" + StoreLanguage.mycarttitle;
            return result;
        }
        [RoleAdmin]
        public ActionResult CreateDataBaseAndXml()
        {
            createdb.CreateAndDeleteDataBase(true);
            ModuleSetting.CreateStoreConfiguration();
            Roles.CreateRole(ModuleSetting.roleChiefStoreManager());
            Roles.CreateRole(ModuleSetting.roleStoreManager());
            return RedirectToAction("SaveModule", "Core");
        }
        [RoleAdmin]
        public ActionResult UninstallModule()
        {
            createdb.CreateAndDeleteDataBase(false);
            return RedirectToAction("InstallModules", "Core");
        }

    }
}
