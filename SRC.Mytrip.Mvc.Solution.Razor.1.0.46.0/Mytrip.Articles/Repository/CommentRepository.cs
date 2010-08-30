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
using Mytrip.Mvc.Settings;

namespace Mytrip.Articles.Repository
{
    public class CommentRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к Entity Репозиторию
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSetting.connectionString());
                return _entities;
            }
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
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == forBlog)
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
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.mytrip_articles.Culture == culture)
               .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == forBlog)
               .Where(x => x.IsApproved == true)
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

        #region Comments Moderation
        /// <summary>
        /// Approve Comment
        /// </summary>
        /// <param name="commentId">CommentId</param>
        public void ApproveComment(int commentId)
        {
            mytrip_articlescomments ac = entities.mytrip_articlescomments
                .FirstOrDefault(x => x.CommentId == commentId);
            ac.IsApproved = true;
            entities.SaveChanges();
        }
        /// <summary>
        /// Close comments moderation
        /// </summary>
        /// <param name="articleId">ArticleId</param>
        public void CloseModeration(int articleId)
        {
            var ac = entities.mytrip_articlescomments
               .Where(x => x.ArticleId == articleId);
            foreach (var item in ac)
            {
                item.IsApproved = true;
            }
            entities.SaveChanges();
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
            return entities.mytrip_articlescomments
                .Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .FirstOrDefault(x => x.CommentId == commentId);
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
        public mytrip_articlescomments CreateComment(int articleId, string body, bool isApproved)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                mytrip_articlescomments x = new mytrip_articlescomments
                {
                    CommentId = CreateCommentId(),
                    ArticleId = articleId,
                    Body = GeneralMethods.CleanHtmlCode(body),
                    CreateDate = DateTime.Now,
                    UserName = HttpContext.Current.User.Identity.Name,
                    UserEmail = MytripUser.UserEmail(),
                    IsAnonym = false,
                    IsApproved = isApproved
                };
                entities.mytrip_articlescomments.AddObject(x);
                entities.SaveChanges();
                return x;
            }
            else return null;
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
        public mytrip_articlescomments CreateComment(int articleId, string body, string username, string email, bool isApproved)
        {
            mytrip_articlescomments x = new mytrip_articlescomments
            {
                CommentId = CreateCommentId(),
                ArticleId = articleId,
                Body = GeneralMethods.CleanHtmlCode(body),
                CreateDate = DateTime.Now,
                UserName = username,
                UserEmail = email,
                IsAnonym = true,
                IsApproved = isApproved
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
            for (commentId = 1; entities.mytrip_articlescomments.Count(x => x.CommentId == commentId) != 0; commentId++) ;
            return commentId;
        }
        #endregion

        #region Обновить комментарий
        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="commentId">mt_article_comment</param>
        public mytrip_articlescomments UpdateComment(int commentId, string body, bool isApproved)
        {
            mytrip_articlescomments ac = GetComment(commentId);
            ac.Body = GeneralMethods.CleanHtmlCode(body);
            ac.IsApproved = isApproved;
            entities.SaveChanges();
            return ac;
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
