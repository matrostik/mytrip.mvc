using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using System.Web.Security;

namespace Mytrip.Tourism.Controllers
{
    class Mytrip_TourismExportController:Controller
    {
        TourismCreateDataBase _createdb;
        public TourismCreateDataBase createdb
        {
            get
            {
                if (_createdb == null)
                    _createdb = new TourismCreateDataBase();
                return _createdb;
            }
        }
        public string HomePage()
        {
            string result = "1_globe_" + ModuleSetting.nameTours();
            return result;
        }
        public string MenuPage()
        {
            string result = "MenuTours_" + ModuleSetting.nameTours()+"|"+
                "MenuToursModule_" + ToursLanguage.searchtour + "|" +
                "MenuToursOrder_" + ToursLanguage.ordertour;
            return result;
        }
        [RoleAdmin]
        public ActionResult CreateDataBaseAndXml()
        {
            createdb.CreateAndDeleteDataBase(true);
            ModuleSetting.CreateStoreConfiguration();
            Roles.CreateRole(ModuleSetting.roleChiefTourManager());
            Roles.CreateRole(ModuleSetting.roleTourManager());
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
