using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.RssParser.Repository;
using mtm.Core.Models;

namespace mtm.RssParser.Controllers
{
    public class mtm_RssparserExportController : Controller
    {
        #region Properties
        RssparserRepository _rss;
        public RssparserRepository rss
        {
            get
            {
                if (_rss == null)
                    _rss = new RssparserRepository();
                return _rss;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        #endregion
        public string HomePage()
        {

            var a = rss.GetAllRssparsers();
            string result = string.Empty;
            bool q = false;
            foreach (var b in a)
            {
                string _culture = "globe_";
                if (!b.AllCulture)
                    _culture = b.Culture.ToLower() + "_";
                if (q)
                    result += "|" + b.RssparserId + "_" + _culture + b.Title;
                else
                {
                    q = true;
                    result = b.RssparserId + "_" + _culture + b.Title;
                }
            }

            return result;
        }
        public string MenuPage()
        {
            string result = "MenuRssparser_" + RssparserLanguage.Rssparser;
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

        public string ProfilePage()
        {
            return null;
        }

        public string SideBarPage()
        {
            return null;
        }
    }
}
