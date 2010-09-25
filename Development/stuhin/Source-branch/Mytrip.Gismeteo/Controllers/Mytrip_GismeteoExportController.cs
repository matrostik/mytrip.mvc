using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Gismeteo;
using Mytrip.Mvc.Interface;

namespace Mytrip.Rssparser.Controllers
{
    public class Mytrip_GismeteoExportController : Controller, IControllerExport
    {
        #region Properties  
        GismeteoCreateDataBase _db;
        public GismeteoCreateDataBase db
        {
            get
            {
                if (_db == null)
                    _db = new GismeteoCreateDataBase();
                return _db;
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

        public string SideBarPage()
        {
            string result = "AccordionGismeteo_" + ModuleSettings.NameGismeteoPage();
            return result;
        }
        [RoleAdmin]
        public ActionResult CreateDataBaseAndXml()
        {
            db.CreateAndDeleteDataBase(true);
            ModuleSettings.CreateGismeteoConfiguration(); 
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
