﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Store.Repository.DataEntities;
using System.Web;
using mtm.Core.Settings;
using System.IO;

namespace mtm.Store.Repository
{
    /// <summary>Репозиторий отделов
    /// </summary>
    public class DepartmentRepository
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

        /// <summary>Все отделы с учетом текущей культуры
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IQueryable &lt; mytrip_storedepartment &gt;</returns>
        public IQueryable<mytrip_storedepartment> GetAllDepartment(string culture)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storedepartment1")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment2.mytrip_storesale")
                .Where(x => x.SubDepartmentId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }

        /// <summary>Все отделы постранично с учетом текущей культуры
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="total">(out int) общее количество отделов</param>
        /// <returns>возвращает IQueryable &lt; mytrip_storedepartment &gt;</returns>
        public IQueryable<mytrip_storedepartment> GetAllDepartment(int pageIndex, int pageSize, string culture, out int total)
        {
            var a = entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment2.mytrip_storesale")
                .Where(x => x.SubDepartmentId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>Все суботделы отдела с учетом культуры
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="id">индентификатор отдела</param>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IQueryable &lt; mytrip_storedepartment &gt;</returns>
        public IQueryable<mytrip_storedepartment> GetSubDepartment(int id, string culture)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment2.mytrip_storesale")
                .Where(x => x.SubDepartmentId == id)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }

        /// <summary>Отдел по индентификатору
        /// </summary>
        /// <param name="id">индентификатор отдела</param>
        /// <returns>возвращает mytrip_storedepartment</returns>
        public mytrip_storedepartment GetDepartment(int id)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment2")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .Include("mytrip_storesale")
                .Include("mytrip_storedepartment2.mytrip_storesale")
                .FirstOrDefault(x => x.DepartmentId == id);
        }

        /// <summary>Все оттделы для дропдауна
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IDictionary &lt; int,string &gt;</returns>
        public IDictionary<int, string> GetDepartmentForDdl(string culture, bool search)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = entities.mytrip_storedepartment.Include("mytrip_storedepartment1")
           .Where(x => x.DepartmentId != 0 && x.SubDepartmentId == 0)
           .Where(x => x.Culture == culture || x.AllCulture == true)
           .OrderBy(x => x.Title);
            if (search)
                mcats.Add(0, StoreLanguage.AllDepartment);
            foreach (mytrip_storedepartment cat in a)
            {
                mcats.Add(cat.DepartmentId, cat.Title);
                foreach (mytrip_storedepartment subcat in cat.mytrip_storedepartment1)
                {
                    string Title = "--" + subcat.Title;
                    mcats.Add(subcat.DepartmentId, Title);
                }
            }
            return mcats;
        }

        /// <summary>Создание отдела
        /// </summary>
        /// <param name="id">индентификатор чьим суботделом будет создаваемый отдел</param>
        /// <param name="title">название отдела</param>
        /// <param name="body">описание отдела (возможен null)</param>
        /// <param name="image">изображение отдела (возможен null)</param>
        /// <param name="allculture">показывать для всех культур (true для всех)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает mytrip_storedepartment</returns>
        public mytrip_storedepartment CreateDepartment(int id, string title, string body, bool allculture, string culture,int saleid,
            string path,string seokeywords,string seodescription,string seotitle)
        {
            CreateDepartmentZero();
            sale.CreateSaleZero();
            mytrip_storedepartment x = new mytrip_storedepartment
            {
                DepartmentId = CreateDepartmentId(),
                Title = title,
                Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title),
                CreationDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Body = body,                
                SubDepartmentId = id,
                AllCulture = allculture,
                Culture = culture,
                SaleId=saleid,
                SeoKeyword = seokeywords,
                SeoDescription = seodescription,
                SeoTitle = (seotitle != null && seotitle.Length > 0) ? seotitle : title
            };
            entities.mytrip_storedepartment.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_storedepartment CreateDepartment(int department,int id, string title, string body, bool allculture, string culture, int saleid,
            string path, string seokeywords, string seodescription, string seotitle)
        {
            CreateDepartmentZero();
            sale.CreateSaleZero();
            mytrip_storedepartment x = new mytrip_storedepartment
            {
                DepartmentId = department,
                Title = title,
                Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title),
                CreationDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Body = body,
                SubDepartmentId = id,
                AllCulture = allculture,
                Culture = culture,
                SaleId = saleid,
                SeoKeyword = seokeywords,
                SeoDescription = seodescription,
                SeoTitle = (seotitle != null && seotitle.Length > 0) ? seotitle : title
            };
            entities.mytrip_storedepartment.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        /// <summary>Редактирование отдела
        /// </summary>
        /// <param name="id">индентификатор отдела</param>
        /// <param name="title">название отдела</param>
        /// <param name="body">описание отдела (возможен null)</param>
        /// <param name="image">изображение отдела (возможен null)</param>
        /// <param name="allculture">показывать для всех культур (true для всех)</param>
        /// <returns>возвращает mytrip_storedepartment</returns>
        public mytrip_storedepartment EditDepartment(int id, string title, string body, bool allculture,int saleid,
            string path, string seokeywords, string seodescription, string seotitle)
        {
            mytrip_storedepartment x = GetDepartment(id);
            x.Title = title;
            x.Body = body;
            x.Path = (path != null && path.Length > 0) ? path : GeneralMethods.DecodingString(title);
            x.AllCulture = allculture;
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
        public void DeleteDepartment(int id)
        {
            mytrip_storedepartment x = GetDepartment(id);
            foreach (var y in x.mytrip_storedepartment1.ToList())
            {
                foreach (var q in y.mytrip_storeproduct.ToList())
                {
                    foreach (var z in q.mytrip_storevotes.ToList())
                    { entities.mytrip_storevotes.DeleteObject(z); }
                    foreach (var z in q.mytrip_storeorderisproduct.ToList())
                    {
                        entities.mytrip_storeorderisproduct.DeleteObject(z);
                    }
                    entities.mytrip_storeproduct.DeleteObject(q);
                }
            }
            foreach (var y in x.mytrip_storeproduct.ToList())
            {
                foreach (var z in y.mytrip_storevotes.ToList())
                { entities.mytrip_storevotes.DeleteObject(z); }
                foreach (var z in y.mytrip_storeorderisproduct.ToList())
                {
                    entities.mytrip_storeorderisproduct.DeleteObject(z);
                }
                entities.mytrip_storeproduct.DeleteObject(y);
            }
            entities.mytrip_storedepartment.DeleteObject(x);
            entities.SaveChanges();
        }
        /// <summary>Создать уникальный DepartmentId
        /// </summary>
        /// <returns>возвращает int</returns>
        private int CreateDepartmentId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storedepartment.Count(x => x.DepartmentId == catId) != 0; catId++) ;
            return catId;
        }

        /// <summary> Создать отдел ZERO
        /// </summary>
        private void CreateDepartmentZero()
        {
            mytrip_storedepartment zero = entities.mytrip_storedepartment.FirstOrDefault(x => x.DepartmentId == 0);
            if (zero == null)
            {
                sale.CreateSaleZero();
                mytrip_storedepartment x = new mytrip_storedepartment
                {
                    DepartmentId = 0,
                    Title = "zero",
                    Path = "zero",
                    CreationDate = DateTime.Now,
                    UserName = "mytripmvc",
                    Body = "null",
                    SubDepartmentId = 0,
                    AllCulture = false,
                    Culture = "zero",
                    SaleId=0
                };
                entities.mytrip_storedepartment.AddObject(x);
                entities.SaveChanges();
            }
        }
        
    }
}
