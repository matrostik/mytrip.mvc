using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mytrip.Store.Repository.DataEntities;

namespace Mytrip.Store.Repository
{
   public class ProductRepository
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
        public IQueryable<mytrip_storeproduct> GetProductForDepartment(int id, int pageIndex, int pageSize,int sort, string culture, out int total)
        {
            var b= entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.DepartmentId == id||x.mytrip_storedepartment.SubDepartmentId==id)
                .Where(x => x.AllCulture == true || x.Culture == culture);
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if(sort==2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storeproduct> GetProductForProducer(int id, int pageIndex, int pageSize,int sort, string culture, out int total)
        {
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.ProducerId == id)
                .Where(x => x.AllCulture == true || x.Culture == culture);
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if (sort == 2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storeproduct> GetProductForProducer(int id, int pageIndex, int pageSize, int sort,int smallprice,int bigprice, string culture,string search, out int total)
        {
            if (smallprice >= bigprice)
                bigprice = int.MaxValue;
            decimal _smallprice = (decimal)smallprice;
            decimal _bigprice = (decimal)bigprice;
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.ProducerId == id)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x=>x.Price>_smallprice)
                .Where(x=>x.Price<_bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Abstract.IndexOf(search) != -1);
            }
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if (sort == 2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storeproduct> GetProductForDepartment(int id, int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture,string search, out int total)
        {
            if (smallprice >= bigprice)
                bigprice = int.MaxValue;
            decimal _smallprice = (decimal)smallprice;
            decimal _bigprice = (decimal)bigprice;
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.DepartmentId == id || x.mytrip_storedepartment.SubDepartmentId == id)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Abstract.IndexOf(search) != -1);
            }
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if (sort == 2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storeproduct> GetProductForDepartmentAndProducer(int id,int producerId, int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture, string search, out int total)
        {
            if (smallprice >= bigprice)
                bigprice = int.MaxValue;
            decimal _smallprice = (decimal)smallprice;
            decimal _bigprice = (decimal)bigprice;
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.DepartmentId == id || x.mytrip_storedepartment.SubDepartmentId == id)
                .Where(x=>x.ProducerId==producerId)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Abstract.IndexOf(search) != -1);
            }
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if (sort == 2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storeproduct> GetProductForDepartmentAndProducer(int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture, string search, out int total)
        {
            if (smallprice >= bigprice)
                bigprice = int.MaxValue;
            decimal _smallprice = (decimal)smallprice;
            decimal _bigprice = (decimal)bigprice;
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Abstract.IndexOf(search) != -1);
            }
            var a = b.OrderByDescending(x => x.CreationDate);
            total = a.Count();
            if (sort == 2)
                a = b.OrderByDescending(x => x.Price);
            else if (sort == 3)
                a = b.OrderByDescending(x => x.TotalVotes);
            else if (sort == 4)
                a = b.OrderByDescending(x => x.Title);
            else if (sort == 5)
                a = b.OrderBy(x => x.CreationDate);
            else if (sort == 6)
                a = b.OrderBy(x => x.Price);
            else if (sort == 7)
                a = b.OrderBy(x => x.TotalVotes);
            else if (sort >= 8)
                a = b.OrderBy(x => x.Title);
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<mytrip_storeproduct> GetProductForViews(string[] id)
        {
            int item = 0;
            int.TryParse(id[0], out item);
            if (item != 0)
            {
                IQueryable<mytrip_storeproduct> a = entities.mytrip_storeproduct
                    .Include("mytrip_storedepartment")
                    .Include("mytrip_storedepartment.mytrip_storedepartment2")
                    .Include("mytrip_storeproducer")
                    .Include("mytrip_storeoptions")
                    .Include("mytrip_storevotes");
                IQueryable<mytrip_storeproduct> b = a.Where(y => y.ProductId == item);
                int _item = item;
                foreach (string x in id)
                {
                    int.TryParse(x, out item);
                    if (item != 0 && item != _item)
                    {
                        _item = item;
                        b = b.Union(a.Where(y => y.ProductId == item));
                    }

                }
                return b.OrderByDescending(x => x.CreationDate);
            }
            else
                return null;
        }
        public IQueryable<mytrip_storeproduct> GetProductForViews(int id)
        {IQueryable<mytrip_storeproduct> a = entities.mytrip_storeproduct
                    .Include("mytrip_storedepartment")
                    .Include("mytrip_storedepartment.mytrip_storedepartment2")
                    .Include("mytrip_storeproducer")
                    .Include("mytrip_storeoptions")
                    .Include("mytrip_storevotes");
        return a.Where(y => y.ProductId == id).OrderByDescending(x => x.CreationDate);
               
        }
   }
}
