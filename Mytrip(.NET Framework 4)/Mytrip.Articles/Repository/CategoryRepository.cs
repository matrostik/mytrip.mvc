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
using Mytrip.Core.Models;
using Mytrip.Core.Repository;
using Mytrip.Core;

namespace Mytrip.Articles.Repository
{
    public class CategoryRepository
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

        #region Другие подключения и переменные
        MembershipRepository mr = new MembershipRepository();
        private string username
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        private string useremail
        {
            get { return mr.mtGetUserEmail(username); }
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все рубрики статей (постранично включительно)
        /// <summary>
        /// Получить все рубрики статей с начала 1->X
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategories(string culture)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.AllCulture == true);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.Culture == culture);
            return a.Union(b)
                .OrderBy(x => x.CategoryId);
        }
        /// <summary>
        /// Получить все рубрики статей с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategories(int pageIndex, int pageSize, string culture,out int total)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId ==x.CategoryId)
                .Where(x => x.AllCulture == true);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.Culture == culture);
            var ab = a.Union(a).OrderBy(x => x.CategoryId);
            total = ab.Count();
            return ab.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить все рубрики и подрубрики статей для выпадающего меню
        /// </summary>
        /// <returns></returns>
        public List<mytrip_ArticlesCategory> GetCategoriesForDdl(string culture)
        {
            List<mytrip_ArticlesCategory> mcats = new List<mytrip_ArticlesCategory>();
            foreach (mytrip_ArticlesCategory cat in GetCategories(culture).OrderBy(x=>x.SeparateBlock))
            {
                mcats.Add(cat);
                if (cat.CategoryId == cat.SubCategoryId)
                    {
                        foreach (mytrip_ArticlesCategory subcat in GetSubCategories(cat.CategoryId,culture))
                        {
                            subcat.Title = "--" + subcat.Title;
                            mcats.Add(subcat);
                        }
                    }
            }
            return mcats;
        }
        /// <summary>
        /// Получить все подрубрики из рубрики статей для выпадающего меню
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <param name="culture">Culture</param>
        /// <returns></returns>
        public List<mytrip_ArticlesCategory> GetCategoriesForDdl(int categoryId,string culture)
        {
            List<mytrip_ArticlesCategory> mcats = new List<mytrip_ArticlesCategory>();
            mytrip_ArticlesCategory category = _db.mytrip_ArticlesCategory.SingleOrDefault(x => x.CategoryId == categoryId);
            mcats.Add(category);
                foreach (mytrip_ArticlesCategory subcat in GetSubCategories(category.CategoryId,culture))
                    {
                        subcat.Title = "--" + subcat.Title;
                        mcats.Add(subcat);
                    }
            return mcats;
        }
        #endregion

        #region Получить количество всех рубрик и подрубрик
        public int GetCategoriesCount(string culture)
        {
            var a = _db.mytrip_ArticlesCategory.Count(x => x.Culture == culture && x.mytrip_ArticlesCategory2.Blog == false);
            return a;
        }
        #endregion

        #region Получить последние добавленные рубрики или подрубрики
        public IQueryable<mytrip_ArticlesCategory> GetLastCategories(int count)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == false);
            return a.OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastCategoriesByDate(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastCategoriesByDate(int endDay, int range,string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastCategories(string culture)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == false)
                .Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        #region Получить все блоги (постранично включительно)
        /// <summary>
        /// Получить все блоги по просмотрам
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetBlogs(string culture)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.Views);
        }
        /// <summary>
        /// Получить все блоги по просмотрам постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetBlogs(int pageIndex, int pageSize, string culture,out int total)
        {
            var  a=_db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.Views);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить количество всех блогов
        public int GetBlogsCount(string culture)
        {
            return _db.mytrip_ArticlesCategory.Count(x => x.Blog == true && x.Culture == culture);
        }
        #endregion

        #region Получить последние добавленные блоги
        public IQueryable<mytrip_ArticlesCategory> GetLastBlogs(int count)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastBlogsByDate(int days,string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastBlogsByDate(int endDay, int range,string culture)
        {
            DateTime date=DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day,23,59,59,999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range-1) * -1);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastBlogs(string culture)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        #region Получить количество всех рубрик и подрубрик
        public int GetTopicsCount(string culture)
        {
            var a = _db.mytrip_ArticlesCategory.Count(x => x.Culture == culture && x.mytrip_ArticlesCategory2.Blog == true&&x.CategoryId!=x.SubCategoryId);
            return a;
        }
        #endregion
        #region Получить последние добавленные темы в блогах
        public IQueryable<mytrip_ArticlesCategory> GetLastTopics(int count)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == true)
                .Where(x=>x.CategoryId!=x.SubCategoryId)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastTopicsByDate(int days, string culture)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CategoryId != x.SubCategoryId)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastTopicsByDate(int endDay, int range, string culture)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CategoryId != x.SubCategoryId)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetLastTopics(string culture)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.mytrip_ArticlesCategory2.Blog == true)
                .Where(x => x.CategoryId != x.SubCategoryId)
                .Where(x => x.Culture == culture)
                .OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        #region Получить все блоги по автору
        /// <summary>
        /// Получить все блоги по автору
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetBlogsByUser(string userName)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesCategory> GetBlogsByUser(string userName, string culture)
        {
            return _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == true)
                .Where(x => x.Culture == culture)
                .Where(x => x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все рубрики статей для меню(постранично включительно)
        /// <summary>
        /// Получить все рубрики статей для меню с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategoriesForMenu(string culture)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SeparateBlock == true)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.Culture == culture);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SeparateBlock == true)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.AllCulture == true);
            return a.Union(b)
                .OrderBy(x => x.CategoryId);
        }
        /// <summary>
        /// Получить все рубрики статей для меню с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategoriesForMenu(int pageIndex, int pageSize, string culture,out int total)
        {
            var a = _db.mytrip_ArticlesCategory
               .Where(x => x.Blog == false)
               .Where(x => x.SeparateBlock == true)
               .Where(x => x.SubCategoryId == 0)
               .Where(x => x.Culture == culture);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.Blog == false)
                .Where(x => x.SeparateBlock == true)
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.AllCulture == true);
            var ab= a.Union(b).OrderBy(x => x.CategoryId);
            total=ab.Count();
            return ab.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить все недобавленные в меню рубрики статей (постранично включительно)
        /// <summary>
        /// Получить все недобавленные в меню рубрики статей с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategoriesNotInMenu(string culture)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.SeparateBlock == false)
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.Culture == culture);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.SeparateBlock == false)
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .Where(x => x.AllCulture == true);
            return a.Union(b)
                .OrderBy(x => x.CategoryId);
        }        
        /// <summary>
        /// Получить все недобавленные в меню рубрики статей с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetCategoriesNotInMenu(int pageIndex, int pageSize, out int total)
        {
            var a=_db.mytrip_ArticlesCategory
                .Where(x => x.SeparateBlock == false)
                .Where(x => x.Blog == false)
                .Where(x => x.SubCategoryId == x.CategoryId)
                .OrderByDescending(x => x.CategoryId);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region Получить все подрубрики из рубрики
        /// <summary>
        /// Получить все подрубрики из рубрики
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesCategory> GetSubCategories(int categoryId, string culture)
        {
            var a = _db.mytrip_ArticlesCategory
                .Where(x => x.SubCategoryId == categoryId)
                .Where(x => x.SubCategoryId != x.CategoryId)
                .Where(x => x.Culture == culture);
            var b = _db.mytrip_ArticlesCategory
                .Where(x => x.SubCategoryId == categoryId)
                .Where(x => x.SubCategoryId != x.CategoryId)
                .Where(x => x.AllCulture == true);
            return a.Union(b);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить одну рубрику по Id
        /// <summary>
        /// Получить одну рубрику по Id
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        /// <returns></returns>
        public mytrip_ArticlesCategory GetCategory(int categoryId)
        {
            return _db.mytrip_ArticlesCategory.SingleOrDefault(x => x.CategoryId == categoryId);
        }
        #endregion

        #region Получить последний блог
        /// <summary>
        /// Получить последний блог
        /// </summary>
        /// <returns></returns>
        public mytrip_ArticlesCategory GetBlogLast()
        {
            return _db.mytrip_ArticlesCategory.OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }
        #endregion

        #region _Создать уникальный CategoryId
        private int CreateCategoryId()
        {
            int catId;
            for (catId = 1; GetCategory(catId) != null; catId++) ;
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
        public mytrip_ArticlesCategory CreateСategory(string title, bool separateBlock, bool allCulture, string culture)
        {
            int categoryId = CreateCategoryId();
            mytrip_ArticlesCategory x = new mytrip_ArticlesCategory
            {
                CategoryId = categoryId,
                Title = title,
                Path = StaticMethod.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = username,
                UserEmail=useremail,
                SeparateBlock = separateBlock,
                Blog = false,
                Views = 0,
                SubCategoryId = categoryId,
                AllCulture = allCulture,
                Culture = culture
            };
            _db.mytrip_ArticlesCategory.AddObject(x);
            _db.SaveChanges();
            return x;
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
        public mytrip_ArticlesCategory CreateSubCategory(string title, int categoryId,bool allCulture)
        {
            mytrip_ArticlesCategory x = new mytrip_ArticlesCategory
            {
                CategoryId = CreateCategoryId(),
                Title = title,
                Path = StaticMethod.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = username,
                UserEmail = useremail,
                SeparateBlock = false,
                Blog = false,
                Views = 0,
                SubCategoryId = categoryId,
                Culture = GetCategory(categoryId).Culture,
                AllCulture = allCulture
            };
            _db.mytrip_ArticlesCategory.AddObject(x);
            _db.SaveChanges();
            return x;
        }
        #endregion

        #region Создать блог
        /// <summary>
        /// Создать блог
        /// </summary>
        /// <param name="email">email пользователя</param>
        /// <param name="culture">культура</param>
        /// <returns></returns>
        public mytrip_ArticlesCategory CreateBlog(string culture)
        {
            int categoryId = CreateCategoryId();
            string title = username + "(blog)";
            mytrip_ArticlesCategory x = new mytrip_ArticlesCategory
            {
                CategoryId = categoryId,
                Title = title,
                Path = StaticMethod.DecodingString(title),
                CreateDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                UserEmail = useremail,
                SeparateBlock = false,
                Blog = true,
                Views = 0,
                SubCategoryId = categoryId,
                AllCulture = false,
                Culture = culture
            };
            _db.mytrip_ArticlesCategory.AddObject(x);
            _db.SaveChanges();
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
            mytrip_ArticlesCategory c = GetCategory(categoryId);
            if (c.CategoryId == c.SubCategoryId&&c.AllCulture!=allCulture&&c.AllCulture==true)
            {
                foreach (var art in c.mytrip_Articles)
                {
                    art.AllCulture = false;
                }
                foreach (mytrip_ArticlesCategory cat in c.mytrip_ArticlesCategory1)
                {
                    cat.AllCulture = false;
                    foreach (var article in cat.mytrip_Articles)
                    {
                        article.AllCulture = false;
                    }
                }
            }
            c.Title = title;
            c.Path = StaticMethod.DecodingString(title);
            c.SeparateBlock = separateBlock;
            c.AllCulture = allCulture;
            _db.SaveChanges();
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
            mytrip_ArticlesCategory c = GetCategory(categoryId);
            c.Title = title;
            c.Path = StaticMethod.DecodingString(title);
            if (c.SubCategoryId != 0)
            {
                mytrip_ArticlesCategory mc = GetCategory(c.SubCategoryId);
                c.AllCulture = mc.AllCulture;
                c.Culture = mc.Culture;
            }
            _db.SaveChanges();
        }
        #endregion        

        #region Удалить рубрику
        /// <summary>
        /// Удалить рубрику (удаляет все подрубрики и статьи)
        /// </summary>
        /// <param name="categoryId">номер рубрики</param>
        public void DeleteCategory(int categoryId)
        {
            mytrip_ArticlesCategory c = _db.mytrip_ArticlesCategory.Single(x => x.CategoryId == categoryId);
            foreach (var mart in c.mytrip_Articles.ToList())
            {
                foreach (var comment in mart.mytrip_ArticlesComments.ToList())
                {
                    _db.mytrip_ArticlesComments.DeleteObject(comment);
                }
                foreach (var tag in mart.mytrip_ArticlesTag.ToList())
                {
                    mart.mytrip_ArticlesTag.Remove(tag);
                }
                foreach (var vote in mart.mytrip_ArticlesVotes.ToList())
                {
                    _db.mytrip_ArticlesVotes.DeleteObject(vote);
                }
                _db.mytrip_Articles.DeleteObject(mart);
            }
            foreach (var v in c.mytrip_ArticlesCategory1.ToList())
            {
                foreach (var art in v.mytrip_Articles.ToList())
                {
                    foreach (var tag in art.mytrip_ArticlesTag.ToList())
                    {
                        art.mytrip_ArticlesTag.Remove(tag);
                    }
                    foreach (var comment in art.mytrip_ArticlesComments.ToList())
                    {
                        _db.mytrip_ArticlesComments.DeleteObject(comment);
                    }
                    foreach (var vote in art.mytrip_ArticlesVotes.ToList())
                    {
                        _db.mytrip_ArticlesVotes.DeleteObject(vote);
                    }
                    _db.mytrip_Articles.DeleteObject(art);
                }
                _db.mytrip_ArticlesCategory.DeleteObject(v);
            }
            _db.mytrip_ArticlesCategory.DeleteObject(c);
            _db.SaveChanges();
        }
        #endregion

        #region Счетчик просмотров блога
        /// <summary>
        /// Счетчик просмотров блога
        /// </summary>
        /// <param name="categoryId">номер блога</param>
        public void BlogViewsIncrease(int categoryId)
        {
            mytrip_ArticlesCategory x = GetCategory(categoryId);
            x.Views++;
            _db.SaveChanges();
        }
        #endregion
    }
}
