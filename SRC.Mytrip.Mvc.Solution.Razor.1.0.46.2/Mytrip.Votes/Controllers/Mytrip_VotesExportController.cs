using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Interface;

namespace Mytrip.Votes.Controllers
{
    public class Mytrip_VotesExportController : Controller, IControllerExport
    {
        VotesCreateDataBase _db;
        public VotesCreateDataBase db
        {
            get
            {
                if (_db == null)
                    _db = new VotesCreateDataBase();
                return _db;
            }
        }
        public string SideBarPage()
        {
            string result = "AccordionVotes_" + VotesLanguage.our_vote;
            return result;
        }
        [RoleAdmin]
        public ActionResult CreateDataBaseAndXml()
        {
            db.CreateAndDeleteDataBase(true);
            ModuleSetting.CreateVotesConfiguration();
            return RedirectToAction("SaveModule", "Core");
        }
        [RoleAdmin]
        public ActionResult UninstallModule()
        {
            db.CreateAndDeleteDataBase(false);
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
