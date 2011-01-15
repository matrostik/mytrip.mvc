using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Helpers;
using mtm.Store.Repository;
using mtm.Core.Settings;
using System.Web;

namespace mtm.Store.Helpers
{
    public static class SearchHelpers
    {
        public static List<SearchQuestion> Search(string search)
        {
            IStoreRepository store = new IStoreRepository();
            List<SearchQuestion> searchQuestion = new List<SearchQuestion>();
            var a = store.product.GetProductForSearch(LocalisationSetting.culture(), search);

            foreach (var x in a)
            {
                string title = null;
                    if (x.Title != null && x.Title.Length > 1)
                        x.Title = GeneralMethods.ReplaceString(x.Title, search);
                    if (x.Body != null && x.Body.Length > 1)
                        x.Body = GeneralMethods.ReplaceString(x.Body, search);
                    if (x.NamberCatalog != null && x.NamberCatalog.Length > 1)
                        x.NamberCatalog = GeneralMethods.ReplaceString(x.NamberCatalog, search);
                    title = "<div class='content'>" + ProductHelpers.ViewProduct(x, 10000, 100)
                        + "</div><div class='last'></div>";
                SearchQuestion b = new SearchQuestion(title, x.CreationDate, "Store");
                searchQuestion.Add(b);
            }
            return searchQuestion;
        }
    }
}
