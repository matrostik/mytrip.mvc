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
using System.Web;
using Mytrip.Mvc.Repository;
using Mytrip.Articles.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Articles.Repository
{
    public class CategoryRepository
    {
        /*  РАЗДЕЛ 1  */

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

        /*  РАЗДЕЛ 2  */

        #region Получить все рубрики статей (постранично включительно)
        /// <summary>
        /// Получить все рубрики статей (в меню или недобавленные в меню) с начала 1->X
        /// </summary>
        /// <param name="forMenu">для меню или нет</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetCategories(bool forMenu, string culture)
        {
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory1")
                .Where(x => x.CategoryId != 0)
                .Where(x => !x.Blog)
                .Where(x => x.SeparateBlock == forMenu)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true);
            return a.OrderBy(x => x.CategoryId);
        }
        /// <summary>
        ///  Получить все рубрики статей (в меню или недобавленные в меню)
        /// </summary>
        /// <param name="forMenu">для меню или нет</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetCategories(bool forMenu)
        {
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory1")
                .Where(x => x.CategoryId != 0)
                .Where(x => !x.Blog)
                .Where(x => x.SeparateBlock == forMenu)
                .Where(x => x.SubCategoryId == 0);
            return a.OrderBy(x => x.CategoryId);
        }
        /// <summary>
        /// Получить все рубрики и подрубрики статей для выпадающего меню
        /// </summary>
        /// <param name="culture">Culture</param>
        /// <returns></returns>
        public List<mytrip_articlescategory> GetCategoriesForDdl(string culture)
        {
            List<mytrip_articlescategory> mcats = new List<mytrip_articlescategory>();
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory1")
           .Where(x => x.CategoryId != 0 && x.SubCategoryId == 0)
           .Where(x => !x.Blog)
           .Where(x => x.Culture == culture || x.AllCulture == true)
           .OrderBy(x => x.SeparateBlock);
            foreach (mytrip_articlescategory cat in a)
            {
                mcats.Add(cat);
                foreach (mytrip_articlescategory subcat in cat.mytrip_articlescategory1)
                {
                    subcat.Title = "--" + subcat.Title;
                    mcats.Add(subcat);
                }
            }
            return mcats;
        }
        /// <summary>
        /// Получить все подрубрики/темы из рубрики/блога для выпадающего меню
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <param name="culture">Culture</param>
        /// <returns></returns>
        public List<mytrip_articlescategory> GetCategoriesForDdl(int categoryId, string culture)
        {
            List<mytrip_articlescategory> mcats = new List<mytrip_articlescategory>();
            mytrip_articlescategory category = entities.mytrip_articlescategory.Include("mytrip_articlescategory2")
                .Include("mytrip_articlescategory1")
                .SingleOrDefault(x => x.CategoryId == categoryId);
            if (category.SubCategoryId != 0)
            {
                mcats.Add(category.mytrip_articlescategory2);
                category = entities.mytrip_articlescategory.Include("mytrip_articlescategory1")
                    .SingleOrDefault(x => x.CategoryId == category.mytrip_articlescategory2.CategoryId);
            }
            else
                mcats.Add(category);
            foreach (mytrip_articlescategory subcat in category.mytrip_articlescategory1)
            {
                subcat.Title = "--" + subcat.Title;
                mcats.Add(subcat);
            }
            return mcats;
        }
        #endregion

        #region Получить все подрубрики из рубрики
        /// <summary>
        /// Получить все подрубрики из рубрики
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetSubCategories(int categoryId)
        {
            return entities.mytrip_articlescategory.Include("mytrip_articles")
                .Where(x => x.SubCategoryId == categoryId);
        }
        #endregion

        #region Получить количество всех рубрик и подрубрик
        /// <summary>
        /// Получить количество всех рубрик и подрубрик
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public int GetCategoriesCount(string culture)
        {
            return entities.mytrip_articlescategory
                .Count(x => !x.Blog && x.Culture == culture && x.CategoryId != 0);
        }
        #endregion

        #region Получить последние добавленные рубрики или подрубрики
        /// <summary>
        /// Получить последние добавленные рубрики или подрубрики
        /// </summary>
        /// <param name="count">количество рубрик или подрубрик</param>
        /// <returns>IQueryable(mytrip_articlescategory)</returns>
        public IQueryable<mytrip_articlescategory> GetLastCategories(int count)
        {
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory2")
                .Where(x => x.CategoryId != 0)
                .Where(x => !x.Blog);
            return a.OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить рубрики или подрубрики добавленные определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns>IQueryable(mytrip_articlescategory)</returns>
        public IQueryable<mytrip_articlescategory> GetLastCategories(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articlescategory
                .Where(x => x.CategoryId != 0)
                .Where(x => !x.Blog)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить рубрики или подрубрики добавленные в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastCategories(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articlescategory
                .Where(x => x.CategoryId != 0)
                .Where(x => !x.Blog)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все блоги (постранично включительно)
        /// <summary>
        /// Получить все блоги по просмотрам
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetBlogs(string culture)
        {
            return entities.mytrip_articlescategory
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.Views);
        }
        /// <summary>
        /// Получить все блоги по просмотрам постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetBlogs(int pageIndex, int pageSize, string culture, out int total)
        {
            var a = entities.mytrip_articlescategory.Include("mytrip_articles").Include("mytrip_articlescategory1.mytrip_articles")
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.Views);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить количество всех блогов
        /// <summary>
        /// Получить количество всех блогов
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public int GetBlogsCount(string culture)
        {
            return entities.mytrip_articlescategory.Count(x => x.Blog && x.SubCategoryId == 0 && x.Culture == culture);
        }
        #endregion

        #region Получить последние добавленные блоги
        /// <summary>
        /// Получить последние добавленные блоги
        /// </summary>
        /// <param name="count">количество блогов</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastBlogs(int count)
        {
            return entities.mytrip_articlescategory
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId == 0)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить блоги добавленные определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastBlogs(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articlescategory
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить блоги добавленные в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastBlogs(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articlescategory
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить количество всех тем в блогах
        /// <summary>
        /// Получить количество всех тем в блогах
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public int GetTopicsCount(string culture)
        {
            var a = entities.mytrip_articlescategory
                .Count(x => x.Blog && x.SubCategoryId != 0 && x.Culture == culture && x.CategoryId != 0);
            return a;
        }
        #endregion

        #region Получить последние добавленные темы в блогах
        /// <summary>
        /// Получить последние добавленные темы в блогах
        /// </summary>
        /// <param name="count">количество блогов</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastTopics(int count)
        {
            return entities.mytrip_articlescategory.Include("mytrip_articlescategory2")
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId != 0)
                .Where(x => x.CategoryId != 0)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить темы в блогах добавленные определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastTopics(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory2")
                .Where(x => x.Blog)
                .Where(x => x.SubCategoryId != 0)
                .Where(x => x.CategoryId != 0)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить темы в блогах добавленные в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetLastTopics(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articlescategory.Include("mytrip_articlescategory2")
                .Where(x => x.Blog)
                .Where(x => x.CategoryId != 0)
                .Where(x => x.SubCategoryId != 0)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить одну рубрику по Id
        /// <summary>
        /// Получить одну рубрику по Id
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public mytrip_articlescategory GetCategory(int categoryId)
        {
            return entities.mytrip_articlescategory
                .Include("mytrip_articles")
                .Include("mytrip_articlescategory2")
                .Include("mytrip_articlescategory1.mytrip_articles")
                .SingleOrDefault(x => x.CategoryId == categoryId);
        }
        #endregion

        #region _Создать уникальный CategoryId
        private int CreateCategoryId()
        {
            int catId;
            for (catId = 1; entities.mytrip_articlescategory.Count(x => x.CategoryId == catId) != 0; catId++) ;
            return catId;
        }
        #endregion

        #region Создать рубрику
        /// <summary>
        /// Создать рубрику
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="email">email пользователя</param>
        /// <param name="separateBlock">добавить в меню</param>
        /// <param name="allCulture">доступна во всех языках</param>
        /// <param name="culture">культура</param>
        /// <returns></returns>
        public mytrip_articlescategory CreateСategory(string title, bool separateBlock, bool allCulture, string culture)
        {
            CreateСategoryZero();
            int categoryId = CreateCategoryId();
            mytrip_articlescategory x = new mytrip_articlescategory
            {
                CategoryId = categoryId,
                Title = title,
                Path = GeneralMethods.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                UserEmail = MytripUser.UserEmail(),
                SeparateBlock = separateBlock,
                Blog = false,
                Views = 0,
                SubCategoryId = 0,
                AllCulture = allCulture,
                Culture = culture
            };
            entities.mytrip_articlescategory.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        #endregion

        #region Создать рубрику ZERO
        /// <summary>
        /// Создать рубрику ZERO
        /// </summary>
        public void CreateСategoryZero()
        {
            mytrip_articlescategory zero = entities.mytrip_articlescategory.FirstOrDefault(x => x.CategoryId == 0);
            if (zero == null)
            {
                mytrip_articlescategory x = new mytrip_articlescategory
                {
                    CategoryId = 0,
                    Title = "zero",
                    Path = "zero",
                    CreateDate = DateTime.Now,
                    UserName = "mytripmvc",
                    UserEmail = "mytripmvc@mytripmvc.com",
                    SeparateBlock = false,
                    Blog = false,
                    Views = 0,
                    SubCategoryId = 0,
                    AllCulture = false,
                    Culture = "zero"
                };
                entities.mytrip_articlescategory.AddObject(x);
                entities.SaveChanges();
            }
        }
        #endregion

        #region Создать подрубрику
        /// <summary>
        /// Создать подрубрику
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="email">email пользователя</param>
        /// <returns></returns>
        public mytrip_articlescategory CreateSubCategory(string title, int categoryId, bool allCulture)
        {
            var cat = entities.mytrip_articlescategory.SingleOrDefault(x => x.CategoryId == categoryId);
            mytrip_articlescategory subcat = new mytrip_articlescategory
            {
                CategoryId = CreateCategoryId(),
                Title = title,
                Path = GeneralMethods.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                UserEmail = MytripUser.UserEmail(),
                SeparateBlock = cat.SeparateBlock,
                Blog = cat.Blog,
                Views = 0,
                SubCategoryId = categoryId,
                Culture = cat.Culture,
                AllCulture = allCulture
            };
            entities.mytrip_articlescategory.AddObject(subcat);
            entities.SaveChanges();
            return subcat;
        }
        #endregion

        #region Создать блог
        /// <summary>
        /// Создать блог
        /// </summary>
        /// <param name="email">email пользователя</param>
        /// <param name="culture">культура</param>
        /// <returns></returns>
        public mytrip_articlescategory CreateBlog(string culture)
        {
            CreateСategoryZero();
            int categoryId = CreateCategoryId();
            string title = HttpContext.Current.User.Identity.Name + "(" + ArticleLanguage.blog + ")";
            mytrip_articlescategory x = new mytrip_articlescategory
            {
                CategoryId = categoryId,
                Title = title,
                Path = GeneralMethods.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                UserEmail = MytripUser.UserEmail(),
                SeparateBlock = false,
                Blog = true,
                Views = 0,
                SubCategoryId = 0,
                AllCulture = false,
                Culture = culture
            };
            entities.mytrip_articlescategory.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        #endregion

        #region Обновить рубрику
        /// <summary>
        /// Обновить рубрику
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="title">название</param>
        /// <param name="separateBlock">добавить в меню</param>
        public void UpdateCategory(int categoryId, string title, bool separateBlock, bool allCulture)
        {
            mytrip_articlescategory c = GetCategory(categoryId);
            if (c.SubCategoryId == 0 && c.AllCulture != allCulture && c.AllCulture == true)
            {
                foreach (var art in c.mytrip_articles)
                {
                    art.AllCulture = false;
                }
                foreach (mytrip_articlescategory cat in c.mytrip_articlescategory1)
                {
                    cat.AllCulture = false;
                    foreach (var article in cat.mytrip_articles)
                    {
                        article.AllCulture = false;
                    }
                }
            }
            if (c.SubCategoryId == 0 && c.SeparateBlock != separateBlock)
            {
                foreach (mytrip_articlescategory cat in c.mytrip_articlescategory1)
                {
                    cat.SeparateBlock = separateBlock;
                }
            }
            c.Title = title;
            c.Path = GeneralMethods.DecodingString(title);
            c.SeparateBlock = separateBlock;
            c.AllCulture = allCulture;
            entities.SaveChanges();
        }
        #endregion

        #region Обновить блог
        /// <summary>
        /// Обновить блог
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <param name="title">название</param>
        public void UpdateBlog(int categoryId, string title)
        {
            mytrip_articlescategory c = entities.mytrip_articlescategory.SingleOrDefault(x => x.CategoryId == categoryId);
            c.Title = title;
            c.Path = GeneralMethods.DecodingString(title);
            entities.SaveChanges();
        }
        #endregion

        #region Удалить рубрику
        /// <summary>
        /// Удалить рубрику (удаляет все подрубрики и статьи)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        public void DeleteCategory(int categoryId)
        {
            List<int> ids = entities.mytrip_articles
                .Where(x => x.mytrip_articlescategory.SubCategoryId == categoryId||x.mytrip_articlescategory.CategoryId == categoryId)
                .Select(x=>x.ArticleId).ToList();
            foreach (int id in ids)
            {
                foreach (var a in entities.mytrip_articles.Where(x => x.SubArticleId == id))
                {
                    entities.mytrip_articles.DeleteObject(a);
                }
                foreach (var v in entities.mytrip_articlesvotes.Where(x => x.ArticleId == id))
                {
                    entities.mytrip_articlesvotes.DeleteObject(v);
                }
                foreach (var v in entities.mytrip_articlescomments.Where(x => x.ArticleId == id))
                {
                    entities.mytrip_articlescomments.DeleteObject(v);
                }
                foreach (var v in entities.mytrip_articlessubscription.Where(x=>x.ArticleId==id))
                {
                    entities.mytrip_articlessubscription.DeleteObject(v);
                }
                mytrip_articles article = entities.mytrip_articles
                    .Include("mytrip_articlestag")
              .FirstOrDefault(x => x.ArticleId == id);
                foreach (var t in article.mytrip_articlestag.ToList())
                {
                    article.mytrip_articlestag.Remove(t);
                }
                entities.mytrip_articles.DeleteObject(article);
            }
            var cats = entities.mytrip_articlescategory
                .Where(x => x.SubCategoryId == categoryId);
            foreach (var cat in cats)
            {
                entities.mytrip_articlescategory.DeleteObject(cat);
            }
            entities.SaveChanges();

            entities.mytrip_articlescategory.DeleteObject(entities.mytrip_articlescategory.FirstOrDefault(x => x.CategoryId == categoryId));

            entities.SaveChanges();
        }
        #endregion

        #region Счетчик просмотров блога
        /// <summary>
        /// Счетчик просмотров блога
        /// </summary>
        /// <param name="categoryId">номер блога</param>
        public void BlogViewsIncrease(int categoryId)
        {
            mytrip_articlescategory cat = entities.mytrip_articlescategory.SingleOrDefault(x => x.CategoryId == categoryId);
            cat.Views++;
            entities.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить все рубрики и подрубки пользователя
        /// <summary>
        /// Получить все рубрики и подрубки пользователя
        /// </summary>
        /// <param name="username">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetCategoriesByUser(string username)
        {
            return entities.mytrip_articlescategory
                .Include("mytrip_articlescategory2")
                .Where(x => x.CategoryId != 0)
                .Where(x => x.Blog==false && x.UserName == username)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все блоги пользователя
        /// <summary>
        /// Получить все блоги пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetBlogsByUser(string userName)
        {
            return entities.mytrip_articlescategory
                .Where(x => x.Blog && x.UserName == userName)
                .Where(x => x.SubCategoryId == 0)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все блоги пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="culture">культура</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetBlogsByUser(string userName, string culture)
        {
            return entities.mytrip_articlescategory.Where(x => x.Blog
                && x.Culture == culture && x.UserName == userName && x.SubCategoryId == 0)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все темы пользователя
        /// <summary>
        /// Получить все темы пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescategory> GetTopicsByUser(string userName)
        {
            return entities.mytrip_articlescategory
                 .Include("mytrip_articlescategory2")
                .Where(x => x.Blog
                && x.SubCategoryId != 0 && x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_articlescategory> GetTopicsByUser(string userName, string culture)
        {
            return entities.mytrip_articlescategory
                 .Include("mytrip_articlescategory2")
                .Where(x => x.Blog
                && x.SubCategoryId != 0 && x.Culture == culture && x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion
    }
}
