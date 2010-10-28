using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mytrip.Store.Repository.DataEntities;
using Mytrip.Mvc.Settings;
using System.Xml.Linq;
using System.IO;
using Mytrip.Mvc.Repository;

namespace Mytrip.Store.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductRepository
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
        SaleRepository _sale;

        public SaleRepository sale
        {
            get
            {
                if (_sale == null)
                    _sale = new SaleRepository();
                return _sale;
            }
        }
        #endregion
        public IQueryable<mytrip_storeproduct> GetProductForStore(string culture, int take)
        {
            return entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.AllCulture == true || x.Culture == culture)
            .OrderByDescending(x => x.CreationDate).Skip(0).Take(take);
        }
        public IQueryable<mytrip_storeproduct> GetProductForStore(int pageIndex, int pageSize, int sort, string culture, out int total)
        {
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="culture"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForDepartment(int id, int pageIndex, int pageSize, int sort, string culture, out int total)
        {
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.DepartmentId == id || x.mytrip_storedepartment.SubDepartmentId == id)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="culture"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForProducer(int id, int pageIndex, int pageSize, int sort, string culture, out int total)
        {
            var b = entities
                .mytrip_storeproduct
                .Include("mytrip_storedepartment")
                .Include("mytrip_storedepartment.mytrip_storedepartment2")
                .Include("mytrip_storeproducer")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="smallprice"></param>
        /// <param name="bigprice"></param>
        /// <param name="culture"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForProducer(int id, int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture, string search, out int total)
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
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.ProducerId == id)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Body.IndexOf(search) != -1 || x.NamberCatalog.IndexOf(search) != -1);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="smallprice"></param>
        /// <param name="bigprice"></param>
        /// <param name="culture"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForDepartment(int id, int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture, string search, out int total)
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
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.DepartmentId == id || x.mytrip_storedepartment.SubDepartmentId == id)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Body.IndexOf(search) != -1 || x.NamberCatalog.IndexOf(search) != -1);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="producerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="smallprice"></param>
        /// <param name="bigprice"></param>
        /// <param name="culture"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForDepartmentAndProducer(int id, int producerId, int pageIndex, int pageSize, int sort, int smallprice, int bigprice, string culture, string search, out int total)
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
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.DepartmentId == id || x.mytrip_storedepartment.SubDepartmentId == id)
                .Where(x => x.ProducerId == producerId)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Body.IndexOf(search) != -1 || x.NamberCatalog.IndexOf(search) != -1);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="smallprice"></param>
        /// <param name="bigprice"></param>
        /// <param name="culture"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
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
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Price > _smallprice)
                .Where(x => x.Price < _bigprice);
            if (search != "x")
            {
                b = b.Where(x => x.Title.IndexOf(search) != -1 || x.Body.IndexOf(search) != -1 || x.NamberCatalog.IndexOf(search) != -1);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForViews(string[] id)
        {
            //множественный запрос к базе надо оптимизировать
            //например закешировать все товары и методом GetProductForViews
            //дергать продукт из кеша
            int item = 0;
            IQueryable<mytrip_storeproduct> b = null;
            foreach (string x in id)
            {
                int.TryParse(x, out item);
                if (b == null)
                    b = GetProductForViews(item);
                else
                    b = b.Concat(GetProductForViews(item));
            }
            return b.OrderByDescending(x => x.CreationDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<mytrip_storeproduct> GetProductForViews(int id)
        {
            //множественный запрос к базе из метода GetProductForViews(string[] id)
            //надо оптимизировать
            //например закешировать все товары и методом GetProductForViews
            //дергать продукт из кеша
            IQueryable<mytrip_storeproduct> a = entities.mytrip_storeproduct
                       .Include("mytrip_storedepartment")
                       .Include("mytrip_storedepartment.mytrip_storedepartment2")
                       .Include("mytrip_storeproducer")
                       .Include("mytrip_storevotes")
                       .Include("mytrip_storesale")
                       .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale");
            return a.Where(y => y.ProductId == id).OrderByDescending(x => x.CreationDate);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public mytrip_storeproduct GetProduct(int id)
        {
            //множественный запрос к базе при формировании корзины покупателя в хелпере MyCart
            //надо оптимизировать
            //например закешировать все товары и методом GetProduct
            //дергать продукт из кеша
            return entities.mytrip_storeproduct
                           .Include("mytrip_storedepartment")
                           .Include("mytrip_storedepartment.mytrip_storedepartment2")
                           .Include("mytrip_storeproducer")
                           .Include("mytrip_storevotes")
                           .Include("mytrip_storesale")
                           .Include("mytrip_storedepartment.mytrip_storesale")
                .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                .Include("mytrip_storeproducer.mytrip_storesale")
                .FirstOrDefault(y => y.ProductId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="producerId"></param>
        /// <param name="title"></param>
        /// <param name="_abstract"></param>
        /// <param name="body"></param>
        /// <param name="culture"></param>
        /// <param name="allculture"></param>
        /// <param name="price"></param>
        /// <param name="totalcount"></param>
        /// <param name="urlfile"></param>
        /// <param name="viewcount"></param>
        /// <param name="viewprice"></param>
        /// <param name="viewvotes"></param>
        /// <param name="packing"></param>
        /// <returns></returns>
        public mytrip_storeproduct CreateProduct(int departmentId, int producerId, string title,
            string _abstract, string body,string culture, bool allculture, decimal price, int totalcount,
            string urlfile, bool viewcount, bool viewprice, bool viewvotes, string packing, 
            string cultureMoney,string namberCatalog,int saleid)
        {
            sale.CreateSaleZero();
            mytrip_storeproduct x = new mytrip_storeproduct
            {
                ProductId = CreateProductId(),
                DepartmentId = departmentId,
                ProducerId = producerId,
                Title = title,
                Path = GeneralMethods.DecodingString(title),
                Body = _abstract,
                Details = body,
                Culture = culture,
                AllCulture = allculture,
                CreationDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Price = price,
                TotalCount = totalcount,
                TotalVotes = 0,
                UrlFile = urlfile,
                ViewCount = viewcount,
                ViewPrice = viewprice,
                ViewVotes = viewvotes,
                Packing=packing,
                SaleId=saleid,
                MoneyId=cultureMoney,
                NamberCatalog=namberCatalog
            };
            entities.mytrip_storeproduct.AddObject(x);
            entities.SaveChanges();
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentId"></param>
        /// <param name="producerId"></param>
        /// <param name="title"></param>
        /// <param name="_abstract"></param>
        /// <param name="body"></param>
        /// <param name="allculture"></param>
        /// <param name="price"></param>
        /// <param name="totalcount"></param>
        /// <param name="urlfile"></param>
        /// <param name="viewcount"></param>
        /// <param name="viewprice"></param>
        /// <param name="viewvotes"></param>
        /// <param name="packing"></param>
        /// <returns></returns>
        public mytrip_storeproduct EditProduct(int id, int departmentId, int producerId, string title,
            string _abstract, string body,bool allculture, decimal price, int totalcount,
            string urlfile, bool viewcount, bool viewprice, bool viewvotes,string packing,
            string cultureMoney, string namberCatalog,int saleid)
        {
            mytrip_storeproduct x = GetProduct(id);
            x.DepartmentId = departmentId;
            x.ProducerId = producerId;
            x.Title = title;
            x.Path = GeneralMethods.DecodingString(title);
            x.Body = _abstract;
            x.Details = body;
            x.AllCulture = allculture;
            x.Price = price;
            x.TotalCount = totalcount;
            x.UrlFile = urlfile;
            x.ViewCount = viewcount;
            x.ViewPrice = viewprice;
            x.ViewVotes = viewvotes;
            x.Packing = packing;
            x.NamberCatalog = namberCatalog;
            x.MoneyId = cultureMoney;
            x.SaleId = saleid;
            entities.SaveChanges();
            return x;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int DeleteProduct(int id,out string path)
        {
            mytrip_storeproduct x = entities.mytrip_storeproduct
                .Include("mytrip_storedepartment")
                           .Include("mytrip_storevotes")
                           .Include("mytrip_storeorderisproduct")
                .FirstOrDefault(y => y.ProductId == id);
            int result = x.DepartmentId;
            path = x.mytrip_storedepartment.Path;
            foreach (var y in x.mytrip_storevotes.ToList())
            {
                entities.mytrip_storevotes.DeleteObject(y);
            }
            foreach (var y in x.mytrip_storeorderisproduct.ToList())
            {
                entities.mytrip_storeorderisproduct.DeleteObject(y);
            }
            entities.mytrip_storeproduct.DeleteObject(x);
            entities.SaveChanges();
            return result;
        }
        /// <summary>Создать уникальный ProductId
        /// </summary>
        /// <returns>возвращает int</returns>
        private int CreateProductId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storeproduct.Count(x => x.ProductId == catId) != 0; catId++) ;
            return catId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int CreateVotesId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storevotes.Count(x => x.VotesId == catId) != 0; catId++) ;
            return catId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="vote"></param>
        /// <returns></returns>
        public decimal CreateVote(int articleId, int vote)
        {
            mytrip_storeproduct article = entities.mytrip_storeproduct.Include("mytrip_storevotes").FirstOrDefault(x => x.ProductId == articleId);
            if (entities.mytrip_storevotes.Where(x => x.ProductId == articleId)
                .FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name) == null)
            {
                mytrip_storevotes x = new mytrip_storevotes
                {
                    VotesId = CreateVotesId(),
                    ProductId = articleId,
                    UserName = HttpContext.Current.User.Identity.Name,
                    Vote = vote,
                    CreationDate=DateTime.Now
                };
                entities.mytrip_storevotes.AddObject(x);
                entities.SaveChanges();
                int countVotes = article.mytrip_storevotes.Count();
                int VotesSumm = 0;
                foreach (mytrip_storevotes v in article.mytrip_storevotes)
                {
                    VotesSumm += v.Vote;
                }
                if (countVotes <= 0)
                    countVotes = 1;
                if (VotesSumm <= 0)
                    VotesSumm = 1;
                decimal total = (decimal)VotesSumm / countVotes;
                article.TotalVotes = total;
                entities.SaveChanges();
                return total;
            }
            else
                return article.TotalVotes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="review"></param>
        public void CreateReview(int id, string review)
        {
            mytrip_storevotes x = entities.mytrip_storevotes.Where(z => z.ProductId == id)
                    .FirstOrDefault(z => z.UserName == HttpContext.Current.User.Identity.Name);
            if (x != null)
            {
                x.Reviews = review;
                entities.SaveChanges();
            }
        }
        public string StatusReview(int id)
        {
            mytrip_storevotes x = entities.mytrip_storevotes.Where(z => z.ProductId == id)
                    .FirstOrDefault(z => z.UserName == HttpContext.Current.User.Identity.Name);
            if (x != null)
            {
                return x.Reviews;
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public int GetVotesCount(int articleId)
        {
            return entities.mytrip_storevotes.Count(x => x.ProductId == articleId);
        }
        private void WriteXml(string directory)
        {
            string[] a = EditePageRepository.WritePage(directory);
            StringBuilder result = new StringBuilder();
            int _close=0;
            foreach (string x in a)
            {

                if (x == "<?mso-application progid=\"Excel.Sheet\"?>")
                { }
                else if (x.Contains("<Workbook"))
                {
                    result.AppendLine("<Workbook>");
                    _close=1;
                }
                else if (_close == 1 && x.Contains("<Worksheet"))
                {
                    result.AppendLine(x.Replace("ss:", "").Replace("x:", ""));
                    _close = 2; 
                }
                else if (_close == 1 && !x.Contains("<Worksheet"))
                {}
                else if (_close == 2 && !x.Contains("</Table>"))
                {
                    result.AppendLine(x.Replace("ss:", "").Replace("x:", ""));
                }
                else if (_close == 2 && x.Contains("</Table>"))
                {
                    result.AppendLine(x.Replace("ss:", "").Replace("x:", ""));
                    _close = 3; 
                }
                else if (_close == 3)
                {
                    result.AppendLine("</Worksheet>");
                    result.AppendLine("</Workbook>");
                    _close = 4; 
                }
                
            }
            EditePageRepository.CreatePage(directory, result.ToString());
        }
        private void CreateProductXmltoHtml(string dirrectory,string extension)
        {
            string[] a = EditePageRepository.WritePage(dirrectory);
            StringBuilder result = new StringBuilder();
            string _a = "";
            foreach (string x in a)
            {
                string _x = replaceforRussia(x);
                _a += _x;
            }
            int _close=0;
            result.AppendLine("<?xml version=\"1.0\"?>");
            result.AppendLine("<Workbook>"); 
            result.AppendLine("<Worksheet Name=\"Sheet1\">");
            string[] __a = _a.Split('>');
            foreach (string x in __a)
            {
                if (_close == 0 && x.Contains("<br"))
                {
                    result.AppendLine(x.Replace("<br/", "").Replace("<br", ""));
                }
                else if (_close == 0 && (x.Contains("<p") || x.Contains("<b")))
                { }                
                else if (_close == 0 && (x.Contains("</p") || x.Contains("</b")))
                { result.AppendLine(x.Replace("</p", "").Replace("</b", "")); }
                else if (_close == 0 && x.Contains("<table"))
                {
                    result.AppendLine("<Table>");
                }
                else if (_close == 0 && x.Contains("</table"))
                {
                    result.AppendLine("</Table>");
                    _close = 2;
                    break;
                }
                else if (_close == 0 && x.Contains("<caption"))
                {
                    _close = 1;
                }
                else if (_close == 1 && x.Contains("</caption"))
                {
                    _close = 0;
                }
                else if (_close == 0 && x.Contains("<tr"))
                {
                    result.AppendLine("<Row>");
                }
                else if (_close == 0 && x.Contains("</tr"))
                {
                    result.AppendLine("</Row>");
                }
                else if (_close == 0 && x.Contains("<td"))
                {
                    result.AppendLine("<Cell><Data>");
                }
                else if (_close == 0 && x.Contains("</td"))
                {
                    result.AppendLine(x.Replace("</td", "</Data></Cell>"));
                }
                else if (_close == 0 && !x.Contains("<"))
                    result.AppendLine(x);
            }
            result.AppendLine("</Worksheet>");
            result.AppendLine("</Workbook>");
            EditePageRepository.CreatePage(dirrectory.Replace(extension,".xml"), result.ToString());

        }
        public void CreateProductXml(int departmentid, int producerid, HttpPostedFileBase file)
        {

            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0, name.LastIndexOf("."));
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            if (!folder.Exists)
                folder.Create();            
            string _absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog/" + name);
            file.SaveAs(_absolutDirectory);
            if (extension == ".html")
            {
                CreateProductXmltoHtml("/Content/Store/Catalog/" + name, extension);
                _absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog/" + name.Replace(extension, ".xml"));
            }
            else
            {
            WriteXml("/Content/Store/Catalog/" + name);
            }
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var doc = _doc.Root.Elements("Worksheet")
                .Elements("Table").FirstOrDefault()
                .Elements("Row");
            foreach (var x in doc)
            {
                int count=1;
                string nambercatalog = "";
                string title = "x";
                string body = "";
                string details = "";
                decimal price = 0;
                string ISO = ModuleSetting.keyMoney().ToUpper();
                string packing = "";
                int countproduct = 0;
                bool viewprice = false;
                bool viewcount = false;
                bool viewvotes = false;
                bool allculture = false;
                bool create=true;
                foreach (var z in x.Elements("Cell"))
                {
                    var data = z.Elements("Data").FirstOrDefault();
                    if (count == 1) {
                        nambercatalog = data.Value.Trim() == "null" ? "" : data.Value;
                    }
                    else if (count == 2) {
                        title = data.Value.Trim() == "null" ? "" : data.Value;
                    }
                    else if (count == 3)
                    {
                        body = data.Value.Trim()=="null"?"":data.Value;
                    }
                    else if (count == 4)
                    {
                        details = data.Value.Trim() == "null" ? "" : data.Value;
                    }
                    else if (count == 5)
                    {
                        if (LocalisationSetting.culture().ToLower() == "ru-ru")
                            decimal.TryParse(data.Value.Trim().Replace(".", ","), out price);
                        if (LocalisationSetting.culture().ToLower() == "en-us")
                            decimal.TryParse(data.Value.Trim().Replace(",", "."), out price);
                    }
                    else if (count == 6)
                    {
                        ISO = data.Value.Trim();
                    }
                    else if (count == 7)
                    {
                        packing = data.Value.Trim();
                    }
                    else if (count == 8)
                    {
                        int.TryParse(data.Value.Trim(), out countproduct);
                    }
                    else if (count == 9)
                    {
                        if (data.Value.Trim().Length > 1)
                            create = false;
                        int _viewprice = 0;
                        int.TryParse(data.Value.Trim(), out _viewprice);
                        if (_viewprice == 1)
                            viewprice = true;
                    }
                    else if (count == 10)
                    {
                        if (data.Value.Trim().Length > 1)
                            create = false;
                        int _viewprice = 0;
                        int.TryParse(data.Value.Trim(), out _viewprice);
                        if (_viewprice == 1)
                            viewcount = true;
                    }
                    else if (count == 11)
                    {
                        if (data.Value.Trim().Length > 1)
                            create = false;
                        int _viewprice = 0;
                        int.TryParse(data.Value.Trim(), out _viewprice);
                        if (_viewprice == 1)
                            viewvotes = true;
                    }
                    else if (count == 12)
                    {
                        if (data.Value.Trim().Length > 1)
                            create = false;
                        int _viewprice = 0;
                        int.TryParse(data.Value.Trim(), out _viewprice);
                        if (_viewprice == 1)
                            allculture = true;
                    }
                    count++;
                }
                if (create&&count>=12)
                {
                    var zz = CreateProduct(departmentid, producerid, title,
                                body, details, LocalisationSetting.culture(), allculture, price, countproduct,
                                "", viewcount, viewprice, viewvotes, packing,
                                ISO, nambercatalog, 0);
                }

            }
            
        }
        private string replaceforRussia(string a)
        {
            return a.Replace("&#1081;", "й").Replace("&#1094;", "ц").Replace("&#1091;", "у")
                    .Replace("&#1082;", "к").Replace("&#1077;", "е").Replace("&#1085;", "н")
                    .Replace("&#1075;", "г").Replace("&#1096;", "ш").Replace("&#1097;", "щ")
                    .Replace("&#1079;", "з").Replace("&#1093;", "х").Replace("&#1098;", "ъ")
                    .Replace("&#1092;", "ф").Replace("&#1099;", "ы").Replace("&#1074;", "в")
                    .Replace("&#1072;", "а").Replace("&#1087;", "п").Replace("&#1088;", "р")
                    .Replace("&#1086;", "о").Replace("&#1083;", "л").Replace("&#1076;", "д")
                    .Replace("&#1078;", "ж").Replace("&#1101;", "э").Replace("&#1103;", "я")
                    .Replace("&#1095;", "ч")
                    .Replace("&#1089;", "с")
                    .Replace("&#1084;", "м")
                    .Replace("&#1080;", "и")
                    .Replace("&#1090;", "т")
                    .Replace("&#1100;", "ь")
                    .Replace("&#1073;", "б")
                    .Replace("&#1102;", "ю")
                    .Replace("&#1025;", "ё")
                    .Replace("&quot;", " ")
                    .Replace("&#8470;", "№")
                     .Replace("&#1049;", "Й")
                    .Replace("&#1062;", "Ц")
                    .Replace("&#1059;", "У")
                    .Replace("&#1050;", "К")
                    .Replace("&#1045;", "Е")
                    .Replace("&#1053;", "Н")
                    .Replace("&#1043;", "Г")
                    .Replace("&#1064;", "Ш")
                    .Replace("&#1065;", "Щ")
                    .Replace("&#1047;", "З")
                    .Replace("&#1061;", "Х")
                    .Replace("&#1066;", "Ъ")
                    .Replace("&#1060;", "Ф")
                    .Replace("&#1067;", "Ы")
                    .Replace("&#1042;", "В")
                    .Replace("&#1040;", "А")
                    .Replace("&#1055;", "П")
                    .Replace("&#1056;", "Р")
                    .Replace("&#1054;", "О")
                    .Replace("&#1051;", "Л")
                    .Replace("&#1044;", "Д")
                    .Replace("&#1046;", "Ж")
                    .Replace("&#1069;", "Э")
                    .Replace("&#1071;", "Я")
                    .Replace("&#1063;", "Ч")
                    .Replace("&#1057;", "С")
                    .Replace("&#1052;", "М")
                    .Replace("&#1048;", "И")
                    .Replace("&#1058;", "Т")
                    .Replace("&#1068;", "Ь")
                    .Replace("&#1041;", "Б")
                    .Replace("&#1070;", "Ю")
                    .Replace("&#1025;", "Ё")
                    .Replace("&#8220;","\"")
                    .Replace("&#8221;", "\"")
                    .Replace("&#8212;", "-");
        }
    }
}
