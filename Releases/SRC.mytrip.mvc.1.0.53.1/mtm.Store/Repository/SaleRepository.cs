using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Store.Repository.DataEntities;
using System.Web;

namespace mtm.Store.Repository
{
    public class SaleRepository
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
        private int CreateSaleId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storesale.Count(x => x.SaleId == catId) != 0; catId++) ;
            return catId;
        }
        public void CreateSaleZero()
        {
            var sale = entities.mytrip_storesale.FirstOrDefault(x => x.SaleId == 0);
            if (sale == null)
            {
                mytrip_storesale x = new mytrip_storesale { 
                SaleId=0,
                Sale=0,
                Title="sale all time 0%",
                ManagerName=HttpContext.Current.User.Identity.Name,
                CreationDate=DateTime.Now,
                StartDate=DateTime.Now,
                CloseDate=DateTime.MaxValue
                };
                entities.mytrip_storesale.AddObject(x);
                entities.SaveChanges();
            }
        
        }
        public void CreateSale(int sale, System.DateTime datestart, System.DateTime dateclose)
        {
            CreateSaleZero();
            mytrip_storesale x = new mytrip_storesale
            {
                SaleId = CreateSaleId(),
                Sale = sale,
                Title = "sale " + sale + "% from " + string.Format("{0:dd.MM.yy}", datestart) + " to " + string.Format("{0:dd.MM.yy}", dateclose),
                ManagerName = HttpContext.Current.User.Identity.Name,
                CreationDate = DateTime.Now,
                StartDate = datestart,
                CloseDate = dateclose
            };
            entities.mytrip_storesale.AddObject(x);
            entities.SaveChanges();
        
        }
        public void CreateSale(int id,int sale,string title, System.DateTime datestart, System.DateTime dateclose)
        {
            if (title == null || title.Length < 2)
                title = "sale " + sale + "% from " + string.Format("{0:dd.MM.yy}", datestart) + " to " + string.Format("{0:dd.MM.yy}", dateclose);
            CreateSaleZero();
            mytrip_storesale x = new mytrip_storesale
            {
                SaleId = id,
                Sale = sale,
                Title = title,
                ManagerName = HttpContext.Current.User.Identity.Name,
                CreationDate = DateTime.Now,
                StartDate = datestart,
                CloseDate = dateclose
            };
            entities.mytrip_storesale.AddObject(x);
            entities.SaveChanges();

        }
        private void DeleteSaleDate()
        {
            var sale = entities.mytrip_storesale
                .Include("mytrip_storedepartment")
                .Include("mytrip_storeproducer")
                .Include("mytrip_storeproduct")
                .Where(x => x.CloseDate < DateTime.Now)
                    .OrderBy(x => x.Sale);
            foreach (var s in sale)
            {
                foreach (var department in s.mytrip_storedepartment.ToList())
                {
                    department.SaleId = 0;
                }
                foreach (var produser in s.mytrip_storeproducer.ToList())
                {
                    produser.SaleId = 0;
                }
                foreach (var product in s.mytrip_storeproduct.ToList())
                {
                    product.SaleId = 0;
                }
                entities.SaveChanges();

                entities.mytrip_storesale.DeleteObject(s);
                entities.SaveChanges();
            }
        }
        public IDictionary<int, string> SaleDictionary()
        {
            CreateSaleZero();
            IDictionary<int, string> result = new Dictionary<int, string>();
            var sale = entities.mytrip_storesale.Where(x => x.CloseDate > DateTime.Now)
                .OrderBy(x => x.Sale);
            if (sale != null)
            {
                foreach (var s in sale)
                {
                    if(s.SaleId==0)
                        result.Add(0, "0%");
                    else
                    result.Add(s.SaleId, string.Format(StoreLanguage.saleView, s.Sale, string.Format("{0:dd MMMM yyyy}", s.CloseDate)));
                }
            }else
                result.Add(0, "0%");
                
            return result;
        }
    }
}
