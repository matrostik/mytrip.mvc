using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using System.Web;
using mtm.Core.Settings;
using mtm.Tourism.Models;

namespace mtm.Tourism.Repository
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
        public void CreateVariant(EditorTourModel m)
        {
            decimal pr = 0;
            if (LocalisationSetting.culture().ToLower() == "ru-ru")
                decimal.TryParse(m.price.Replace(".", ","), out pr);
            if (LocalisationSetting.culture().ToLower() == "en-us")
                decimal.TryParse(m.price.Replace(",", "."), out pr);
            if (m.id == 0)
            {
                ToursRepository dd = new ToursRepository();
                dd.CreateTourZero();
            }
            if (m.services == "<br>" || m.services == "<br/>" || m.services == "<BR>" || m.services == "<BR/>")
                m.services = null;
            mytrip_toursvariants x = new mytrip_toursvariants
            {
                VariantId = CreateVariantId(),
                Hotel = m.hotel,
                Latitude = 0,
                Longitude = 0,
                MoneyId = m.momeyid,
                Price = pr,
                Services = m.services,
                TourId = m.id,
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
        public mytrip_toursvariants GetVariant(int id)
        {

            return entities.mytrip_toursvariants.Include("mytrip_tours").Include("mytrip_tours.mytrip_tourscountry")
                .FirstOrDefault(x=>x.VariantId==id);
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
