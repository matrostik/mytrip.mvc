using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Gismeteo;

namespace Mytrip.Rssparser.Controllers
{
    [HandleError]
    [Localization]
    public class Mytrip_GismeteoExportController : Controller
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
        GismeteoSettings _ds;
        public GismeteoSettings ds
        {
            get
            {
                if (_ds == null)
                    _ds = new GismeteoSettings();
                return _ds;
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
            string result = "AccordionGismeteo_" + ds.NameGismeteoPage();
            return result;
        }
        public void CreateDataBaseAndXml()
        {
            db.CreateAndDeleteDataBase(true);
            ds.CreateGismeteoConfiguration();
        }
        [CoreSqlSetting]
        public ActionResult UninstallModule()
        {
            db.CreateAndDeleteDataBase(false);
            return RedirectToAction("InstallModules", "Core");
        }
    }
}
