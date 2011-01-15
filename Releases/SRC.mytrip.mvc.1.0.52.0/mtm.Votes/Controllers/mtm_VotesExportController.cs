using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;

namespace mtm.Votes.Controllers
{
    public class mtm_VotesExportController : Controller
    {
        public string SideBarPage()
        {
            string result = "AccordionVotes_" + VotesLanguage.our_vote;
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
