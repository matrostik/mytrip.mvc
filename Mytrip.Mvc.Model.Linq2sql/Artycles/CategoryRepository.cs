/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Model.Linq2sql;

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
        public IEnumerable<mt_artycle_category> Get_categories()
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// Получить все рубрики статей с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_page(int a, int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все блоги (постранично включительно)
        /// <summary>
        /// Получить все блоги по просмотрам
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs()
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Views);
        }
        /// <summary>
        /// Получить все блоги по автору
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs_user()
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .Where(x => x.AddedBy == HttpContext.Current.User.Identity.Name)
                .OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все блоги по просмотрам постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_blogs_page(int a, int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Views)
                .Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все рубрики статей для меню(постранично включительно)
        /// <summary>
        /// Получить все рубрики статей для меню с начала 1->X 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_true()
        {
            return _db.mt_artycle_category
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.AddMenu == true)
                .Where(x => x.CategoryId == 0)
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
        public IEnumerable<mt_artycle_category> Get_categories_addmenu_false()
        {
            return _db.mt_artycle_category
                .Where(x => x.AddMenu == false)
                .Where(x => x.Blog == false)
                .Where(x => x.News == false)
                .Where(x => x.CategoryId == 0)
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
        public IEnumerable<mt_artycle_category> Get_category_news()
        {
            return _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id);
        }        
        /// <summary>
        /// Получить все рубрики новостей с начала 1->X постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_category> Get_category_news_page(int a, int b)
        {
            return _db.mt_artycle_category
                .Where(x => x.News == true)
                .Where(x => x.CategoryId == 0)
                .OrderByDescending(x => x.Id)
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
        public mt_artycle_category Create_category(string title, bool addmenu)
        {
            
            mt_artycle_category x = new mt_artycle_category
            {
                Title = title,
                Path = DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = addmenu,
                Blog=false,
                Views=0,
                Email=Get_email().aspnet_Membership.Email,
                News=false,
                CategoryId=0

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
                Path = DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = false,
                CategoryId = categoryId

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
        public mt_artycle_category Create_blog()
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title = HttpContext.Current.User.Identity.Name + " (блог)",
                Path = HttpContext.Current.User.Identity.Name + "_blog",
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = true,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News=false,
                CategoryId=0

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
                Path = DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = true,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = false,
                CategoryId = categoryId

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
        public mt_artycle_category Create_category_news(string title, bool addmenu)
        {

            mt_artycle_category x = new mt_artycle_category
            {
                Title =title,
                Path = DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = addmenu,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = true,
                CategoryId=0

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
                Path = DecodingString(title),
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                AddMenu = false,
                Blog = false,
                Views = 0,
                Email = Get_email().aspnet_Membership.Email,
                News = true,
                CategoryId = categoryId

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
        public void Update_category(int a, string title, bool addmenu)
        {
            mt_artycle_category y = Get_category(a);
            y.Title = title;
            y.Path = DecodingString(title);
            y.AddMenu = addmenu;            
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
            mt_artycle_category y = Get_category(a);
            y.Title = title;
            y.Path = DecodingString(title);            
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

        #region Перевод из русского на латиницу
        /// <summary>
        /// Перевод из русского на латиницу
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
        public string DecodingString(string a)
        {
            a = a.Trim();
            a = a.Replace(" ", "_");
            a = a.Replace("А", "A");
            a = a.Replace("а", "a");
            a = a.Replace("Б", "B");
            a = a.Replace("б", "b");
            a = a.Replace("В", "V");
            a = a.Replace("в", "v");
            a = a.Replace("Г", "G");
            a = a.Replace("г", "g");
            a = a.Replace("Д", "D");
            a = a.Replace("д", "d");
            a = a.Replace("Е", "E");
            a = a.Replace("е", "e");
            a = a.Replace("Ё", "E");
            a = a.Replace("ё", "e");
            a = a.Replace("Ж", "J");
            a = a.Replace("ж", "j");
            a = a.Replace("З", "Z");
            a = a.Replace("з", "z");
            a = a.Replace("И", "I");
            a = a.Replace("и", "i");
            a = a.Replace("Й", "Y");
            a = a.Replace("й", "y");
            a = a.Replace("К", "K");
            a = a.Replace("к", "k");
            a = a.Replace("Л", "L");
            a = a.Replace("л", "l");
            a = a.Replace("М", "M");
            a = a.Replace("м", "m");
            a = a.Replace("Н", "N");
            a = a.Replace("н", "n");
            a = a.Replace("О", "O");
            a = a.Replace("о", "o");
            a = a.Replace("П", "P");
            a = a.Replace("п", "p");
            a = a.Replace("Р", "R");
            a = a.Replace("р", "r");
            a = a.Replace("С", "S");
            a = a.Replace("с", "s");
            a = a.Replace("Т", "T");
            a = a.Replace("т", "t");
            a = a.Replace("У", "U");
            a = a.Replace("у", "u");
            a = a.Replace("Ф", "F");
            a = a.Replace("ф", "f");
            a = a.Replace("Х", "H");
            a = a.Replace("х", "h");
            a = a.Replace("Ц", "C");
            a = a.Replace("ц", "c");
            a = a.Replace("Ч", "Ch");
            a = a.Replace("ч", "ch");
            a = a.Replace("Ш", "Sh");
            a = a.Replace("ш", "sh");
            a = a.Replace("Щ", "Sh");
            a = a.Replace("щ", "sh");
            a = a.Replace("Ъ", "");
            a = a.Replace("ъ", "");
            a = a.Replace("Ы", "Y");
            a = a.Replace("ы", "y");
            a = a.Replace("Ь", "");
            a = a.Replace("ь", "");
            a = a.Replace("Э", "E");
            a = a.Replace("э", "e");
            a = a.Replace("Ю", "Yu");
            a = a.Replace("ю", "yu");
            a = a.Replace("Я", "Ya");
            a = a.Replace("я", "ya");
            a = a.Replace("!", "");
            a = a.Replace(",", "");
            a = a.Replace(".", "");
            
            return a;

        }
        #endregion

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
