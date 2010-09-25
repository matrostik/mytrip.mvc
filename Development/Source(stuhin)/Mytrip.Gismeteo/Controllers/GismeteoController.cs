using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Mvc;
using System.Xml.Linq;
using Mytrip.Gismeteo.Repository;
using Mytrip.Gismeteo;
using Mytrip.Gismeteo.Models;
using Mytrip.Mvc.Settings;

namespace Mytrip.Rssparser.Controllers
{
    [RoleAdminAndEditor]
    public class GismeteoController : Controller
    {
        #region Properties
        GismeteoRepository _rss;
        public GismeteoRepository rss
        {
            get
            {
                if (_rss == null)
                    _rss = new GismeteoRepository();
                return _rss;
            }
        }
        #endregion

        public ActionResult Setting()
        {
            GismeteoSettingModel model = new GismeteoSettingModel();
            model.nameGismeteo = ModuleSettings.NameGismeteoPage();
            model.unlockGismeteo = ModuleSettings.unlockGismeteo();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Setting(GismeteoSettingModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в MytripConfiguration.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("Mytrip.Gismeteo").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "unlockGismeteo")
                    .SetAttributeValue("value", model.unlockGismeteo.ToString());
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameGismeteo").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameGismeteo);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("gm_namegismeteo");
                GeneralMethods.MytripCacheRemove("gm_unlockgismeteo", true);
                #endregion

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public ActionResult Manager()
        {
            ManagerModel model = new ManagerModel();

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Manager(ManagerModel model)
        {
            rss.CreateGismeteo(model.Title, LocalisationSetting.culture(), model.AllCulture, model.UrlXml, model.VisibleInformer);
            

            return View();
        }
        public ActionResult Delete(int id)
        {
            rss.DeleteGismeteo(id);
            return RedirectToAction("Manager");
        }
        public ActionResult Edit(int id)
        {
            ManagerModel model = new ManagerModel();
            var a=rss.GetOnlyGismeteo(id);
            model.GismeteoId = id;
            model.Title = a.Title;
            model.UrlXml = a.UrlXml;
            model.VisibleInformer = a.VisibleInformer;
            model.AllCulture = a.AllCulture;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ManagerModel model,int id)
        {
            rss.EditGismeteo(id, model.Title, model.AllCulture, model.UrlXml, model.VisibleInformer);
            return RedirectToAction("Manager");
        }
    }
}
