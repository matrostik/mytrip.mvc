using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using System.Web;

namespace Mytrip.Store.Repository
{
   public class ProducerRepository
    {
        #region Подключение к Entity Репозиторию
        StoreSettings _core;
        public StoreSettings core
        {
            get
            {
                if (_core == null)
                    _core = new StoreSettings();
                return _core;
            }
        }
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(core.connectionString());
                return _entities;
            }
        }
        #endregion
        #region Другие подключения и переменные
        private string username
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        #endregion
        public IQueryable<mytrip_storeproducer> GetAllProducer(string culture)
        {
            return entities.mytrip_storeproducer
                .Include("mytrip_storeproduct")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
       public IQueryable<mytrip_storeproducer> GetAllProducer(int pageIndex, int pageSize, string culture, out int total)
        {
            var a= entities.mytrip_storeproducer
                .Include("mytrip_storeproduct")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
       public mytrip_storeproducer GetProducer(int id)
       {
           return entities.mytrip_storeproducer
               .Include("mytrip_storeproduct")
               .FirstOrDefault(x => x.ProducerId == id);
       }
       public IDictionary<int, string> GetProducerForDdl(string culture)
       {
           IDictionary<int, string> mcats = new Dictionary<int, string>();
           var a = entities.mytrip_storeproducer
          .Where(x => x.Culture == culture || x.AllCulture == true)
          .OrderBy(x => x.Title);
           mcats.Add(0, StoreLanguage.AllProducer);
           foreach (mytrip_storeproducer cat in a)
           {
               mcats.Add(cat.ProducerId, cat.Title);
           }
           return mcats;
       }
    }
}
