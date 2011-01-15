using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Helpers;
using mtm.Tourism.Repository;
using mtm.Core.Settings;

namespace mtm.Tourism.Helpers
{
   public static class SearchHelpers
   {
       public static List<SearchQuestion> Search(string search)
       {
           ToursRepository store = new ToursRepository();
           List<SearchQuestion> searchQuestion = new List<SearchQuestion>();
           var a = store.GetToursForSearch(search,LocalisationSetting.culture());

           foreach (var x in a)
           {
               string title = null;
               if (x.Title != null && x.Title.Length > 1)
                   x.Title = GeneralMethods.ReplaceString(x.Title, search);
               if (x.Body != null && x.Body.Length > 1)
                   x.Body = GeneralMethods.ReplaceString(x.Body, search);
               StringBuilder _content = new StringBuilder();
               _content.Append(GeneralMethods.ImageForAbstract(x.Imige, ModuleSetting.widthImgTours()));
               _content.Append("<h3 class='title'><a href='/Tours/View/" + x.TourId + "/" + x.Path + "'>" + x.Title + "</a></h3>");
               var date = x.StopDate - x.StartDate;
               _content.Append("<b>" + string.Format(ToursLanguage.DateTour, string.Format("{0:dd MMMM yyyy}", x.StartDate), string.Format("{0:dd MMMM yyyy}", x.StopDate), date.Days + 1) + "</b><br/>");
               _content.Append("<b class='sale'>" + string.Format(ToursLanguage.priceMin, MoneyHelpers.ConvertMoney(x.MoneyId, x.MinPrice)) + "</b><br/>");
               if (ModuleSetting.viewDescription())
                   _content.Append(x.Body + "<br/>");
               if (x.mytrip_tourscategory.SubCategoryId == 0)
               {
                   _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + x.CategoryId + "/" + x.mytrip_tourscategory.Path + "'>" + x.mytrip_tourscategory.Title + "</a><br/>");
               }
               else if (x.mytrip_tourscategory.SubCategoryId > 0)
               {
                   _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + x.mytrip_tourscategory.SubCategoryId + "/" + x.mytrip_tourscategory.mytrip_tourscategory2.Path + "'>" + x.mytrip_tourscategory.mytrip_tourscategory2.Title + "</a><br/>");
                   _content.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + x.CategoryId + "/" + x.mytrip_tourscategory.Path + "'>" + x.mytrip_tourscategory.Title + "</a><br/>");
               }
               title = "<div class='content'>" + _content
                   + "</div><div class='last'></div>";
               SearchQuestion b = new SearchQuestion(title, x.CreateDate, "Tourism");
               searchQuestion.Add(b);
           }
           return searchQuestion;
       }
    }
}
