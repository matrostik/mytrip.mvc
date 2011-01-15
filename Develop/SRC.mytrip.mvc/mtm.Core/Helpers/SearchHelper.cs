using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mtm.Core.Repository;
using mtm.Core.Settings;
using System.Xml.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace mtm.Core.Helpers
{
    public static class SearchHelper
    {
        public static HtmlString SearchPage(this HtmlHelper html,object searchQuestion,int page,int size,string search,out int total)
        {
            var acts = new List<SearchQuestion>();
            IDictionary<string, object> _menu = (searchQuestion == null ? new RouteValueDictionary() : new RouteValueDictionary(searchQuestion));
            foreach (string key in _menu.Keys)
            {
                var a = _menu[key] as List<SearchQuestion>;
                acts.AddRange(a);
            }
            ICoreRepository core = new ICoreRepository();
            var corpage = core.corePageRepo.GetPagesForSearch(LocalisationSetting.culture(), search);
            foreach (var a in corpage)
            {
                string abstracts = string.Empty;
                if (a.Title != null && a.Title.Length > 1)
                    a.Title = GeneralMethods.ReplaceString(a.Title, search);
                if (a.Body != null && a.Body.Length > 1)
                {
                    char[] sec = { '<', '>' };
                    string[] _desc = a.Body.Split(sec);
                    bool _sec = true;
                    string desc2 = string.Empty;
                    foreach (string x in _desc)
                    {
                        if (_sec && x.IndexOf("href=") == -1 && x.IndexOf("src=") == -1 && x.IndexOf("href =") == -1 && x.IndexOf("src =") == -1)
                        {
                            desc2 += x;
                            _sec = false;
                        }
                        else
                            _sec = true;

                    }
                    
                    if (desc2.Length <= 1000)
                        abstracts = a.Body;
                    else
                        abstracts = desc2.Remove(1000) + "...";
                    abstracts = abstracts.Replace("<BR>", " ");
                    abstracts = abstracts.Replace("<BR/>", " ");
                    abstracts = abstracts.Replace("<br/>", " ");
                    abstracts = GeneralMethods.ReplaceString(abstracts, search);
                }
                string content = "<div class='content'>"
  +"<h3 class='title'><a href='/Home/Page/"+a.PageId+"/"+a.Path+"'>"+a.Title+"</a></h3>"
  +abstracts                     
  + "</div><div class='last'></div>";
                SearchQuestion s = new SearchQuestion(content, DateTime.Now, "Core");
                acts.Add(s);
            }
            StringBuilder result = new StringBuilder();
            total = acts.Count();
            foreach (var b in acts.OrderByDescending(x => x.Date).Skip((page - 1) * size).Take(size))
            {
                result.AppendLine(b.Content);
            }
            return new HtmlString(result.ToString());
        }
    }
    public class SearchQuestion
    {
        private string content;
        private DateTime date;
        private string namemodule;
        /// <summary>
        /// Empty constructor
        /// </summary>
        public SearchQuestion()
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="place">место</param>
        /// <param name="activity">действие</param>
        /// <param name="date">дата</param>
        public SearchQuestion(string content, DateTime date, string namemodule)
        {
            Content = content;
            Date = date;
            Namemodule = namemodule;
        }
        /// <summary>
        /// Date
        /// Дата
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        /// <summary>
        /// Action
        /// Действие
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public string Namemodule
        {
            get { return namemodule; }
            set { namemodule = value; }
        }
    }
}