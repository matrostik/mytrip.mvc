using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Articles.Repository.DataEntities;
using System.Web;
using Mytrip.Mvc.Repository;

namespace Mytrip.Articles.Repository
{
    public class SubscriptionRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Entity Repository connection
        Entities _entities;
        Entities entities
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

        #region isSubscribed
        public bool isSubscribed(int articleId)
        {
            int count = entities.mytrip_articlessubscription.Where(x => x.ArticleId == articleId 
                    && x.UserName == HttpContext.Current.User.Identity.Name).Count();
            if (count == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Subscribe
        public bool Subscribe(int articleId)
        {
            if (!isSubscribed(articleId))
            {
                mytrip_articlessubscription mas = new mytrip_articlessubscription()
                {
                    SubscribeId = CreateSubscribeId(),
                    ArticleId = articleId,
                    UserName = HttpContext.Current.User.Identity.Name
                };
                entities.mytrip_articlessubscription.AddObject(mas);
                entities.SaveChanges();
                return true;
            }
            else
            {
                var subs = entities.mytrip_articlessubscription.FirstOrDefault(x => x.ArticleId == articleId && x.UserName == HttpContext.Current.User.Identity.Name);
                entities.DeleteObject(subs);
                entities.SaveChanges();
                return false;
            }
        }
        #endregion
        
        #region  Get subscriptions by ArticleId
        /// <summary>
        /// Get subscriptions by ArticleId
        /// </summary>
        /// <param name="articleId">subscribeId</param>
        /// <returns></returns>
        public IQueryable<mytrip_articlessubscription> GetSubsciptions(int articleId)
        {
            return entities.mytrip_articlessubscription
                .Where(x => x.ArticleId == articleId);
        }
        #endregion

        #region Get subscription by Id
        /// <summary>
        /// Get subscription by Id
        /// </summary>
        /// <param name="subscribeId">subscribe Id</param>
        /// <returns></returns>
        public mytrip_articlessubscription GetSubsciption(int subscribeId)
        {
            return entities.mytrip_articlessubscription
                .FirstOrDefault(x=>x.SubscribeId==subscribeId);
        }
        #endregion

        #region Create unique SubscribeId
        private int CreateSubscribeId()
        {
            int subsId;
            for (subsId = 1; entities.mytrip_articlessubscription.Count(x=>x.SubscribeId==subsId) != 0; subsId++) ;
            return subsId;
        }
        #endregion
    }
}
