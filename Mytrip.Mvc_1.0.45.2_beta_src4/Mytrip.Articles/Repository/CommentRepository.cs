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
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc;
using Mytrip.Articles.Repository.DataEntities;

namespace Mytrip.Articles.Repository
{
    public class CommentRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к Entity Репозиторию
        ArticlesSetting _core;
        public ArticlesSetting core
        {
            get
            {
                if (_core == null)
                    _core = new ArticlesSetting();
                return _core;
            }
        }
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(core.connectionString());
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
            get { return mr.mtGetUserEmail(userName); }
        }
        #endregion

        /*  РАЗДЕЛ 2  */

        #region Получить количество всех комментариев
        /// <summary>
        /// Получить количество всех комментариев
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public int GetCommentsCount(string culture, bool forBlog)
        {
            int a = 0;
            if (!forBlog)
                a = entities.mytrip_articlescomments.Count(x => x.mytrip_articles.Culture == culture
                && !x.mytrip_articles.mytrip_articlescategory.Blog);
            else
                a = entities.mytrip_articlescomments.Count(x => x.mytrip_articles.Culture == culture &&
                    x.mytrip_articles.mytrip_articlescategory.Blog);
            return a;
        }
        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public int GetCommentsCount(string userName)
        {
            return entities.mytrip_articlescomments
                .Where(x => x.IsAnonym == false)
                .Where(x => x.UserName == userName)
                .Count();
        }
        #endregion

        #region Получить последние добавленные комментарии
        /// <summary>
        /// Получить последние добавленные комментарии
        /// </summary>
        /// <param name="count">количество комментариев</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetLastComments(int count, bool forBlog)
        {
            if (!forBlog)
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
                   .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false)
                   .OrderByDescending(x => x.CreateDate).Take(count);
            else
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
                .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true)
                .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить последние добавленные комментарии
        /// </summary>
        /// <param name="count">количество комментариев</param>
        /// <param name="forBlog">для блога</param>
        /// <param name="take">колличество количество для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetLastComments(string culture, bool forBlog, int take)
        {
            if (!forBlog)
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
                   .Where(x => x.mytrip_articles.Culture == culture)
                   .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false)
                   .OrderByDescending(x => x.CreateDate).Take(take);
            else
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
                    .Where(x => x.mytrip_articles.Culture == culture)
                    .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true)
                    .OrderByDescending(x => x.CreateDate).Take(take);
        }
        /// <summary>
        /// Получить последние добавленные комментарии определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetLastComments(int days, string culture, bool forBlog)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_articles.Culture == culture);
            if (!forBlog)
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false);
            if (forBlog)
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true);
            return a.OrderByDescending(x => x.CreateDate);
        }
        /// <summary>
        /// Получить последние добавленные комментарии в определенный интервал времени
        /// </summary>
        /// <param name="endDay">по какой день(количество дней назад)</param>
        /// <param name="range">интервал(количество дней)</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetLastComments(int endDay, int range, string culture, bool forBlog)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_articles.Culture == culture);
            if (!forBlog)
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false);
            if (forBlog)
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true);
            return a.OrderByDescending(x => x.CreateDate);
        }
        #endregion

        #region Получить все комментарии пользователя
        /// <summary>
        /// Получить все комментарии пользователя по дате с конца Х->1
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetComments(string userName, bool forBlog)
        {
            if (!forBlog)
                return entities.mytrip_articlescomments
                    .Include("mytrip_articles.mytrip_articlescategory")
                    .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false
                        && x.IsAnonym == false && x.UserName == userName)
                    .OrderByDescending(x => x.CreateDate);
            else
                return entities.mytrip_articlescomments
                .Include("mytrip_articles.mytrip_articlescategory")
                .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true
                    && x.IsAnonym == false && x.UserName == userName)
                .OrderByDescending(x => x.CreateDate);
        }
        #endregion

        /*  РАЗДЕЛ 3  */

        #region Получить комментарий по Id
        /// <summary>
        /// Получить комментарий
        /// </summary>
        /// <param name="commentId">номер комментария</param>
        /// <returns></returns>
        public mytrip_articlescomments GetComment(int commentId)
        {
            return entities.mytrip_articlescomments.Include("mytrip_articles").FirstOrDefault(x => x.CommentId == commentId);
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
        public mytrip_articlescomments CreateComment(int articleId, string body)
        {
            mytrip_articlescomments x = new mytrip_articlescomments
            {
                CommentId = CreateCommentId(),
                ArticleId = articleId,
                Body = GeneralMethods.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = userName,
                UserEmail = userEmail,
                IsAnonym = false
            };
            entities.mytrip_articlescomments.AddObject(x);
            entities.SaveChanges();
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
        public mytrip_articlescomments CreateComment(int articleId, string body, string username, string email)
        {
            mytrip_articlescomments x = new mytrip_articlescomments
            {
                CommentId = CreateCommentId(),
                ArticleId = articleId,
                Body = GeneralMethods.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = username,
                UserEmail = email,
                IsAnonym = true
            };
            entities.mytrip_articlescomments.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        /// <summary>
        /// _Создать уникальный CommentId
        /// </summary>
        /// <returns></returns>
        private int CreateCommentId()
        {
            int commentId;
            for (commentId = 1; GetComment(commentId) != null; commentId++) ;
            return commentId;
        }
        #endregion

        #region Обновить комментарий
        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="commentId">mt_article_comment</param>
        public void UpdateComment(int commentId, string body)
        {
            mytrip_articlescomments ac = GetComment(commentId);
            ac.Body = GeneralMethods.CleanHtmlCode(body);
            entities.SaveChanges();
        }
        #endregion

        #region Удалить комментарий
        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="commentId">номер комментария</param>
        public void DeleteComment(int commentId)
        {
            mytrip_articlescomments x = GetComment(commentId);
            entities.mytrip_articlescomments.DeleteObject(x);
            entities.SaveChanges();
        }
        #endregion
    }
}
