﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Rssparser.Repository;
using Mytrip.Rssparser.Models;
using Mytrip.Mvc.Models;
using Mytrip.Mvc;
using System.Xml.Linq;

namespace Mytrip.Rssparser.Controllers
{
    [HandleError]
    [Localization]
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
        RssparserSetting _vs;
        public RssparserSetting vs
        {
            get
            {
                if (_vs == null)
                    _vs = new RssparserSetting();
                return _vs;
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
                model.RssparserContent = rss.GetRssparsers(id, id2, id3, out img, out link, out title, out _total);
                model.total = _total;
                model.Title = title;
                model.img = img;
                model.link = link;
                model.category = false;
            }
            else
            {
                model.RssparserCategory = rss.GetAllRssparsers(id, id2, Session["culture"].ToString(), out total);
                model.Title = RssparserLanguage.Rssparser;
                model.total = total;
                model.category = true;
            }
            return View(model);
        }
        [RoleAdmin]
        public ActionResult RssparserSetting()
        {
            RssparserSettingModel model = new RssparserSettingModel();
            model.nameRssparser = vs.nameRssparser();
            model.unlockRssparser = vs.unlockRssparser();
            return View(model);
        }
        [RoleAdmin]
        [HttpPost]
        public ActionResult RssparserSetting(RssparserSettingModel model)
        {
            if (ModelState.IsValid)
            {
                string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("Mytrip.Rssparser").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "unlockRssparser")
                    .SetAttributeValue("value", model.unlockRssparser.ToString());
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameRssparser").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameRssparser);
                _doc.Save(_absolutDirectory);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [RoleAdmin]
        public ActionResult Manager()
        {
            ManagerModel model = new ManagerModel();

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Manager(ManagerModel model)
        {
            rss.Create(model.Title, culture, model.AllCulture, model.RssUrl, model.ImgUrl);
            

            return View();
        }
        [RoleAdmin]
        public ActionResult Delete(int id)
        {
            rss.Delete(id);
            return RedirectToAction("Manager");
        }
        [RoleAdmin]
        public ActionResult Edit(int id)
        {
            ManagerModel model = new ManagerModel();
            var a=rss.GetRssparser(id);
            model.RssId = id;
            model.Title = a.Title;
            model.RssUrl = a.RssUrl;
            model.ImgUrl = a.ImageUrl;
            model.AllCulture = a.AllCulture;
            return View(model);
        }
        [RoleAdmin]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ManagerModel model)
        {
            rss.Edit(model.RssId, model.Title, model.AllCulture, model.RssUrl, model.ImgUrl);
            return RedirectToAction("Manager");
        }
    }
}
