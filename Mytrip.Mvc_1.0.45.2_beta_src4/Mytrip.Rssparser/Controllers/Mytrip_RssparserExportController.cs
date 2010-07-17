using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Rssparser.Repository;
using Mytrip.Mvc.Models;

namespace Mytrip.Rssparser.Controllers
{
    [HandleError]
    [Localization]
    public class Mytrip_RssparserExportController : Controller
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
        RssparserCreateDataBase _db;
        public RssparserCreateDataBase db
        {
            get
            {
                if (_db == null)
                    _db = new RssparserCreateDataBase();
                return _db;
            }
        }
        RssparserSetting _ds;
        public RssparserSetting ds
        {
            get
            {
                if (_ds == null)
                    _ds = new RssparserSetting();
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
        public void CreateDataBaseAndXml()
        {
            db.CreateAndDeleteDataBase(true);
            ds.CreateRssparserConfiguration();
        }
        [CoreSqlSetting]
        public ActionResult UninstallModule()
        {
            db.CreateAndDeleteDataBase(false);
            return RedirectToAction("InstallModules", "Core");
        }
    }
}
