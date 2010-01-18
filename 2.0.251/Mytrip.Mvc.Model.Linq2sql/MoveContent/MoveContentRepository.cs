using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Mvc.Model.Linq2sql.MoveContent
{
    public class MoveContentRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public MoveContentRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */
        /// <summary>
        /// получить все новости посты и статьи
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_all_move_content(string culture)
        {
            var b= _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .OrderByDescending(x => x.Id);
        
        }
        /// <summary>
        /// получить только новости
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_news_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .Where(x=>x.mt_artycle_category.News==true)
                .OrderByDescending(x => x.Id);

        }
        /// <summary>
        /// получить только посты
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_post_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .Where(x => x.mt_artycle_category.Blog == true)
                .OrderByDescending(x => x.Id);

        }
        /// <summary>
        /// получить только статьи
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_article_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .Where(x => x.mt_artycle_category.Blog == false)
                .Where(x => x.mt_artycle_category.News == false)
                .OrderByDescending(x => x.Id);

        }
        /// <summary>
        /// получить статьи и новости 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_article_news_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .Where(x => x.mt_artycle_category.Blog == false)
                .OrderByDescending(x => x.Id);

        }
        /// <summary>
        /// получить статьи и посты
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_article_post_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Distinct()
                .Where(x => x.mt_artycle_category.News == false)
                .OrderByDescending(x => x.Id);

        }
        /// <summary>
        /// получить новости и посты
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_news_post_move_content(string culture)
        {
            var b = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mt_artycle_category.News == true)
                .Where(x => x.Culture == culture);
            var a = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mt_artycle_category.News == true)
                .Where(x => x.AllCulture == true);
            var c = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mt_artycle_category.Blog == true)
                .Where(x => x.Culture == culture);
            var d = _db.mt_artycle.Where(x => x.CloseDate >= DateTime.Now)
                .Where(x => x.mt_artycle_category.Blog == true)
                .Where(x => x.AllCulture == true);
            return b.Concat(a)
                .Concat(c)
                .Concat(d)
                .Distinct()                
                .OrderByDescending(x => x.Id);

        }
        public IEnumerable<mt_artycle> Get_no_move_content()
        {
            return _db.mt_artycle.Where(x => x.Culture == "error");           

        }

    }
}
