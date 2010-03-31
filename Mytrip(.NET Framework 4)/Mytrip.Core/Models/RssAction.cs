using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Mytrip.Core.Models
{
    public class RssFeedResult : ActionResult
    {
        public RssFeedResult(SyndicationFeed feed)
        {
            RssFeed = feed;
        }
        public SyndicationFeed RssFeed { get; set; }
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
    }
}
