/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Model.Linq2sql;

namespace Mytrip.Mvc.Model.Linq2sql.SiteModel
{
    public class SiteModelRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public SiteModelRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить модель
        /// <summary>
        /// Получить модель
        /// </summary>        
        /// <returns></returns>
        public mt_model Get_model()
        {
            return _db.mt_model.FirstOrDefault(x => x.Id == 1);
        }
        #endregion

        #region получить все модели
        /// <summary>
        /// получить все модели
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_model> Get_models()
        {
            return _db.mt_model.OrderBy(x => x.Id);
        }
        #endregion

        #region Получить схему провайдера
        /// <summary>
        /// Получить схему провайдера
        /// </summary>        
        /// <returns></returns>
        public aspnet_SchemaVersions Get_Shema(string a)
        {
            return _db.aspnet_SchemaVersions.FirstOrDefault(x => x.Feature == a);
        }
        #endregion

        #region Создать схему провайдера
        /// <summary>
        /// Создать схему провайдера
        /// </summary>        
        /// <returns></returns>
        public aspnet_SchemaVersions Shema1_common()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "common",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        public aspnet_SchemaVersions Shema2_health_monitoring()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "health monitoring",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        public aspnet_SchemaVersions Shema3_membership()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "membership",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        public aspnet_SchemaVersions Shema4_personalization()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "personalization",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        public aspnet_SchemaVersions Shema5_profile()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "profile",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        public aspnet_SchemaVersions Shema6_role_manager()
        {
            aspnet_SchemaVersions x = new aspnet_SchemaVersions
            {
                Feature = "role manager",
                CompatibleSchemaVersion = "1",
                IsCurrentVersion = true

            };
            _db.aspnet_SchemaVersions.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Создать модель
        /// <summary>
        /// Создать модель
        /// </summary>        
        /// <returns></returns>
        public mt_model Create_model()
        {
            mt_model x = new mt_model
            {
                Id = 1,
                DomainName = "mytripmvc.net",
                DateApproved = DateTime.MaxValue,
                Blog = true,
                CloseBlog = false,
                CountComment = 5,
                News = true,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Artycle = true,
                Captcha_approved=true,
                Email_approved=false,
                Email= Get_email().aspnet_Membership.Email,
                Port=25,
                EnableSsl=true,
                Smtp="smtp...",
                Login_email = Get_email().aspnet_Membership.Email,
                Password_email="password"

            };
            _db.mt_model.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить модель
        /// <summary>
        /// Обновить модель
        /// </summary>
        /// <param name="x">mt_artycle_comment</param>
        public void Update_model(string domainName, bool blog, int countComment, bool news,
            bool artycle, bool captcha_approved, bool email_approved, string email, int port,
            bool enableSsl, string smtp, string login_email, string password_email)
        {
            mt_model abc = Get_model();
            abc.DomainName = domainName;
            abc.Blog = blog;
            abc.CountComment = countComment;
            abc.News = news;
            abc.Artycle = artycle;
            abc.Captcha_approved = captcha_approved;
            abc.Email_approved = email_approved;
            abc.Email = email;
            abc.Port = port;
            abc.EnableSsl = enableSsl;
            abc.Smtp = smtp;
            abc.Login_email = login_email;
            abc.Password_email = password_email;
            _db.SubmitChanges();
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить роль
        /// <summary>
        /// Получить роль
        /// </summary>        
        /// <returns></returns>
        public aspnet_Roles Get_Roles(string a)
        {
            return _db.aspnet_Roles.FirstOrDefault(x => x.RoleName == a);
        }
        #endregion 

        #region Получить роль пользователя
        /// <summary>
        /// Получить роль пользователя
        /// </summary>        
        /// <returns></returns>
        public aspnet_UsersInRoles Get_Role_user(string a, string b)
        {
         var abc= _db.aspnet_UsersInRoles.Where(x => x.aspnet_Users.UserName == a);
         return abc.FirstOrDefault(x=>x.aspnet_Roles.RoleName== b);
        }
        #endregion

        #region Получить все роли пользователя
        /// <summary>
        /// Получить все роли пользователя
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<aspnet_UsersInRoles> Get_Roles_user(string a)
        {
            var abc = _db.aspnet_UsersInRoles.Where(x => x.aspnet_Users.UserName == a);
            return abc.OrderBy(x => x.aspnet_Roles.RoleName);
        }
        #endregion

        #region Получить пользователя
        /// <summary>
        /// Получить пользователя
        /// </summary>        
        /// <returns></returns>
        public aspnet_Users Get_User(string a)
        {
            return _db.aspnet_Users.FirstOrDefault(x => x.UserName == a);
        }
        #endregion

        #region получить всех пользователей
        /// <summary>
        /// получить всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<aspnet_Users> Get_Users()
        {
            var abc = _db.aspnet_Users.Where(x => x.UserName != "gfg567dfgy756");
            var cba = abc.Where(x => x.UserName != HttpContext.Current.User.Identity.Name);
            return cba.OrderByDescending(x => x.LastActivityDate);
        }
        /// <summary>
        /// получить всех пользователей постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<aspnet_Users> Get_Users_page(int a, int b)
        {
            var abc = _db.aspnet_Users.Where(x => x.UserName != "gfg567dfgy756");
            var  bcd= abc.OrderByDescending(x => x.LastActivityDate);
            var cba = bcd.Where(x => x.UserName != HttpContext.Current.User.Identity.Name);
            return cba.Skip((a - 1) * b).Take(b);
        }
        #endregion

        #region получить все роли
        /// <summary>
        /// получить все роли
        /// </summary>
        /// <returns></returns>
        public IEnumerable<aspnet_Roles> Get_Roles_all()
        {
            return _db.aspnet_Roles.OrderBy(x => x.RoleName);            
        }
        #endregion

        #region Поиск пользователей (постранично включительно)
        /// <summary>
        /// Поиск пользователей        
        /// </summary>
        /// <returns></returns>
        public IEnumerable<aspnet_Users> Search_users(string a)
        {
            var abc = _db.aspnet_Users.Where(x => x.UserName.IndexOf(a) != -1);
            var bca = _db.aspnet_Users.Where(x => x.aspnet_Membership.Email.IndexOf(a) != -1);           
            var qwe = abc.Concat(bca).Distinct();
            var amn = qwe.Where(x => x.UserName != "gfg567dfgy756");
            var cba = amn.Where(x => x.UserName != HttpContext.Current.User.Identity.Name);
            return cba.OrderByDescending(x => x.LastActivityDate);
        }
        /// <summary>
        /// Поиск пользователей постранично
        /// </summary>
        /// <param name="a">номер страницы</param>
        /// <param name="b">количество на странице</param>
        /// <returns></returns>
        public IEnumerable<aspnet_Users> Search_users_page(int a, int b, string c)
        {
            var abc = _db.aspnet_Users.Where(x => x.UserName.IndexOf(c) != -1);
            var bca = _db.aspnet_Users.Where(x => x.aspnet_Membership.Email.IndexOf(c) != -1);
            var qwe = abc.Concat(bca).Distinct();
            var amn = qwe.Where(x => x.UserName != "gfg567dfgy756");
            var cba = amn.Where(x => x.UserName != HttpContext.Current.User.Identity.Name);
            var abcd = cba.OrderByDescending(x => x.LastActivityDate);
            return abcd.Skip((a - 1) * b).Take(b);
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
        /// <summary>
        /// Заблокировать пользователя после регистрации
        /// </summary>       
        public void Close_user()
        {
            Guid dfg = Get_email().UserId;
            aspnet_Membership abc = _db.aspnet_Membership.First(x=>x.UserId==dfg);
            abc.IsApproved = false;
            _db.SubmitChanges();
        }
        /// <summary>
        /// Одобрение по емайл
        /// </summary>
        /// <param name="a"></param>
        public void Approved_user(Guid a)
        {
            aspnet_Membership abc = _db.aspnet_Membership.First(x => x.UserId == a);
            abc.IsApproved = true;
            _db.SubmitChanges();
        }
    }
}
