using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using System.Web.Security;
using mtm.Core.Repository;
using mtm.Core.Settings;

namespace mtm.Tourism.Controllers
{
   public class mtm_TourismExportController:Controller
    {
        public string HomePage()
        {
            string result = "1_globe_" + ModuleSetting.nameTours();
            return result;
        }
        public string MenuPage()
        {
            string result = "MenuTours_" + ModuleSetting.nameTours()+"|"+
                "MenuToursCountry_" + ModuleSetting.nameCountry() + "|" +
                "MenuToursOrder_" + ToursLanguage.ordertour;
            return result;
        }
        public string SearchPage()
        {
            string result = "Search_" + ToursLanguage.searchtour;
            return result;
        }
        public string SideBarPage()
        {
            string result = "mtmTourismExportAccordionOrder_" + ModuleSetting.nameOrderTours();
            return result;
        }
        [RoleAdmin]
        public ActionResult InstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(true);
            InstallAndUninstall.CreateModuleConfiguration();
            Roles.CreateRole(ModuleSetting.roleChiefTourManager());
            Roles.CreateRole(ModuleSetting.roleTourManager());
            if(!MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name,ModuleSetting.roleChiefTourManager());
            if (!MytripUser.UserInRole(ModuleSetting.roleTourManager()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name, ModuleSetting.roleTourManager());
            GeneralMethods.MytripCacheRemove("mtm_cacherole");
            InstallAndUninstall.CreateAllFiles();
            return RedirectToAction("SaveModule", "Core");
        }
        [RoleAdmin]
        public ActionResult UninstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(false);
            InstallAndUninstall.DeleteAllFiles();
            return RedirectToAction("InstallModules", "Core");
        }
    }
}
