using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Tourism.Repository.DataEntities;
using System.Web;

namespace Mytrip.Tourism.Repository
{
    public class VariantyRepository
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
        #region _Создать уникальный TourId
        private int CreateVariantId()
        {
            int catId;
            for (catId = 1; entities.mytrip_toursvariants.Count(x => x.VariantId == catId) != 0; catId++) ;
            return catId;
        }
        #endregion
        public IEnumerable<mytrip_toursvariants> GetVariantsForTour(int id)
        {
            return entities.mytrip_toursvariants.Where(x => x.TourId == id)
                .OrderBy(x => x.Price);
        }
        public void CreateVariant(int tourid, string hotel, string services, decimal price, string momeyid,
            double latitude, double longitude)
        {
            if (tourid == 0)
            {
                ToursRepository dd = new ToursRepository();
                dd.CreateTourZero();
            }
            if (services == "<br>" || services == "<br/>" || services == "<BR>" || services == "<BR/>")
                services = null;
            mytrip_toursvariants x = new mytrip_toursvariants
            {
                VariantId = CreateVariantId(),
                Hotel = hotel,
                Latitude = latitude,
                Longitude = longitude,
                MoneyId = momeyid,
                Price = price,
                Services = services,
                TourId = tourid,
                UserName = HttpContext.Current.User.Identity.Name
            };
            entities.mytrip_toursvariants.AddObject(x);
            entities.SaveChanges();
        }
        public int DeleteVariant(int id)
        {
            var a = entities.mytrip_toursvariants.FirstOrDefault(x => x.VariantId == id);
            int aa = a.TourId;
            entities.mytrip_toursvariants.DeleteObject(a);
            entities.SaveChanges();
            return aa;
        }
        public mytrip_toursvariants GetPriceForTour(int id)
        {
            if (id == 0)
            
                return entities.mytrip_toursvariants.Where(x => x.TourId == 0)
                    .Where(x => x.UserName == HttpContext.Current.User.Identity.Name)
                    .OrderBy(x => x.Price).FirstOrDefault();
            else
                return entities.mytrip_toursvariants.Where(x => x.TourId == id)
                    .OrderBy(x => x.Price).FirstOrDefault();
        }
        public void MoveVariants(int id)
        {
            var a = entities.mytrip_toursvariants.Where(x => x.TourId == 0)
                      .Where(x => x.UserName == HttpContext.Current.User.Identity.Name);
            foreach (var x in a)
            {
                x.TourId = id;
            }
            entities.SaveChanges();
        }
    }
}
