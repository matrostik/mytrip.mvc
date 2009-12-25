/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Model.Linq2sql;

namespace Mytrip.Mvc.Model.Linq2sql.Artycles
{
    public class ArtycleRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public ArtycleRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все статьи, новости и посты (постранично включительно)
        /// <summary>
        /// Получить все статьи, новости и посты с конца Х->1        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles()
        {
            return _db.mt_artycle.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все статьи, новости и посты с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_page(int a, int b)
        {
            var abcd = _db.mt_artycle.OrderByDescending(x => x.Id);
            return abcd.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все статьи не закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_date_true()
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == false);
            var bca = abc.Where(x => x.mt_artycle_category.News == false);           
            var bcd = bca.Where(x => x.CloseDate >= DateTime.Now);
            return bcd.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_date_true_page(int a, int b)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == false);
            var bca = abc.Where(x => x.mt_artycle_category.News == false);
            var bcd = bca.Where(x => x.CloseDate >= DateTime.Now);
            var cde = bcd.OrderByDescending(x => x.Id);
            return cde.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все статьи закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить все статьи закрытые по дате с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_date_false()
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == false);
            var bca = abc.Where(x => x.mt_artycle_category.News == false);
            var bcd = bca.Where(x => x.CloseDate < DateTime.Now);
            return bcd.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все одобренные статьи закрытые по дате с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_date_false_page(int a, int b)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == false);
            var bca = abc.Where(x => x.mt_artycle_category.News == false);
            var bcd = bca.Where(x => x.CloseDate < DateTime.Now);
            var cde = bcd.OrderByDescending(x => x.Id);
            return cde.Skip((a - 1) * b).Take(b);
        }
        #endregion
       

        /*  РАЗДЕЛ 3  */

        #region Получить все статьи из рубрики (постранично включительно)
        /// <summary>
        /// Получить все статьи из рубрики с конца Х->1
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category(int a)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все статьи из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .OrderByDescending(x => x.Id)
                .Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все статьи не закрытые по дате из рубрики (постранично включительно)
        /// <summary>
        /// Получить все одобренные статьи не закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category_date_true(int a)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category_date_true_page(int a, int b,int c)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .Where(x => x.CloseDate >= DateTime.Now)
                .OrderByDescending(x => x.Id)
                .Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все статьи закрытые по дате из рубрики (постранично включительно)
        /// <summary>
        /// Получить все статьи закрытые по дате из рубрики с конца Х->1
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category_date_false(int a)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все статьи закрытые по дате из рубрики с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер рубрики</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_artycles_from_category_date_false_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Id == a);
            var dfg = _db.mt_artycle.Where(x => x.mt_artycle_category.mt_artycle_category1.Id == a);
            return abc.Concat(dfg)
                .Where(x => x.CloseDate < DateTime.Now)
                .OrderByDescending(x => x.Id)
                .Skip((b - 1) * c).Take(c);
        }
        #endregion
        

        /*  РАЗДЕЛ 4  */

        #region Получить статью по Id
        /// <summary>
        /// Получить статью по Id
        /// </summary>
        /// <param name="a">номер статьи</param>
        /// <returns></returns>
        public mt_artycle Get_artycle(int a)
        {

            return _db.mt_artycle.SingleOrDefault(x => x.Id == a);

        }
        public mt_artycle Get_artycle_first()
        {
            var adc = _db.mt_artycle.OrderByDescending(x => x.Id);
            return adc.FirstOrDefault(x => x.Id >0);

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
        /// <param name="approved">одобрена</param>
        /// <param name="urlimagedescription">фото в кратко</param>
        /// <param name="urlimagebody">фото в статью</param>
        /// <param name="descriptionbody">фото в кратко как фото в статью</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="regstruser">просмотр только зарегистрированным</param>
        /// <param name="closedate">дата закрытия</param>
        /// <returns></returns>
        public mt_artycle Create_artycle(int a, string title,string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate)
        {
            mt_artycle x = new mt_artycle
            {
                CategoryId=a,
                Title=title,
                Description=DecodingDescription(description),
                Body=body,
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Views = 0,
                Votes = 0,
                AddComment = addComment,
                UrlImageDescription=urlImageDescription,
                UrlImageBody=urlImageBody,
                DescriptionBody = descriptionBody,
                Path = DecodingString(title),
                RegistrUser = registrUser,
                ApprovedVotes = approvedVotes,
                CloseDate = closeDate,
                Warning = true,
                Email = Get_email().aspnet_Membership.Email
            };
            _db.mt_artycle.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить статью
        /// <summary>
        /// Обновить статью
        /// </summary>        
        public void Update_artycle(int a,int b, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate)
        {
            mt_artycle abc = Get_artycle(a);
            abc.CategoryId = b;
            abc.Title = title;
            abc.Description = DecodingDescription(description);
            abc.Body =body;
            abc.UrlImageDescription = urlImageDescription;
            abc.UrlImageBody =urlImageBody;
            abc.DescriptionBody =descriptionBody;
            abc.AddComment =addComment;
            abc.ApprovedVotes =approvedVotes;
            abc.RegistrUser = registrUser;
            abc.CloseDate =closeDate;
            abc.Path = DecodingString(title);
            _db.SubmitChanges();
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
        /// <param name="approved">одобрена</param>
        /// <param name="urlimagedescription">фото в кратко</param>
        /// <param name="urlimagebody">фото в статью</param>
        /// <param name="descriptionbody">фото в кратко как фото в статью</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="regstruser">просмотр только зарегистрированным</param>
        /// <param name="closedate">дата закрытия</param>
        /// <returns></returns>
        public mt_artycle Create_post(int a, string title, string description,
            string body, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser)
        {
            mt_artycle x = new mt_artycle
            {
                CategoryId = a,
                Title = title,
                Description = DecodingDescription(description),
                Body = body,
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Views = 0,
                Votes = 0,
                AddComment = true,
                UrlImageDescription = urlImageDescription,
                UrlImageBody = urlImageBody,
                DescriptionBody = descriptionBody,
                Path = DecodingString(title),
                RegistrUser = registrUser,
                ApprovedVotes = true,
                CloseDate = DateTime.MaxValue,
                Warning = true,
                Email = Get_email().aspnet_Membership.Email
            };
            _db.mt_artycle.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить пост
        /// <summary>
        /// Обновить пост
        /// </summary>        
        public void Update_post(int a, string title, string description,
            string body, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser)
        {
            mt_artycle abc = Get_artycle(a);            
            abc.Title = title;
            abc.Description = DecodingDescription(description);
            abc.Body = body;
            abc.UrlImageDescription = urlImageDescription;
            abc.UrlImageBody = urlImageBody;
            abc.DescriptionBody = descriptionBody;
            abc.RegistrUser = registrUser;
            abc.Path = DecodingString(title);
            _db.SubmitChanges();
        }

        #endregion

        #region Создать новость
        /// <summary>
        /// Создать новость
        /// </summary>
        /// <param name="categoryid">номер рубрики</param>
        /// <param name="title">заголовок</param>
        /// <param name="description">кратко</param>
        /// <param name="body">статья</param>
        /// <param name="approved">одобрена</param>
        /// <param name="urlimagedescription">фото в кратко</param>
        /// <param name="urlimagebody">фото в статью</param>
        /// <param name="descriptionbody">фото в кратко как фото в статью</param>
        /// <param name="addcomment">разрешить комментарии</param>
        /// <param name="approvedvotes">разрешить голосование</param>
        /// <param name="regstruser">просмотр только зарегистрированным</param>
        /// <param name="closedate">дата закрытия</param>
        /// <returns></returns>
        public mt_artycle Create_news(int a, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate, bool warning)
        {
            mt_artycle x = new mt_artycle
            {
                CategoryId = a,
                Title = title,
                Description = DecodingDescription(description),
                Body = body,
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Views = 0,
                Votes = 0,
                AddComment = addComment,
                UrlImageDescription = urlImageDescription,
                UrlImageBody = urlImageBody,
                DescriptionBody = descriptionBody,
                Path = DecodingString(title),
                RegistrUser = registrUser,
                ApprovedVotes = approvedVotes,
                CloseDate = closeDate,
                Warning = warning,
                Email = Get_email().aspnet_Membership.Email
            };
            _db.mt_artycle.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить новость
        /// <summary>
        /// Обновить новость
        /// </summary>        
        public void Update_news(int a, int b, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate, bool warning)
        {
            mt_artycle abc = Get_artycle(a);
            abc.CategoryId = b;
            abc.Title = title;
            abc.Description = DecodingDescription(description);
            abc.Body = body;
            abc.UrlImageDescription = urlImageDescription;
            abc.UrlImageBody = urlImageBody;
            abc.DescriptionBody = descriptionBody;
            abc.AddComment = addComment;
            abc.ApprovedVotes = approvedVotes;
            abc.RegistrUser = registrUser;
            abc.CloseDate = closeDate;
            abc.Path = DecodingString(title);
            abc.Warning = warning;
            _db.SubmitChanges();
        }

        #endregion

        #region Удалить статью
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="id">номер статьи</param>
        public void Delete_artycle(int id)
        {
            mt_artycle x = Get_artycle(id);
            _db.mt_artycle.DeleteOnSubmit(x);
            _db.SubmitChanges();
        }
        #endregion

        #region Счетчик просмотров статьи
        /// <summary>
        /// Счетчик просмотров статьи
        /// </summary>
        /// <param name="a">номер статьи</param>
        public void Views_artycle(int a)
        {
            mt_artycle x = Get_artycle(a);
            x.Views++;
            _db.SubmitChanges();
        }
        #endregion

        #region Голосование плюс
        /// <summary>
        /// Голосование плюс
        /// </summary>
        /// <param name="a">номер статьи</param>
        public void Votes_artycle_plus(int a)
        {
            mt_artycle x = Get_artycle(a);
            x.Votes++;
            _db.SubmitChanges();
        }
        #endregion

        #region Голосование минус
        /// <summary>
        /// Голосование минус
        /// </summary>
        /// <param name="a">номер статьи</param>
        public void Votes_artycle_minus(int a)
        {
            mt_artycle x = Get_artycle(a);
            x.Votes--;
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

        #region Преобразование краткого содержания
        /// <summary>
        /// Преобразование краткого содержания
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
        public string DecodingDescription(string a)
        {
            a = a.Trim();
            a = a.Replace("<p>", "");
            a = a.Replace("</p>", " "); 
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

        /*  РАЗДЕЛ 5  */        

        #region Получить все статьи, новости и посты не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи, новости и посты не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_blog_news_from_teg_date_true(int a)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a); 
            var cba = abc.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            return cba.OrderByDescending(x => x.mt_artycle.Id);
        }
        /// <summary>
        /// Получить все статьи, новости и посты не закрытые по дате из тега с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_blog_news_from_teg_date_true_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var cba = abc.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            var cde = cba.OrderByDescending(x => x.mt_artycle.Id);
            return cde.Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все статьи не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_from_teg_date_true(int a)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bce = abc.Where(x => x.mt_artycle.mt_artycle_category.News == false);
            var bcr = bce.Where(x => x.mt_artycle.mt_artycle_category.Blog == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            return cba.OrderByDescending(x => x.mt_artycle.Id);
        }
        /// <summary>
        /// Получить все статьи не закрытые по дате из тега с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_from_teg_date_true_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bce = abc.Where(x => x.mt_artycle.mt_artycle_category.News == false);
            var bcr = bce.Where(x => x.mt_artycle.mt_artycle_category.Blog == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            var cde = cba.OrderByDescending(x => x.mt_artycle.Id);
            return cde.Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все статьи и новости не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи и новости не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_news_from_teg_date_true(int a)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bcr = abc.Where(x => x.mt_artycle.mt_artycle_category.Blog == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            return cba.OrderByDescending(x => x.mt_artycle.Id);
        }
        /// <summary>
        /// Получить все статьи и новости не закрытые по дате из тега с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_news_from_teg_date_true_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bcr = abc.Where(x => x.mt_artycle.mt_artycle_category.Blog == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            var cde = cba.OrderByDescending(x => x.mt_artycle.Id);
            return cde.Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все статьи и посты не закрытые по дате из тега (постранично включительно)
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца Х->1
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_blog_from_teg_date_true(int a)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bcr = abc.Where(x => x.mt_artycle.mt_artycle_category.News == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            return cba.OrderByDescending(x => x.mt_artycle.Id);
        }
        /// <summary>
        /// Получить все статьи и посты не закрытые по дате из тега с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <param name="b">номер страницы</param>
        /// <param name="c">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_artycles_blog_from_teg_date_true_page(int a, int b, int c)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.TegId == a);
            var bcr = abc.Where(x => x.mt_artycle.mt_artycle_category.News == false);
            var cba = bcr.Where(x => x.mt_artycle.CloseDate >= DateTime.Now);
            var cde = cba.OrderByDescending(x => x.mt_artycle.Id);
            return cde.Skip((b - 1) * c).Take(c);
        }
        #endregion

        #region Получить все теги
        /// <summary>
        /// Получить все теги        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_teg> Get_tegs()
        {
            return _db.mt_artycle_teg.OrderBy(x => x.Id);
        }
        #endregion

        #region Получить все теги из статьи
        /// <summary>
        /// Получить все теги из статьи        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_in_teg> Get_tegs_in_artycles(int a)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.ArtycleId == a);
            return abc.OrderBy(x => x.mt_artycle_teg.Id);
        }
        #endregion

        #region Получить тег по Id
        /// <summary>
        /// Получить тег по Id
        /// </summary>
        /// <param name="a">номер тега</param>
        /// <returns></returns>
        public mt_artycle_teg Get_teg(int a)
        {

            return _db.mt_artycle_teg.FirstOrDefault(x => x.Id == a);

        }
        /// <summary>
        /// Получить тег по Id и по статье
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public mt_artycle_in_teg Get_teg_artycle(int a,int b)
        {
            var abc = _db.mt_artycle_in_teg.Where(x => x.ArtycleId == a);
            return abc.FirstOrDefault(x => x.TegId == b);

        }
        #endregion

        #region Создать тег
        /// <summary>
        /// Создать тег
        /// </summary>
        /// <returns></returns>
        public mt_artycle_teg Create_teg(string title)
        {
            mt_artycle_teg x = new mt_artycle_teg
            {
                Title=title,
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Path = DecodingString(title)
            };
            _db.mt_artycle_teg.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить тег
        /// <summary>
        /// Обновить тег
        /// </summary>
        /// <param name="x">mt_artycle_teg</param>
        public void Update_teg(int a, string title)
        {
            mt_artycle_teg abc = Get_teg(a);
            abc.Title = title;
            abc.Path = DecodingString(title);
            _db.SubmitChanges();
        }
        #endregion

        #region Удалить teg
        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="a">номер тега</param>
        public void Delete_teg(int a)
        {
            mt_artycle_teg x = Get_teg(a);
            _db.mt_artycle_teg.DeleteOnSubmit(x);
            _db.SubmitChanges();
        }
        #endregion

        #region Добавить тег в статью
        /// <summary>
        /// Добавить тег в статью
        /// </summary>
        /// <returns></returns>
        public mt_artycle_in_teg Add_teg_in_artycle(int a, int b)
        {
            mt_artycle_in_teg x = new mt_artycle_in_teg
            {
               ArtycleId=a,
               TegId=b
            };
            _db.mt_artycle_in_teg.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Удалить teg из статьи
        /// <summary>
        /// Удалить тег из статьи
        /// </summary>
        /// <param name="a">номер тега</param>
        public void Delete_teg_in_artycle(int a, int b)
        {
            mt_artycle_in_teg x = Get_teg_artycle(a,b);
            _db.mt_artycle_in_teg.DeleteOnSubmit(x);
            _db.SubmitChanges();
        }
        #endregion

        /*  РАЗДЕЛ 6  */

        #region Получить все посты  (постранично включительно)
        /// <summary>
        /// Получить все посты с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_posts()
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == true);
            return abc.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все посты  с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_posts_page(int a, int b)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.Blog == true);
            var cde = abc.OrderByDescending(x => x.Id);
            return cde.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region Получить все новости не закрытые по дате (постранично включительно)
        /// <summary>
        /// Получить все новости не закрытые по дате с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_news_date_true()
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.News == true);
            var bcd = abc.Where(x => x.CloseDate >= DateTime.Now);
            return bcd.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все новости не закрытые по дате с конца Х->1 постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle> Get_news_date_true_page(int a, int b)
        {
            var abc = _db.mt_artycle.Where(x => x.mt_artycle_category.News == true);
            var bcd = abc.Where(x => x.CloseDate >= DateTime.Now);
            var cde = bcd.OrderByDescending(x => x.Id);
            return cde.Skip((a - 1) * b).Take(b);
        }
        #endregion
    }
}
