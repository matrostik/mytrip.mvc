using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Rssparser.Repository.DataEntities;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace Mytrip.Rssparser.Models
{
   public class RssparserModel
    {
       public IQueryable<mytrip_rssparser> RssparserCategory { get; set; }
       public IEnumerable<XElement> RssparserContent { get; set; }
       public string Title { get; set; }
       public bool img { get; set; }
       public string link { get; set; }
       public int total { get; set; }
       public bool category { get; set; }

    }
   [MetadataType(typeof(RssparserSettingModel))]
   public class RssparserSettingModel
   {
       public bool unlockRssparser { get; set; }
       [Required(ErrorMessageResourceType = typeof(RssparserLanguage), ErrorMessageResourceName = "nullvalue")]
       public string nameRssparser { get; set; }
   }
   [MetadataType(typeof(ManagerModel))]
   public class ManagerModel
   {
       public int RssId { get; set; }
       [Required(ErrorMessageResourceType = typeof(RssparserLanguage), ErrorMessageResourceName = "title_null")]
       public string Title { get; set; }
       public bool AllCulture { get; set; }
       [Required(ErrorMessageResourceType = typeof(RssparserLanguage), ErrorMessageResourceName = "rssurl_null")]
       [RegularExpression(@"^(http|https)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*$", ErrorMessageResourceType = typeof(RssparserLanguage), ErrorMessageResourceName = "rssurl_wrong")]
       public string RssUrl { get; set; }
       public string ImgUrl { get; set; }
   }
}
