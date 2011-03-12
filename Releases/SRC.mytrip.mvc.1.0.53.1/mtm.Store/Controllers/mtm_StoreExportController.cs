using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using System.Web.Security;
using mtm.Core.Settings;
using mtm.Store.Repository;
using mtm.Store.Helpers;
using mtm.Core.Repository;

namespace mtm.Store.Controllers
{
    public class mtm_StoreExportController : Controller
    {
        private IStoreRepository _IStoreRepository;

        /// <summary>Инициализация репозитория магазина
        /// </summary>
        public IStoreRepository store
        {
            get
            {
                if (_IStoreRepository == null)
                    _IStoreRepository = new IStoreRepository();
                return _IStoreRepository;
            }
        }
        public string HomePage()
        { StringBuilder result = new StringBuilder();
            result.AppendLine("1_globe_" + ModuleSetting.nameStore());
            return result.ToString();
        }
        public string MenuPage()
        {
            string result = "MenuStore_" + ModuleSetting.nameStore();
            return result;
        }
        public string SideBarPage()
        {
            string result = "mtmStoreExportAccordionSearch_" + ModuleSetting.NameSearchPage() +
                            "|mtmStoreExportAccordionStore_" + ModuleSetting.nameStore() +
                            "|mtmStoreExportAccordionProducer_" + ModuleSetting.nameProducer() +
                            "|mtmStoreExportAccordionCart_" + StoreLanguage.mycarttitle;
            return result;
        }
        public string AnoncePage()
        {
            string result = "AnnounceCart_" + StoreLanguage.mycarttitle;
            return result;
        }
        public string SearchPage()
        {
            string result = "Search_" + StoreLanguage.Search;
            return result;
        }
        [RoleAdmin]
        public ActionResult InstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(true);
            InstallAndUninstall.CreateModuleConfiguration();
            Roles.CreateRole(ModuleSetting.roleChiefStoreManager());
            Roles.CreateRole(ModuleSetting.roleStoreManager());
            if (!MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name, ModuleSetting.roleChiefStoreManager());
            if (!MytripUser.UserInRole(ModuleSetting.roleStoreManager()))
                MytripUser.UnlockUserInRole(HttpContext.User.Identity.Name, ModuleSetting.roleStoreManager());
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
