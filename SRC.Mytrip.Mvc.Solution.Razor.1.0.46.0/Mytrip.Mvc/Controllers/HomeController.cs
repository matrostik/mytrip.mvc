/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Controllers
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
                model.title = CoreLanguage.license_agreement;
            else
                model.title = CoreSetting.NameHomePage();
            model.developer = develop;
            return View(model);
        }

        /// <summary>GET: /Home/About
        /// Страница About
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult About()
        {
            AboutModel model = new AboutModel();
            model.body = new HtmlString(coreRepo.aboutRepo.GetAbout(LocalisationSetting.culture()));
            model.approvedemail = EmailSetting.unlockSendEmail();
            model.title = CoreSetting.NameAboutPage();
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = MytripUser.UserEmail();
            }
            return View(model);
        }

        /// <summary>POST: /Home/About
        /// Отправка письма со страницы About
        /// </summary>
        /// <param name="model">AboutModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult About(AboutModel model)
        {
            coreRepo.emailRepo.SendEmail(EmailSetting.from_email(), (model.email + " " + model.name), model.messege);
            model.body = new HtmlString(coreRepo.aboutRepo.GetAbout(LocalisationSetting.culture()));
            model.approvedemail = EmailSetting.unlockSendEmail();
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = MytripUser.UserEmail();
            }
            return View(model);
        }

        /// <summary>GET: /Home/Profile
        /// Профиль пользователя
        /// </summary>
        /// <param name="id">имя пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult Profile(string id)
        {
            ProfileUsersModel model = new ProfileUsersModel();
            model.UserName = id;
            model.Email = MytripUser.UserEmail(id);
            return View(model);
        }
    }
}
