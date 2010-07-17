using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Mytrip.Mvc.StylesTable;

namespace Mytrip.Mvc
{
    public class GeneralMethods
    {
        #region Перевод из русского на латиницу
        /// <summary>
        /// Перевод из русского на латиницу
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
        public static string DecodingString(string a)
        {
            a = a.Trim();
            #region знаки препинания
            a = a.Replace(" ", "_");
            a = a.Replace("!", "_");
            a = a.Replace(",", "_");
            a = a.Replace(".", "_");
            a = a.Replace("?", "_");
            a = a.Replace(":", "_");
            a = a.Replace("/", "_");
            a = a.Replace("|", "_");
            a = a.Replace("#", "_");
            a = a.Replace("%", "_");
            a = a.Replace("<", "_");
            a = a.Replace(">", "_");
            a = a.Replace("$", "_");
            a = a.Replace("&", "_");
            a = a.Replace("*", "_");
            a = a.Replace(">", "_");
            a = a.Replace("-", "_");
            a = a.Replace("=", "_");
            a = a.Replace("+", "_");
            a = a.Replace("`", "_");
            a = a.Replace("~", "_");
            a = a.Replace("@", "_");
            a = a.Replace("^", "_");
            a = a.Replace("{", "_");
            a = a.Replace("}", "_");
            a = a.Replace("}", "_");
            a = a.Replace("?", "_");
            a = a.Replace("*", "_");
            a = a.Replace("%", "_");
            a = a.Replace(@"\", "_");
            a = a.Replace("\"", "_");
            #endregion
            #region иврит
            a = a.Replace("א", "a");
            a = a.Replace("ב", "b");
            a = a.Replace("ג", "g");
            a = a.Replace("ד", "d");
            a = a.Replace("ה", "a");
            a = a.Replace("ו", "v");
            a = a.Replace("ז", "z");
            a = a.Replace("ח", "h");
            a = a.Replace("ט", "t");
            a = a.Replace("י", "i");
            a = a.Replace("כ", "k");
            a = a.Replace("ך", "k");
            a = a.Replace("ל", "l");
            a = a.Replace("מ", "m");
            a = a.Replace("ם", "m");
            a = a.Replace("נ", "n");
            a = a.Replace("ן", "n");
            a = a.Replace("ס", "s");
            a = a.Replace("ע", "e");
            a = a.Replace("פ", "p");
            a = a.Replace("ף", "f");
            a = a.Replace("צ", "c");
            a = a.Replace("ץ", "c");
            a = a.Replace("ק", "k");
            a = a.Replace("ר", "r");
            a = a.Replace("ש", "sh");
            a = a.Replace("ת", "t");
            #endregion
            #region русский
            a = a.Replace("А", "A");
            a = a.Replace("а", "a");
            a = a.Replace("Б", "B");
            a = a.Replace("б", "b");
            a = a.Replace("В", "V");
            a = a.Replace("в", "v");
            a = a.Replace("Г", "G");
            a = a.Replace("г", "g");
            a = a.Replace("Д", "D");
            a = a.Replace("д", "d");
            a = a.Replace("Е", "E");
            a = a.Replace("е", "e");
            a = a.Replace("Ё", "E");
            a = a.Replace("ё", "e");
            a = a.Replace("Ж", "J");
            a = a.Replace("ж", "j");
            a = a.Replace("З", "Z");
            a = a.Replace("з", "z");
            a = a.Replace("И", "I");
            a = a.Replace("и", "i");
            a = a.Replace("Й", "Y");
            a = a.Replace("й", "y");
            a = a.Replace("К", "K");
            a = a.Replace("к", "k");
            a = a.Replace("Л", "L");
            a = a.Replace("л", "l");
            a = a.Replace("М", "M");
            a = a.Replace("м", "m");
            a = a.Replace("Н", "N");
            a = a.Replace("н", "n");
            a = a.Replace("О", "O");
            a = a.Replace("о", "o");
            a = a.Replace("П", "P");
            a = a.Replace("п", "p");
            a = a.Replace("Р", "R");
            a = a.Replace("р", "r");
            a = a.Replace("С", "S");
            a = a.Replace("с", "s");
            a = a.Replace("Т", "T");
            a = a.Replace("т", "t");
            a = a.Replace("У", "U");
            a = a.Replace("у", "u");
            a = a.Replace("Ф", "F");
            a = a.Replace("ф", "f");
            a = a.Replace("Х", "H");
            a = a.Replace("х", "h");
            a = a.Replace("Ц", "C");
            a = a.Replace("ц", "c");
            a = a.Replace("Ч", "Ch");
            a = a.Replace("ч", "ch");
            a = a.Replace("Ш", "Sh");
            a = a.Replace("ш", "sh");
            a = a.Replace("Щ", "Sh");
            a = a.Replace("щ", "sh");
            a = a.Replace("Ъ", "");
            a = a.Replace("ъ", "");
            a = a.Replace("Ы", "Y");
            a = a.Replace("ы", "y");
            a = a.Replace("Ь", "");
            a = a.Replace("ь", "");
            a = a.Replace("Э", "E");
            a = a.Replace("э", "e");
            a = a.Replace("Ю", "Yu");
            a = a.Replace("ю", "yu");
            a = a.Replace("Я", "Ya");
            a = a.Replace("я", "ya");
            #endregion
            return a;

        }
        #endregion

        #region Подсветка результатов поиска
        public static string ReplaceString(string original, string findText)
        {
            string insert1 = "<span style='background-color: #ffff00; color: #ff0000;'>";
            string insert2 = "</span>";
            int at1 = 0;
            while (true)
            {
                at1 = original.IndexOf(findText, at1, original.Length - at1, StringComparison.OrdinalIgnoreCase);

                if (at1 == -1)
                    break;

                original = original.Insert(at1, insert1).Insert(at1 + findText.Length + insert1.Length, insert2);

                at1 += insert1.Length + insert2.Length;
            }
            return original;
        }
        #endregion

        #region Кодировка спец символов в поиске
        public static string DecodingSearch(string search)
        {
            search = search.Replace("?", "[x_1_x]");
            search = search.Replace("<", "[x_2_x]");
            search = search.Replace(">", "[x_3_x]");
            search = search.Replace("*", "[x_4_x]");
            search = search.Replace("%", "[x_5_x]");
            search = search.Replace("&", "[x_6_x]");
            search = search.Replace(@"\", "[x_7_x]");
            search = search.Replace("|", "[x_8_x]");
            search = search.Replace(":", "[x_9_x]");
            search = search.Replace("\"", "[x_10_x]");
            search = search.Replace(".", "[x_11_x]");
            search = search.Replace("/", "[x_12_x]");
            return search;
        }
        public static string DecodingSearch2(string search)
        {
            search = search.Replace("[x_1_x]", "?");
            search = search.Replace("[x_2_x]", "<");
            search = search.Replace("[x_3_x]", ">");
            search = search.Replace("[x_4_x]", "*");
            search = search.Replace("[x_5_x]", "%");
            search = search.Replace("[x_6_x]", "&");
            search = search.Replace("[x_7_x]", @"\");
            search = search.Replace("[x_8_x]", "|");
            search = search.Replace("[x_9_x]", ":");
            search = search.Replace("[x_10_x]", "\"");
            search = search.Replace("[x_11_x]", ".");
            search = search.Replace("[x_12_x]", "/");
            return search;
        }
        #endregion

        #region Чистка кода в редакторе
        public static string CleanHtmlCode(string code)
        {
            while (code.StartsWith("<P>&nbsp;</P>"))
            {
                code = code.Remove(0, 13);
            }
            while (code.EndsWith("<P>&nbsp;</P>"))
            {
                code = code.Remove(code.Length - 13, 13);
            }
            code = code.Replace("<P>&nbsp;</P>", "<br/>").Replace("<P>", "").Replace("</P>", "<br/>");
            return code;
        }
        #endregion
        public static string dllAbsoluteDirectory(string filename)
        {
            return HttpContext.Current.Server.MapPath("/bin/" + filename + ".dll");
        }
        #region XML
        public static string xmlAbsoluteDirectory(string filename)
        {
            return HttpContext.Current.Server.MapPath("/Configuration/" + filename + ".xml");
        }
        public static bool xmlLoadFile(string filename)
        {
            bool _result = false;
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Configuration");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            FileInfo[] result = _absolutDirectory.GetFiles();
            foreach (FileInfo x in result)
            {
                if ((filename + ".xml") == x.Name)
                    _result = true;
            }
            return _result;
        }
        #endregion

        #region Cache
        public static T GetObjectFromCache<T>(string key, Func<T> getObject, Action<Cache, string, T> addToCache) where T : class
        {
            T obj = HttpContext.Current.Cache.Get(key) as T;
            if (obj == null)
            {
                obj = getObject();
                addToCache(HttpContext.Current.Cache, key, obj);
            }
            return obj;
        }
        #endregion

        #region Helpers
        public static string[] UrlDictionary(string urlPath)
        {
            string[] _urlPath = urlPath.Split('/');
            return _urlPath;

        }
        public static string Image(string url, int width, int height, string alt, int border)
        {
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = "border-width: " + border + "px; width: " + width + "px; height: " + height + "px;";
            if (width == 0 && height > 0)
                style = "border-width: " + border + "px; height: " + height + "px;";
            if (width > 0 && height == 0)
                style = "border-width: " + border + "px; width: " + width + "px;";
            if (width == 0 && height == 0)
                style = "border-width: " + border + "px;";
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("style", style);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }
        public static string Image(string url, string alt, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            _result.AddCssClass(cssClass);
            result.Append(_result.ToString());
            return result.ToString();
        }
        public static string Image(string url, string alt)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }
        public static string Flag(string culture)
        {
            culture = culture.ToLower();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/" + culture + ".png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", culture);
            img.MergeAttribute("title", culture);
            return img.ToString();
        }
        public static string Globe(bool show, string title)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/globe.png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", "all languages");
            img.MergeAttribute("title", title);// VotesLanguage.all_languages);
            return img.ToString();
        }
        public static string Keys(bool show, string title)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/Keys.png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", "for registered users");
            img.MergeAttribute("title", title);// VotesLanguage.only_for_registered_users);
            return img.ToString();
        }
        public static string Accordion(string id,string title,string content)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.MergeAttribute("id", id);
            div_accordion.AddCssClass("accordion");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.MergeAttribute("id", "_"+id);
            div_accordiontitle.AddCssClass("accordiontitle");
            string _title = "<div><div class=\"accordiontopright\"></div><div class=\"accordiontopleft\"></div>" +
                "<div class=\"accordiontopcon\"></div></div><div class=\"accordioncontent\">" + title +
                "</div><div><div class=\"accordionbottomright\"></div><div class=\"accordionbottomleft\"></div>"+
        "<div class=\"accordionbottomcon\"></div></div>";
            div_accordiontitle.InnerHtml = _title;
            TagBuilder div_accordioncontent = new TagBuilder("div");
            div_accordioncontent.AddCssClass("accordioncontentground");
            string _content = "<div><div class=\"accordiontoprightvisible\"></div><div class=\"accordiontopleftvisible\">" +
                "</div><div class=\"accordiontopconvisible\"></div></div><div class=\"accordioncontentvisible\">" + content +
                "</div><div><div class=\"accordionbottomrightvisible\"></div><div class=\"accordionbottomleftvisible\">"+
        "</div><div class=\"accordionbottomconvisible\"></div></div>";
            div_accordioncontent.InnerHtml = _content;
            div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            result.AppendLine(div_accordion.ToString() + "<div class=\"acfooter\"></div>");
            return result.ToString();
            /*
<div class="accordioncontentground">
<div>
                <div class="accordiontoprightvisible">
                </div>
                <div class="accordiontopleftvisible">
                </div>
                <div class="accordiontopconvisible">
                </div>
            </div>
            <div class="accordioncontentvisible">
Поддержка проекта<br />Поддержка проекта<br />Поддержка проекта<br />Поддержка проекта<br /></div>
        <div>
        <div class="accordionbottomrightvisible">
        </div>
        <div class="accordionbottomleftvisible">
        </div>
        <div class="accordionbottomconvisible">
        </div>
    </div>
</div>
</div>*/
        }
        public static string Accordion2(string id, string title, string content)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.MergeAttribute("id", id);
            div_accordion.AddCssClass("accordion");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.MergeAttribute("id", "_" + id);
            div_accordiontitle.AddCssClass("accordiontitle");
            string _title = "<div><div class=\"accordiontopright\"></div><div class=\"accordiontopleft\"></div>" +
                "<div class=\"accordiontopcon\"></div></div><div class=\"accordioncontent\">" + title +
                "</div><div><div class=\"accordionbottomright\"></div><div class=\"accordionbottomleft\"></div>" +
        "<div class=\"accordionbottomcon\"></div></div>";
            div_accordiontitle.InnerHtml = _title;
            TagBuilder div_accordioncontent = new TagBuilder("div");
            div_accordioncontent.AddCssClass("accordioncontentground");
            string _content = "<div><div class=\"accordiontoprightvisible\"></div><div class=\"accordiontopleftvisible\">" +
                "</div><div class=\"accordiontopconvisible\"></div></div><div class=\"accordioncontentvisible2\">" + content +
                "</div><div><div class=\"accordionbottomrightvisible\"></div><div class=\"accordionbottomleftvisible\">" +
        "</div><div class=\"accordionbottomconvisible\"></div></div>";
            div_accordioncontent.InnerHtml = _content;
            div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            result.AppendLine(div_accordion.ToString() + "<div class=\"acfooter\"></div>");
            return result.ToString();
        }
        public static string Accordion2(string id,bool accordion, string title, string content)
        {
            string a = string.Empty;
            if (!accordion)
                a = "no";
            StringBuilder result = new StringBuilder();
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.MergeAttribute("id", id);
            div_accordion.AddCssClass(a+"accordion");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.MergeAttribute("id", "_" + id);
            div_accordiontitle.AddCssClass("accordiontitle");
            string _title = "<div><div class=\"" + a + "accordiontopright\"></div><div class=\"" + a + "accordiontopleft\"></div>" +
                "<div class=\"" + a + "accordiontopcon\"></div></div><div class=\"" + a + "accordioncontent\">" + title +
                "</div><div><div class=\"" + a + "accordionbottomright\"></div><div class=\"" + a + "accordionbottomleft\"></div>" +
        "<div class=\"" + a + "accordionbottomcon\"></div></div>";
            div_accordiontitle.InnerHtml = _title;
            if (accordion)
            {
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontentground");
                string _content = "<div><div class=\"accordiontoprightvisible\"></div><div class=\"accordiontopleftvisible\">" +
                    "</div><div class=\"accordiontopconvisible\"></div></div><div class=\"accordioncontentvisible2\">" + content +
                    "</div><div><div class=\"accordionbottomrightvisible\"></div><div class=\"accordionbottomleftvisible\">" +
            "</div><div class=\"accordionbottomconvisible\"></div></div>";
                div_accordioncontent.InnerHtml = _content;
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            }
            else
                div_accordion.InnerHtml = div_accordiontitle.ToString();
            result.AppendLine(div_accordion.ToString() + "<div class=\"acfooter\"></div>");
            return result.ToString();
        }

        public static string StyleTable(int column, int style, int tr, int width,string content,
            int count,int count2,int line,int line2,out int outtr, out int outline, out string finaltr,
            out string start,out string end,out string styletable)
        {
            outtr = 0;
            outline = 0;
            finaltr = string.Empty;
            start = string.Empty;
            end = string.Empty;
            styletable = string.Empty;
            string result = string.Empty;
            if (style == 1)
                result = StyleTableClass.StyleTable1(column, tr, width, content, count, count2, line, line2, out outtr, out outline, out finaltr, out start, out end, out styletable);
            else if (style == 2)
                result = StyleTableClass.StyleTable2(column, width, content, count, count2, line, line2, out outline,out styletable);
           return result;
        }
        public static string CoreRating(bool approvedvotes, bool active, double totalvote, int votescount)
        {
            StringBuilder result = new StringBuilder();
            if (approvedvotes)
            {
                if (votescount != -1)
                    result.AppendLine(String.Format(CoreLanguage.score_votes, totalvote.ToString("N2"), votescount.ToString()));
                ThemeSetting theme = new ThemeSetting();
                for (int rate = 0; rate < 5; rate++)
                {
                    double rate12 = rate + 0.125;
                    double rate37 = rate + 0.375;
                    double rate62 = rate + 0.625;
                    double rate87 = rate + 0.875;
                    if (active)
                    {
                        TagBuilder input = new TagBuilder("input");
                        input.MergeAttribute("type", "submit");
                        input.MergeAttribute("value", (rate + 1).ToString());
                        input.MergeAttribute("name", "vote");
                        input.MergeAttribute("id", "vote" + (rate + 1));
                        string num = "";
                        if (totalvote > rate12 && totalvote < rate37)
                            num = "25";
                        else if (totalvote > rate37 && totalvote < rate62)
                            num = "50";
                        else if (totalvote > rate62 && totalvote < rate87)
                            num = "75";
                        else if (totalvote < rate87)
                            num = "100";
                        input.MergeAttribute("style", "background:url('/Theme/" + theme.theme() + "/images/star" + num + ".png')");
                        input.AddCssClass("rating");
                        input.MergeAttribute("title", (rate + 1).ToString());
                        if (!HttpContext.Current.User.Identity.IsAuthenticated)
                            input.MergeAttribute("onclick", "location.href('/Account/LogOn?returnUrl=" + HttpContext.Current.Request.Url.AbsolutePath + "')");
                        result.AppendLine(input.ToString());
                    }
                    else
                    {
                        TagBuilder input = new TagBuilder("img");
                        if (totalvote > rate12 && totalvote < rate37)
                            input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star25.png");
                        if (totalvote > rate37 && totalvote < rate62)
                            input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star50.png");
                        if (totalvote > rate62 && totalvote < rate87)
                            input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star75.png");
                        if (totalvote < rate87)
                            input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star100.png");
                        if (totalvote > rate87)
                            input.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/star.png");
                        input.MergeAttribute("style", "width: 15px; height: 15px;");
                        result.AppendLine(input.ToString());
                    }
                }
            }
            return result.ToString();
        }
        public static string ImageForAbstract(string image, int width)
        {
            if (image != null && image.IndexOf("src") != -1)
            {
                int q = image.IndexOf("src");
                image = image.Remove(0, q);
                int qq = image.IndexOf("\"");
                image = image.Remove(0, (qq + 1));
                int qqq = image.IndexOf("\"");
                image = image.Remove(qqq);
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", image);
                img.MergeAttribute("alt", "");
                img.AddCssClass("imgabstract");
                img.MergeAttribute("style", "width:" + width + "px; border:0;");
                return img.ToString();
            }
            else
                return string.Empty;
        }
        public static string Menu(string title, IDictionary<int, string> content, bool visible, bool warning, bool menu, bool drop)
        {
            string drop2 = string.Empty;
            string _drop = string.Empty;
            if (drop)
            {
                // drop2 = "<span style=\"font-size:x-small;\">&nbsp;&nbsp;&nabla;</span>";
                _drop = "drop";
            }
            string _menu = "menu";
            if (!menu)
                _menu = "logon";
            string _visible = string.Empty;
            if (warning && !visible)
                _visible = "warning";
            if (visible)
                _visible = "visible";
            StringBuilder _result = new StringBuilder();
            bool count = false;
            if (content != null)
            {
                foreach (var x in content)
                {
                    _result.AppendLine("<div class=\"" + _menu + "li\">" + x.Value + "</div>");
                    count = true;
                }
            }
            string _content = string.Empty;
            if (count)
                _content = "<div class=\"" + _menu + "ul\">" + _result + "</div>";
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class=\"item" + _menu + "\"><div>");
            result.AppendLine("<div class=\"" + _menu + "topright" + _visible + "\"></div>");
            result.AppendLine("<div class=\"" + _menu + "topleft" + _visible + "\"></div>");
            result.AppendLine("<div class=\"" + _menu + "topcon" + _visible + "\"></div></div>");
            result.AppendLine("<div class=\"" + _menu + "content" + _visible + _drop + "\" >");
            result.AppendLine(title + drop2);
            result.AppendLine("</div><div><div class=\"" + _menu + "bottomright" + _visible + "\"></div>");
            result.AppendLine("<div class=\"" + _menu + "bottomleft" + _visible + "\"></div>");
            result.AppendLine("<div class=\"" + _menu + "bottomcon" + _visible + "\"></div></div>" + _content + "</div>");
            return result.ToString();
        }
        #endregion
    }

}
