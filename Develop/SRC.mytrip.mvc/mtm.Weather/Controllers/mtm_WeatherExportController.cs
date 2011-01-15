using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using mtm.Weather;

namespace mtm.Weather.Controllers
{
    public class mtm_WeatherExportController : Controller
    {
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }

        public string SideBarPage()
        {
            string result = "AccordionWeather_" + ModuleSettings.NameWeatherPage();
            return result;
        }
        [RoleAdmin]
        public ActionResult InstallModule()
        {
            InstallAndUninstall.CreateAndDeleteDataBase(true);
            InstallAndUninstall.CreateModuleConfiguration();
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
        public string HomePage()
        {
           return null;
        }

        public string MenuPage()
        {
            return null;
        }

        public string ProfilePage()
        {
            return null;
        }
    }
}
