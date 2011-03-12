using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using mtm.Core.Settings;
using System.Web;
using mtm.Tourism.Models;

namespace mtm.Tourism.Repository
{
   public class ToursRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;

        /// <summary>Подключение к Entity Репозиторию 
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSetting.connectionString());
                return _entities;
            }
        }
        CategoryRepository _cat;
        CategoryRepository cat { get { if (_cat == null)_cat = new CategoryRepository(); return _cat; } }
        VariantyRepository _varianty;
        VariantyRepository varianty { get { if (_varianty == null)_varianty = new VariantyRepository(); return _varianty; } }
       
        #endregion
        public IQueryable<mytrip_tours> GetAllTours(string culture)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x=>x.Culture==culture||x.AllCulture==true)
                .OrderByDescending(x => x.CloseTourDate);
        }
        public IQueryable<mytrip_tours> GetAllTours(string culture, int take)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate).Skip(0).Take(take);
        }
        public IQueryable<mytrip_tours> GetAllTours(int pageIndex, int pageSize,string culture,out int total)
        {
            var a= entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x=>x.CloseTourDate>DateTime.Now)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetAllToursArhiv(int pageIndex, int pageSize, string culture, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate <= DateTime.Now)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetToursForCategory(int pageIndex, int pageSize,int category, string culture, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x=>x.CategoryId==category||x.mytrip_tourscategory.SubCategoryId==category)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetToursForSearch(int pageIndex, int pageSize, int category,int country, string culture,string search,string startdate,string stopdate, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x=>x.Culture==culture||x.AllCulture==true)
                .OrderBy(x => x.CloseTourDate);
            if(category>0)
                a = a.Where(x => x.CategoryId == category || x.mytrip_tourscategory.SubCategoryId == category)
                .OrderBy(x => x.CloseTourDate);
            if(country>0)
                a = a.Where(x => x.CountryId == country)
                .OrderBy(x => x.CloseTourDate);
            if (search!="x")
                a = a.Where(x => x.Title.Contains(search) == true||x.Body.Contains(search) == true)
                .OrderBy(x => x.CloseTourDate);
            DateTime start = DateTime.Now;
            DateTime stop = DateTime.Now;
            if(startdate!=null&&DateTime.TryParse(startdate,out start)&&
                stopdate!=null&&DateTime.TryParse(stopdate,out stop))
                a = a.Where(x => x.StartDate>=start && x.StartDate <=stop)
                                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetToursForCountry(int pageIndex, int pageSize, int category, string culture, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x => x.CountryId == category)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetToursForCategoryArhiv(int pageIndex, int pageSize, int category, string culture, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate <= DateTime.Now)
                .Where(x => x.CategoryId == category || x.mytrip_tourscategory.SubCategoryId == category)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public mytrip_tours GetTour(int id)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Include("mytrip_toursvariants")
                .FirstOrDefault(x => x.TourId == id);
        }
        public IQueryable<mytrip_tours> GetToursForSearch(string search, string culture)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscountry")
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .Where(x => x.Title.Contains(search) == true || x.Body.Contains(search) == true)
                .OrderByDescending(x => x.CreateDate);
        }
        #region _Создать уникальный TourId
        private int CreateTourId()
        {
            int catId;
            for (catId = 1; entities.mytrip_tours.Count(x => x.TourId == catId) != 0; catId++) ;
            return catId;
        }
        #endregion
       public void CreateTourZero()
        {
            cat.CreateCountryZero();
            var z = entities.mytrip_tours.FirstOrDefault(x => x.TourId == 0);
            if (z == null)
            {
                mytrip_tours x = new mytrip_tours
                {
                    Body = "zero",
                    CategoryId = 0,
                    StopDate = DateTime.Now,
                    CloseTourDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    MinPrice = 0,
                    MoneyId = "zero",
                    Path = "zero",
                    Title = "zero",
                    TourId = 0,
                    StartDate = DateTime.Now,
                    UserName = "zero",
                    AllCulture = false,
                    Culture = "zero",
                    CountryId=0
                };
                entities.mytrip_tours.AddObject(x);
                entities.SaveChanges();
            }
        }

        public mytrip_tours CreateTour(EditorTourModel m)
        {
            var a = varianty.GetPriceForTour(0);
            DateTime stopdate=DateTime.Parse(m.stopdate);
            DateTime closetourdate=DateTime.Parse(m.startdate).AddDays(ModuleSetting.closeTour());
            DateTime startdate=DateTime.Parse(m.startdate);
            Decimal minprice=a.Price;
            string moneyid=a.MoneyId;
            mytrip_tours x = new mytrip_tours { 
            Body=m.body,
            CategoryId=m.categoryid,
            StopDate=stopdate,
            CloseTourDate=closetourdate,
            CreateDate=DateTime.Now,
            Imige=m.image,
            Latitude=0,
            Longitude=0,
            MinPrice=minprice,
            MoneyId=moneyid,
            Path = (m.path!=null&&m.path.Length>0)?m.path:GeneralMethods.DecodingString(m.title),
            Title=m.title,
            TourId = CreateTourId(),
            StartDate=startdate,
            UserName = HttpContext.Current.User.Identity.Name,
            AllCulture=m.allculture,
            Culture=LocalisationSetting.culture(),
            SeoTitle=(m.seoTitle!=null&&m.seoTitle.Length>0)?m.seoTitle:m.title,
            SeoDescription=m.seodescription,
            SeoKeyword=m.seokeywords,
            CountryId=m.countryid
            };
            entities.mytrip_tours.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_tours EditTour(EditorTourModel m)
        {

            var a = varianty.GetPriceForTour(m.id);
            DateTime stopdate = DateTime.Parse(m.stopdate);
            DateTime closetourdate = DateTime.Parse(m.startdate).AddDays(ModuleSetting.closeTour());
            DateTime startdate = DateTime.Parse(m.startdate);
            Decimal minprice = a.Price;
            string moneyid = a.MoneyId;

            var z = entities.mytrip_tours.FirstOrDefault(x => x.TourId == m.id);
            z.Title = m.title;
            z.Body = m.body;
            z.CategoryId = m.categoryid;
            z.StopDate = stopdate;
            z.CloseTourDate = closetourdate;
            z.StartDate = startdate;
            z.Imige = m.image;
            z.Latitude = 0;
            z.Longitude = 0;
            z.MinPrice = minprice;
            z.MoneyId = moneyid;
            z.AllCulture = m.allculture;
            z.Path = (m.path != null && m.path.Length > 0) ? m.path : GeneralMethods.DecodingString(m.title);
            z.SeoDescription = m.seodescription;
            z.SeoKeyword = m.seokeywords;
            z.SeoTitle = (m.seoTitle != null && m.seoTitle.Length > 0) ? m.seoTitle : m.title;
            z.CountryId = m.countryid;
            entities.SaveChanges();
            return z;
        }
        public void DeleteTour(int id)
        {
            var y = GetTour(id);
            foreach (var a in y.mytrip_toursvariants.ToList())
            {
                entities.mytrip_toursvariants.DeleteObject(a);
            }
            entities.mytrip_tours.DeleteObject(y);
            entities.SaveChanges();
        }
    }
}
