using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Rssparser.Repository;
using System.Web;
using Mytrip.Rssparser.Repository.DataEntities;
using System.Xml.Linq;
using System.Web.Mvc;
using Mytrip.Mvc;

namespace Mytrip.Rssparser
{
    public static class Export
    {
        public static string MenuRssparser()
        {
            RssparserSetting artset = new RssparserSetting();
            if (artset.unlockRssparser())
            {
                string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
                string Controller = urlpath[1];
                RssparserRepository ar = new RssparserRepository();
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Rssparser/Index/1/10/0/Rssparser");
                article.InnerHtml = artset.nameRssparser();
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.GetAllRssparsers(HttpContext.Current.Session["culture"].ToString()))
                {

                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Rssparser/Index/1/10/" + item.RssparserId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.Add(key, a.ToString());
                    key++;
                } bool drop = true;
                if (key == 1)
                    drop = false;
                if (Controller == "Rssparser")
                    tab_category = true;
                return GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop);
            }
            else { return string.Empty; }
        }

        public static string SettingRssparser()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Rssparser/RssparserSetting");
            a.InnerHtml = RssparserLanguage.Rssparsersetting;
            li.InnerHtml = a.ToString();
            return li.ToString();

        }
        public static string ManagerRssparser()
        {
            TagBuilder li_votes = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Rssparser/Manager");
            a_archive.InnerHtml = RssparserLanguage.rssparser_manager;
            li_votes.InnerHtml = a_archive.ToString();
            return li_votes.ToString();
        }

        public static string HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            RssparserRepository ar = new RssparserRepository();
            int take = line * column;
            bool img;
            bool culture;
            string link=string.Empty;
            string title = string.Empty; string rsslink = string.Empty;
            IEnumerable<XElement> articles = ar.GetRssparsers(take,categoryId,HttpContext.Current.Session["culture"].ToString(),
                out img,out link,out title,out culture,out rsslink);
            if (culture)
            {
                int _count = articles.Count();
                if (column > _count)
                    column = _count;
                int _count2 = 0;
                int _line = 1;
                if (_count > column)
                {
                    Math.DivRem(_count, column, out _count2);
                    _line = (int)Math.Ceiling((double)_count / column);
                }
                int count = 1;
                int tr = 0;
                int width = 100;
                if (column > 0)
                    width = 100 / column;
                string CategoryName = title;
                string CategoryPath = GeneralMethods.DecodingString(title);
                StringBuilder result = new StringBuilder();

                TagBuilder table = new TagBuilder("table");
                int _line2 = 0;
                string finaltr = string.Empty;
                string start = string.Empty;
                string end = string.Empty;
                string styletable = string.Empty;
                foreach (var item in articles)
                {
                    string _content = string.Empty;
                    string desc = item.Element("description").Value;
                        string url = item.Element("link").Value;
                        char[] sec = {'<','>'};
                        string[] _desc = desc.Split(sec);
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
                        desc = desc2;
                  if (desc.Length > content)
                      desc = desc.Remove(content) + "...";
                    if (imgwidth > 0)
                    {
                        
                  if (img)
                  {
                      string urlimg = item.Element("enclosure").Attribute("url").Value;
                      int imglength = urlimg.Length;
                      if (urlimg.IndexOf(".jpg") != -1 && urlimg.IndexOf(".jpg") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".jpg");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }
                      else if (urlimg.IndexOf(".png") != -1 && urlimg.IndexOf(".png") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".png");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }
                      else if (urlimg.IndexOf(".gif") != -1 && urlimg.IndexOf(".gif") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".gif");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }
                     _content="<img src=\""+urlimg+ "\" alt=\"\" style=\"position: relative; width: "+imgwidth+"px; float: right\" />";
                      
                      
                       }
                    }
                    _content += "<b><a href=\"" + url + "\" class=\"hometitle\">" + item.Element("title").Value + "</a><b><br/>" + desc;
                    int tr2 = 0;
                    int _line3 = 0;
                    result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                        count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
                    tr = tr2;
                    _line2 = _line3;                    
                    count++;
                }
                if (tr > 0 && tr % 2 != 0)
                    result.AppendLine(finaltr);
                table.AddCssClass(styletable);
                table.InnerHtml = result.ToString();
                RssparserSetting rssset=new RssparserSetting();
                
                string LangName = "<a href=\"/Rssparser/Index/1/10/" + categoryId + "/" + CategoryPath + "\">" + CategoryName + "</a>";
                if(!rssset.unlockRssparser())
                    LangName = "<a href=\"" + link + "\">" + CategoryName + "</a>";               
                string _CategoryName =string.Empty;
                if(viewtitle)
                _CategoryName = "<h3 class=\"title\">" + LangName +" <a href=\""+rsslink+"\">"+imgrss()+"</a></h3>";
                if (column > 0)
                    return _CategoryName + start + table.ToString() + end;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
        private static string imgrss()
        {
            ThemeSetting theme = new ThemeSetting();
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/rss.png");
            img.MergeAttribute("alt", "Rss ");
            img.MergeAttribute("style", "width:14px;border:0px;");
            return img.ToString();

        }
    }
}
