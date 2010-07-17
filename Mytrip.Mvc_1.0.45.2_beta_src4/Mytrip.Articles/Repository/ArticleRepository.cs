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
using Mytrip.Mvc;
using System.Web.Caching;
using Mytrip.Articles.Repository.DataEntities;
using System.Data;

namespace Mytrip.Articles.Repository
{
    public class ArticleRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к Entity Репозиторию
        ArticlesSetting _core;
        public ArticlesSetting core
        {
            get
            {
                if (_core == null)
                    _core = new ArticlesSetting();
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

        /*  РАЗДЕЛ 2  */

        #region Получить статьи не закрытые по дате (постранично включительно)
        ///<summary>
        /// Получить статьи не входящие в меню и не закрытые по дате с конца 
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
        /// Получить статьи не входящие в меню и не закрытые по дате с конца
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string culture, int take)
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
        }
        /// <summary>
        /// Получить статьи не закрытые по дате с конца не входящие в меню постранично
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string culture, int pageIndex, int pageSize, out int total)
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
            var d = b.Union(c);
            total = d.Count();
            return d.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате по поиску постранично
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="search"></param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="total">общее количество статей</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(string culture, string search, int pageIndex, int pageSize, out int total)
        {
            var ad = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                   .Include("mytrip_articlescomments").Include("mytrip_articlestag")
                   .Where(x => x.Culture == culture || x.AllCulture == true)
                   .Where(x => x.Title.Contains(search) || x.Abstract.Contains(search))
                   .Where(x => x.CloseDate >= DateTime.Now);
            if (!core.articles())
            {
                ad = ad.Where(x => x.mytrip_articlescategory.Blog == true);
            }
            else if (!core.blogs())
            {
                ad = ad.Where(x => x.mytrip_articlescategory.Blog == false);
            }
            total = ad.Count();
            return ad.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(int categoryId)
        {
            return entities.mytrip_articles
                .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                    || x.mytrip_articlescategory.SubCategoryId == categoryId)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(int categoryId, int take)
        {
            return entities.mytrip_articles
              .Include("mytrip_articlescategory")
              .Include("mytrip_articlesvotes")
              .Include("mytrip_articlestag")
              .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                  || x.mytrip_articlescategory.SubCategoryId == categoryId)
              .Where(x => x.CloseDate >= DateTime.Now)
              .OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        public IQueryable<mytrip_articles> GetArticles(int categoryId, string culture, int take)
        {
            return entities.mytrip_articles
              .Include("mytrip_articlescategory")
              .Include("mytrip_articlescategory.mytrip_articlescategory2")
              .Include("mytrip_articlesvotes")
              .Include("mytrip_articlestag")
              .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                  || x.mytrip_articlescategory.SubCategoryId == categoryId)
                  .Where(x => x.AllCulture == true || x.Culture == culture)
              .Where(x => x.CloseDate >= DateTime.Now)
              .OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        /// <summary>
        /// Получить статьи не закрытые по дате из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticles(int categoryId, string culture, int pageIndex, int pageSize, out int total)
        {
            var a = entities.mytrip_articles
                .Include("mytrip_articlescategory")
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Where(x => x.mytrip_articlescategory.CategoryId == categoryId
                    || x.mytrip_articlescategory.SubCategoryId == categoryId)
                .Where(x => x.AllCulture == true || x.Culture == culture)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.CreateDate);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить популярные статьи не закрытые по просмотрам
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesPopular(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == false)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.Views).Skip(0).Take(take);
        }
        /// <summary>
        /// Получить популярные статьи не закрытые по оценкам
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesRated(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == false)
                 .Where(x => x.ApprovedVotes == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.TotalVotes).Skip(0).Take(take);
        }
        /// <summary>
        /// Получить последние статьи не закрытые
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns>IQueryable<mytrip_articles></returns>
        public IQueryable<mytrip_articles> GetArticlesRecent(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == false)
                 .Where(x => x.ApprovedVotes == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.CreateDate).Skip(0).Take(take);
        }
        /// <summary>
        /// Get related articles
        /// </summary>
        /// <param name="culture">current culture</param>
        /// <param name="isBlog">is blog</param>
        /// <param name="title">title to find</param>
        /// <param name="take">count of items to return</param>
        /// <returns>List<mytrip_articles></returns>
        public List<mytrip_articles> GetArticlesSimilar(string culture, bool isBlog, string title, int take)
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
                similar=similar.Distinct().ToList();
                if (similar.Count > take)
                    break;
            }
            return similar.Take(take).ToList();
        }
        #endregion

        #region Получить статьи закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить статьи закрытые по дате с конца Х->1
        /// </summary>
        /// <param name="count">количество статей</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesClosed(int count)
        {
            return entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
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
                .Where(x => x.CloseDate < DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить колличество статей
        /// <summary>
        /// Получить колличество всех статей по культуре        
        /// </summary>
        /// <returns></returns>
        public int GetArticlesCount(string culture)
        {
            return entities.mytrip_articles.Count(x => x.Culture == culture
                && !x.mytrip_articlescategory.Blog
                && x.CloseDate >= DateTime.Now);
        }
        /// <summary>
        /// Получить колличество всех статей закрытых по дате по культуре
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public int GetArticlesClosedCount(string culture)
        {
            return entities.mytrip_articles.Count(x => x.CloseDate < DateTime.Now && x.Culture == culture);
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
            return entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => !x.mytrip_articlescategory.Blog)
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
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => !x.mytrip_articlescategory.Blog)
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
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog == false)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все посты  (постранично включительно)
        /// <summary>
        /// Получить все посты с конца Х->1
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
        /// <summary>
        /// Получить посты с конца
        /// </summary>
        /// <param name="take">колличество статей для выборки</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPosts(int take, string culture)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Where(x => x.mytrip_articlescategory.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate).Skip(0).Take(take); ;
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
            return entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog)
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
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog)
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
            var a = entities.mytrip_articles.Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.mytrip_articlescategory.Blog)
                .Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.CreateDate < eDate)
                .Where(x => x.CreateDate > sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить популярные посты не закрытые по просмотрам
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="take">колличество статей для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPostsPopular(string culture, int take)
        {
            var b = entities.mytrip_articles.Include("mytrip_articlescategory")
                 .Where(x => x.mytrip_articlescategory.Blog == true)
                 .Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now);
            return b.OrderByDescending(x => x.Views).Skip(0).Take(take);
        }
        /// <summary>
        /// Получить популярные статьи не закрытые по оценкам
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
        /// <summary>
        /// Получить последние статьи не закрытые
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

        #region Получить количество всех постов по культуре
        /// <summary>
        /// Получить количество всех постов по культуре        
        /// </summary>
        /// <returns></returns>
        public int GetPostsCount(string culture)
        {
            return entities.mytrip_articles.Count(x => x.Culture == culture && x.mytrip_articlescategory.Blog);
        }
        #endregion

        #region Получить статьи посты пользователя
        /// <summary>
        /// Получить статьи пользователя
        /// </summary>
        /// <param name="username">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesByUser(string username)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articlescomments")
                .Where(x => x.UserName == username && x.mytrip_articlescategory.Blog == false)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все посты пользователя
        /// </summary>
        /// <param name="username">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetPostsByUser(string username)
        {
            return entities.mytrip_articles
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articlescomments")
                .Where(x => x.UserName == username && x.mytrip_articlescategory.Blog == true)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить статью по Id
        /// <summary>
        /// Получить статью по Id
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <returns></returns>
        public mytrip_articles GetArticleById(int articleId)
        {
            var a = entities.mytrip_articles
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlestag")
                .Include("mytrip_articlescategory.mytrip_articlescategory2")
                .FirstOrDefault(x => x.ArticleId == articleId);
            return a;
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
        public mytrip_articles CreateArticle(int categoryId, string title, string description,
            string body, bool addComment, string imageForAbstract, bool onlyRegistered, bool approvedVotes,
            bool includeAnonymComment, DateTime closeDate, bool allCulture)
        {
            mytrip_articlescategory category = entities.mytrip_articlescategory.Single(c => c.CategoryId == categoryId);
            if (!category.AllCulture)
                allCulture = false;
            mytrip_articles x = new mytrip_articles
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
                Culture = category.Culture
            };
            entities.mytrip_articles.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        //_Создать уникальный ArticleId
        private int CreateArticleId()
        {
            int articleId;
            for (articleId = 1; GetArticleById(articleId) != null; articleId++) ;
            return articleId;
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
            if (!entities.mytrip_articlescategory.Single(c => c.CategoryId == categoryId).AllCulture)
                allCulture = false;
            mytrip_articles x = GetArticleById(id);
            x.CategoryId = categoryId;
            x.Title = title;
            x.Abstract = GeneralMethods.CleanHtmlCode(description);
            x.Body = GeneralMethods.CleanHtmlCode(body);
            x.ImageForAbstract = imageForAbstract;
            x.ApprovedComment = addComment;
            x.ApprovedVotes = approvedVotes;
            x.IncludeAnonymComment = includeAnonymComment;
            x.OnlyForRegisterUser = onlyRegistered;
            x.CloseDate = closeDate;
            x.Path = GeneralMethods.DecodingString(title);
            x.AllCulture = allCulture;
            entities.SaveChanges();
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
        public mytrip_articles CreatePost(int categoryId, string title, string description,
            string body, string imageForAbstract, bool onlyRegistered, bool includeAnonymComment)
        {
            mytrip_articlescategory cat = entities.mytrip_articlescategory.First(y => y.CategoryId == categoryId);
            mytrip_articles x = new mytrip_articles
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
                AllCulture = cat.AllCulture
            };
            entities.mytrip_articles.AddObject(x);
            entities.SaveChanges();
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
            mytrip_articles a = GetArticleById(id);
            mytrip_articlescategory cat = entities.mytrip_articlescategory.First(y => y.CategoryId == a.CategoryId);
            a.Title = title;
            a.Abstract = GeneralMethods.CleanHtmlCode(description);
            a.Body = GeneralMethods.CleanHtmlCode(body);
            a.ImageForAbstract = imageForAbstract;
            a.OnlyForRegisterUser = onlyRegistered;
            a.Path = GeneralMethods.DecodingString(title);
            a.Culture = cat.Culture;
            a.AllCulture = cat.AllCulture;
            entities.SaveChanges();
        }
        #endregion

        #region Удалить статью
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void DeleteArticle(int id)
        {
            mytrip_articles article = entities.mytrip_articles
                .Include("mytrip_articlescomments")
                .Include("mytrip_articlesvotes")
                .FirstOrDefault(x => x.ArticleId == id);
            foreach (var t in article.mytrip_articlesvotes.ToList())
            {
                entities.mytrip_articlesvotes.DeleteObject(t);
            }
            foreach (var t in article.mytrip_articlescomments.ToList())
            {
                entities.mytrip_articlescomments.DeleteObject(t);
            }
            foreach (var t in article.mytrip_articlestag.ToList())
            {
                article.mytrip_articlestag.Remove(t);
            }
            entities.mytrip_articles.DeleteObject(article);
            entities.SaveChanges();
        }
        #endregion

        #region Счетчик просмотров статьи
        /// <summary>
        /// Счетчик просмотров статьи
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void IncreaseViews(int id)
        {
            mytrip_articles x = GetArticleById(id);
            x.Views++;
            entities.SaveChanges();
        }
        #endregion

        #region Открыть Закрыть комментарии
        /// <summary>
        /// Открыть комментарии
        /// </summary>
        /// <param name="articleId">mt_article_comment</param>
        public void OpenComments(int articleId)
        {
            mytrip_articles a = GetArticleById(articleId);
            a.ApprovedComment = true;
            entities.SaveChanges();
        }
        /// <summary>
        /// Закрыть комментарии
        /// </summary>
        /// <param name="articleId">mt_article_comment</param>
        public void CloseComments(int articleId)
        {
            mytrip_articles a = GetArticleById(articleId);
            a.ApprovedComment = false;
            a.IncludeAnonymComment = false;
            entities.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить все статьи и посты не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesPostsByTag(string culture, int tagId)
        {
            var t = entities.mytrip_articlestag
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articles.mytrip_articlesvotes")
                .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            if (!core.articles())
            {
                a = a.Where(x => x.mytrip_articlescategory.Blog == true);
            }
            else if (!core.blogs())
            {
                a = a.Where(x => x.mytrip_articlescategory.Blog == false);
            }
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца постранично
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="total">колличество статей и постов</param>
        /// <returns></returns>
        public IQueryable<mytrip_articles> GetArticlesPostsByTag(string culture, int tagId, int pageIndex, int pageSize, out int total)
        {
            var t = entities.mytrip_articlestag
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Include("mytrip_articles.mytrip_articlesvotes")
                .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Where(x => x.Culture == culture || x.AllCulture == true)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            if (!core.articles())
            {
                a = a.Where(x => x.mytrip_articlescategory.Blog == true);
            }
            else if (!core.blogs())
            {
                a = a.Where(x => x.mytrip_articlescategory.Blog == false);
            }
            total = a.Count();
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

        #region Получить количество всех постов в теге
        /// <summary>
        /// Получить количество всех постов в теге        
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="tagId">номер тега</param>
        /// <returns></returns>
        public int GetArticlesInTagCount(string culture, int tagId)
        {
            //fix for "There is already an open DataReader associated with this Command"
            //if (entities.Connection.State == ConnectionState.Open)
            //{
            //    entities.Connection.Close();
            //    entities.Connection.Open();
            //}
            var t = entities.mytrip_articlestag
               .Include("mytrip_articles")
               .FirstOrDefault(x => x.TagId == tagId);
            var a = t.mytrip_articles.Where(x => x.Culture.ToLower() == culture)
                 .Where(x => x.CloseDate >= DateTime.Now).AsQueryable();
            int s = a.Count();
            return a.Count();
        }
        #endregion

        /*  РАЗДЕЛ 5  */

        #region Получить все теги
        /// <summary>
        /// Получить все теги        
        /// </summary>
        /// <returns></returns>
        private IQueryable<mytrip_articlestag> _GetAllTags()
        {
            return entities.mytrip_articlestag.Include("mytrip_articles").OrderBy(x => x.TagName);
        }
        /// <summary>
        /// Получить все теги
        /// </summary>
        /// <param name="cache">использовать кэш</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlestag> GetAllTags(bool cache)
        {
            if (cache)
            {
                return GeneralMethods.GetObjectFromCache<IQueryable<mytrip_articlestag>>(
                   "GetAllTags",
                   () => _GetAllTags(),
                   (c, k, o) => c.Add(k, o, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(core.cacheSeconds()), CacheItemPriority.Normal, null)
                );
            }
            else
                return _GetAllTags();
        }
        #endregion

        #region Создать тег
        /// <summary>
        /// Создать тег
        /// </summary>
        /// <param name="name">название тега</param>
        /// <returns></returns>
        public mytrip_articlestag CreateTag(string name)
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
            return tag;
        }
        //_Создать уникальный TagId
        private int CreateTagId()
        {
            int tagId;
            for (tagId = 1; GetTag(tagId) != null; tagId++) ;
            return tagId;
        }
        #endregion

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

        #region Обновить тег
        /// <summary>
        ///  Обновить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        /// <param name="name">имя тега</param>
        public void UpdateTag(int tagId, string name)
        {
            mytrip_articlestag t = GetTag(tagId);
            t.TagName = name;
            t.Path = "(Tag)" + GeneralMethods.DecodingString(name);
            entities.SaveChanges();
        }
        #endregion

        #region Удалить teg
        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="tagId">номер тега</param>
        public void DeleteTag(mytrip_articlestag tag)
        {
            foreach (var article in tag.mytrip_articles.ToList())
            {
                article.mytrip_articlestag.Remove(tag);
            }
            entities.mytrip_articlestag.DeleteObject(tag);
            entities.SaveChanges();
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
            mytrip_articlestag mt = GetTag(tagId);
            mytrip_articles ma = GetArticleById(articleId);
            if (mt.mytrip_articles.FirstOrDefault(x => x.ArticleId == articleId) != null)
                return;
            mt.mytrip_articles.Add(ma);
            entities.SaveChanges();
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
            mytrip_articlestag mt = GetTag(tagId);
            mytrip_articles ma = GetArticleById(articleId);
            if (mt.mytrip_articles.FirstOrDefault(x => x.ArticleId == articleId) == null)
                return;
            mt.mytrip_articles.Remove(ma);
            entities.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 6  */

        #region Создать голосование для статьи
        /// <summary>
        /// Создать голосование для статьи
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="vote">оценка</param>
        /// <returns></returns>
        public decimal CreateVote(int articleId, int vote)
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
                return total;
            }
            else
                return article.TotalVotes;
        }
        // Создать уникальный VotesId
        private int CreateVotesId()
        {
            int VotesId;
            for (VotesId = 1; GetVote(VotesId) != null; VotesId++) ;
            return VotesId;
        }
        /// <summary>
        /// Получить один голос
        /// </summary>
        /// <param name="votesId">номер голоса</param>
        /// <returns></returns>
        public mytrip_articlesvotes GetVote(int votesId)
        {
            return entities.mytrip_articlesvotes.FirstOrDefault(x => x.VotesId == votesId);
        }
        #endregion

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

    }
}
