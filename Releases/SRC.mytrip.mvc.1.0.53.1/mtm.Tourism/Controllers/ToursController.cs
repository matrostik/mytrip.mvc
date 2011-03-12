using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Tourism.Repository;
using mtm.Tourism.Models;
using mtm.Core.Settings;
using mtm.Core;
using mtm.Tourism.Helpers;
using System.Net.Mail;
using mtm.Core.Repository;
using System.Xml.Linq;

namespace mtm.Tourism.Controllers
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
        public ActionResult Index(int id, int id2, int id3,int id4, string id5,string id6,string id7)
        {
            int total = 0;
            int pagesize = int.MaxValue;
            pagesize = id2 * ModuleSetting.columnTours();
            ToursIndexModel model = new ToursIndexModel();
            model.__category = id3;
            model.__country = id4;
            model.__categorylist = new SelectList(tour.category.GetAllCategoryDdlsearch(LocalisationSetting.culture()),"Key","Value",model.__category);
            model.__countrylist = new SelectList(tour.category.GetAllCountryDdlsearch(LocalisationSetting.culture()), "Key", "Value", model.__country);
            model.__startdate = (id6 == null) ? DateTime.Now.ToString("yyyy-MM-dd") : id6;
            model.__stopdate = (id7 == null) ? DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd") : id7;
            model.Category = null;
            model._country = false;
            if (id4 == 0)
                model.countryview = true;
            else
                model.countryview = false;
            if (id3 == 0&&id4==0&&id5=="Tours")
            {
                model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
                string[] a = { ModuleSetting.nameTours() };
                model.bread = a;
                model.PageTitle = ModuleSetting.nameTours();
                model.seoTitle = ModuleSetting.ToursTitle();
                model.seokeywords = ModuleSetting.ToursKeyWords();
                model.seodescription = ModuleSetting.ToursDesc();
                model.CategoryOnly = null;
                model.Tours = tour.tours.GetAllTours(id, pagesize, LocalisationSetting.culture(), out total);
            }
            else if (id3 == 0 && id4 == 0 && id5 == "Country")
            {
                model._country = true;
                model.Country = tour.category.GetAllCountry(LocalisationSetting.culture());
                string[] a = { "<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                 ModuleSetting.nameCountry() };
                model.bread = a;
                model.PageTitle = ModuleSetting.nameCountry();
                model.seoTitle = ModuleSetting.CountryTitle();
                model.seokeywords = ModuleSetting.CountryKeyWords();
                model.seodescription = ModuleSetting.CountryDesc();
                model.CategoryOnly = null;
                model.Tours = tour.tours.GetAllTours(id, pagesize, LocalisationSetting.culture(), out total);
            }
            else if (id3 != 0 && id4 == 0 && !id5.Contains("(Search)"))
            {
                model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
                var category = tour.category.GetCategory(id3);
                if (category.SubCategoryId == 0)
                {
                    string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                 category.Title};
                    model.bread = a;
                }
                else {
                    string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+category.SubCategoryId+"/0/"+category.mytrip_tourscategory2.Path+"'>"+ category.mytrip_tourscategory2.Title+"</a>",
                                 category.Title};
                    model.bread = a;
                }
                model.seoTitle = category.SeoTitle;
                model.seokeywords = category.SeoKeyword;
                model.seodescription = category.SeoDescription;
                model.PageTitle = category.Title;
                model.CategoryOnly = category;
                model.Tours = tour.tours.GetToursForCategory(id, pagesize, id3, LocalisationSetting.culture(), out total);
            }
            else if (id3 == 0 && id4 != 0 && !id5.Contains("(Search)"))
            {
                model._country = true;
                var category = tour.category.GetCountry(id4);
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                 "<a href='/Tours/Index/1/10/0/0/Country'>"+ ModuleSetting.nameCountry()+"</a>",
                                 category.Title};
                    model.bread = a;
                model.seoTitle = category.SeoTitle;
                model.seokeywords = category.SeoKeyword;
                model.seodescription = category.SeoDescription;
                model.PageTitle = category.Title;
                model.CountryOnly = category;
                model.Tours = tour.tours.GetToursForCategory(id, pagesize, id3, LocalisationSetting.culture(), out total);
            }
            else if (id5.Contains("(Search)"))
            {
                id5 = id5.Replace("(Search)","");
               id5 = GeneralMethods.UndecodingSearch(id5);
               if (id5 != "x")
                model.__search = id5;                
                if (id5 == "x") {
                    model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
                    string[] a = { ModuleSetting.nameTours() };
                    model.bread = a;
                    model.PageTitle = ModuleSetting.nameTours();
                    model.seoTitle = ModuleSetting.ToursTitle();
                    model.seokeywords = ModuleSetting.ToursKeyWords();
                    model.seodescription = ModuleSetting.ToursDesc();
                    model.CategoryOnly = null;
                }
                else if (id5 != "x")
                {
                    model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
                    var category = (id3 == 0 && id4 != 0) ? tour.category.GetCountry(id4) : null;
                    string[] a = { "<a href='/Tours/Index/1/10/0/0/Tours'>" + ModuleSetting.nameTours() + "</a>","search" };
                    model.bread = a;
                    model.seoTitle = ModuleSetting.CountryTitle();
                    model.seokeywords = ModuleSetting.CountryKeyWords();
                    model.seodescription = ModuleSetting.CountryDesc();
                    model.PageTitle = "Search";
                    model.CountryOnly = null;
                }
                var _tour = tour.tours.GetToursForSearch(id, id2, id3, id4, LocalisationSetting.culture(), id5, id6, id7, out total);
                if (id5 != "x")
                {
                    foreach (var art in _tour)
                    {
                        art.Title = GeneralMethods.ReplaceString(art.Title, id5);
                        art.Body = GeneralMethods.ReplaceString(art.Body, id5);
                    }
                }
                model.Tours = _tour;
            }
            model.total = total;

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ToursIndexModel model)
        {
            string Search = string.Empty;
            if (model.__search != null)
                Search = "(Search)"+GeneralMethods.DecodingSearch(model.__search);
            else
                Search = "(Search)x";
            return RedirectToAction("Index", new {id=1,id2=10,id3=model.__category,id4=model.__country,id5=Search,id6=model.__startdate,id7=model.__stopdate });
            
        }
        [RoleTour]
        public ActionResult Arhiv(int id, int id2, int id3,int id4, string id5)
        {
            id4 = 0;
            int total = 0;
            int pagesize = int.MaxValue;
            pagesize = id2 * ModuleSetting.columnTours();
            ToursIndexModel model = new ToursIndexModel();
            model.Country = null;
            model.Category = tour.category.GetSubCategory(id3, LocalisationSetting.culture());
            if (id3 == 0)
            {
                string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                     ToursLanguage.arhiv };
                model.bread = a;
                model.PageTitle = ToursLanguage.arhiv;
                model.seoTitle = ToursLanguage.arhiv;
                model.seokeywords = ModuleSetting.ToursKeyWords();
                model.seodescription = ModuleSetting.ToursDesc();
                model.CategoryOnly = null;
                model.Tours = tour.tours.GetAllToursArhiv(id, pagesize, LocalisationSetting.culture(), out total);
            }
            else
            {
                var category = tour.category.GetCategory(id3);
                if (category.SubCategoryId == 0)
                {
                    string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                     "<a href='/Tours/Arhiv/1/10/0/0/Tours'>"+ ToursLanguage.arhiv+"</a>",
                                 category.Title};
                    model.bread = a;
                }
                else
                {
                    string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                                     "<a href='/Tours/Arhiv/1/10/0/0/Tours'>"+ ToursLanguage.arhiv+"</a>",
                                     "<a href='/Tours/Arhiv/1/10/"+category.SubCategoryId+"/0/"+category.mytrip_tourscategory2.Path+"'>"+ category.mytrip_tourscategory2.Title+"</a>",
                                 category.Title};
                    model.bread = a;
                }
                model.seoTitle = category.SeoTitle;
                model.seokeywords = category.SeoKeyword;
                model.seodescription = category.SeoDescription;
                model.PageTitle = category.Title;
                model.CategoryOnly = category;
                model.Tours = tour.tours.GetToursForCategoryArhiv(id, pagesize, id3, LocalisationSetting.culture(), out total);
            }
            model.total = total;

            return View(model);
        }
        public ActionResult View(int id)
        {
            TourViewModel model = new TourViewModel();
            model.Tours = tour.tours.GetTour(id);
            if (model.Tours.mytrip_tourscategory.SubCategoryId == 0)
            {
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+model.Tours.CategoryId+"/0/"+model.Tours.mytrip_tourscategory.Path+"'>"+ model.Tours.mytrip_tourscategory.Title+"</a>",
                                 model.Tours.Title};
                model.bread = a;
            }
            else
            {
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+model.Tours.mytrip_tourscategory.SubCategoryId+"/0/"+model.Tours.mytrip_tourscategory.mytrip_tourscategory2.Path+"'>"+ model.Tours.mytrip_tourscategory.mytrip_tourscategory2.Title+"</a>",
                                 "<a href='/Tours/Index/1/10/"+model.Tours.CategoryId+"/0/"+model.Tours.mytrip_tourscategory.Path+"'>"+ model.Tours.mytrip_tourscategory.Title+"</a>",
                                 model.Tours.Title};
                model.bread = a;
            }
            model.PageTitle = model.Tours.Title + " (<a href='/Tours/Index/1/10/0/" + model.Tours.CountryId + "/" + model.Tours.mytrip_tourscountry.Path + "'>" + model.Tours.mytrip_tourscountry.Title + "</a>)";
            model.seoTitle = model.Tours.SeoTitle;
            model.seokeywords = model.Tours.SeoKeyword;
            model.seodescription = model.Tours.SeoDescription;
            return View(model);
        }
        [RoleTour]
        public ActionResult EditorCategory(int id, string id2)
        {
            EditorCategoryModel model = new EditorCategoryModel();
            model.id = id;
            model.id2 = id2;
            if (id2 == "CreateCategory")
            {
                model.submit = CoreLanguage.create;                
                if (id == 0)
                { model.TitlePage = ToursLanguage.createcategory;
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     model.TitlePage};
                model.bread = a;
                model.seodescription = ModuleSetting.ToursDesc();
                model.seokeywords = ModuleSetting.ToursKeyWords();
                }
                else
                {
                    var a = tour.category.GetCategory(id);

                    model.TitlePage = string.Format(ToursLanguage.createsubcategory, a.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+id+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                 model.TitlePage};
                    model.bread = _a;
                    model.seodescription = a.SeoDescription;
                    model.seokeywords = a.SeoKeyword;
                }
            }
            else if (id2 == "EditCategory")
            {
                model.submit = CoreLanguage.edit;
                var a = tour.category.GetCategory(id);
                if (a.SubCategoryId == 0)
                {
                    model.TitlePage = string.Format(ToursLanguage.editcategory, a.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                      "<a href='/Tours/Index/1/10/"+a.CategoryId+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                     model.TitlePage};
                    model.bread = _a;
                }
                else
                {
                
                    model.TitlePage = string.Format(ToursLanguage.editsubcategory, a.Title, a.mytrip_tourscategory2.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+a.SubCategoryId+"/0/"+a.mytrip_tourscategory2.Path+"'>"+ a.mytrip_tourscategory2.Title+"</a>",
                                     "<a href='/Tours/Index/1/10/"+a.CategoryId+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                 model.TitlePage};
                    model.bread = _a;
                }
                model.seodescription = a.SeoDescription;
                model.seokeywords = a.SeoKeyword;
                model.seoTitle = a.SeoTitle;
                model.path = a.Path;
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
                if(HttpContext.Request.UrlReferrer.ToString().Contains("/Arhiv/"))
                    return RedirectToAction("Arhiv", new { id = 1, id2 = 10, id3 = cat,id4=0, id5 = path });
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = cat,id4=0, id5 = path });
            }
            if (id2 == "CreateCountry")
            {
                model.submit = CoreLanguage.create;
                model.TitlePage = ToursLanguage.createcountry;
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Country'>"+ ModuleSetting.nameCountry()+"</a>",
                                     model.TitlePage};
                    model.bread = a;
                    model.seodescription = ModuleSetting.CountryDesc();
                    model.seokeywords = ModuleSetting.CountryKeyWords();
                
            }
            else if (id2 == "EditCountry")
            {
                model.submit = CoreLanguage.edit;
                var a = tour.category.GetCountry(id);
                    model.TitlePage = string.Format(ToursLanguage.editcountry, a.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Country'>"+ ModuleSetting.nameCountry()+"</a>",
                                      "<a href='/Tours/Index/1/10/0/"+a.CountryId+"/"+a.Path+"'>"+ a.Title+"</a>",
                                     model.TitlePage};
                    model.bread = _a;                
                model.seodescription = a.SeoDescription;
                model.seokeywords = a.SeoKeyword;
                model.seoTitle = a.SeoTitle;
                model.path = a.Path;
                model.title = a.Title;
                model.body = a.Body;
                model.allculture = a.AllCulture;
            }
            else if (id2 == "DeleteCountry")
            {
                string path = "Country";
                tour.category.DeleteCountry(id);
                if (HttpContext.Request.UrlReferrer.ToString().Contains("/Arhiv/"))
                    return RedirectToAction("Arhiv", new { id = 1, id2 = 10, id3 = 0,id4=0, id5 = path });
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = 0,id4=0, id5 = path });
            }
            return View(model);
        }
        [RoleTour]
        [HttpPost]
        public ActionResult EditorCategory(EditorCategoryModel model)
        {
            if (ModelState.IsValid && model.id2 == "CreateCategory")
            {
                var x = tour.category.CreateCategory(model);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = x.CategoryId,id4=0, id5 = x.Path });

            }
            else if (ModelState.IsValid && model.id2 == "EditCategory")
            {
                var x = tour.category.EditCategory(model);
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = x.CategoryId,id4=0, id5 = x.Path });

            }
            else if (ModelState.IsValid && model.id2 == "CreateCountry")
            {
                var x = tour.category.CreateCountry(model);
                return RedirectToAction("Index", new { id = 1, id2 = 10,id3=0, id4 = x.CountryId, id5 = x.Path });

            }
            else if (ModelState.IsValid && model.id2 == "EditCountry")
            {
                var x = tour.category.EditCountry(model);
                return RedirectToAction("Index", new { id = 1, id2 = 10,id3=0, id4 = x.CountryId, id5 = x.Path });

            }
            if (model.id2 == "CreateCategory")
            {
                model.submit = CoreLanguage.create;
                if (model.id == 0)
                {
                    model.TitlePage = ToursLanguage.createcategory;
                    string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     model.TitlePage};
                    model.bread = a;
                }
                else
                {
                    var a = tour.category.GetCategory(model.id);
                    model.TitlePage = string.Format(ToursLanguage.createsubcategory, a.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+model.id+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                 model.TitlePage};
                    model.bread = _a;
                }
            }
            else if (model.id2 == "EditCategory")
            {
                model.submit = CoreLanguage.edit;
                var a = tour.category.GetCategory(model.id);
                if (a.SubCategoryId == 0)
                {
                    model.TitlePage = string.Format(ToursLanguage.editcategory, a.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                      "<a href='/Tours/Index/1/10/"+a.CategoryId+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                     model.TitlePage};
                    model.bread = _a;
                }
                else
                {
                    model.TitlePage = string.Format(ToursLanguage.editsubcategory, a.Title, a.mytrip_tourscategory2.Title);
                    string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     "<a href='/Tours/Index/1/10/"+a.SubCategoryId+"/0/"+a.mytrip_tourscategory2.Path+"'>"+ a.mytrip_tourscategory2.Title+"</a>",
                                     "<a href='/Tours/Index/1/10/"+a.CategoryId+"/0/"+a.Path+"'>"+ a.Title+"</a>",
                                 model.TitlePage};
                    model.bread = _a;
                }
            }
            else if (model.id2 == "CreateCountry")
            {
                model.submit = CoreLanguage.create;
                model.TitlePage = ToursLanguage.createcountry;
                string[] a = {"<a href='/Tours/Index/1/10/0/0/Country'>"+ ModuleSetting.nameTours()+"</a>",
                                     model.TitlePage};
                model.bread = a;

            }
            else if (model.id2 == "EditCountry")
            {
                model.submit = CoreLanguage.edit;
                var a = tour.category.GetCountry(model.id);
                model.TitlePage = string.Format(ToursLanguage.editcountry, a.Title);
                string[] _a = {"<a href='/Tours/Index/1/10/0/0/(Country)Tours'>"+ ModuleSetting.nameCountry()+"</a>",
                                      "<a href='/Tours/Index/1/10/0/"+a.CountryId+"/"+a.Path+"'>"+ a.Title+"</a>",
                                     model.TitlePage};
                model.bread = _a;
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
                model.country = new SelectList(tour.category.GetAllCountryDdl(LocalisationSetting.culture()), "key", "value");
                if (id > 0) {
                    var cat=tour.category.GetCategory(id);
                    model.seodescription = cat.SeoDescription;
                    model.seokeywords = cat.SeoKeyword;
                } else {
                    model.seodescription = ModuleSetting.ToursDesc();
                    model.seokeywords = ModuleSetting.ToursKeyWords();
                }
                string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.createtour};
                model.bread = _a;
            }
            else if (id2 == "EditTour")
            {
                model.tourid = id;
                model.TitlePage = ToursLanguage.edittour;
                model.submit = CoreLanguage.edit;
                var a = tour.tours.GetTour(id);
                model.categoryid = a.CategoryId;
                model.countryid = a.CountryId;
                model.varianty = tour.variants.GetVariantsForTour(id);
                model.category = new SelectList(tour.category.GetAllCategoryDdl(LocalisationSetting.culture()), "key", "value", model.categoryid);
                model.country = new SelectList(tour.category.GetAllCountryDdl(LocalisationSetting.culture()), "key", "value", model.countryid);
                model.allculture = a.AllCulture;
                model.body = a.Body;
                model.startdate = a.StartDate.ToString("yyyy-MM-dd");
                model.stopdate = a.StopDate.ToString("yyyy-MM-dd");
                model.title = a.Title;
                model.image = a.Imige;
                model.path = a.Path;
                model.seoTitle = a.SeoTitle;
                model.seokeywords = a.SeoKeyword;
                model.seodescription = a.SeoDescription;
                string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.edittour};
                model.bread = _a;
            }
            else if (id2 == "DeleteTour")
            {
                var a = tour.tours.GetTour(id);
                int catid = a.CategoryId;
                string path = a.Path;
                tour.tours.DeleteTour(id);
                if (HttpContext.Request.UrlReferrer.ToString().Contains("/Arhiv/"))
                    return RedirectToAction("Arhiv", new { id = 1, id2 = 10, id3 = catid,id4=0, id5 = path });
                return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = catid,id4=0, id5 = path });
            }
            model.momeyid = ModuleSetting.keyMoney();
            model.money = new SelectList(MoneyHelpers.CultureMoney(), "key", "value", model.momeyid);
            return View(model);
        }
        [RoleTour]
        [HttpPost]
        public ActionResult EditorTour(EditorTourModel model)
        {
            if (ModelState.IsValid && model.id2 == "CreateTour")
            { 
               var x = tour.tours.CreateTour(model);
               tour.variants.MoveVariants(x.TourId);
               return RedirectToAction("View", new { id = x.TourId, id2 = x.Path });
            }
            else if (ModelState.IsValid && model.id2 == "EditTour")
            {
                var x = tour.tours.EditTour(model);
            return RedirectToAction("View", new { id = x.TourId, id2 = x.Path });
            }
            if (model.id2 == "CreateTour")
            {
                string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.createtour};
                model.bread = _a;
                model.tourid = 0;
                model.varianty = tour.variants.GetVariantsForTour(0);
                model.TitlePage = ToursLanguage.createtour;
                model.submit = CoreLanguage.create;
            }
            if (model.id2 == "EditTour")
            {
                string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.edittour};
                model.bread = _a;
                model.tourid = model.id;
                model.varianty = tour.variants.GetVariantsForTour(model.id);
                model.TitlePage = ToursLanguage.edittour;
                model.submit = CoreLanguage.edit;
            }
            model.momeyid = ModuleSetting.keyMoney();
            model.money = new SelectList(MoneyHelpers.CultureMoney(), "key", "value", model.momeyid);
            model.category = new SelectList(tour.category.GetAllCategoryDdl(LocalisationSetting.culture()), "key", "value", model.categoryid);
            model.country = new SelectList(tour.category.GetAllCountryDdl(LocalisationSetting.culture()), "key", "value", model.countryid);
               
            return View(model);
        }
        [RoleTour]
        [HttpPost]
        public ActionResult AddVariant(EditorTourModel model)
        {
            if (model.hotel!=null&&model.price!=null)
            {
               tour.variants.CreateVariant(model);
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
        public ActionResult OrderTour(int? id)
        {
            OrderTourModel model = new OrderTourModel();
            model.PageTitle = ModuleSetting.nameOrderTours();
            model.seoTitle = ModuleSetting.OrderToursTitle();
            model.seokeywords = ModuleSetting.OrderToursKeyWords();
            model.seodescription = ModuleSetting.OrderToursDesc();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                model.Name = HttpContext.User.Identity.Name;
                model.Email = MytripUser.UserEmail(HttpContext.User.Identity.Name);
            }
            if (id != null) {
                var x = tour.variants.GetVariant((int)id);
                string hotel = x.Hotel;
                if (hotel.Contains("href"))
                {
                    string[] _hotel = hotel.Split('>');
                    bool add = false;
                    foreach (var a in _hotel) {
                        if (add)
                        {
                            hotel = a;
                            break;
                        }
                        else {
                            if (a.Contains("href"))
                                add = true;
                        }
                    }
                }
                model.Hotel = hotel;
                model.StartDate = x.mytrip_tours.StartDate.ToString("yyyy-MM-dd");
                model.StopDate=x.mytrip_tours.StopDate.ToString("yyyy-MM-dd");
                model.Resort = x.mytrip_tours.Title;
                model.Country = x.mytrip_tours.mytrip_tourscountry.Title;
            }
            string[] _a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.ordertour};
            model.bread = _a;
            model.mess = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult OrderTour(OrderTourModel model)
        {
            if (ModelState.IsValid)
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
                    string domain = CoreSetting.applicationName();
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
            }
            string[] a = {"<a href='/Tours/Index/1/10/0/0/Tours'>"+ ModuleSetting.nameTours()+"</a>",
                                     ToursLanguage.ordertour};
            model.bread = a;
            model.mess = true;
            return View(model);
        }
        [RoleTour]
        public ActionResult Setting() {
            TourSetting model = new TourSetting();
            model.closeTour = ModuleSetting.closeTour();
            model.columnTours = ModuleSetting.columnTours();
            model.MoneyProcent = ModuleSetting.MoneyProcent();
            model.nameCountry_description_en_us = ModuleSetting.CountryDesc("en-us");
            model.nameCountry_description_ru_ru = ModuleSetting.CountryDesc("ru-ru");
            model.nameCountry_en_us = ModuleSetting.nameCountry("en-us");
            model.nameCountry_keywords_en_us = ModuleSetting.CountryKeyWords("en-us");
            model.nameCountry_keywords_ru_ru = ModuleSetting.CountryKeyWords("ru-ru");
            model.nameCountry_ru_ru = ModuleSetting.nameCountry("ru-ru");
            model.nameCountry_title_en_us = ModuleSetting.CountryTitle("en-us");
            model.nameCountry_title_ru_ru = ModuleSetting.CountryTitle("ru-ru");
            model.nameOrderTours_description_en_us = ModuleSetting.OrderToursDesc("en-us");
            model.nameOrderTours_description_ru_ru = ModuleSetting.OrderToursDesc("ru-ru");
            model.nameOrderTours_en_us = ModuleSetting.nameOrderTours("en-us");
            model.nameOrderTours_keywords_en_us = ModuleSetting.OrderToursKeyWords("en-us");
            model.nameOrderTours_keywords_ru_ru = ModuleSetting.OrderToursKeyWords("ru-ru");
            model.nameOrderTours_ru_ru = ModuleSetting.nameOrderTours("ru-ru");
            model.nameOrderTours_title_en_us = ModuleSetting.OrderToursTitle("en-us");
            model.nameOrderTours_title_ru_ru = ModuleSetting.OrderToursTitle("ru-ru");
            model.nameTours_description_en_us = ModuleSetting.ToursDesc("en-us");
            model.nameTours_description_ru_ru = ModuleSetting.ToursDesc("ru-ru");
            model.nameTours_en_us = ModuleSetting.nameTours("en-us");
            model.nameTours_keywords_en_us = ModuleSetting.ToursKeyWords("en-us");
            model.nameTours_keywords_ru_ru = ModuleSetting.ToursKeyWords("ru-ru");
            model.nameTours_ru_ru = ModuleSetting.nameTours("ru-ru");
            model.nameTours_title_en_us = ModuleSetting.ToursTitle("en-us");
            model.nameTours_title_ru_ru = ModuleSetting.ToursTitle("ru-ru");
            model.partialAccordion = ModuleSetting.partialAccordion();
            model.partialMenuLogon = ModuleSetting.partialMenuLogon();
            model.partialMenuLogonWrap = ModuleSetting.partialMenuLogonWrap();
            model.partialNoAccordion = ModuleSetting.partialNoAccordion();
            model.roleChiefTourManager = ModuleSetting.roleChiefTourManager();
            model.roleTourManager = ModuleSetting.roleTourManager();
            model.styleTours = ModuleSetting.styleTours();
            model.unlockTours = ModuleSetting.unlockTours();
            model.viewDescription = ModuleSetting.viewDescription();
            model.widthImgTours = ModuleSetting.widthImgTours();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", 
                             ToursLanguage.setting};
            model.bread = a;
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            return View(model);
        }
        [HttpPost]
        public ActionResult Setting(TourSetting model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var votes = _doc.Root.Elements("mtm.Tourism").Elements("add");
                votes.FirstOrDefault(x => x.Attribute("name").Value == "unlockTours")
                    .SetAttributeValue("value", model.unlockTours.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "viewDescription")
                    .SetAttributeValue("value", model.viewDescription.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "columnTours")
                    .SetAttributeValue("value", model.columnTours.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "styleTours")
                    .SetAttributeValue("value", model.styleTours.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "widthImgTours")
                    .SetAttributeValue("value", model.widthImgTours.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "MoneyProcent")
                    .SetAttributeValue("value", model.MoneyProcent.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "closeTour")
                    .SetAttributeValue("value", model.closeTour.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "roleChiefTourManager")
                    .SetAttributeValue("value", model.roleChiefTourManager.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "roleTourManager")
                    .SetAttributeValue("value", model.roleTourManager.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialAccordion")
                    .SetAttributeValue("value", model.partialAccordion.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialNoAccordion")
                    .SetAttributeValue("value", model.partialNoAccordion.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogon")
                    .SetAttributeValue("value", model.partialMenuLogon.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogonWrap")
                    .SetAttributeValue("value", model.partialMenuLogonWrap.ToString());


                var votesname = votes.FirstOrDefault(x => x.Attribute("name").Value == "nameTours").Elements("add");
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameTours_ru_ru);

                votesname.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.nameTours_title_ru_ru);
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.nameTours_keywords_ru_ru);
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.nameTours_description_ru_ru);

                votesname.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameTours_en_us);

                votesname.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.nameTours_title_en_us);
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.nameTours_keywords_en_us);
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.nameTours_description_en_us);


                var votesname2 = votes.FirstOrDefault(x => x.Attribute("name").Value == "nameCountry").Elements("add");
                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameCountry_ru_ru);

                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.nameCountry_title_ru_ru);
                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.nameCountry_keywords_ru_ru);
                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.nameCountry_description_ru_ru);

                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameCountry_en_us);

                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.nameCountry_title_en_us);
                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.nameCountry_keywords_en_us);
                votesname2.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.nameCountry_description_en_us);



                var votesname3 = votes.FirstOrDefault(x => x.Attribute("name").Value == "nameOrderTours").Elements("add");
                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameOrderTours_ru_ru);

                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.nameOrderTours_title_ru_ru);
                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.nameOrderTours_keywords_ru_ru);
                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.nameOrderTours_description_ru_ru);

                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameOrderTours_en_us);

                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.nameOrderTours_title_en_us);
                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.nameOrderTours_keywords_en_us);
                votesname3.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.nameOrderTours_description_en_us);


                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("st_unlocktours");
                GeneralMethods.MytripCacheRemove("st_viewdescription");
                GeneralMethods.MytripCacheRemove("st_columntours");
                GeneralMethods.MytripCacheRemove("st_styletours");
                GeneralMethods.MytripCacheRemove("st_nametours", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_nametours", "en-us");
                GeneralMethods.MytripCacheRemove("st_tourskeywords", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_tourskeywords", "en-us");
                GeneralMethods.MytripCacheRemove("st_tourstitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_tourstitle", "en-us");
                GeneralMethods.MytripCacheRemove("st_toursdescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_toursdescription", "en-us");
                GeneralMethods.MytripCacheRemove("st_namecountry", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_namecountry", "en-us");
                GeneralMethods.MytripCacheRemove("st_countrykeywords", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_countrykeywords", "en-us");
                GeneralMethods.MytripCacheRemove("st_countrytitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_countrytitle", "en-us");
                GeneralMethods.MytripCacheRemove("st_countrydescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_countrydescription", "en-us");
                GeneralMethods.MytripCacheRemove("st_nameordertours", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_nameordertours", "en-us");
                GeneralMethods.MytripCacheRemove("st_ordertourskeywords", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_ordertourskeywords", "en-us");
                GeneralMethods.MytripCacheRemove("st_ordertourstitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_ordertourstitle", "en-us");
                GeneralMethods.MytripCacheRemove("st_ordertoursdescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("st_ordertoursdescription", "en-us");
                GeneralMethods.MytripCacheRemove("st_moneyprocent");
                GeneralMethods.MytripCacheRemove("st_widthimgtours");
                GeneralMethods.MytripCacheRemove("st_closetour");
                GeneralMethods.MytripCacheRemove("st_rolechieftourmanager");
                GeneralMethods.MytripCacheRemove("st_partialaccordion");
                GeneralMethods.MytripCacheRemove("st_partialnoaccordion");
                GeneralMethods.MytripCacheRemove("st_partialmenulogon");
                GeneralMethods.MytripCacheRemove("st_partialmenulogonwrap");
                GeneralMethods.MytripCacheRemove("st_roletourmanager");
                #endregion

                return RedirectToAction("ControlPanel", "Core");
            } string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", 
                             ToursLanguage.setting};
            model.bread = a;
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            return View(model);
        }
    }
}
