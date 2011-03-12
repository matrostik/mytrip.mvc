using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.RssParser.Repository;
using mtm.RssParser.Models;
using mtm.Core.Models;
using mtm.Core;
using System.Xml.Linq;
using mtm.Core.Settings;

namespace mtm.RssParser.Controllers
{
    public class RssparserController : Controller
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

        public ActionResult Index(int id, int id2, int id3, string id4)
        {
            bool img = false;
            int total = 0;
            int _total = 0;
            string link = string.Empty;
            string title = string.Empty;
            RssparserModel model = new RssparserModel();
            model.RssparserCategory = null;
            model.RssparserContent = null;
            if (id3 > 0)
            {
                model.RssparserContent = rss.GetRssparsers(id, id2, id3, out img, out link, out _total);
                var b = rss.GetRssparser(id3);
                model.total = _total;
                model.Title = b.Title;
                model.img = img;
                model.link = link;
                model.seotitle = b.SeoTitle;
                model.seokeywords = b.SeoKeyword;
                model.seodescription = b.SeoDescription;
                model.category = false;
                string[] a = { "<a href='/Rssparser/Index/1/10/0/Rssparser'>" + ModuleSetting.nameRssparser() + "</a>", 
                             model.Title};
                model.bread = a;
            }
            else
            {
                model.seotitle = ModuleSetting.RssparserTitle();
                model.seokeywords = ModuleSetting.RssparserKeyWords();
                model.seodescription = ModuleSetting.RssparserDesc();
                model.RssparserCategory = rss.GetAllRssparsers(id, id2, Session["culture"].ToString(), out total);
                model.Title = ModuleSetting.nameRssparser();
                model.total = total;
                model.category = true;
                string[] a = { model.Title};
                model.bread = a;
            }
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            RssparserSettingModel model = new RssparserSettingModel();
            model.nameRssparser_ru_ru = ModuleSetting.nameRssparser("ru-ru");
            model.nameRssparser_en_us = ModuleSetting.nameRssparser("en-us");
            model.partialMenuLogon = ModuleSetting.partialMenuLogon();
            model.partialMenuLogonWrap = ModuleSetting.partialMenuLogonWrap();
            model.Rssparser_seodescription_en_us = ModuleSetting.RssparserDesc("en-us");
            model.Rssparser_seodescription_ru_ru = ModuleSetting.RssparserDesc("ru-ru");
            model.Rssparser_seokeywords_en_us = ModuleSetting.RssparserKeyWords("en-us");
            model.Rssparser_seokeywords_ru_ru = ModuleSetting.RssparserKeyWords("ru-ru");
            model.titleRssparser_en_us = ModuleSetting.RssparserTitle("en-us");
            model.titleRssparser_ru_ru = ModuleSetting.RssparserTitle("ru-ru");
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            model.unlockRssparser = ModuleSetting.unlockRssparser();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             RssparserLanguage.Rssparsersetting};
            model.bread = a;
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult Setting(RssparserSettingModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("mtm.RssParser").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "unlockRssparser")
                    .SetAttributeValue("value", model.unlockRssparser.ToString());

                article.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogon")
                     .SetAttributeValue("value", model.partialMenuLogon);
                article.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogonWrap")
                     .SetAttributeValue("value", model.partialMenuLogonWrap);

                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameRssparser").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameRssparser_ru_ru);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.titleRssparser_ru_ru);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.Rssparser_seokeywords_ru_ru);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.Rssparser_seodescription_ru_ru);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameRssparser_en_us);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.titleRssparser_en_us);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.Rssparser_seokeywords_en_us);
                artpage.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.Rssparser_seodescription_en_us);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("rsp_unlockrssparser");
                GeneralMethods.MytripCacheRemove("rsp_partialmenulogon");
                GeneralMethods.MytripCacheRemove("rsp_partialmenulogonwrap");
                GeneralMethods.MytripCacheRemove("rsp_namerssparser", "ru-ru");
                GeneralMethods.MytripCacheRemove("rsp_namerssparser", "en-us");
                GeneralMethods.MytripCacheRemove("rsp_rssparserdescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("rsp_rssparserdescription", "en-us");
                GeneralMethods.MytripCacheRemove("rsp_rssparsertitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("rsp_rssparsertitle", "en-us");
                GeneralMethods.MytripCacheRemove("rsp_rssparserkeywords", "ru-ru");
                GeneralMethods.MytripCacheRemove("rsp_rssparserkeywords", "en-us");
                #endregion

                return RedirectToAction("Index", "Home");
            } string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             RssparserLanguage.Rssparsersetting};
            model.bread = a;
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult Manager()
        {
            ManagerModel model = new ManagerModel();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             RssparserLanguage.rssparser_manager};
            model.bread = a;
            model.seokeywords = ModuleSetting.RssparserKeyWords();
            model.seodescription = ModuleSetting.RssparserDesc();
            return View(model);
        }
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Manager(ManagerModel model)
        {
            rss.Create(model);
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             RssparserLanguage.rssparser_manager};
            model.bread = a;
            return View(model);
        }
        [RoleAdminAndEditor]
        public ActionResult Delete(int id)
        {
            rss.Delete(id);
            return RedirectToAction("Manager");
        }
        [RoleAdminAndEditor]
        public ActionResult Edit(int id)
        {
            ManagerModel model = new ManagerModel();
            var a=rss.GetRssparser(id);
            model.RssId = id;
            model.Title = a.Title;
            model.RssUrl = a.RssUrl;
            model.ImgUrl = a.ImageUrl;
            model.AllCulture = a.AllCulture;
            model.seodescription = a.SeoDescription;
            model.path = a.Path;
            model.seokeywords = a.SeoKeyword;
            model.seotitle = a.SeoTitle;
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                              "<a href='/Rssparser/Manager'>" + RssparserLanguage.rssparser_manager + "</a>",
                             RssparserLanguage.edit_rss_feed};
            model.bread = _a;
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult Edit(ManagerModel model)
        {
            if (ModelState.IsValid)
            {
                rss.Edit(model);
                return RedirectToAction("Manager");
            } 
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                              "<a href='/Rssparser/Manager'>" + RssparserLanguage.rssparser_manager + "</a>",
                             RssparserLanguage.edit_rss_feed};
            model.bread = _a;
            return View(model);
        }
    }
}
