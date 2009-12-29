using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Mytrip.Mvc.Web.Linq2sql.HtmlHelpers
{
    public static class PagerHelper
    {       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="a">обратный url</param>
        /// <param name="b">номер рубрики</param>
        /// <param name="c">текущая страница</param>
        /// <param name="d">количество на странице</param>
        /// <param name="e">обратный path</param>
        /// <param name="f">общее количество статей</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper html, string a, int b, int c, int d, string e, int f)
        {
            int j = (int)Math.Ceiling((double)f / d);
            StringBuilder result = new StringBuilder();
            if (c == 1)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.AddCssClass("page_static");
                a_1.InnerHtml = "1";
                result.AppendLine(a_1.ToString());
            }
            else
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", a + b + "/1/" + d + "/" + e);
                a_1.AddCssClass("pager");
                a_1.InnerHtml = "1";
                result.AppendLine(a_1.ToString());
            }


            int g;
            int h = c;
            if (c == 1)
                h = 2;
            if (c == 3)
                h = c - 1;
            if (c > 3)
                h = c - 2;
            if (c <= 4)
            {
                //начало со второй                                                
                for (g = h; j >= g; g++)
                {
                    if (g == c)
                    {
                        TagBuilder a_2 = new TagBuilder("a");
                        a_2.AddCssClass("page_static");
                        a_2.InnerHtml = g.ToString();
                        result.AppendLine(a_2.ToString());
                    }
                    else
                    {
                        TagBuilder a_2 = new TagBuilder("a");
                        a_2.MergeAttribute("href", a + b + "/" + g + "/" + d + "/" + e);
                        a_2.AddCssClass("pager");
                        a_2.InnerHtml = g.ToString();
                        result.AppendLine(a_2.ToString());
                    } if (g == j - 1)
                        break;
                    if (g == c + 3)
                        break;
                } if (g <= c + 3)
                {
                    if (g <= j - 3)
                    {
                        TagBuilder a_3 = new TagBuilder("a");
                        a_3.AddCssClass("page_static2");
                        a_3.InnerHtml = "...";
                        result.AppendLine(a_3.ToString());
                    }
                }
            }
            else
            {

                if (c > 5)
                {
                    TagBuilder a_4 = new TagBuilder("a");
                    a_4.AddCssClass("page_static2");
                    a_4.InnerHtml = "...";
                    result.AppendLine(a_4.ToString());
                }
                //начало с пятой                                                
                for (g = h - 1; j >= g; g++)
                {
                    if (g == c)
                    {
                        TagBuilder a_5 = new TagBuilder("a");
                        a_5.AddCssClass("page_static");
                        a_5.InnerHtml = g.ToString();
                        result.AppendLine(a_5.ToString());
                    }
                    else
                    {
                        TagBuilder a_5 = new TagBuilder("a");
                        a_5.MergeAttribute("href", a + b + "/" + g + "/" + d + "/" + e);
                        a_5.AddCssClass("pager");
                        a_5.InnerHtml = g.ToString();
                        result.AppendLine(a_5.ToString()); 
                    } if (g == j - 1)
                        break;
                    if (g == c + 3)
                        break;
                } if (g <= c + 3)
                {
                    if (g <= j - 3)
                    {
                        TagBuilder a_6 = new TagBuilder("a");
                        a_6.AddCssClass("page_static2");
                        a_6.InnerHtml = "...";
                        result.AppendLine(a_6.ToString());
                    }
                }
            }
            if (j > 5)
            {
                if (c == j-5)
                {
                    TagBuilder a_7 = new TagBuilder("a");
                    a_7.AddCssClass("page_static2");
                    a_7.InnerHtml = "...";
                    result.AppendLine(a_7.ToString());
                }
            }            
            if (j >= 3)
            {
                if (c == j)
                {
                    TagBuilder a_8 = new TagBuilder("a");
                    a_8.AddCssClass("page_static");
                    a_8.InnerHtml = j.ToString();
                    result.AppendLine(a_8.ToString());
                }
                else
                {
                    TagBuilder a_8 = new TagBuilder("a");
                    a_8.MergeAttribute("href", a + b + "/" + j + "/" + d + "/" + e);
                    a_8.AddCssClass("pager");
                    a_8.InnerHtml = j.ToString();
                    result.AppendLine(a_8.ToString());
                }
            }
            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="a">обратный url</param>
        /// <param name="b">номер рубрики</param>
        /// <param name="c">количество на странице</param>
        /// <param name="d">обратный path</param>
        /// <param name="e">общее количество статей</param>
        /// <returns></returns>
        public static string PagerCount(this HtmlHelper html, string a, int b, int c, string d, int e)
        {
            StringBuilder result = new StringBuilder();
            if (c == 5)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.AddCssClass("page_static");
                a_1.InnerHtml = "5";
                result.AppendLine(a_1.ToString());
            }
            else
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", a + b + "/1/5/" + d);
                a_1.AddCssClass("pager");
                a_1.InnerHtml = "5";
                result.AppendLine(a_1.ToString());
            }

            if (c == 10)
            {
                TagBuilder a_2 = new TagBuilder("a");
                a_2.AddCssClass("page_static");
                a_2.InnerHtml = "10";
                result.AppendLine(a_2.ToString());
            }
            else
            {
                TagBuilder a_2 = new TagBuilder("a");
                a_2.MergeAttribute("href", a + b + "/1/10/" + d);
                a_2.AddCssClass("pager");
                a_2.InnerHtml = "10";
                result.AppendLine(a_2.ToString());
            }
            if (e > 10)
            {
                if (c == 25)
                {
                    TagBuilder a_3 = new TagBuilder("a");
                    a_3.AddCssClass("page_static");
                    a_3.InnerHtml = "25";
                    result.AppendLine(a_3.ToString());
                }
                else
                {
                    TagBuilder a_3 = new TagBuilder("a");
                    a_3.MergeAttribute("href", a + b + "/1/25/" + d);
                    a_3.AddCssClass("pager");
                    a_3.InnerHtml = "25";
                    result.AppendLine(a_3.ToString());
                }
            } if (e > 25)
            {
                if (c == 50)
                {
                    TagBuilder a_4 = new TagBuilder("a");
                    a_4.AddCssClass("page_static");
                    a_4.InnerHtml = "50";
                    result.AppendLine(a_4.ToString());
                }
                else
                {
                    TagBuilder a_4 = new TagBuilder("a");
                    a_4.MergeAttribute("href", a + b + "/1/50/" + d);
                    a_4.AddCssClass("pager");
                    a_4.InnerHtml = "50";
                    result.AppendLine(a_4.ToString());
                }
            }
            return result.ToString();
        }
    }
}
