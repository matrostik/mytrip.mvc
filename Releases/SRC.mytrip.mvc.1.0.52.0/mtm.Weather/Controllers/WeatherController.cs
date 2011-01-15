using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using mtm.Core;
using System.Xml.Linq;
using mtm.Weather.Repository;
using mtm.Weather;
using mtm.Weather.Models;
using mtm.Core.Settings;
using mtm.Weather.Files;

namespace mtm.Weather.Controllers
{
    
    public class WeatherController : Controller
    {
        #region Properties
        WeatherRepository _rss;
        public WeatherRepository rss
        {
            get
            {
                if (_rss == null)
                    _rss = new WeatherRepository();
                return _rss;
            }
        }
        #endregion
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            WeatherSettingModel model = new WeatherSettingModel();
            model.nameWeather = ModuleSettings.NameWeatherPage();
            model.unlockWeather = ModuleSettings.unlockWeather();
            return View(model);
        }
        
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Setting(WeatherSettingModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("mtm.Weather").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "unlockWeather")
                    .SetAttributeValue("value", model.unlockWeather.ToString());
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameWeather").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == LocalisationSetting.culture().ToLower())
                    .SetAttributeValue("name", model.nameWeather);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("gm_nameWeather");
                GeneralMethods.MytripCacheRemove("gm_unlockWeather", true);
                #endregion

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult Manager()
        {
            ManagerModel model = new ManagerModel();

            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Manager(ManagerModel model)
        {
            rss.CreateWeather(model.Title, LocalisationSetting.culture(), model.AllCulture, model.UrlXml, model.VisibleInformer);
            

            return View();
        }
        [RoleAdminAndEditor]
        public ActionResult Delete(int id)
        {
            rss.DeleteWeather(id);
            return RedirectToAction("Manager");
        }
        [RoleAdminAndEditor]
        public ActionResult Edit(int id)
        {
            ManagerModel model = new ManagerModel();
            var a=rss.GetOnlyWeather(id);
            model.WeatherId = id;
            model.Title = a.Title;
            model.UrlXml = a.UrlXml;
            model.VisibleInformer = a.VisibleInformer;
            model.AllCulture = a.AllCulture;
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Edit(ManagerModel model,int id)
        {
            rss.EditWeather(id, model.Title, model.AllCulture, model.UrlXml, model.VisibleInformer);
            return RedirectToAction("Manager");
        }
        public FileContentResult Image(string id)
        {
            byte[] _image = Images.ResourceManager.GetObject(id) as byte[];
            return File(_image, "image/png");
        }
    }
}
