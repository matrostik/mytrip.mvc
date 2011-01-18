/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Install;
using mtm.Core.Models;
using mtm.Core.Repository;
using mtm.Core.Settings;

namespace mtm.Core.Controllers
{
    /// <summary>Контроллер главной страницы сайта и страницы About
    /// </summary>
    public class HomeController : ICoreController
    {

        /// <summary>GET: /Home/Index
        /// Главная страница сайта
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            bool develop = CoreSetting.Development();
            if (develop)
            {
                Crypto.ReplaceKeyEncrypt();
                model.title = CoreLanguage.license_agreement; 
            }
            else
                model.title = CoreSetting.NameHomePage();
            model.developer = develop;
            return View(model);
        }
        /// <summary>GET: /Home/Profile
        /// Профиль пользователя
        /// </summary>
        /// <param name="id">имя пользователя</param>
        /// <param name="path">путь</param>
        /// <returns>ActionResult</returns>
        public ActionResult Profile(string id, string id2)
        {
            ProfileUsersModel model = new ProfileUsersModel();
            TempData["username"] = model.UserName = id;
            TempData["useremail"] = model.Email = MytripUser.UserEmail(id);
            model.UserProfile = coreRepo.membershipRepo.GetProfile(id);
            model.closeprofile = model.UserProfile.ProfileClose;
            if (User.Identity.IsAuthenticated && User.Identity.Name == id)
                model.closeprofile = false;
            if(!model.closeprofile&&GeoSetting.unlockGeo())
                model.bodyonload = "GetMap();";
            if (string.IsNullOrEmpty(id2))
                model.Path = CoreLanguage.all;
            else
                model.Path = id2;
            return View(model);
        }
        [Authorize]
        public ActionResult EditProfile(string id)
        {
            if (HttpContext.User.Identity.Name == id)
            {
                EditProfileModel model = new EditProfileModel();
                TempData["username"] = model.username = id;
                TempData["useremail"] = model.username = MytripUser.UserEmail(id);
                var x = coreRepo.membershipRepo.GetProfile(id);
                model.UserProfile = x;
                model.description = x.Description;
                model.firstname = x.FirstName;
                model.lastname = x.LastName;
                
                model.latitude = (x.Latitude!=null)?x.Latitude.ToString():"0";
                model.longitude = (x.Longitude != null) ? x.Longitude.ToString() : "0";
                model.phone = x.Phone;
                model.profilclose = x.ProfileClose;
                model.site = x.Site;
                model.skype = x.skype;
                model.icq = x.icq;
                if (GeoSetting.unlockGeo())
                    model.bodyonload = "GetMap();";
                return View(model);
            }else
                return RedirectToAction("Profile",new{id});       
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(string id, EditProfileModel model)
        {
            if (HttpContext.User.Identity.Name == id)
            {
                decimal latitude = 0;
                decimal longitude = 0;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                { decimal.TryParse(model.latitude.Replace(".", ","), out latitude);
                decimal.TryParse(model.longitude.Replace(".", ","), out longitude);
                }
                if (LocalisationSetting.culture().ToLower() == "en-us")
                { decimal.TryParse(model.latitude.Replace(",", "."), out latitude);
                decimal.TryParse(model.longitude.Replace(",", "."), out longitude);
                }
                coreRepo.membershipRepo.EditProfile(id,latitude,longitude,model.firstname,model.lastname,model.description,model.icq,model.phone,model.profilclose,model.site,model.skype);
                return RedirectToAction("Profile", new { id });
                
            }
            if (GeoSetting.unlockGeo())
                model.bodyonload = "GetMap();";
            TempData["username"] = model.username = id;
            TempData["useremail"] = model.username = MytripUser.UserEmail(id);
            var x = coreRepo.membershipRepo.GetProfile(id);
            model.UserProfile = x;
            return View(model);
        }
        public ActionResult Page(int id)
        {
            CorePageModel model = new CorePageModel();
            model.id = id;
            var x = coreRepo.corePageRepo.GetPages(id);
            model.pages = x;
            if (x.EmailForm && EmailSetting.unlockSendEmail())
                model.approvedemail = true;
            else
                model.approvedemail = false;
            model.title = x.Title;
            model.body = new HtmlString(x.Body);
            model.sideBar = x.SideBar;
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = MytripUser.UserEmail();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Page(int id,CorePageModel model)
        {
            coreRepo.emailRepo.SendEmail(EmailSetting.from_email(), (model.email + " " + model.name), model.messege);
            model.approvedemail = EmailSetting.unlockSendEmail();
            model.id = id;
            var x = coreRepo.corePageRepo.GetPages(id);
            model.pages = x;
            if (x.EmailForm && EmailSetting.unlockSendEmail())
                model.approvedemail = true;
            else
                model.approvedemail = false;
            model.title = x.Title;
            model.body = new HtmlString(x.Body);
            model.sideBar = x.SideBar;
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = MytripUser.UserEmail();
            }
            return View(model);
        }
        public ActionResult Search(int id, int id2, string id3)
        {
            HomeSearchModel model = new HomeSearchModel();
            int total = 0;
            model.page = id;
            model.count = id2;
            model.search = GeneralMethods.UndecodingSearch(id3);
            model._total = /*id == 1 ?*/ true /*: false*/;
            model.defsize = 10;
            model.total = total;
            model.title = CoreLanguage.search;
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(string search, string url)
        {
            if (search != string.Empty)
            {
                search = GeneralMethods.DecodingSearch(search);
                return RedirectToAction("Search", new { id = 1, id2 = 10, id3=search.Trim() });
            }
            else
                return Redirect(url);
        }
        [Statistic]
        public ActionResult Statistic(string id,string id2)
        {
           
            if(id==null)
                id = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            if(id2==null)
                id2 = DateTime.Now.ToString("yyyy-MM-dd");
            string _start = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Parse(_start);
            DateTime start = DateTime.Parse(_start);
            DateTime.TryParse(id, out start);
            DateTime end = DateTime.Parse(_start);
            DateTime.TryParse(id2, out end);
            StatisticModel model = new StatisticModel();
            if(GeoSetting.unlockGeo())
            model.bodyonload = "GetMap();";
            if (start >= today&&end >= today)
            {
                model.title = string.Format(CoreLanguage.statisticTitle,DateTime.Now.ToString("dd MMMM yyyy"));
                model.body = coreRepo.statisticRepo.GetTodayStatistic();
            }
            else
            {
                if(start<end)
                model.title = string.Format(CoreLanguage.statisticTitle, start.ToString("dd MMMM yyyy") + " - " + end.ToString("dd MMMM yyyy"));
                else if(start>end)
                    model.title = string.Format(CoreLanguage.statisticTitle, end.ToString("dd MMMM yyyy") + " - " + start.ToString("dd MMMM yyyy"));
                else if (start==end)
                    model.title = string.Format(CoreLanguage.statisticTitle, start.ToString("dd MMMM yyyy"));
                
                model.body = coreRepo.statisticRepo.GetStatistic(start,end);
            }
            if (start <= end)
            {
                model.DateStart = start.ToString("yyyy-MM-dd");
                model.DateEnd = end.ToString("yyyy-MM-dd");
            }
            else
            {
                model.DateStart = end.ToString("yyyy-MM-dd");
                model.DateEnd = start.ToString("yyyy-MM-dd");
            }
            
            return View(model);
        }
        [Statistic]
        [HttpPost]
        public ActionResult Statistic(StatisticModel model)
        {
            string _start = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Parse(_start);
            DateTime start = DateTime.Parse(_start);
            DateTime.TryParse(model.DateStart, out start);
            DateTime end = DateTime.Parse(_start);
            DateTime.TryParse(model.DateEnd, out end);
            return RedirectToAction("Statistic", new { id = start.ToString("yyyy-MM-dd"), id2 = end.ToString("yyyy-MM-dd") });
        }
    }
}
