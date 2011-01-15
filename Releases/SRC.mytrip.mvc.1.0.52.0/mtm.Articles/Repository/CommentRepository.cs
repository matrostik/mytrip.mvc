//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about mytrip.mvc.Entyty visit 
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
using mtm.Core.Models;
using mtm.Core.Repository;
using mtm.Core;
using mtm.Articles.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Articles.Repository
{
    #region Enum CommentType
    /// <summary>
    /// Enum for sort criteria
    /// </summary>
    public enum CommentType { Articles, Blogs, Unapproved };
    #endregion
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
        public int GetCount(string culture, CommentType ctype)
        {
            if (ctype == CommentType.Articles)
            {
                return entities.mytrip_articlescomments.Count(x => x.mytrip_articles.Culture == culture
                && x.mytrip_articles.mytrip_articlescategory.Blog == false && x.IsApproved == true);
            }
            else if (ctype == CommentType.Blogs)
            {
                return entities.mytrip_articlescomments.Count(x => x.mytrip_articles.Culture == culture
                && x.mytrip_articles.mytrip_articlescategory.Blog == true && x.IsApproved == true);
            }
            else
            {
                return entities.mytrip_articlescomments.Count(x => x.mytrip_articles.Culture == culture
                  && x.IsApproved == false);
            }
        }
        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        public int GetCount(string userName)
        {
            return entities.mytrip_articlescomments
                .Count(x => x.IsAnonym == false && x.UserName == userName);
        }
        #endregion

        #region Получить последние добавленные комментарии
        /// <summary>
        /// Получить последние добавленные комментарии (для статей, для блогов или неодобренные)
        /// </summary>
        /// <param name="count">количество комментариев</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetComments(CommentType ctype, int count)
        {
            if (ctype == CommentType.Articles)
            {
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
              .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false)
              .Where(x => x.IsApproved == true)
              .OrderByDescending(x => x.CreateDate).Take(count);
            }
            else if (ctype == CommentType.Blogs)
            {
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
              .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true)
              .Where(x => x.IsApproved == true)
              .OrderByDescending(x => x.CreateDate).Take(count);
            }
            else
            {
                return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.IsApproved == false)
               .OrderByDescending(x => x.CreateDate).Take(count);
            }

        }
        /// <summary>
        /// Получить последние добавленные комментарии
        /// </summary>
        /// <param name="count">количество комментариев</param>
        /// <param name="forBlog">для блога</param>
        /// <param name="take">колличество количество для выборки</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetComments(bool forBlog, string culture, int count)
        {
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.mytrip_articles.Culture == culture)
               .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == forBlog)
               .Where(x => x.IsApproved == true)
               .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить последние добавленные комментарии определенное количество дней назад
        /// </summary>
        /// <param name="days">количество дней назад</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetComments(int days, string culture, CommentType ctype)
        {
            DateTime date = DateTime.Now.AddDays(days);
            var a = entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.CreateDate.Day == date.Day && x.CreateDate.Month == date.Month && x.CreateDate.Year == date.Year);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_articles.Culture == culture);

            if (ctype == CommentType.Articles)
            {
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false && x.IsApproved == true);
            }
            else if (ctype == CommentType.Blogs)
            {
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true && x.IsApproved == true);
            }
            else
            {
                a = a.Where(x => x.IsApproved == false);
            }
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
        public IQueryable<mytrip_articlescomments> GetComments(int endDay, int range, string culture, CommentType ctype)
        {
            DateTime date = DateTime.Now.AddDays(endDay);
            DateTime eDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            DateTime sDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 1).AddDays((range - 1) * -1);
            var a = entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
                .Where(x => x.CreateDate <= eDate)
                .Where(x => x.CreateDate >= sDate);
            if (!string.IsNullOrEmpty(culture))
                a = a.Where(x => x.mytrip_articles.Culture == culture);
            if (ctype == CommentType.Articles)
            {
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false && x.IsApproved == true);
            }
            else if (ctype == CommentType.Blogs)
            {
                a = a.Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true && x.IsApproved == true);
            }
            else
            {
                a = a.Where(x => x.IsApproved == false);
            }
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
        public IQueryable<mytrip_articlescomments> GetComments(string userName, CommentType type)
        {
            if (type == CommentType.Articles)
            {
                return entities.mytrip_articlescomments
                .Include("mytrip_articles.mytrip_articlescategory")
                .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == false
                    && x.IsAnonym == false && x.UserName == userName && x.IsApproved == true)
                .OrderByDescending(x => x.CreateDate);
            }
            else if (type == CommentType.Blogs)
            {
                return entities.mytrip_articlescomments
                 .Include("mytrip_articles.mytrip_articlescategory")
                 .Where(x => x.mytrip_articles.mytrip_articlescategory.Blog == true
                     && x.IsAnonym == false && x.UserName == userName && x.IsApproved == true)
                 .OrderByDescending(x => x.CreateDate);
            }
            else
            {
                return entities.mytrip_articlescomments
                 .Include("mytrip_articles.mytrip_articlescategory")
                 .Where(x => x.IsAnonym == false && x.UserName == userName && x.IsApproved == false)
                 .OrderByDescending(x => x.CreateDate);
            }
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
               .Where(x => x.ArticleId == articleId && x.IsApproved == false);
            foreach (var item in ac)
            {
                item.IsApproved = true;
            }
            entities.SaveChanges();
        }
        /// <summary>
        /// Получить неодобренные комментарии
        /// </summary>
        /// <param name="count">количество комментариев</param>
        /// <param name="forBlog">для блога</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlescomments> GetComments(int count)
        {
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.IsApproved == false)
               .OrderByDescending(x => x.CreateDate).Take(count);
        }
        /// <summary>
        /// Получить колличество неодобренные комментарии для модерации
        /// </summary>
        /// <param name="username">username</param>
        /// <returns></returns>
        public int GetCountModeration(string username)
        {
            // || x.mytrip_articles.mytrip_articlescategory.UserName == username
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory")
               .Where(x => x.IsApproved == false)
               .Where(x => x.mytrip_articles.UserName == username)
               .Count();
        }
        public IQueryable<mytrip_articlescomments> GetComments(string username)
        {
            // || x.mytrip_articles.mytrip_articlescategory.UserName == username
            return entities.mytrip_articlescomments.Include("mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2")
               .Where(x => x.IsApproved == false)
               .Where(x => x.mytrip_articles.UserName == username).OrderBy(x => x.CreateDate);
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
                    IsApproved = isApproved,
                    Votes = 0
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
                IsApproved = isApproved,
                Votes = 0
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
            mytrip_articlescomments c = entities.mytrip_articlescomments.Include("mytrip_commentvotes").FirstOrDefault(x => x.CommentId == commentId);
            foreach (var v in c.mytrip_commentvotes.ToList())
            {
                entities.mytrip_commentvotes.DeleteObject(v);
            }
            entities.mytrip_articlescomments.DeleteObject(c);
            entities.SaveChanges();
        }
        #endregion

        /*  РАЗДЕЛ 4  */

        #region Создать оценку комментария
        /// <summary>
        ///  Создать оценку комментария
        /// </summary>
        /// <param name="commentId">commentId</param>
        /// <param name="vote">true for positive and false for negative</param>
        /// <returns>int</returns>
        public int CreateCommentVote(int commentId, bool vote)
        {
            mytrip_commentvotes mcv = new mytrip_commentvotes()
            {
                Id = CreateCommentVoteId(),
                CommentId = commentId,
                UserName = HttpContext.Current.User.Identity.Name
            };
            entities.mytrip_commentvotes.AddObject(mcv);
            var com = entities.mytrip_articlescomments.FirstOrDefault(x => x.CommentId == commentId);
            if (vote)
                com.Votes++;
            else
                com.Votes--;
            entities.SaveChanges();
            return com.Votes;
        }
        /// <summary>
        /// _Создать уникальный commentVoteId
        /// </summary>
        /// <returns>int</returns>
        private int CreateCommentVoteId()
        {
            int commentVoteId;
            for (commentVoteId = 1; entities.mytrip_commentvotes.Count(x => x.Id == commentVoteId) != 0; commentVoteId++) ;
            return commentVoteId;
        }
        #endregion

        #region Проверить голосовал ли юзер
        /// <summary>
        /// Проверить голосовал ли юзер
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns>bool</returns>
        public bool CheckCommentVote(int commentId)
        {
            if (entities.mytrip_commentvotes.Count(x => x.CommentId == commentId && x.UserName == HttpContext.Current.User.Identity.Name) == 0)
                return true;
            else
                return false;
        }
        #endregion

    }
}
