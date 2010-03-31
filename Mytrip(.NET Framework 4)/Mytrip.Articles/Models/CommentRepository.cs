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

namespace Mytrip.Articles.Models
{
    public class CommentRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к Entity Репозиторию
        /// <summary>
        /// Подключение к Entity Репозиторию
        /// </summary>
       // articlesEntities _db = new articlesEntities(ArticlesSetting.connectionString);
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
        public string userName
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        public string userEmail
        {
            get { return mr.mtGetUserByUserName(userName).mytrip_Membership.Email; }
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить все комментарии
        /// <summary>
        /// Получить все комментарии с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsDesc()
        {
            return _db.mytrip_ArticlesComments.OrderByDescending(x => x.CommentId);
        }
        /// <summary>
        /// Получить все комментарии с начала 1->Х
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsAsc()
        {
            return _db.mytrip_ArticlesComments.OrderBy(x => x.CommentId);
        }
        #endregion.

        #region Получить количество всех комментариев
        public int GetCommentsCount(string culture, bool forBlog)
        {
            return _db.mytrip_ArticlesComments.Count(x => x.mytrip_Articles.mytrip_ArticlesCategory.Culture == culture
                &&x.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog==forBlog);
        }
        #endregion

        #region Получить последние добавленные комментарии
        public IQueryable<mytrip_ArticlesComments> GetLastComments(int count,bool forBlog)
        {
            return _db.mytrip_ArticlesComments
                .Where(x=>x.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog==forBlog)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        public IQueryable<mytrip_ArticlesComments> GetLastCommentsByDate(int days, string culture, bool forBlog)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a=_db.mytrip_ArticlesComments
                .Where(x => x.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == forBlog)
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_Articles.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesComments> GetLastCommentsByDate(int endDay, int range, string culture, bool forBlog)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = _db.mytrip_ArticlesComments
                .Where(x => x.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == forBlog)
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_Articles.Culture == culture);
            return a.OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<mytrip_ArticlesComments> GetLastComments(string culture, bool forBlog)
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.mytrip_Articles.Culture == culture)
                .Where(x => x.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog == forBlog)
                .OrderByDescending(x => x.CreateDate).Take(5);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить все комментарии к статье
        /// <summary>
        /// Получить все комментарии к статье с начала 1->X
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByArticleAsc(int articleId)
        {
            return _db.mytrip_ArticlesComments.Where(x => x.mytrip_Articles.ArticleId == articleId)
                .OrderBy(x => x.CreateDate);
        }
        /// <summary>
        /// получить все комментарии к статье с конца X->1
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByArticleDesc(int articleId)
        {
            return _db.mytrip_ArticlesComments.Where(x => x.mytrip_Articles.ArticleId == articleId)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion        

        /*  РАЗДЕЛ 4  */

        #region Получить комментарий по Id
        /// <summary>
        /// Получить комментарий
        /// </summary>
        /// <param name="commentId">номер комментария</param>
        /// <returns></returns>
        public mytrip_ArticlesComments GetComment(int commentId)
        {
            return _db.mytrip_ArticlesComments.FirstOrDefault(x => x.CommentId == commentId);
        }
        #endregion

        #region _Создать уникальный CommentId
        private int CreateCommentId()
        {
            int commentId;
            for (commentId = 1; GetComment(commentId) != null; commentId++) ;
            return commentId;
        }
        #endregion

        #region Создать комментарий
        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="body">комментарий</param>
        /// <param name="name">имя пользователя</param>
        /// <param name="email">email пользователя</param>
        /// <param name="isAnonym">авторизован ли пользователь</param>
        /// <returns></returns>
        public mytrip_ArticlesComments CreateComment(int articleId, string body)
        {
            mytrip_ArticlesComments x = new mytrip_ArticlesComments
            {
                CommentId = CreateCommentId(),
                ArticleId = articleId,
                Body = StaticMethod.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = userName,
                UserEmail = userEmail,
                IsAnonym = false
            };
            _db.mytrip_ArticlesComments.AddObject(x);
            _db.SaveChanges();
            return x;
        }
        /// <summary>
        /// Создать комментарий для анонима
        /// </summary>
        /// <param name="articleId">номер статьи</param>
        /// <param name="body">комментарий</param>
        /// <param name="name">имя пользователя</param>
        /// <param name="email">email пользователя</param>
        /// <param name="isAnonym">авторизован ли пользователь</param>
        /// <returns></returns>
        public mytrip_ArticlesComments CreateComment(int articleId, string body,string username,string email)
        {
            mytrip_ArticlesComments x = new mytrip_ArticlesComments
            {
                CommentId = CreateCommentId(),
                ArticleId = articleId,
                Body = StaticMethod.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = username,
                UserEmail = email,
                IsAnonym = true
            };
            _db.mytrip_ArticlesComments.AddObject(x);
            _db.SaveChanges();
            return x;
        }
        #endregion

        #region Обновить комментарий
        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="commentId">mt_article_comment</param>
        public void UpdateComment(int commentId, string body)
        {
            mytrip_ArticlesComments ac = GetComment(commentId);
            ac.Body = StaticMethod.CleanHtmlCode(body);
            _db.SaveChanges();
        }
        #endregion

        #region Удалить комментарий
        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="commentId">номер комментария</param>
        public void DeleteComment(int commentId)
        {
            mytrip_ArticlesComments x = GetComment(commentId);
            _db.mytrip_ArticlesComments.DeleteObject(x);
            _db.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 5  */

        #region Получить все комментарии пользователя
        /// <summary>
        /// Получить все комментарии пользователя с конца Х->1
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByCurrentUserDesc()
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == HttpContext.Current.User.Identity.Name)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все комментарии пользователя с начала 1->Х
        /// </summary>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByCurrentUserAsc()
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == HttpContext.Current.User.Identity.Name)
                .OrderBy(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все комментарии пользователя с конца Х->1
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByUserDesc(string userName)
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все комментарии пользователя с конца Х->1 постранично
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByUserDesc(int pageIndex, int pageSize, string userName, out int total)
        {
            var a = _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
            total = a.Count();
            return a
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// Получить все комментарии пользователя с начала 1->Х
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public IQueryable<mytrip_ArticlesComments> GetCommentsByUserAsc(string userName)
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == userName)
                .OrderBy(x => x.CreateDate);
        }
        /// <summary>
        /// Получить все комментарии пользователя с начала 1->Х
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public int GetCountCommentsByUser(string userName)
        {
            return _db.mytrip_ArticlesComments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == userName)
                .Count();
        }
        #endregion
    }
}
