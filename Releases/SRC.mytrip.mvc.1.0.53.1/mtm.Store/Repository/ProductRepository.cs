﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using mtm.Store.Repository.DataEntities;
using mtm.Core.Settings;
using System.Xml.Linq;
using System.IO;
using mtm.Core.Repository;

namespace mtm.Store.Repository
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
        public IQueryable<mytrip_storeproduct> GetProductForSearch(string culture, string search)
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
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.Title.Contains(search) == true || x.Body.Contains(search) == true || x.NamberCatalog.Contains(search) == true);
            return b.OrderByDescending(x => x.CreationDate);
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
            string cultureMoney,string namberCatalog,int saleid,
            string path, string seokeywords, string seodescription, string seotitle)
        {
            sale.CreateSaleZero();
            mytrip_storeproduct x = new mytrip_storeproduct
            {
                ProductId = CreateProductId(),
                DepartmentId = departmentId,
                ProducerId = producerId,
                Title = title,
                Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title),
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
                NamberCatalog = namberCatalog,
                SeoKeyword = seokeywords,
                SeoDescription = seodescription,
                SeoTitle = (seotitle != null && seotitle.Length > 0) ? seotitle : title
            };
            entities.mytrip_storeproduct.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_storeproduct CreateProduct(int id,int departmentId, int producerId, string title,
            string _abstract, string body, string culture, bool allculture, decimal price, int totalcount,
            string urlfile, bool viewcount, bool viewprice, bool viewvotes, string packing,
            string cultureMoney, string namberCatalog, int saleid,
            string path, string seokeywords, string seodescription, string seotitle)
        {
            sale.CreateSaleZero();
            mytrip_storeproduct x = new mytrip_storeproduct
            {
                ProductId = id,
                DepartmentId = departmentId,
                ProducerId = producerId,
                Title = title,
                Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title),
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
                Packing = packing,
                SaleId = saleid,
                MoneyId = cultureMoney,
                NamberCatalog = namberCatalog,
                SeoKeyword = seokeywords,
                SeoDescription = seodescription,
                SeoTitle = (seotitle != null && seotitle.Length > 0) ? seotitle : title
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
            string cultureMoney, string namberCatalog, int saleid,
            string path, string seokeywords, string seodescription, string seotitle)
        {
            mytrip_storeproduct x = GetProduct(id);
            x.DepartmentId = departmentId;
            x.ProducerId = producerId;
            x.Title = title;
            x.Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title);
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
            x.SeoKeyword = seokeywords;
            x.SeoDescription = seodescription;
            x.SeoTitle = (seotitle != null && seotitle.Length > 0) ? seotitle : title;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="producerid"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string CreateProductXml(HttpPostedFileBase file)
        {

            string name = Path.GetFileName(file.FileName);
            string extension = name.Remove(0, name.LastIndexOf("."));
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog");
            DirectoryInfo folder = new DirectoryInfo(absolutDirectory);
            if (!folder.Exists)
                folder.Create();            
            string _absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Catalog/" + name);
            file.SaveAs(_absolutDirectory);
            return name;
           
        }
    }
}
