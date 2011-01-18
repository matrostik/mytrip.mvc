using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core;
using mtm.RssParser.Repository;
using mtm.RssParser.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.RssParser.Helpers
{
   public static class RssparserHelpers
   {



       public static HtmlString RssparserList(this HtmlHelper html)
       {
           RssparserRepository rssrepo = new RssparserRepository();
           TagBuilder table = new TagBuilder("table");
           table.MergeAttribute("style", "border:0;padding:0;");
           #region table
           int ctr = 1;
           TagBuilder tr = null;
           var allrss = rssrepo.GetAllRssparsers();
           int count = allrss.Count();
           foreach (var r in allrss)
           {
               if (ctr % 2 == 1)
               {
                   tr = new TagBuilder("tr");
                   tr.MergeAttribute("style", "border:0px;");
                   TagBuilder td1 = new TagBuilder("td");
                   td1.MergeAttribute("style", "vertical-align:top;border:0px;width:50%");
                   td1.InnerHtml = SetAccordion(r);
                   tr.InnerHtml = td1.ToString();
               }
               else
               {
                   TagBuilder td1 = new TagBuilder("td");
                   td1.MergeAttribute("style", "vertical-align:top;border:0px;width:50%");
                   td1.InnerHtml = SetAccordion(r);
                   tr.InnerHtml += td1.ToString();
                   table.InnerHtml += tr.ToString();
               }
               if (count == ctr && ctr % 2 == 1)
               {
                   TagBuilder td1 = new TagBuilder("td");
                   td1.MergeAttribute("style", "vertical-align:top;border:0px;width:50%");
                   tr.InnerHtml += td1.ToString();
                   table.InnerHtml += tr.ToString();
               }
               ctr++;
           }
           #endregion
           return new HtmlString(table.ToString());
       }

       static string SetAccordion(mytrip_rssparser rss )
       {
           TagBuilder div_accordion = new TagBuilder("div");
           div_accordion.AddCssClass("accordion2");
           TagBuilder div_accordiontitle = new TagBuilder("div");
           div_accordiontitle.AddCssClass("accordiontitle2");
           div_accordiontitle.InnerHtml = EditDelete(rss.RssparserId) + "<b>" + rss.Title + "</b> " 
               + Globe(rss.AllCulture) + " " + Flag(rss.Culture);
           div_accordion.InnerHtml = div_accordiontitle.ToString() ;
           return div_accordion.ToString();
       }

       #region Icons
       static string EditDelete(int rssId)
       {
           string linkEdit = "/Rssparser/Edit/" + rssId;
           string linkDelete = "/Rssparser/Delete/" + rssId;
           string a = GeneralMethods.ImgInput("/images/edite.png", linkEdit, "rename", 14) +
                      " " + GeneralMethods.ImgInput("/images/delete.png", linkDelete, "delete", 14);
           return a + " ";
       }
       static string Flag(string culture)
       {
           culture = culture.ToLower();
           TagBuilder img = new TagBuilder("img");
           img.MergeAttribute("src", "/Content/images/" + culture + ".png");
           img.MergeAttribute("style", "border-width:0px;width:15px");
           img.MergeAttribute("alt", culture);
           img.MergeAttribute("title", culture);
           return img.ToString();
       }
       static string Globe(bool show)
       {
           if (!show)
               return string.Empty;
           TagBuilder img = new TagBuilder("img");
           img.MergeAttribute("src", "/Content/images/globe.png");
           img.MergeAttribute("style", "border-width:0px;width:15px");
           img.MergeAttribute("alt", "all languages");
           img.MergeAttribute("title", RssparserLanguage.all_languages);
           return img.ToString();
       }
       #endregion
    }
}
