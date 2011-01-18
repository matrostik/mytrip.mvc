using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core;
using mtm.Weather.Repository;
using mtm.Weather.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Weather.Helpers
{
   public static class WeatherHelpers
   {



       public static HtmlString WeatherList(this HtmlHelper html)
       {
           WeatherRepository rssrepo = new WeatherRepository();
           TagBuilder table = new TagBuilder("table");
           table.MergeAttribute("style", "border:0;padding:0;");
           #region table
           int ctr = 1;
           TagBuilder tr = null;
           var allrss = rssrepo.GetWeatherForManager();
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

       static string SetAccordion(mytrip_weather rss )
       {
           TagBuilder div_accordion = new TagBuilder("div");
           div_accordion.AddCssClass("accordion2");
           TagBuilder div_accordiontitle = new TagBuilder("div");
           div_accordiontitle.AddCssClass("accordiontitle2");
           div_accordiontitle.InnerHtml = EditDelete(rss.weatherId) + "<b>" + rss.Title + "</b> " 
               + Globe(rss.AllCulture) + " " + Flag(rss.Culture);
           div_accordion.InnerHtml = div_accordiontitle.ToString() ;
           return div_accordion.ToString();
       }

       #region Icons
       static string EditDelete(int rssId)
       {
           string linkEdit = "/Weather/Edit/" + rssId;
           string linkDelete = "/Weather/Delete/" + rssId;
           string a = GeneralMethods.ImgInput("/images/edite.png", linkEdit, "rename", 14) +
                      " " + GeneralMethods.ImgInput("/images/delete.png", linkDelete, "delete", 14);
           return a + " ";
       }
       static string Flag(string culture)
       {
           return GeneralMethods.Flag(culture);
       }
       static string Globe(bool show)
       {
           return GeneralMethods.Globe(show,"");
       }
       #endregion
    }
}
