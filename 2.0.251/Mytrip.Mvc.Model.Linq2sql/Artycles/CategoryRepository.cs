/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Model.Linq2sql;
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Model.Linq2sql.Artycles
{
    public class CategoryRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public CategoryRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все рубрики статей (постранично включительно)
        /// <summary>
        /// Получить все рубрики статей с начала 1->X
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories(string a)
        {
            var b = _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.AllCulture == true);
            var c = _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.Culture == a);
            return b.Concat(c)
                .Distinct()
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// Получить все рубрики статей с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_page(int a, int b, string c)
        {
            var d = _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.AllCulture == true);
            var e = _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.Culture == c);
            return d.Concat(e)
                .Distinct()
                .OrderBy(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все блоги (постранично включительно)
        /// <summary>
        /// Получить все блоги по просмотрам
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs(string a)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.Culture==a)
                .OrderByDescending(x => x.Views);
        }
        /// <summary>
        /// Получить все блоги по автору
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs_user(string a)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.AddedBy == HttpContext.Current.User.Identity.Name)
                .Where(x=>x.Culture==a)
                .OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все блоги по просмотрам постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs_page(int a, int b, string c)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.Culture==c)
                .OrderByDescending(x => x.Views)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все рубрики статей для меню(постранично включительно)
        /// <summary>
        /// Получить все рубрики статей для меню с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_true(string culture)
        {
            var a= _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.Culture==culture);
            var b= _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.AllCulture==true);
            return a.Concat(b)
                .Distinct()
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// Получить все рубрики статей для меню с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_true_page(int a,int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все недобавленные в меню рубрики статей (постранично включительно)
        /// <summary>
        /// Получить все недобавленные в меню рубрики статей с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_false(string culture)
        {
            var a= _db.mt_artycle_category
                .Where(x => x.AddMenu == false)
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.Culture==culture);
            var b= _db.mt_artycle_category
                .Where(x => x.AddMenu == false)
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.AllCulture==true);
            return a.Concat(b)
                .Distinct()
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// Получить все недобавленные в меню рубрики статей с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_false_page(int a,int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.AddMenu == false)
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все рубрики новостей (постранично включительно)
        /// <summary>
        /// Получить все рубрики новостей с начала 1->X
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_category_news(string a)
        {
            var b= _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.AllCulture==true);
            var c = _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.Culture == a);
            return b.Concat(c)
                .Distinct()
                .OrderBy(x => x.Id);
        }        
        /// <summary>
        /// Получить все рубрики новостей с начала 1->X постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_category_news_page(int a, int b, string c)
        {
            var d= _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .Where(x=>x.AllCulture==true);
            var e = _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.Culture == c);
            return d.Concat(e)
                .Distinct()
                .OrderBy(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все рубрики новостей для меню(постранично включительно)
        /// <summary>
        /// Получить все рубрики новостей для меню с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_news_addmenu_true()
        {
            return _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// Получить все рубрики новостей для меню с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_news_addmenu_true_page(int a, int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region получить все подрубрики из рубрики
        /// <summary>
        /// получить все подрубрики из рубрики
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_re_categories(int a)
        {
            return _db.mt_artycle_category
                .Where(x => x.CategoryId==a);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить одну рубрику по Id
        /// <summary>
        /// Получить одну рубрику по Id
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <returns></returns>
        public mt_artycle_category Get_category(int a)
        {
            return _db.mt_artycle_category.SingleOrDefault(x => x.Id == a);
        }
        #endregion

        #region Получить последний блог
        /// <summary>
        /// Получить последний блог
        /// </summary>
        /// <returns></returns>
        public mt_artycle_category Get_blog_first()
        {
            return _db.mt_artycle_category
                .OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.Id > 0);
        }
        #endregion

        #region Создать рубрику
        /// <summary>
        /// Создать рубрику
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="addmenu">добавить в меню</param>        
        /// <returns></returns>
        public mt_artycle_category Create_category(string title, bool addmenu, bool allCulture, string culture)
        {
            
            mt_artycle_category x = new mt_artycle_category
            {
                Title = title,
                Path = StaticMethod.DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = addmenu,
                Blog=false,
                Views=0,
                Email=Get_email().aspnet_Membership.Email,
                News=false,
                CategoryId=0,
                AllCulture=allCulture,
                Culture=culture

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать подрубрику
        /// <summary>
        /// Создать подрубрику
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="addmenu">добавить в меню</param>        
        /// <returns></returns>
        public mt_artycle_category Create_re_category(string title, int categoryId)
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title = title,
                Path = StaticMethod.DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = false,
                CategoryId = categoryId,
                Culture=Get_category(categoryId).Culture,
                AllCulture=Get_category(categoryId).AllCulture

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать блог
        /// <summary>
        /// Создать блог
        /// </summary>       
        /// <returns></returns>
        public mt_artycle_category Create_blog(string a)
        {
            string b = HttpContext.Current.User.Identity.Name + Mytrip_Mvc_Language.create_blog_text;
            mt_artycle_category x = new mt_artycle_category
            {
                Title = b,
                Path = StaticMethod.DecodingString(b),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = true,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News=false,
                CategoryId=0,
                AllCulture=false,
                Culture=a

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать подблог
        /// <summary>
        /// Создать подблог
        /// </summary>       
        /// <returns></returns>
        public mt_artycle_category Create_re_blog(string title, int categoryId)
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title = title,
                Path = StaticMethod.DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = true,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = false,
                CategoryId = categoryId,
                Culture = Get_category(categoryId).Culture,
                AllCulture = Get_category(categoryId).AllCulture

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать рубрику новостей
        /// <summary>
        /// Создать рубрику новостей
        /// </summary>       
        /// <returns></returns>
        public mt_artycle_category Create_category_news(string title, bool allCulture, string culture)
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title =title,
                Path = StaticMethod.DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = true,
                CategoryId=0,
                AllCulture = allCulture,
                Culture = culture

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать подрубрику новостей
        /// <summary>
        /// Создать подрубрику новостей
        /// </summary>       
        /// <returns></returns>
        public mt_artycle_category Create_re_category_news(string title, int categoryId)
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title = title,
                Path = StaticMethod.DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = true,
                CategoryId = categoryId,
                Culture=Get_category(categoryId).Culture,
                AllCulture=Get_category(categoryId).AllCulture

            };
            _db.mt_artycle_category.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить рубрику
        /// <summary>
        /// Обновить рубрику
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <param name="title">название</param>
        /// <param name="addmenu">добавить в меню</param>
        public void Update_category(int a, string title, bool addmenu, bool allCulture)
        {
            mt_artycle_category y = Get_category(a);
            y.Title = title;
            y.Path = StaticMethod.DecodingString(title);
            y.AddMenu = addmenu;
            y.AllCulture = allCulture;
            _db.SubmitChanges();
        }
        #endregion

        #region Обновить блог
        /// <summary>
        /// Обновить блог
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <param name="title">название</param>
        /// <param name="addmenu">добавить в меню</param>
        public void Update_blog(int a, string title)
        {
            int b = Get_category(a).CategoryId;            
            mt_artycle_category y = Get_category(a);
            y.Title = title;
            y.Path = StaticMethod.DecodingString(title);
            if (b != 0) {
                bool c = Get_category(b).AllCulture;
                string d = Get_category(b).Culture;
                y.AllCulture = c;
                y.Culture = d;
            }
             _db.SubmitChanges();
        }
        #endregion        

        #region Удалить рубрику
        /// <summary>
        /// Удалить рубрику
        /// </summary>
        /// <param name="a">номер рубрики</param>
        public void Delete_category(int a)
        {
            
            var abc = _db.mt_artycle_category.Where(z => z.CategoryId == a);
            foreach (mt_artycle_category y in abc) {
                Delete_re_category(y.Id);          
            }
            Delete_re_category(a);
        }
        public void Delete_re_category(int a)
        {
            mt_artycle_category x = Get_category(a);           
            _db.mt_artycle_category.DeleteOnSubmit(x);
            _db.SubmitChanges();
        }

        #endregion

        #region Счетчик просмотров блога
        /// <summary>
        /// Счетчик просмотров блога
        /// </summary>
        /// <param name="a">номер блога</param>
        public void Views_blog(int a)
        {
            mt_artycle_category x = Get_category(a);
            x.Views++;
            _db.SubmitChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */       

        #region Получить email пользователя
        /// <summary>
        /// Получить комментарий
        /// </summary>
        /// <param name="a">номер комментария</param>
        /// <returns></returns>
        public aspnet_Users Get_email()
        {
            return _db.aspnet_Users.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

        }
        #endregion
    }
}
