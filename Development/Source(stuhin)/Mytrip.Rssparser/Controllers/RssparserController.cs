using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Rssparser.Repository;
using Mytrip.Rssparser.Models;
using Mytrip.Mvc.Models;
using Mytrip.Mvc;
using System.Xml.Linq;
using Mytrip.Mvc.Settings;

namespace Mytrip.Rssparser.Controllers
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
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            RssparserSettingModel model = new RssparserSettingModel();
            model.nameRssparser = ModuleSetting.nameRssparser();
            model.unlockRssparser = ModuleSetting.unlockRssparser();
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult Setting(RssparserSettingModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в MytripConfiguration.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var article = _doc.Root.Elements("Mytrip.Rssparser").Elements("add");
                article.FirstOrDefault(x => x.Attribute("name").Value == "unlockRssparser")
                    .SetAttributeValue("value", model.unlockRssparser.ToString());
                var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameRssparser").Elements("add");
                artpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameRssparser);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("rsp_unlockrssparser");
                GeneralMethods.MytripCacheRemove("rsp_namerssparser", true);
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
        [ValidateInput(false)]
        public ActionResult Manager(ManagerModel model)
        {
            rss.Create(model.Title, culture, model.AllCulture, model.RssUrl, model.ImgUrl);
            return View();
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
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ManagerModel model)
        {
            rss.Edit(model.RssId, model.Title, model.AllCulture, model.RssUrl, model.ImgUrl);
            return RedirectToAction("Manager");
        }
    }
}
