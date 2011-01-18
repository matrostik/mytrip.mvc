﻿//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about mytrip.mvc.Entyty visit 
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
using mtm.Core;
using System.Web.Caching;
using mtm.Articles.Repository.DataEntities;
using System.Data;
using mtm.Core.Settings;

namespace mtm.Articles.Repository
{
    #region Enum SortBy
    /// <summary>Enum for sort criteria
    /// </summary>
    public enum SortBy { CreateDate, Views, TotalVotes };
    #endregion

    public class ArticleRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;
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

        #region cache
        /// <summary>Получить все теги        
        /// </summary>
        /// <returns></returns>
        internal IQueryable<mytrip_articlestag> GetAllTags()
        {
            if (HttpContext.Current.Cache["as_getalltags"] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetAllTags();
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getalltags", x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getalltags"] as IQueryable<mytrip_articlestag>;

        }
        /// <summary>Получить определенное колличество не закрытых статей по культуре с учетом сортировки (SideBar)
        /// Выбор из всех статей, те что в меню и те что не в меню
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="sortBy">сортировка</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetArticles(string culture, SortBy sortBy, int take)
        {
            if (HttpContext.Current.Cache["as_getarticles"+sortBy+take+culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetArticles(culture, sortBy, take);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getarticles" + sortBy + take + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getarticles" + sortBy + take + culture] as IQueryable<mytrip_articles>;

        }
        /// <summary>Получить популярные посты не закрытые по просмотрам
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetPostsPopular(string culture, int take)
        {
            if (HttpContext.Current.Cache["as_getpostspopular" + take + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPostsPopular(culture,take);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getpostspopular" + take + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getpostspopular" + take + culture] as IQueryable<mytrip_articles>;

        }
        /// <summary>Получить статьи не закрытые по дате из рубрики с конца (Homepage)
        /// Выбор статей из рубрики с учетом культуры (если рубрика мультиязычна)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetArticles(int categoryId, string culture, int take)
        {
            if (HttpContext.Current.Cache["as_getarticles" + categoryId+"_" + take + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetArticles(categoryId,culture, take);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getarticles" + categoryId + "_" + take + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getarticles" + categoryId + "_" + take + culture] as IQueryable<mytrip_articles>;

        }
        /// <summary>Получить статьи не входящие в меню и не закрытые по дате с конца (Homepage)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetArticles(string culture, int take)
        {
            if (HttpContext.Current.Cache["as_getarticles" + take + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetArticles(culture, take);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getarticles" + take + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getarticles" + take + culture] as IQueryable<mytrip_articles>;

        }
        /// <summary>Получить посты с конца (Homepage)
        /// </summary>
        /// <param name="take">колличество статей для выборки</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetPosts(string culture, int take)
        {
            if (HttpContext.Current.Cache["as_getposts" + take + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPosts(culture, take);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getposts" + take + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getposts" + take + culture] as IQueryable<mytrip_articles>;

        }
        internal IQueryable<mytrip_articles> GetArticles(string culture, string search)
        {
            if (HttpContext.Current.Cache["as_getarticlessearch" + search + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(1800);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetArticles(culture, search);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("as_getarticlessearch" + search + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["as_getarticlessearch" + search + culture] as IQueryable<mytrip_articles>;

        }
        /// <summary>Получить статьи не закрытые по дате, по культуре с учетом сортировки, не входящие в меню, постранично (Индекс статей)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="sortBy">сортировка</param>
        /// <returns></returns>
        internal IQueryable<mytrip_articles> GetArticles(string culture, int pageIndex, int pageSize, SortBy sortBy, out int total)
        {
            if (HttpContext.Current.Cache["as_getarticles" + pageIndex+"_"+pageSize + sortBy + culture] == null||
                HttpContext.Current.Cache["as_getarticlestotal" + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(1800);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetArticles(culture, pageIndex, pageSize, sortBy, out total);
                if (x == null)
                {
                    return null;
                    total = 0;
                }
                HttpContext.Current.Cache.Insert("as_getarticles" + pageIndex + "_" + pageSize + sortBy + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
                HttpContext.Current.Cache.Insert("as_getarticlestotal" + culture, total, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            total = int.Parse(HttpContext.Current.Cache["as_getarticlestotal" + culture].ToString());
            return HttpContext.Current.Cache["as_getarticles" + pageIndex + "_" + pageSize + sortBy + culture] as IQueryable<mytrip_articles>;

        }

        #endregion

        #region Получить статьи не закрытые по дате (постранично включительно)
        ///<summary>
        /// Получить статьи по культуре не входящие в меню и не закрытые по дате с конца (RSS)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string culture)
        {
            var b = entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.Culture == culture || x.AllCulture)
                .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи или посты пользователя (включая закрытые)  (Profile)
        /// </summary>
        /// <param name="username">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string username, bool blog)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articlescomments")
                .Where(x => x.UserName == username && x.mytrip_articlescategory.Blog == blog && x.SubArticleId == 0)
                .OrderByDescending(x => x.CreateDate);
        }
        
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца (RSS)
        /// Выбор статей из рубрики с учетом культуры (если рубрика мультиязычна)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(int categoryId, string culture)
        {
            return entities.mytrip_articles
                .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                    || x.mytrip_articlescategory.SubCategoryId == categoryId)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.SubArticleId == 0)
                .OrderByDescending(x => x.CreateDate);
        }  
        
        
        /// <summary>
        /// Получить статьи не закрытые по дате, по поиску, по культуре, постранично с учетом сортировки (Индекс статей)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="search"></param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="total">общее количество статей</param>
        /// <param name="sortBy">сортировка</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string culture, string search, int pageIndex, int pageSize, SortBy sortBy, out int total)
        {
            var ad = entities.mytrip_articles
                   .Include("mytrip_articlescomments").Include("mytrip_articlestag")
                   .Include("mytrip_articlescategory.mytrip_articlescategory2")
                   .Where(x => x.Culture == culture || x.AllCulture == true)
                   .Where(x => x.Title.Contains(search) || x.Abstract.Contains(search))
                   .Where(x => x.CloseDate >= DateTime.Now);
            if (!ModuleSetting.articles())
                ad = ad.Where(x => x.mytrip_articlescategory.Blog == true);
            else if (!ModuleSetting.blogs())
                ad = ad.Where(x => x.mytrip_articlescategory.Blog == false);
            total = ad.Count();
            if (sortBy == SortBy.Views)
                return ad.OrderByDescending(x => x.Views).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sortBy == SortBy.TotalVotes)
                return ad.OrderByDescending(x => x.TotalVotes).ThenByDescending(x => x.ApprovedVotes).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return ad.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики, по культуре, постранично, с учетом сортировки (Индекс статей)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="sortBy">сортировка</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(int categoryId, string culture, int pageIndex, int pageSize, SortBy sortBy, out int total)
        {
            var a = entities.mytrip_articles
                .Include("mytrip_articlescategory")
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                    || x.mytrip_articlescategory.SubCategoryId == categoryId)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now);
            total = a.Count();
            if (sortBy == SortBy.Views)
                return a.OrderByDescending(x => x.Views).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sortBy == SortBy.TotalVotes)
                return a.OrderByDescending(x => x.TotalVotes).ThenByDescending(x => x.ApprovedVotes).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return a.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Get related articles (View article)
        /// </summary>
        /// <param name="culture">current culture</param>
        /// <param name="isBlog">is blog</param>
        /// <param name="title">title to find</param>
        /// <param name="take">count of items to return</param>
        /// <returns>List<mytrip_articles></returns>
        public List<mytrip_articles> GetRelated(string culture, bool isBlog, string title, int take)
        {
            List<string> exps = title.Split(' ').ToList();
            for (int i = 0; i < exps.Count; i++)
            {
                if (exps[i].Length <= 2)
                    exps.RemoveAt(i);
            }
            var arts = entities.mytrip_articles.Include("mytrip_articlescategory")
                   .Where(x => x.mytrip_articlescategory.Blog == isBlog)
                   .Where(x => x.Culture == culture || x.AllCulture == true)
                   .Where(x => x.CloseDate >= DateTime.Now)
                   .Where(x => x.Title != title);
            List<mytrip_articles> similar = new List<mytrip_articles>();
            foreach (string str in exps)
            {
                List<mytrip_articles> found = arts.Where(x => x.Title.Contains(str)).ToList();
                int fsdfg = found.Count();
                similar.AddRange(found);
                similar = similar.Distinct().ToList();
                if (similar.Count > take)
                    break;
            }
            return similar.Take(take).ToList();
        }
        #endregion

        #region Получить статьи закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить статьи закрытые по дате с конца
        /// </summary>
        /// <param name="count">количество статей</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesClosed(int count)
        {
            return entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.CloseDate).Take(count);
        }
        /// <summary>
        /// Получить статьи закрытые по дате определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesClosed(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate < DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить статьи закрытые по дате в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesClosed(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.CloseDate < DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить колличество статей или постов
        /// <summary>
        /// Get artilses or posts count by categoryId (including articles from subcategories)
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns></returns>
        public int GetCount(int categoryId)
        {
            int[] c = entities.mytrip_articlescategory.Where(x => x.CategoryId == categoryId || x.SubCategoryId == categoryId)
                .Select(x => x.CategoryId).ToArray();
            int count = 0;
            foreach (int id in c)
            {
                count += entities.mytrip_articles.Where(x => x.CategoryId == id && x.SubArticleId == 0).Count();
            }
            return count;
        }
        /// <summary>
        /// Получить колличество всех статей или постов по культуре        
        /// </summary>
        /// <returns></returns>
        public int GetCount(string culture, bool blog)
        {
            return entities.mytrip_articles
                .Count(x => x.Culture == culture
                && x.mytrip_articlescategory.Blog == blog
                && x.CloseDate >= DateTime.Now && x.SubArticleId == 0);
        }
        /// <summary>
        /// Получить колличество всех статей закрытых по дате по культуре
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public int GetClosedCount(string culture)
        {
            return entities.mytrip_articles.Count(x => x.CloseDate < DateTime.Now && x.Culture == culture && x.SubArticleId == 0);
        }
        #endregion

        #region Получить все посты  (постранично включительно)
        /// <summary>
        /// Получить все посты с конца (RSS)
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPosts(string culture)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate);
        }        
        #endregion

        #region Получить последние добавленные статьи
        /// <summary>
        /// Получить последние добавленные статьи
        /// </summary>
        /// <param name="count">количество статей</param>
        /// <returns>IQueryable(mytrip_articles)</returns>
        public IQueryable<mytrip_articles> GetLastArticles(int count)
        {
            return entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => !x.mytrip_articlescategory.Blog)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить статьи добавленные определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetLastArticles(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => !x.mytrip_articlescategory.Blog)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить статьи добавленные в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetLastArticles(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить последние добавленные посты
        /// <summary>
        /// Получить последние добавленные посты
        /// </summary>
        /// <param name="count">количество статей</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetLastPosts(int count)
        {
            return entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.mytrip_articlescategory.Blog)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить посты добавленные определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetLastPosts(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.mytrip_articlescategory.Blog)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.ArticleId != 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить посты добавленные в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetLastPosts(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.mytrip_articlescategory.Blog)
                .Where(x => x.SubArticleId == 0)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate < eDate)
                .Where(x => x.CreateDate > sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }

        /// <summary>Получить популярные статьи не закрытые по оценкам
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPostsRated(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == true)
                 .Where(x => x.ApprovedVotes == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.TotalVotes).Skip(0).Take(take);
        }
        /// <summary>Получить последние статьи не закрытые
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPostsRecent(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == true)
                 .Where(x => x.ApprovedVotes == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить статью по Id
        /// <summary>
        /// Получить статью по Id (с проверкой на подстатью)
        /// Если запрашивается подстатья, то тянется статья и ее содержание заменяется на содержание подстатьи
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>mytrip_articles</returns>
        public mytrip_articles GetArticle(int articleId)
        {
            int subid = entities.mytrip_articles.Where(x => x.ArticleId == articleId)
                .Select(x => x.SubArticleId).FirstOrDefault();
            if (subid == 0)
                return entities.mytrip_articles
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .FirstOrDefault(x => x.ArticleId == articleId);
            else
            {
                var a = entities.mytrip_articles
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .FirstOrDefault(x => x.ArticleId == subid);
                a.Body = entities.mytrip_articles.Where(x => x.ArticleId == articleId)
                    .Select(x => x.Body).FirstOrDefault();
                return a;
            }
        }
        /// <summary>
        /// Get subArticles
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>IQueryable<mytrip_articles></returns>
        IQueryable<mytrip_articles> GetSubArticles(int articleId)
        {
            var a = entities.mytrip_articles
                .Include("mytrip_articles2")
                .Where(x => x.SubArticleId == articleId)
                .OrderBy(x => x.ArticleId);
            return a;
        }
        /// <summary>
        /// Get Ids of article pages including main article
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>int[]</returns>
        public int[] GetArticlePagesIds(int articleId)
        {
            int subid = entities.mytrip_articles.Where(x => x.ArticleId == articleId)
                .Select(x => x.SubArticleId).FirstOrDefault();
            if (subid == 0)
                return entities.mytrip_articles
                .Where(x => x.ArticleId == articleId || x.SubArticleId == articleId)
                .OrderBy(x => x.ArticleId).Select(x => x.ArticleId).ToArray();
            else
                return entities.mytrip_articles
                .Where(x => x.ArticleId == subid || x.SubArticleId == subid)
                .OrderBy(x => x.ArticleId).Select(x => x.ArticleId).ToArray();
        }
        /// <summary>
        /// Get article pages (body only)
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>string[]</returns>
        public string[] GetArticlePages(int articleId)
        {
            var a = entities.mytrip_articles
                .Include("mytrip_articles2")
                .Where(x => x.SubArticleId == articleId)
                .OrderBy(x => x.ArticleId)
                .Select(x => x.Body).ToArray();
            return a;
        }
        #endregion

        #region Открыть Закрыть комментарии
        /// <summary>
        /// Открыть Закрыть комментарии
        /// </summary>
        /// <param name="articleId">articleId</param>
        public void OnOffComments(int articleId)
        {
            mytrip_articles a = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == articleId);
            if (a.ApprovedComment)
            {
                a.ApprovedComment = false;
                a.IncludeAnonymComment = false;
            }
            else
                a.ApprovedComment = true;
            entities.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить все статьи и посты не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца (RSS)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetByTag(string culture, int tagId)
        {
            var t = entities.mytrip_articlestag
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articles.mytrip_articlesvotes")
                .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Where(x => x.Culture.ToLower() == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            if (!ModuleSetting.articles())
                a = a.Where(x => x.mytrip_articlescategory.Blog == true);
            else if (!ModuleSetting.blogs())
                a = a.Where(x => x.mytrip_articlescategory.Blog == false);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате, с учетом сортировки из тега с конца постранично
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="sortBy">сортировка</param>
        /// <param name="total">колличество статей и постов</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetByTag(string culture, int tagId, int pageIndex, int pageSize, SortBy sortBy, out int total)
        {
            var t = entities.mytrip_articlestag
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                 .Include("mytrip_articles.mytrip_articlestag")
                .FirstOrDefault(x => x.TagId == tagId);
            var f = entities.mytrip_articlestag
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                 .Include("mytrip_articles.mytrip_articlescomments")
                .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Where(x => x.Culture.ToLower() == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            var b = f.mytrip_articles.Where(x => x.Culture.ToLower() == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            a = a.Union(b);

            if (!ModuleSetting.articles())
                a = a.Where(x => x.mytrip_articlescategory.Blog == true);
            else if (!ModuleSetting.blogs())
                a = a.Where(x => x.mytrip_articlescategory.Blog == false);
            total = a.Count();
            if (sortBy == SortBy.Views)
                return a.OrderByDescending(x => x.Views).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else if (sortBy == SortBy.TotalVotes)
                return a.OrderByDescending(x => x.TotalVotes).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return a.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить количество всех тегов
        /// <summary>
        /// Получить количество всех тегов        
        /// </summary>
        /// <returns></returns>
        public int GetTagsCount()
        {
            return entities.mytrip_articlestag.Count();
        }
        #endregion

        #region Получить количество всех статей и постов в теге
        /// <summary>
        /// Получить количество всех статей и постов в теге по культуре       
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public int GetInTagCount(string culture, int tagId)
        {
            var t = entities.mytrip_articlestag
               .Include("mytrip_articles")
               .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Count(x => x.Culture.ToLower() == culture && x.CloseDate >= DateTime.Now);
            return a;
        }
        #endregion

        /*  РАЗДЕЛ 5  */
        #region Получить тег по Id
        /// <summary>
        /// Получить тег по Id
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public mytrip_articlestag GetTag(int tagId)
        {
            return entities.mytrip_articlestag.Include("mytrip_articles").FirstOrDefault(x => x.TagId == tagId);
        }
        #endregion

        #region no cache
        /// <summary>Обновить пос
        /// </summary>
        /// <param name="id">номер поста</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        internal void UpdatePost(int id, string title, string description,
            string body, string imageForAbstract, bool onlyRegistered, bool moderateComments, bool commentVotes, string[] pages)
        {
            mytrip_articles a = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == id);
            mytrip_articlescategory cat = entities.mytrip_articlescategory.First(y => y.CategoryId == a.CategoryId);
            a.Title = title;
            a.Abstract = GeneralMethods.CleanHtmlCode(description);
            a.Body = GeneralMethods.CleanHtmlCode(body);
            a.ImageForAbstract = imageForAbstract;
            a.OnlyForRegisterUser = onlyRegistered;
            a.Path = GeneralMethods.DecodingString(title);
            a.Culture = cat.Culture;
            a.AllCulture = cat.AllCulture;
            a.ModerateComments = moderateComments;
            a.CommentVotes = commentVotes;
            foreach (var item in GetSubArticles(id))
            {
                entities.mytrip_articles.DeleteObject(item);
            }
            entities.SaveChanges();
            CreateSubArticles(a, pages);
            GeneralMethods.MytripCacheRemoveContains("as_getposts");
        }
        /// <summary>Создать пост
        /// </summary>
        /// <param name="categoryid">номер рубрики</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="imageForAbstract">фото в кратко</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="onlyRegistered">просмотр только зарегистрированным</param>
        /// <param name="moderateComments">moderate comments</param>
        /// <returns></returns>
        internal mytrip_articles CreatePost(int categoryId, string title, string description, string body, string imageForAbstract,
            bool onlyRegistered, bool includeAnonymComment, bool moderateComments, bool commentVotes, string[] pages)
        {
            CreateArticleZero();
            mytrip_articlescategory cat = entities.mytrip_articlescategory.First(y => y.CategoryId == categoryId);
            mytrip_articles p = new mytrip_articles
            {
                ArticleId = CreateArticleId(),
                CategoryId = categoryId,
                Title = title,
                Abstract = GeneralMethods.CleanHtmlCode(description),
                Body = GeneralMethods.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Views = 0,
                ApprovedComment = true,
                ImageForAbstract = imageForAbstract,
                Path = GeneralMethods.DecodingString(title),
                OnlyForRegisterUser = onlyRegistered,
                ApprovedVotes = true,
                TotalVotes = 0,
                IncludeAnonymComment = includeAnonymComment,
                CloseDate = DateTime.MaxValue,
                Culture = cat.Culture,
                AllCulture = cat.AllCulture,
                ModerateComments = moderateComments,
                CommentVotes = commentVotes
            };
            entities.mytrip_articles.AddObject(p);
            entities.SaveChanges();
            CreateSubArticles(p, pages);
            GeneralMethods.MytripCacheRemoveContains("as_getposts");
            return p;
        }
        /// <summary>Проголосовать за статью
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="vote">оценка</param>
        /// <returns></returns>
        internal decimal CreateVote(int articleId, int vote)
        {
            mytrip_articles article = entities.mytrip_articles.Include("mytrip_articlesvotes").FirstOrDefault(x => x.ArticleId == articleId);
            if (entities.mytrip_articlesvotes.Where(x => x.ArticleId == articleId)
                .FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name) == null)
            {
                mytrip_articlesvotes x = new mytrip_articlesvotes
                {
                    VotesId = CreateVotesId(),
                    ArticleId = articleId,
                    UserName = HttpContext.Current.User.Identity.Name,
                    Vote = vote
                };
                entities.mytrip_articlesvotes.AddObject(x);
                entities.SaveChanges();
                int countVotes = article.mytrip_articlesvotes.Count();
                int VotesSumm = 0;
                foreach (mytrip_articlesvotes v in article.mytrip_articlesvotes)
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
                GeneralMethods.MytripCacheRemoveContains("as_getarticles");
                return total;
            }
            else
                return article.TotalVotes;
        }
        /// <summary>Смена автора статьи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        internal void UpdateArtiсle(int id, string username)
        {
            mytrip_articles a = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == id);
            a.UserName = username;
            entities.SaveChanges();
        }
        /// <summary>Счетчик просмотров статьи
        /// </summary>
        /// <param name="id">номер статьи</param>
        internal void IncreaseViews(int id)
        {
            mytrip_articles a = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == id);
            if (a.SubArticleId == 0)
            {
                a.Views++;
                entities.SaveChanges();
                GeneralMethods.MytripCacheRemoveContains("as_getarticles");
                GeneralMethods.MytripCacheRemoveContains("as_getpostspopular");
            }
        }
        /// <summary>Обновить статью
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
        ///  <param name="moderateComments">модерация комментариев</param>
        /// <param name="commentVotes">оценка комментариев</param>
        internal void UpdateArtiсle(int id, int categoryId, string title, string description,
            string body, bool addComment, string imageForAbstract, bool onlyRegistered,
            bool approvedVotes, bool includeAnonymComment, DateTime closeDate, bool allCulture, bool moderateComments, bool commentVotes, string[] pages)
        {
            mytrip_articles a = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == id);
            a.CategoryId = categoryId;
            a.Title = title;
            a.Abstract = GeneralMethods.CleanHtmlCode(description);
            a.Body = GeneralMethods.CleanHtmlCode(body);
            a.ImageForAbstract = imageForAbstract;
            a.ApprovedComment = addComment;
            a.ApprovedVotes = approvedVotes;
            a.IncludeAnonymComment = includeAnonymComment;
            a.OnlyForRegisterUser = onlyRegistered;
            a.CloseDate = closeDate;
            a.Path = GeneralMethods.DecodingString(title);
            a.AllCulture = allCulture;
            a.ModerateComments = moderateComments;
            a.CommentVotes = commentVotes;
            foreach (var item in GetSubArticles(id))
            {
                entities.mytrip_articles.DeleteObject(item);
            }
            entities.SaveChanges();
            CreateSubArticles(a, pages);
            GeneralMethods.MytripCacheRemoveContains("as_getarticles");
        }
        /// <summary>Создать статью
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
        /// <param name="allCulture">show in all cultures</param>
        /// <param name="moderateComments">moderate comments</param>
        /// <param name="commentVotes">оценка комментариев</param>
        /// <returns></returns>
        internal mytrip_articles CreateArticle(int categoryId, string title, string description,
            string body, bool addComment, string imageForAbstract, bool onlyRegistered, bool approvedVotes,
            bool includeAnonymComment, DateTime closeDate, bool allCulture, bool moderateComments, bool commentVotes, string[] pages)
        {
            CreateArticleZero();
            mytrip_articlescategory category = entities.mytrip_articlescategory.Single(c => c.CategoryId == categoryId);
            if (!category.AllCulture)
                allCulture = false;
            mytrip_articles a = new mytrip_articles
            {
                ArticleId = CreateArticleId(),
                CategoryId = categoryId,
                Title = title,
                Abstract = GeneralMethods.CleanHtmlCode(description),
                Body = GeneralMethods.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Views = 0,
                ApprovedComment = addComment,
                ImageForAbstract = imageForAbstract,
                Path = GeneralMethods.DecodingString(title),
                OnlyForRegisterUser = onlyRegistered,
                ApprovedVotes = approvedVotes,
                TotalVotes = 0,
                IncludeAnonymComment = includeAnonymComment,
                CloseDate = closeDate,
                AllCulture = allCulture,
                Culture = category.Culture,
                ModerateComments = moderateComments,
                CommentVotes = commentVotes
            };
            entities.mytrip_articles.AddObject(a);
            entities.SaveChanges();
            CreateSubArticles(a, pages);
            GeneralMethods.MytripCacheRemoveContains("as_getarticles");
            return a;
        }
        /// <summary>Create subArticles
        /// </summary>
        /// <param name="a">Article</param>
        /// <param name="pages">array of pages (strings)</param>
        internal void CreateSubArticles(mytrip_articles a, string[] pages)
        {
            if (pages != null)
            {
                foreach (var p in pages)
                {
                    mytrip_articles s = new mytrip_articles
                    {
                        ArticleId = CreateArticleId(),
                        SubArticleId = a.ArticleId,
                        CategoryId = 0,
                        Title = a.Title,
                        Abstract = "",
                        Body = GeneralMethods.CleanHtmlCode(p),
                        CreateDate = a.CreateDate,
                        UserName = a.UserName,
                        Views = 0,
                        ApprovedComment = a.ApprovedComment,
                        ImageForAbstract = "",
                        Path = a.Path,
                        OnlyForRegisterUser = a.OnlyForRegisterUser,
                        ApprovedVotes = a.ApprovedVotes,
                        TotalVotes = 0,
                        IncludeAnonymComment = a.IncludeAnonymComment,
                        CloseDate = a.CloseDate,
                        AllCulture = a.AllCulture,
                        Culture = "none",
                        ModerateComments = a.ModerateComments,
                        CommentVotes = false
                    };
                    entities.mytrip_articles.AddObject(s);
                    entities.SaveChanges();
                }
            }
        }
        /// <summary>Удалить статью
        /// </summary>
        /// <param name="id">номер статьи</param>
        internal void DeleteArticle(int id)
        {
            mytrip_articles subs = entities.mytrip_articles
                   .Include("mytrip_articles1")
                   .Include("mytrip_articlessubscription")
                   .FirstOrDefault(x => x.ArticleId == id);
            foreach (var t in subs.mytrip_articlessubscription.ToList())
            {
                entities.mytrip_articlessubscription.DeleteObject(t);
            }
            foreach (var t in subs.mytrip_articles1.ToList())
            {
                entities.mytrip_articles.DeleteObject(t);
            }
            mytrip_articles article = entities.mytrip_articles
                .Include("mytrip_articlescomments.mytrip_commentvotes")
                .Include("mytrip_articlesvotes")
                .FirstOrDefault(x => x.ArticleId == id);
            foreach (var t in article.mytrip_articlessubscription.ToList())
            {
                entities.mytrip_articlessubscription.DeleteObject(t);
            }
            foreach (var t in article.mytrip_articlesvotes.ToList())
            {
                entities.mytrip_articlesvotes.DeleteObject(t);
            }
            foreach (var t in article.mytrip_articlescomments.ToList())
            {
                foreach (var v in t.mytrip_commentvotes.ToList())
                {
                    entities.mytrip_commentvotes.DeleteObject(v);
                }
                entities.mytrip_articlescomments.DeleteObject(t);
            }
            foreach (var t in article.mytrip_articlestag.ToList())
            {
                article.mytrip_articlestag.Remove(t);
            }
            entities.mytrip_articles.DeleteObject(article);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
            GeneralMethods.MytripCacheRemoveContains("as_getarticles");
            GeneralMethods.MytripCacheRemoveContains("as_getposts");
        }
        /// <summary>Создать тег
        /// </summary>
        /// <param name="name">название тега</param>
        /// <returns></returns>
        internal mytrip_articlestag CreateTag(string name)
        {
            if (entities.mytrip_articlestag.FirstOrDefault(x => x.TagName == name) != null)
                return entities.mytrip_articlestag.FirstOrDefault(x => x.TagName == name);
            mytrip_articlestag tag = new mytrip_articlestag
            {
                TagId = CreateTagId(),
                TagName = name,
                Path = "(Tag)" + GeneralMethods.DecodingString(name)
            };
            entities.mytrip_articlestag.AddObject(tag);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
            return tag;
        }
        /// <summary>Обновить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <param name="name">имя тега</param>
        internal void UpdateTag(int tagId, string name)
        {
            mytrip_articlestag t = GetTag(tagId);
            t.TagName = name;
            t.Path = "(Tag)" + GeneralMethods.DecodingString(name);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
        }
        /// <summary>Удалить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        internal void DeleteTag(mytrip_articlestag tag)
        {
            foreach (var article in tag.mytrip_articles.ToList())
            {
                article.mytrip_articlestag.Remove(tag);
            }
            entities.mytrip_articlestag.DeleteObject(tag);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
        }
        /// <summary>Добавить тег в статью
        /// </summary>
        ///<param name="articleId">номер статьи</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        internal void AddTagInArticle(int articleId, int tagId)
        {
            mytrip_articlestag mt = GetTag(tagId);
            mytrip_articles ma = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == articleId);
            if (mt.mytrip_articles.FirstOrDefault(x => x.ArticleId == articleId) != null)
                return;
            mt.mytrip_articles.Add(ma);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
        }
        /// <summary>Удалить тег из статьи
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="tagId">номер тега</param>
        internal void DeleteTagFromArticle(int articleId, int tagId)
        {
            mytrip_articlestag mt = GetTag(tagId);
            mytrip_articles ma = entities.mytrip_articles
                .FirstOrDefault(x => x.ArticleId == articleId);
            if (mt.mytrip_articles.FirstOrDefault(x => x.ArticleId == articleId) == null)
                return;
            mt.mytrip_articles.Remove(ma);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("as_getalltags");
        }
        #endregion

        /*  РАЗДЕЛ 6  */

        #region Получить колличество голосов по статье
        /// <summary>
        /// Получить колличество голосов по статье
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <returns></returns>
        public int GetVotesCount(int articleId)
        {
            return entities.mytrip_articlesvotes.Count(x => x.ArticleId == articleId);
        }
        #endregion

        #region private
        /// <summary>
        /// Получить статьи не закрытые по дате, по культуре с учетом сортировки, не входящие в меню, постранично (Индекс статей)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="sortBy">сортировка</param>
        /// <returns></returns>
        private IQueryable<mytrip_articles> _GetArticles(string culture, int pageIndex, int pageSize, SortBy sortBy, out int total)
        {
            //Проблема при сортировке по просмотрам не тянется mytrip_articlescategory2 на новом коннекторе проверить
            // а пока делаем через жопу
            if (sortBy == SortBy.CreateDate)
            {
                var ids = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.ArticleId);
                total = ids.Count();
                ids = ids.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                List<mytrip_articles> a = new List<mytrip_articles>();
                foreach (var id in ids.ToList())
                {
                    a.Add(entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2").Include("mytrip_articlestag").Include("mytrip_articlescomments")
                         .FirstOrDefault(x => x.ArticleId == id));
                }
                return a.AsQueryable();
            }
            else if (sortBy == SortBy.TotalVotes)
            {
                var ids = entities.mytrip_articles.Include("mytrip_articlescategory")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .OrderByDescending(x => x.TotalVotes).ThenByDescending(x => x.ApprovedVotes)
                .Select(x => x.ArticleId);
                total = ids.Count();
                ids = ids.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                List<mytrip_articles> a = new List<mytrip_articles>();
                foreach (var id in ids.ToList())
                {
                    a.Add(entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2").Include("mytrip_articlestag").Include("mytrip_articlescomments")
                         .FirstOrDefault(x => x.ArticleId == id));
                }
                return a.AsQueryable();
            }
            else
            {
                var ids = entities.mytrip_articles.Include("mytrip_articlescategory")
               .Where(x => x.Culture == culture || x.AllCulture == true)
               .Where(x => x.CloseDate >= DateTime.Now)
               .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
               .Where(x => x.mytrip_articlescategory.Blog == false)
               .OrderByDescending(x => x.Views)
               .Select(x => x.ArticleId);
                total = ids.Count();
                ids = ids.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                List<mytrip_articles> a = new List<mytrip_articles>();
                foreach (var id in ids.ToList())
                {
                    a.Add(entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2").Include("mytrip_articlestag").Include("mytrip_articlescomments")
                         .FirstOrDefault(x => x.ArticleId == id));
                }
                return a.AsQueryable();

                //var b = entities.mytrip_articles
                //.Include("mytrip_articlescategory")
                //.Include("mytrip_articlestag")
                //.Include("mytrip_articlescomments")
                //.Where(x => x.Culture == culture || x.AllCulture == true)
                //.Where(x => x.CloseDate >= DateTime.Now)
                //.Where(x => x.mytrip_articlescategory.SeparateBlock == false)
                //.Where(x => x.mytrip_articlescategory.Blog == false);
                //total = b.Count();
                //if (sortBy == SortBy.Views)
                //    return b.OrderByDescending(x => x.Views).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                //else if (sortBy == SortBy.TotalVotes)
                //    return b.OrderByDescending(x => x.TotalVotes).ThenByDescending(x => x.ApprovedVotes).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                //else
                //    return b.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        private IQueryable<mytrip_articles> _GetArticles(string culture, string search)
        {
            var ad = entities.mytrip_articles
                   .Include("mytrip_articlescomments").Include("mytrip_articlestag")
                   .Include("mytrip_articlescategory.mytrip_articlescategory2")
                   .Where(x => x.Culture == culture || x.AllCulture == true)
                   .Where(x => x.Title.Contains(search) || x.Abstract.Contains(search))
                   .Where(x => x.CloseDate >= DateTime.Now);
            return ad.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>Получить посты с конца (Homepage)
        /// </summary>
        /// <param name="take">колличество статей для выборки</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        private IQueryable<mytrip_articles> _GetPosts(string culture, int take)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Where(x => x.mytrip_articlescategory.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate).Skip(0).Take(take); ;
        }
        /// <summary>Получить статьи не входящие в меню и не закрытые по дате с конца (Homepage)
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        private IQueryable<mytrip_articles> _GetArticles(string culture, int take)
        {
            var b = entities.mytrip_articles
               .Include("mytrip_articlescategory")
               .Include("mytrip_articlestag")
                //.Include("mytrip_articlescategory.mytrip_articlescategory2")
               .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
               .Where(x => x.mytrip_articlescategory.Blog == false)
               .Where(x => x.mytrip_articlescategory.SubCategoryId == 0)
               .Where(x => x.Culture == culture || x.AllCulture == true)
               .Where(x => x.CloseDate >= DateTime.Now).OrderByDescending(x => x.CreateDate);
            var c = entities.mytrip_articles
                //.Include("mytrip_articlescategory")
                 .Include("mytrip_articlestag")
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                 .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
                 .Where(x => x.mytrip_articlescategory.Blog == false)
                 .Where(x => x.mytrip_articlescategory.SubCategoryId != 0)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).OrderByDescending(x => x.CreateDate);
            return b.Union(c).OrderByDescending(x => x.CreateDate).Skip(0).Take(take);

            //var b = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
            //    .Where(x => x.mytrip_articlescategory.SeparateBlock == false)
            //  .Where(x => x.mytrip_articlescategory.Blog == false)
            //  .Where(x => x.Culture == culture || x.AllCulture == true)
            //  .Where(x => x.CloseDate >= DateTime.Now);
            //return b.OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        /// <summary>Получить статьи не закрытые по дате из рубрики с конца (Homepage)
        /// Выбор статей из рубрики с учетом культуры (если рубрика мультиязычна)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        private IQueryable<mytrip_articles> _GetArticles(int categoryId, string culture, int take)
        {
            return entities.mytrip_articles
              .Include("mytrip_articlescategory.mytrip_articlescategory2")
              .Include("mytrip_articlesvotes")
              .Include("mytrip_articlestag")
              .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                  || x.mytrip_articlescategory.SubCategoryId == categoryId)
              .Where(x => x.AllCulture == true || x.Culture == culture)
              .Where(x => x.CloseDate >= DateTime.Now)
              .OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        private IQueryable<mytrip_articles> _GetPostsPopular(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.Views).Skip(0).Take(take);
        }
        /// <summary>Создать уникальный VotesId
        /// </summary>
        /// <returns>votesId</returns>
        private int CreateVotesId()
        {
            int votesId;
            for (votesId = 1; entities.mytrip_articlesvotes.Count(x => x.VotesId == votesId) != 0; votesId++) ;
            return votesId;
        }
        private IQueryable<mytrip_articles> _GetArticles(string culture, SortBy sortBy, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == false)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            if (sortBy == SortBy.Views)
                return b.OrderByDescending(x => x.Views).Skip(0).Take(take);
            else if (sortBy == SortBy.TotalVotes)
                return b.Where(x => x.ApprovedVotes == true).OrderByDescending(x => x.TotalVotes).Skip(0).Take(take);
            else
                return b.OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        /// <summary>Получить все теги        
        /// </summary>
        /// <returns></returns>
        private IQueryable<mytrip_articlestag> _GetAllTags()
        {
            return entities.mytrip_articlestag.Include("mytrip_articles").OrderBy(x => x.TagName);
        }
        /// <summary>Создать уникальный TagId
        /// </summary>
        /// <returns>TagId</returns>
        private int CreateTagId()
        {
            int tagId;
            for (tagId = 1; entities.mytrip_articlestag.Count(x => x.TagId == tagId) != 0; tagId++) ;
            return tagId;
        }
        /// <summary>Создать уникальный ArticleId 
        /// </summary>
        /// <returns></returns>
        private int CreateArticleId()
        {
            int articleId;
            for (articleId = 1; entities.mytrip_articles.Count(x => x.ArticleId == articleId) != 0; articleId++) ;
            return articleId;
        }
        /// <summary>Create article ZERO
        /// </summary>
        private void CreateArticleZero()
        {
            mytrip_articles zero = entities.mytrip_articles.FirstOrDefault(x => x.ArticleId == 0);
            if (zero == null)
            {
                mytrip_articles x = new mytrip_articles
                {
                    ArticleId = 0,
                    SubArticleId = 0,
                    CategoryId = 0,
                    Title = "zero",
                    Abstract = "zero",
                    Body = "zero",
                    Path = "zero",
                    CreateDate = DateTime.Now,
                    CloseDate = DateTime.Now,
                    UserName = "mytripmvc",
                    ApprovedComment = false,
                    ApprovedVotes = false,
                    IncludeAnonymComment = false,
                    ModerateComments = false,
                    OnlyForRegisterUser = false,
                    Views = 0,
                    TotalVotes = 0,
                    AllCulture = false,
                    Culture = "zero",
                };
                entities.mytrip_articles.AddObject(x);
                entities.SaveChanges();
            }
        }
        #endregion

    }
}
