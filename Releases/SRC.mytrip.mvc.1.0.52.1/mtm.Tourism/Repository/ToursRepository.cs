using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using mtm.Core.Settings;
using System.Web;

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
        #endregion
        public IQueryable<mytrip_tours> GetAllTours(string culture)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .OrderByDescending(x => x.CloseTourDate);
        }
        public IQueryable<mytrip_tours> GetAllTours(string culture, int take)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .OrderBy(x => x.CloseTourDate).Skip(0).Take(take);
        }
        public IQueryable<mytrip_tours> GetAllTours(int pageIndex, int pageSize,string culture,out int total)
        {
            var a= entities.mytrip_tours
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x=>x.CloseTourDate>DateTime.Now)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_tours> GetToursForCategory(int pageIndex, int pageSize,int category, string culture, out int total)
        {
            var a = entities.mytrip_tours
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Where(x => x.CloseTourDate > DateTime.Now)
                .Where(x=>x.CategoryId==category||x.mytrip_tourscategory.SubCategoryId==category)
                .OrderBy(x => x.CloseTourDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public mytrip_tours GetTour(int id)
        {
            return entities.mytrip_tours
                .Include("mytrip_tourscategory")
                .Include("mytrip_tourscategory.mytrip_tourscategory2")
                .Include("mytrip_toursvariants")
                .FirstOrDefault(x => x.TourId == id);
        }
        public IQueryable<mytrip_tours> GetToursForSearch(string search, string culture)
        {
            return entities.mytrip_tours
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
                    Culture = "zero"
                };
                entities.mytrip_tours.AddObject(x);
                entities.SaveChanges();
            }
        }

        public mytrip_tours CreateTour(string title, string body,
            int categoryid, DateTime stopdate,DateTime closetourdate,DateTime startdate,
            string image, decimal latitude, decimal longitude, decimal minprice, string moneyid,
            bool allculture,string culture)
        {
            mytrip_tours x = new mytrip_tours { 
            Body=body,
            CategoryId=categoryid,
            StopDate=stopdate,
            CloseTourDate=closetourdate,
            CreateDate=DateTime.Now,
            Imige=image,
            Latitude=latitude,
            Longitude=longitude,
            MinPrice=minprice,
            MoneyId=moneyid,
            Path = GeneralMethods.DecodingString(title),
            Title=title,
            TourId = CreateTourId(),
            StartDate=startdate,
            UserName = HttpContext.Current.User.Identity.Name,
            AllCulture=allculture,
            Culture=culture
            };
            entities.mytrip_tours.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_tours EditTour(int id,string title, string body,
            int categoryid, DateTime stopdate, DateTime closetourdate, DateTime startdate,
            string image, decimal latitude, decimal longitude, decimal minprice, string moneyid,
            bool allculture)
        {
            var z = entities.mytrip_tours.FirstOrDefault(x => x.TourId == id);
            z.Title = title;
            z.Body = body;
            z.CategoryId = categoryid;
            z.StopDate = stopdate;
            z.CloseTourDate = closetourdate;
            z.StartDate = startdate;
            z.Imige = image;
            z.Latitude = latitude;
            z.Longitude = longitude;
            z.MinPrice = minprice;
            z.MoneyId = moneyid;
            z.AllCulture = allculture;
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
