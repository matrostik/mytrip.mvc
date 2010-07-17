using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Rssparser.Repository;
using Mytrip.Rssparser.Repository.DataEntities;

namespace Mytrip.Rssparser.Helpers
{
   public static class RssparserHelpers
   {
       
       

       public static string VotesList(this HtmlHelper html)
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
           return table.ToString();
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
           TagBuilder imgEdit = new TagBuilder("img");
           imgEdit.MergeAttribute("src", "/Content/images/edite.png");
           imgEdit.MergeAttribute("title", RssparserLanguage.edit);
           imgEdit.MergeAttribute("style", "width:14px;border:0px;");
           TagBuilder imgDelete = new TagBuilder("img");
           imgDelete.MergeAttribute("src", "/Content/images/delete.png");
           imgDelete.MergeAttribute("title", RssparserLanguage.delete);
           imgDelete.MergeAttribute("style", "width:14px;border:0px;");
           TagBuilder EditCategory = new TagBuilder("a");
           EditCategory.MergeAttribute("href", linkEdit);
           EditCategory.InnerHtml = imgEdit.ToString();
           TagBuilder DeleteCategory = new TagBuilder("a");
           DeleteCategory.MergeAttribute("href", linkDelete);
           DeleteCategory.MergeAttribute("onclick", "return confirm ('" + RssparserLanguage.are_you_sure + "');");
           DeleteCategory.InnerHtml = imgDelete.ToString();
           return EditCategory.ToString() + " " + DeleteCategory.ToString() + " ";
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
