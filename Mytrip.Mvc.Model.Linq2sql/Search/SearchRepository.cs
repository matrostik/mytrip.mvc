/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Model.Linq2sql;

namespace Mytrip.Mvc.Model.Linq2sql.Search
{
    public class SearchRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public SearchRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Поиск во всех статьях (постранично включительно)
        /// <summary>
        /// Поиск во всех статьях с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            return sdf.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск во всех статьях с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var abcd = sdf.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Поиск только в статьях (постранично включительно)
        /// <summary>
        /// Поиск только в статьях с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_only(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hjk = sdf.Where(x => x.mt_artycle_category.Blog == false);
            var hui = hjk.Where(x => x.mt_artycle_category.News == false);
            return hui.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск только в статьях с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_only_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hjk = sdf.Where(x => x.mt_artycle_category.Blog == false);
            var hui = hjk.Where(x => x.mt_artycle_category.News == false);
            var abcd = hui.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Поиск только в статьях и блогах (постранично включительно)
        /// <summary>
        /// Поиск только в статьях и блогах с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_blog(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.News == false);
            return hui.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск только в статьях и блогах с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_blog_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.News == false);
            var abcd = hui.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Поиск только в статьях и новостях (постранично включительно)
        /// <summary>
        /// Поиск только в статьях и новостях с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_news(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.Blog == false);
            return hui.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск только в статьях и новостях с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_artycles_news_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.Blog == false);
            var abcd = hui.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion


        #region Поиск только в блогах (постранично включительно)
        /// <summary>
        /// Поиск только в блогах с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_blog(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.Blog == true);
            return hui.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск только в блогах с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_blog_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.Blog == true);
            var abcd = hui.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Поиск только в новостях (постранично включительно)
        /// <summary>
        /// Поиск только в новостях с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_news(string a)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(a) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(a) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(a) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.News == true);
            return hui.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Поиск только в новостях с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Search_news_page(int a, int b, string c)
        {
            var abc = _db.mt_artycle.Where(x => x.Title.IndexOf(c) != -1);
            var bca = _db.mt_artycle.Where(x => x.Description.IndexOf(c) != -1);
            var cab = _db.mt_artycle.Where(x => x.Body.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var sdf = qwe.Concat(cab).Distinct();
            var hui = sdf.Where(x => x.mt_artycle_category.Blog == true);
            var abcd = hui.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion
        
        /*  РАЗДЕЛ 5  */

        
    }
}
