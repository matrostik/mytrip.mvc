using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;

namespace Mytrip.Votes.Controllers
{
    [HandleError]
    [Localization]
    public class Mytrip_VotesExportController : Controller
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
        VotesSetting _ds;
        public VotesSetting ds
        {
            get
            {
                if (_ds == null)
                    _ds = new VotesSetting();
                return _ds;
            }
        }
        public string SideBarPage()
        {
            string result = "AccordionVotes_" + VotesLanguage.our_vote;
            return result;
        }
        public void CreateDataBaseAndXml()
        {
            db.CreateAndDeleteDataBase(true);
            ds.CreateVotesConfiguration();
        }
        [CoreSqlSetting]
        public ActionResult UninstallModule()
        {
            db.CreateAndDeleteDataBase(false);
            return RedirectToAction("InstallModules", "Core");
        }
    }
}
