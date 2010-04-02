//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using Mytrip.Core;

namespace Mytrip.Articles.Repository
{
    public class ArticleRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к Entity Репозиторию
        /// <summary>
        /// Подключение к Entity Репозиторию
        /// </summary>
        //articlesEntities _db = new articlesEntities(ArticlesSetting.connectionString);
        articlesEntities _entities;
        public articlesEntities _db
        {
            get
            {
                if (_entities == null)
                    _entities = new articlesEntities(ArticlesSetting.connectionString);
                return _entities;
            }
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все статьи, новости и посты (постранично включительно)
        /// <summary>
        /// Получить все статьи и посты с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetAllArticles()
        {
            return _db.mytrip_Articles.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи и посты с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetAllArticlesPaged(int pageIndex, int pageSize, out int total)
        {
            var a = _db.mytrip_Articles;
            total = a.Count();
            return a.OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить колличество всех статей по культуре
        /// <summary>
        /// ППолучить колличество всех статей по культуре        
        /// </summary>
        /// <returns></returns>
        public int GetAllArticlesCount(string culture)
        {
            return _db.mytrip_Articles.Count(x => x.Culture==culture
                &&x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog==false
                &&x.CloseDate >= DateTime.Now);
        }
        #endregion

        #region Получить последние добавленные статьи
        public IQueryable<mytrip_Articles> GetLastArticles(int count)
        {
            return _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_Articles> GetLastArticlesByDate(int days,string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);  
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetLastArticlesByDate(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a= _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetLastArticles(string culture)
        {
            return _db.mytrip_Articles
                .Where(x => x.Culture == culture)
                .Where(x=>x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog==false)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        #region Получить все статьи не закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца Х->1
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpened(string culture)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            return b.Union(c)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedPaged(int pageIndex, int pageSize, string culture)
        {
            var d = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now);
            var e = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            return d.Union(e)
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца не входящие в меню
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedNoMenu(string culture)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var d = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var e = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            return b.Union(c).Union(d).Union(e)
                .OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetArticlesOpenedNoMenu(int take, string culture)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var d = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var e = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            return b.Union(c).Union(d).Union(e).Take(take)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца не входящие в меню постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedPagedNoMenu(int pageIndex, int pageSize, string culture, out int total)
        {
            var b = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var c = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var d = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var e = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            var f = b.Union(c).Union(d).Union(e)
                .OrderByDescending(x => x.CreateDate);
            total = f.Count();
            return f.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца для главной в отдельном блоке
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedMenu(string culture)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId == x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.mytrip_ArticlesCategory.SeparateBlock == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            var d = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            var e = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.Blog == false)
                .Where(x => x.mytrip_ArticlesCategory.CategoryId != x.mytrip_ArticlesCategory.SubCategoryId)
                .Where(x => x.AllCulture == true)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock == true)
                .Where(x => x.CloseDate >= DateTime.Now);
            return b.Union(c).Union(d).Union(e)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все статьи закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить все статьи закрытые по дате с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetClosedArticles(int count)
        {
            return _db.mytrip_Articles
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.CloseDate).Take(count);
        }
        public IQueryable<mytrip_Articles> GetClosedArticlesByDate(int days,string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CloseDate < DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
           return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetClosedArticlesByDate(int endDay, int range,string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CloseDate < DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи закрытые по дате с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetClosedArticlesPaged(int pageIndex, int pageSize)
        {
            return _db.mytrip_Articles
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить все статьи закрытые по дате (постранично включительно)
        public int GetClosedArticlesCount(string culture)
        {
            return _db.mytrip_Articles.Count(x => x.CloseDate < DateTime.Now && x.Culture == culture);
        }
        #endregion

        #region Получить статьи не закрытые по дате по поиску постранично
        /// <summary>
        /// Получить статьи не закрытые по дате по поиску постранично
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedBySearchPaged(string search, int pageIndex, int pageSize, out int total, string culture)
        {
            IQueryable<mytrip_Articles> ad = null;
            if (ArticlesSetting.blogs && ArticlesSetting.articles)
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Title.Contains(search)).Where(x => x.CloseDate >= DateTime.Now);
                var b = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Abstract.Contains(search)).Where(x => x.CloseDate >= DateTime.Now);
                var c = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Title.Contains(search)).Where(x => x.CloseDate >= DateTime.Now);
                var d = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Abstract.Contains(search)).Where(x => x.CloseDate >= DateTime.Now);
                ad = a.Union(b).Union(c).Union(d).OrderByDescending(x => x.CreateDate);
            }
            else if (!ArticlesSetting.blogs)
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Title.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false);
                var b = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Abstract.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false);
                var c = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Title.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false);
                var d = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Abstract.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false);
                ad = a.Union(b).OrderByDescending(x => x.CreateDate);
            }
            else
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Title.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true);
                var b = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.Abstract.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true);
                var c = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Title.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true);
                var d = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.Abstract.Contains(search))
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true);
                ad = a.Union(b).OrderByDescending(x => x.CreateDate);
            }
            total = ad.Count();
            return ad.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить все статьи из рубрики (постранично включительно)
        /// <summary>
        /// Получить все статьи из рубрики с конца Х->1
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetAllArticlesByCategory(int categoryId)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            return b.Concat(c).OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetAllArticlesByCategoryPaged(int categoryId, int pageIndex, int pageSize, out int total)
        {
            var a = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            var c = a.Concat(b);
            total = c.Count();
            return c.OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить статьи не закрытые по дате из рубрики (постранично включительно)
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedByCategory(int categoryId)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            return b.Union(c)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesOpenedByCategoryPaged(int categoryId, int pageIndex, int pageSize, out int total, string culture)
        {
            var a = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            var ab = a.Union(b).Where(x => x.CloseDate >= DateTime.Now);
            var c = ab.Where(x => x.AllCulture == true);
            var d = ab.Where(x => x.Culture == culture);
            var cd = c.Union(d).OrderByDescending(x => x.CreateDate);
            total = cd.Count();
            return cd.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить статьи закрытые по дате из рубрики (постранично включительно)
        /// <summary>
        /// Получить статьи закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesClosedByCategory(int categoryId)
        {
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var c = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            return b.Concat(c)
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить статьи закрытые по дате из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesClosedByCategoryPaged(int categoryId, int pageIndex, int pageSize, out int total)
        {
            var a = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.CategoryId == categoryId);
            var b = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.CategoryId == categoryId);
            var ab = a.Concat(b).Where(x => x.CloseDate < DateTime.Now);
            total = ab.Count();
            return ab
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить статью по Id
        /// <summary>
        /// Получить статью по Id
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <returns></returns>
        public mytrip_Articles GetArticleById(int articleId)
        {
            return _db.mytrip_Articles.FirstOrDefault(x => x.ArticleId == articleId);
        }
        /// <summary>
        /// Получить последнюю статью
        /// </summary>
        /// <returns></returns>
        public mytrip_Articles GetArticleFirst()
        {
            return _db.mytrip_Articles.LastOrDefault();
        }
        #endregion

        #region _Создать уникальный ArticleId
        private int CreateArticleId()
        {
            int articleId;
            for (articleId = 1; GetArticleById(articleId) != null; articleId++) ;
            return articleId;
        }
        #endregion

        #region Создать статью
        /// <summary>
        /// Создать статью
        /// </summary>
        /// <param name="categoryid">номер рубрики</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="includeAnonymComment">разрешить анонимам оставлять комментарии</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        /// <param name="closedate">дата закрытия</param>
        /// <returns></returns>
        public mytrip_Articles CreateArticle(int categoryId, string title, string description,
            string body, bool addComment, string imageForAbstract, bool onlyRegistered, bool approvedVotes,
            bool includeAnonymComment, DateTime closeDate, bool allCulture)
        {
            mytrip_ArticlesCategory category = _db.mytrip_ArticlesCategory.Single(c => c.CategoryId == categoryId);
            if (!category.AllCulture)
                allCulture = false;
            mytrip_Articles x = new mytrip_Articles
            {
                ArticleId = CreateArticleId(),
                CategoryId = categoryId,
                Title = title,
                Abstract = StaticMethod.CleanHtmlCode(description),
                Body = StaticMethod.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Views = 0,
                ApprovedComment = addComment,
                ImageForAbstract = imageForAbstract,
                Path = StaticMethod.DecodingString(title),
                OnlyForRegisterUser = onlyRegistered,
                ApprovedVotes = approvedVotes,
                TotalVotes = 0,
                IncludeAnonymComment = includeAnonymComment,
                CloseDate = closeDate,
                AllCulture = allCulture,
                Culture = category.Culture
            };
            _db.mytrip_Articles.AddObject(x);
            _db.SaveChanges();
            return x;
        }
        #endregion

        #region Обновить статью
        /// <summary>
        /// Обновить статью
        /// </summary>
        /// <param name="id">номер статьи</param>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="addComment">разрешить комментарии</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        /// <param name="approvedVotes">разрешить голосование</param>
        /// <param name="includeAnonymComment">разрешить анонимам оставлять комментарии</param>
        /// <param name="closeDate">дата закрытия</param>
        /// <param name="allCulture">для всех языков</param>
        public void UpdateArtiсle(int id, int categoryId, string title, string description,
            string body, bool addComment, string imageForAbstract, bool onlyRegistered,
            bool approvedVotes, bool includeAnonymComment, DateTime closeDate, bool allCulture)
        {
            if (!_db.mytrip_ArticlesCategory.Single(c => c.CategoryId == categoryId).AllCulture)
                allCulture = false;
            mytrip_Articles x = GetArticleById(id);
            x.CategoryId = categoryId;
            x.Title = title;
            x.Abstract = StaticMethod.CleanHtmlCode(description);
            x.Body = StaticMethod.CleanHtmlCode(body);
            x.ImageForAbstract = imageForAbstract;
            x.ApprovedComment = addComment;
            x.ApprovedVotes = approvedVotes;
            x.IncludeAnonymComment = includeAnonymComment;
            x.OnlyForRegisterUser = onlyRegistered;
            x.CloseDate = closeDate;
            x.Path = StaticMethod.DecodingString(title);
            x.AllCulture = allCulture;
            _db.SaveChanges();
        }

        #endregion

        #region Создать пост
        /// <summary>
        /// Создать пост
        /// </summary>
        /// <param name="categoryid">номер рубрики</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        /// <returns></returns>
        public mytrip_Articles CreatePost(int categoryId, string title, string description,
            string body, string imageForAbstract, bool onlyRegistered, bool includeAnonymComment)
        {
            mytrip_Articles x = new mytrip_Articles
            {
                ArticleId = CreateArticleId(),
                CategoryId = categoryId,
                Title = title,
                Abstract = StaticMethod.CleanHtmlCode(description),
                Body = StaticMethod.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Views = 0,
                ApprovedComment = true,
                ImageForAbstract = imageForAbstract,
                Path = StaticMethod.DecodingString(title),
                OnlyForRegisterUser = onlyRegistered,
                ApprovedVotes = true,
                TotalVotes = 0,
                IncludeAnonymComment = includeAnonymComment,
                CloseDate = DateTime.MaxValue,
                Culture = _db.mytrip_ArticlesCategory.First(y => y.CategoryId == categoryId).Culture,
                AllCulture = _db.mytrip_ArticlesCategory.First(y => y.CategoryId == categoryId).AllCulture
            };
            _db.mytrip_Articles.AddObject(x);
            _db.SaveChanges();
            return x;
        }
        #endregion

        #region Обновить пост
        /// <summary>
        /// Обновить пос
        /// </summary>
        /// <param name="id">номер поста</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        public void UpdatePost(int id, string title, string description,
            string body, string imageForAbstract, bool onlyRegistered)
        {
            mytrip_Articles a = GetArticleById(id);
            a.Title = title;
            a.Abstract = StaticMethod.CleanHtmlCode(description);
            a.Body = StaticMethod.CleanHtmlCode(body);
            a.ImageForAbstract = imageForAbstract;
            a.OnlyForRegisterUser = onlyRegistered;
            a.Path = StaticMethod.DecodingString(title);
            a.Culture = _db.mytrip_ArticlesCategory.First(y => y.CategoryId == a.CategoryId).Culture;
            a.AllCulture = _db.mytrip_ArticlesCategory.First(y => y.CategoryId == a.CategoryId).AllCulture;
            _db.SaveChanges();
        }
        #endregion

        #region Удалить статью
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void DeleteArticle(int id)
        {
            mytrip_Articles art = GetArticleById(id);
            foreach (var t in art.mytrip_ArticlesVotes.ToList())
            {
                _db.mytrip_ArticlesVotes.DeleteObject(t);
            }
            foreach (var t in art.mytrip_ArticlesComments.ToList())
            {
                _db.mytrip_ArticlesComments.DeleteObject(t);
            }
            foreach (var t in art.mytrip_ArticlesTag.ToList())
            {
                art.mytrip_ArticlesTag.Remove(t);
            }
            _db.mytrip_Articles.DeleteObject(art);
            _db.SaveChanges();
        }
        #endregion

        #region Удалить все статьи из категории
        /// <summary>
        /// Удалить все статьи из категории
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void DeleteArticlesByCategory(int categoryId)
        {
            IQueryable<mytrip_Articles> articles = GetAllArticlesByCategory(categoryId);
            foreach (mytrip_Articles article in articles)
            {
                DeleteArticle(article.ArticleId);
            }
            _db.SaveChanges();
        }
        #endregion

        #region Счетчик просмотров статьи
        /// <summary>
        /// Счетчик просмотров статьи
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void IncrementArticleViews(int id)
        {
            mytrip_Articles x = GetArticleById(id);
            x.Views++;
            _db.SaveChanges();
        }
        #endregion

        #region Открыть комментарии
        /// <summary>
        /// Открыть комментарии
        /// </summary>
        /// <param name="articleId">mt_article_comment</param>
        public void OpenComments(int articleId)
        {
            mytrip_Articles a = GetArticleById(articleId);
            a.ApprovedComment = true;
            _db.SaveChanges();
        }
        #endregion

        #region Закрыть комментарии
        /// <summary>
        /// Закрыть комментарии
        /// </summary>
        /// <param name="articleId">mt_article_comment</param>
        public void CloseComments(int articleId)
        {
            mytrip_Articles a = GetArticleById(articleId);
            a.ApprovedComment = false;
            a.IncludeAnonymComment = false;
            _db.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить все статьи и посты не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetArticlesPostsOpenedByTag(int tagId, string culture)
        {

            var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
            var b = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
            return a.Union(b).OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetArticlesPostsOpenedByTagPaged(int pageIndex, int pageSize, int tagId, string culture, out int total)
        {
            IQueryable<mytrip_Articles> ab = null;
            if (ArticlesSetting.blogs && ArticlesSetting.articles)
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                var b = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                ab = a.Union(b).OrderByDescending(x => x.CreateDate);
            }
            else if (!ArticlesSetting.blogs)
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                var b = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == false)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                ab = a.Union(b).OrderByDescending(x => x.CreateDate);
            }
            else
            {
                var a = _db.mytrip_Articles.Where(x => x.Culture == culture)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                var b = _db.mytrip_Articles.Where(x => x.AllCulture == true)
                    .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                    .Where(x => x.CloseDate >= DateTime.Now)
                    .Where(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId);
                ab = a.Union(b).OrderByDescending(x => x.CreateDate);
            }
            total = ab.Count();
            return ab.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить количество всех тегов
        /// <summary>
        /// Получить количество всех тегов        
        /// </summary>
        /// <returns></returns>
        public int GetTagsCount()
        {
            return _db.mytrip_ArticlesTag.Count();
        }
        #endregion

        #region Получить количество всех постов в теге
        /// <summary>
        /// Получить количество всех постов в теге        
        /// </summary>
        /// <returns></returns>
        public int GetArticlesInTagsCount(int tagId,string culture)
        {
            return _db.mytrip_Articles.Count(x => x.mytrip_ArticlesTag.FirstOrDefault(y => y.TagId == tagId).TagId == tagId && 
                x.Culture == culture && x.CloseDate >= DateTime.Now);
        }
        #endregion

        //Теги

        #region Получить все теги
        /// <summary>
        /// Получить все теги        
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesTag> GetAllTags()
        {
            return _db.mytrip_ArticlesTag.OrderBy(x => x.TagName);
        }
        #endregion

        #region Создать тег
        /// <summary>
        /// Создать тег
        /// </summary>
        /// <returns></returns>
        public mytrip_ArticlesTag CreateTag(string name)
        {
            if (_db.mytrip_ArticlesTag.FirstOrDefault(x => x.TagName == name) != null)
                return _db.mytrip_ArticlesTag.FirstOrDefault(x => x.TagName == name);
            mytrip_ArticlesTag tag = new mytrip_ArticlesTag
            {
                TagId = CreateTagId(),
                TagName = name,
                Path = "(Tag)" + StaticMethod.DecodingString(name)
            };
            _db.mytrip_ArticlesTag.AddObject(tag);
            _db.SaveChanges();
            return tag;
        }
        #endregion

        #region Получить тег по Id
        /// <summary>
        /// Получить тег по Id
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public mytrip_ArticlesTag GetTag(int tagId)
        {
            return _db.mytrip_ArticlesTag.FirstOrDefault(x => x.TagId == tagId);
        }
        #endregion

        #region Обновить тег
        /// <summary>
        ///  Обновить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <param name="name">имя тега</param>
        public void UpdateTag(int tagId, string name)
        {
            mytrip_ArticlesTag t = GetTag(tagId);
            t.TagName = name;
            t.Path = "(Tag)" + StaticMethod.DecodingString(name);
            _db.SaveChanges();
        }
        #endregion

        #region Удалить teg
        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        public void DeleteTag(int tagId)
        {
            mytrip_ArticlesTag x = GetTag(tagId);
            foreach (var article in x.mytrip_Articles.ToList())
            {
                article.mytrip_ArticlesTag.Remove(x);
            }
            _db.mytrip_ArticlesTag.DeleteObject(x);
            _db.SaveChanges();
        }
        #endregion

        #region _Создать уникальный TagId
        private int CreateTagId()
        {
            int tagId;
            for (tagId = 1; GetTag(tagId) != null; tagId++) ;
            return tagId;
        }
        #endregion

        #region Добавить тег в статью
        /// <summary>
        /// Добавить тег в статью
        /// </summary>
        ///<param name="articleId">номер статьи</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public void AddTagInArticle(int articleId, int tagId)
        {
            mytrip_ArticlesTag mt = GetTag(tagId);
            mytrip_Articles ma = GetArticleById(articleId);
            if (mt.mytrip_Articles.FirstOrDefault(x => x.ArticleId == articleId) != null)
                return;
            mt.mytrip_Articles.Add(ma);
            _db.SaveChanges();
        }
        #endregion

        #region Удалить тег из статьи
        /// <summary>
        /// Удалить тег из статьи
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="tagId">номер тега</param>
        public void DeleteTagFromArticle(int articleId, int tagId)
        {
            mytrip_ArticlesTag mt = GetTag(tagId);
            mytrip_Articles ma = GetArticleById(articleId);
            if (mt.mytrip_Articles.FirstOrDefault(x => x.ArticleId == articleId) == null)
                return;
            mt.mytrip_Articles.Remove(ma);
            _db.SaveChanges();
        }
        #endregion

        #region Удалить все теги из статьи
        /// <summary>
        /// Удалить тег из статьи
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="tagId">номер тега</param>
        public void DeleteAllTagsFromArticle(int articleId)
        {
            mytrip_Articles ma = GetArticleById(articleId);
            foreach (var t in ma.mytrip_ArticlesTag.ToList())
            {
                t.mytrip_Articles.Remove(ma);
            }
            _db.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 6  */

        #region Получить все посты  (постранично включительно)
        /// <summary>
        /// Получить все посты с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetPosts(string culture)
        {
            return _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все посты  с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_Articles> GetPostsPaged(int pageIndex, int pageSize, string culture, out int total)
        {
            var a = _db.mytrip_Articles.Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить количество всех постов по культуре
        /// <summary>
        /// ППолучить количество всех постов по культуре        
        /// </summary>
        /// <returns></returns>
        public int GetAllPostsCount(string culture)
        {
            return _db.mytrip_Articles.Count(x => x.Culture == culture && x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true);
        }
        #endregion

        #region Получить последние добавленные посты
        public IQueryable<mytrip_Articles> GetLastPosts(int count)
        {
            return _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_Articles> GetLastPostsByDate(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetLastPostsByDate(int endDay, int range,string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = _db.mytrip_Articles
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate < eDate)
                .Where(x => x.CreateDate > sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_Articles> GetLastPosts(string culture)
        {
            return _db.mytrip_Articles
                .Where(x => x.Culture == culture)
                .Where(x => x.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        /*  РАЗДЕЛ 7  */

        #region Создать голосование для статьи
        public bool CreateVote(int articleId, int vote)
        {
            bool result = false;
            if (_db.mytrip_ArticlesVotes.Where(x => x.ArticleId == articleId)
                .FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name) == null)
            {
                mytrip_ArticlesVotes x = new mytrip_ArticlesVotes
                {
                    VotesId = CreateVotesId(),
                    ArticleId = articleId,
                    UserName = HttpContext.Current.User.Identity.Name,
                    Vote = vote
                };
                _db.mytrip_ArticlesVotes.AddObject(x);
                _db.SaveChanges();
                result = true;
            }
            return result;
        }
        #endregion

        #region Пересчет оценки голосования
        public decimal GetNewTotalVotes(int articleId, int vote)
        {
            bool votesCreate = CreateVote(articleId, vote);
            mytrip_Articles article = GetArticleById(articleId);
            if (votesCreate)
            {
                int countVotes = article.mytrip_ArticlesVotes.Count();
                int VotesSumm = 0;
                foreach (mytrip_ArticlesVotes x in article.mytrip_ArticlesVotes)
                {
                    VotesSumm += x.Vote;
                }
                if (countVotes <= 0)
                    countVotes = 1;
                if (VotesSumm <= 0)
                    VotesSumm = 1;
                decimal total = VotesSumm / countVotes;
                article.TotalVotes = total;
                _db.SaveChanges();
                return total;
            }
            else
            {
                decimal total = article.TotalVotes;
                return total;
            }
        }
        #endregion

        #region _Создать уникальный VotesId
        private int CreateVotesId()
        {
            int VotesId;
            for (VotesId = 1; GetVotes(VotesId) != null; VotesId++) ;
            return VotesId;
        }
        public mytrip_ArticlesVotes GetVotes(int votesId)
        {
            return _db.mytrip_ArticlesVotes.FirstOrDefault(x => x.VotesId == votesId);
        }
        #endregion
    }
}
