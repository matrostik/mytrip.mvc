/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using Mytrip.Mvc.Model.Linq2sql;

namespace Mytrip.Mvc.Model.Linq2sql.Artycles
{
    public class CommentRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public CommentRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все комментарии
        /// <summary>
        /// Получить все комментарии с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_end()
        {
            return _db.mt_artycle_comment.OrderByDescending(x => x.Id);
        }
        /// <summary>
        /// Получить все комментарии с начала 1->Х
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_start()
        {
            return _db.mt_artycle_comment.OrderBy(x => x.Id);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить все комментарии к статье
        /// <summary>
        /// Получить все комментарии к статье с начала 1->X
        /// </summary>
        /// <param name="a">номер статьи</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_from_artycle_start(int a)
        {
            return _db.mt_artycle_comment.Where(x => x.mt_artycle.Id == a)
                .OrderBy(x => x.Id);
        }
        /// <summary>
        /// получить все комментарии к статье с конца X->1
        /// </summary>
        /// <param name="a">номер статьи</param>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_from_artycle_end(int a)
        {
            return _db.mt_artycle_comment.Where(x => x.mt_artycle.Id == a)
                .OrderByDescending(x => x.Id);
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Получить комментарий по Id
        /// <summary>
        /// Получить комментарий
        /// </summary>
        /// <param name="a">номер комментария</param>
        /// <returns></returns>
        public mt_artycle_comment Get_сomment(int a)
        {
            return _db.mt_artycle_comment.FirstOrDefault(x => x.Id == a);
        }
        #endregion

        #region Создать комментарий
        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <param name="id">номер статьи</param>
        /// <param name="body">комментарий</param>
        /// <returns></returns>
        public mt_artycle_comment Create_сomment(int id, string body)
        {
            mt_artycle_comment x = new mt_artycle_comment
            {
                ArtycleId=id,
                Body = body,
                AddedDate = DateTime.Now,
                AddedBy = HttpContext.Current.User.Identity.Name,
                Email = Get_email().aspnet_Membership.Email
            };
            _db.mt_artycle_comment.InsertOnSubmit(x);
            _db.SubmitChanges();
            return x;
        }
        #endregion

        #region Обновить комментарий
        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="x">mt_artycle_comment</param>
        public void Update_сomment(int a,string body)
        {
            mt_artycle_comment abc = Get_сomment(a);
            abc.Body = body;
            _db.SubmitChanges();
        }
        #endregion

        #region Удалить комментарий
        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="a">номер комментария</param>
        public void Delete_сomment(int a)
        {
            mt_artycle_comment x = Get_сomment(a);
            _db.mt_artycle_comment.DeleteOnSubmit(x);
            _db.SubmitChanges();
        }
        #endregion

        /*  РАЗДЕЛ 5  */

        #region Получить все комментарии пользователя
        /// <summary>
        /// Получить все комментарии пользователя с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_end_user()
        {
            return _db.mt_artycle_comment.Where(x => x.AddedBy == HttpContext.Current.User.Identity.Name)
                .OrderByDescending(x => x.AddedDate);
        }
        /// <summary>
        /// Получить все комментарии пользователя с начала 1->Х
        /// </summary>
        /// <returns></returns>
        public IEnumerable<mt_artycle_comment> Get_сomments_start_user()
        {
            return _db.mt_artycle_comment.Where(x => x.AddedBy == HttpContext.Current.User.Identity.Name)
                .OrderBy(x => x.AddedDate);
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
