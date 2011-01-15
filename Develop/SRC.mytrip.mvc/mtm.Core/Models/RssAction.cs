/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace mtm.Core.Models
{
    /// <summary>
    /// Преобразование результата в xml поток
    /// </summary>
    public class RssFeedResult : ActionResult
    {
        #region Преобразование результата в xml поток
        // **********************************************
        // Преобразование результата в xml поток
        // **********************************************

        /// <summary>
        /// Преобразование результата в xml поток
        /// </summary>
        /// <param name="feed"></param>
        public RssFeedResult(SyndicationFeed feed)
        {
            RssFeed = feed;
        }
        /// <summary>
        /// Преобразование результата в xml поток
        /// </summary>
        public SyndicationFeed RssFeed { get; set; }

        /// <summary>
        /// Преобразование результата в xml поток
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase resp = context.HttpContext.Response;
            resp.ContentType = "application/rss+xml";
            Rss20FeedFormatter rss = new Rss20FeedFormatter(RssFeed);
            using (XmlWriter xml = XmlWriter.Create(resp.Output))
            {
                rss.WriteTo(xml);
            }
        }

        //****************** E N D **********************
        #endregion
    }
}
