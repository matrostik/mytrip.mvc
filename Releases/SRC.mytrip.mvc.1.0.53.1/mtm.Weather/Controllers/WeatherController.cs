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
            model.nameWeather_ru_ru = ModuleSetting.NameWeatherPage("ru-ru");
            model.nameWeather_en_us = ModuleSetting.NameWeatherPage("en-us");
            model.partialAccordion = ModuleSetting.partialAccordion();
            model.partialNoAccordion = ModuleSetting.partialNoAccordion();
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            model.unlockWeather = ModuleSetting.unlockWeather();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             WeatherLanguage.Weathersetting};
            model.bread = a;
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
                article.FirstOrDefault(x => x.Attribute("name").Value == "partialAccordion")
                    .SetAttributeValue("value", model.partialAccordion);
                article.FirstOrDefault(x => x.Attribute("name").Value == "partialNoAccordion")
                    .SetAttributeValue("value", model.partialNoAccordion);
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameWeather").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameWeather_ru_ru);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameWeather_en_us);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("gm_nameWeather");
                GeneralMethods.MytripCacheRemove("gm_unlockWeather", "ru-ru");
                GeneralMethods.MytripCacheRemove("gm_unlockWeather", "en-us");
                GeneralMethods.MytripCacheRemove("gm_partialaccordion");
                GeneralMethods.MytripCacheRemove("gm_partialnoaccordion");
                #endregion

                return RedirectToAction("ControlPanel", "Core");
            }
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             WeatherLanguage.Weathersetting};
            model.bread = a;
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult Manager()
        {
            ManagerModel model = new ManagerModel();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             WeatherLanguage.Weather_manager};
            model.bread = a;
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Manager(ManagerModel model)
        {
            rss.CreateWeather(model);
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             WeatherLanguage.Weather_manager};
            model.bread = a;
            return View(model);
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
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                              "<a href='/Weather/Mamager'>" + WeatherLanguage.Weather_manager + "</a>",
                             WeatherLanguage.edit_Weather};
            model.bread = _a;
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Edit(ManagerModel model)
        {
            if (ModelState.IsValid)
            {
                rss.EditWeather(model);
                return RedirectToAction("Manager");
            }
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                              "<a href='/Weather/Mamager'>" + WeatherLanguage.Weather_manager + "</a>",
                             WeatherLanguage.edit_Weather};
            model.bread = _a;
            return View(model);
        
        }
        public FileContentResult Image(string id)
        {
            byte[] _image = Images.ResourceManager.GetObject(id) as byte[];
            return File(_image, "image/png");
        }
    }
}
