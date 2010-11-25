using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Tourism.Repository;
using Mytrip.Tourism.Models;
using Mytrip.Mvc.Settings;
using Mytrip.Mvc;
using Mytrip.Tourism.Helpers;
using System.Net.Mail;
using Mytrip.Mvc.Repository;

namespace Mytrip.Tourism.Controllers
{
    public class ToursController : Controller
    {
        private IToursRepository _ToursRepository;

        /// <summary>
        /// 
        /// </summary>
        public IToursRepository tour
        {
            get
            {
                if (_ToursRepository == null)
                    _ToursRepository = new IToursRepository();
                return _ToursRepository;
            }
        }
        public ActionResult Index(int id, int id2, int id3, string id4)
        {
            int total = 0;
            int pagesize = int.MaxValue;
            if (!ModuleSetting.paginalTours())
                id = 1;
            else
                pagesize = id2 * ModuleSetting.columnTours();
            ToursIndexModel model = new ToursIndexModel();
            model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
            if (id3 == 0)
            {
                model.PageTitle = ModuleSetting.nameTours();
                model.CategoryOnly = null;
                model.Tours = tour.tours.GetAllTours(id, pagesize, LocalisationSetting.culture(), out total);
            }
            else
            {
                var category = tour.category.GetCategory(id3);
                model.PageTitle = category.Title;
                model.CategoryOnly = category;
                model.Tours = tour.tours.GetToursForCategory(id, pagesize, id3, LocalisationSetting.culture(), out total);
            }
            model.total = total;

            return View(model);
        }
        public ActionResult View(int id)
        {
            TourViewModel model = new TourViewModel();
            model.Tours = tour.tours.GetTour(id);
            model.PageTitle = model.Tours.Title;
            return View(model);
        }
        [RoleTour]
        public ActionResult EditorCategory(int id, string id2)
        {
            EditorCategoryModel model = new EditorCategoryModel();
            if (id2 == "CreateCategory")
            {
                model.submit = CoreLanguage.create;
                if (id == 0)
                    model.TitlePage = ToursLanguage.createcategory;
                else
                {
                    var a = tour.category.GetCategory(id);
                    model.TitlePage = string.Format(ToursLanguage.createsubcategory, a.Title);
                }
            }
            else if (id2 == "EditCategory")
            {
                model.submit = CoreLanguage.edit;
                var a = tour.category.GetCategory(id);
                if (a.SubCategoryId == 0)
                    model.TitlePage = string.Format(ToursLanguage.editcategory, a.Title);
                else
                    model.TitlePage = string.Format(ToursLanguage.editsubcategory, a.Title, a.mytrip_tourscategory2.Title);
                model.title = a.Title;
                model.body = a.Body;
                model.allculture = a.AllCulture;
            }
            else if (id2 == "DeleteCategory")
            {
                var a = tour.category.GetCategory(id);
                int cat = a.SubCategoryId;
                string path = "Tours";
                if (cat > 0)
                    path = a.mytrip_tourscategory2.Path;
                tour.category.DeleteCategory(id);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = cat, id4 = path });
            }
            return View(model);
        }
        [RoleTour]
        [HttpPost]
        public ActionResult EditorCategory(int id, string id2, EditorCategoryModel model)
        {
            if (ModelState.IsValid && id2 == "CreateCategory")
            {
                var x = tour.category.CreateCategory(model.title, model.allculture, model.body, LocalisationSetting.culture(), id);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = x.CategoryId, id4 = x.Path });

            }
            if (ModelState.IsValid && id2 == "EditCategory")
            {
                var x = tour.category.EditCategory(id, model.title, model.allculture, model.body);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = x.CategoryId, id4 = x.Path });

            }
            return View(model);
        }
        [RoleTour]
        public ActionResult EditorTour(int id, string id2)
        {
            EditorTourModel model = new EditorTourModel();
            if (id2 == "CreateTour")
            {
                model.tourid = 0;
                model.startdate = DateTime.Now.ToString("yyyy-MM-dd");
                model.stopdate = DateTime.Now.ToString("yyyy-MM-dd");
                model.TitlePage = ToursLanguage.createtour;
                model.submit = CoreLanguage.create;
                model.categoryid = id;
                model.varianty = tour.variants.GetVariantsForTour(0);
                model.category = new SelectList(tour.category.GetAllCategoryDdl(LocalisationSetting.culture()), "key", "value", model.categoryid);
            }
            else if (id2 == "EditTour")
            {
                model.tourid = id;
                model.TitlePage = ToursLanguage.edittour;
                model.submit = CoreLanguage.edit;
                var a = tour.tours.GetTour(id);
                model.categoryid = a.CategoryId;
                model.varianty = tour.variants.GetVariantsForTour(id);
                model.category = new SelectList(tour.category.GetAllCategoryDdl(LocalisationSetting.culture()), "key", "value", model.categoryid);
                model.allculture = a.AllCulture;
                model.body = a.Body;
                model.startdate = a.StartDate.ToString("yyyy-MM-dd");
                model.stopdate = a.StopDate.ToString("yyyy-MM-dd");
                model.title = a.Title;
                model.image = a.Imige;
            }
            else if (id2 == "DeleteTour")
            {
                var a = tour.tours.GetTour(id);
                int catid = a.CategoryId;
                string path = a.Path;
                tour.tours.DeleteTour(id);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = catid, id4 = path });
            }
            model.momeyid = ModuleSetting.keyMoney();
            model.money = new SelectList(MoneyHelpers.CultureMoney(), "key", "value", model.momeyid);
            return View(model);
        }

        [RoleTour]
        [HttpPost]
        public ActionResult EditorTour(int id, string id2, EditorTourModel model)
        {
            if (ModelState.IsValid && id2 == "CreateTour")
            { 
               var a = tour.variants.GetPriceForTour(0);
               var x = tour.tours.CreateTour(model.title, model.body, model.categoryid,
                   DateTime.Parse(model.stopdate), DateTime.Parse(model.stopdate).AddDays(ModuleSetting.closeTour()),
                   DateTime.Parse(model.startdate), model.image, 0, 0, a.Price, a.MoneyId,
                   model.allculture, LocalisationSetting.culture());
               tour.variants.MoveVariants(x.TourId);
               return RedirectToAction("View", new { id = x.TourId, id2 = x.Path });
            }
            else if (ModelState.IsValid && id2 == "EditTour")
            { var a = tour.variants.GetPriceForTour(id);
            var x = tour.tours.EditTour(id, model.title, model.body, model.categoryid,
                DateTime.Parse(model.stopdate), DateTime.Parse(model.stopdate).AddDays(ModuleSetting.closeTour()),
                   DateTime.Parse(model.startdate), model.image, 0, 0, a.Price, a.MoneyId, model.allculture);
            return RedirectToAction("View", new { id = x.TourId, id2 = x.Path });
            }
            if (id2 == "CreateTour")
            {
                model.tourid = 0;
                model.varianty = tour.variants.GetVariantsForTour(0);
                model.TitlePage = ToursLanguage.createtour;
                model.submit = CoreLanguage.create;
            }
            if (id2 == "EditTour")
            {
                model.tourid = id;
                model.varianty = tour.variants.GetVariantsForTour(id);
                model.TitlePage = ToursLanguage.edittour;
                model.submit = CoreLanguage.edit;
            }
            model.momeyid = ModuleSetting.keyMoney();
            model.money = new SelectList(MoneyHelpers.CultureMoney(), "key", "value", model.momeyid);
            model.category = new SelectList(tour.category.GetAllCategoryDdl(LocalisationSetting.culture()), "key", "value", model.categoryid);
            return View(model);
        }
        [RoleTour]
        [HttpPost]
        public ActionResult AddVariant(EditorTourModel model)
        {
            if (model.hotel!=null&&model.price!=null)
            {
                decimal pr = 0;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                    decimal.TryParse(model.price.Replace(".", ","), out pr);
                if (LocalisationSetting.culture().ToLower() == "en-us")
                    decimal.TryParse(model.price.Replace(",", "."), out pr);
                tour.variants.CreateVariant(model.tourid, model.hotel, model.services, pr, model.momeyid, 0, 0);
                var a = tour.variants.GetVariantsForTour(model.tourid);
                return Content(ToursHelper._TourVariantyForEditor(a).ToString());
            }
            else
                return Content("");
        }
        [RoleTour]
        [HttpPost]
        public ActionResult DeleteVariant(int id)
        {
            int tor=tour.variants.DeleteVariant(id);
            var a = tour.variants.GetVariantsForTour(tor);
            return Content(ToursHelper._TourVariantyForEditor(a).ToString());
        }
        public ActionResult TourModule()
        {
            return View();
        }
        public ActionResult OrderTour()
        {
            OrderTourModel model = new OrderTourModel();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                model.Name = HttpContext.User.Identity.Name;
                model.Email = MytripUser.UserEmail(HttpContext.User.Identity.Name);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult OrderTour(OrderTourModel model)
        {
            try
            {
                DateTime start = DateTime.MinValue;
                DateTime stop = DateTime.MinValue;
                DateTime.TryParse(model.StartDate, out start);
                DateTime.TryParse(model.StopDate, out stop);
                string body = CoreLanguage.UserName + ": " + model.Name + "<br/>"
                    + CoreLanguage.Email + ": " + model.Email + "<br/>"
                    + ToursLanguage.Phone + ": " + model.Phone + "<br/>"
                    + ToursLanguage.Country + ": " + model.Country + "<br/>"
                    + ToursLanguage.FromCity + ": " + model.FromCity + "<br/>"
                    + ToursLanguage.CountPeople + ": " + model.CountPeople + "<br/>"
                    + ToursLanguage.Hotel + ": " + model.Hotel + "<br/>"
                    + ToursLanguage.Resort + ": " + model.Resort + "<br/>"
                    + string.Format(ToursLanguage.DateTour2, start, stop)
                    + "<br/>" + model.Body;
                string domain = UsersSetting.applicationName();
                string domainlink = "<a href='http://" + domain + "'>" + domain + "</a>";
                MailMessage msg = new MailMessage();
                msg.To.Add(EmailSetting.from_email());
                msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                msg.Subject = string.Format(CoreSetting.NameTitlePage(), model.Name);
                msg.Body = body + "<br/>" + domainlink;
                msg.IsBodyHtml = true;
                EmailSetting.SendEmail(msg);
            }
            catch { }
            return View(model);
        }
    }
}
