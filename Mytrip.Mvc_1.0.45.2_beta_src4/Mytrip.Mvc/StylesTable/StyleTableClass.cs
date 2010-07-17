using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Mytrip.Mvc.StylesTable
{
    public static class StyleTableClass
    {
        public static string StyleTable1(int column, int tr, int width, string content,
               int count, int count2, int line, int line2, out int outtr, out int outline, out string finaltr,
               out string start, out string end, out string styletable)
        {
            StringBuilder result = new StringBuilder();
            if (column == 1 && tr == 0)
            {
                result.AppendLine("<tr><td style=\"width:" + width + "%;border:0;\">");
                result.AppendLine(content + "</td></tr>");
                tr = tr + 2;
            }
            else if (column == 1 && tr > 0)
            {
                result.AppendLine("<tr><td style=\"width:" + width + "%; border-right:0;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td></tr>");
                tr = tr + 2;
            }
            else if ((count == 1 || count % column == 1) && tr <= 1)
            {
                result.AppendLine("<tr><td style=\"width:" + width + "%;border-top:0;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td>");
                tr++;
            }
            else if ((count == 1 || count % column == 1) && tr > 1)
            {
                result.AppendLine("<tr><td style=\"width:" + width + "%;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td>");
                tr++;
            }
            else if (count % column == 0 && tr <= 1)
            {
                result.AppendLine("<td style=\"width:" + width + "%;border:0;\">");
                result.AppendLine(content + "</td></tr>");
                tr++;
            }
            else if (count % column == 0 && tr > 1)
            {
                result.AppendLine("<td style=\"width:" + width + "%;border-right:0;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td></tr>");
                tr++;
            }
            else if (tr <= 1)
            {
                result.AppendLine("<td style=\"width:" + width + "%;border-top:0;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td>");
            }
            else if (tr > 1)
            {
                result.AppendLine("<td style=\"width:" + width + "%;border-left:0;border-bottom:0;\">");
                result.AppendLine(content + "</td>");
            }
            styletable = "homepage";
            outtr = tr;
            outline = line2;
            finaltr = "</td>";
            start = "<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div><div class=\"content\">";
            end = "</div><div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\"></div></div>";
            return result.ToString();
        }
        public static string StyleTable2(int column, int width, string content,
            int count, int count2, int line, int line2, out int outline, out string styletable)
        {
            StringBuilder result = new StringBuilder();
            if (column == 1)
            {
                result.AppendLine("<tr><td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                result.AppendLine("<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div>");
                result.AppendLine("</td></tr>");
                result.AppendLine("<tr><td style=\"width:" + width + "%;border-top:0;border-bottom:0;background:#fff;padding-left: 5px;\">");
                result.AppendLine(content + "</td></tr>");
                result.AppendLine("<tr><td style=\"width:" + width + "%;border:0;background:none;padding:0;padding-bottom:1px;\">");
                result.AppendLine("<div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\"></div></div>");
                result.AppendLine("</td></tr>");
            }
            else if (count == 1 || count % column == 1)
            {
                line2++;
                if (count2 != 0 && line2 == line)
                {
                    result.AppendLine("<tr>");
                    int q = 1;
                    while (q <= count2)
                    {
                        if (q == 1)
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                        else
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;padding-left:1px;\">");
                        result.AppendLine("<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div>");
                        result.AppendLine("</td>");
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                else if (count2 == 0 || line2 < line)
                {
                    result.AppendLine("<tr>");
                    int q = 1;
                    while (q <= column)
                    {
                        if (q == 1)
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                        else
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;padding-left:1px;\">");
                        result.AppendLine("<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div>");
                        result.AppendLine("</td>");
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                result.AppendLine("<tr><td style=\"width:" + width + "%;border-top:0;border-bottom:0;background:#fff;padding-left: 5px;\">");
                result.AppendLine(content + "</td>");
                if (count2 == 1 && line2 == line)
                {
                    result.AppendLine("</tr><tr>");
                    result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                    result.AppendLine("<div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\" style=\"margin-bottom: 1px;\"></div></div>");
                    result.AppendLine("</td>");
                    result.AppendLine("</tr>");
                }
            }
            else if (count % column == 0)
            {

                result.AppendLine("<td style=\"width:" + width + "%;border-left:0;border-top:0;border-bottom:0;\">");
                result.AppendLine(content + "</td></tr>");
                result.AppendLine("<tr>");
                int q = 1;
                while (q <= column)
                {
                    if (q == 1)
                        result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                    else
                        result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;padding-left:1px;\">");
                    result.AppendLine("<div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\" style=\"margin-bottom: 1px;\"></div></div>");
                    result.AppendLine("</td>");
                    q++;
                }
                result.AppendLine("</tr>");
            }
            else
            {
                result.AppendLine("<td style=\"width:" + width + "%;border-left:0;border-top:0;border-bottom:0;\">");
                result.AppendLine(content + "</td>");
                if (count2 != 0 && line2 == line && (count - ((line2 - 1) * column)) == count2)
                {
                    result.AppendLine("</tr><tr>");
                    int q = 1;
                    while (q <= count2)
                    {
                        if (q == 1)
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;\">");
                        else
                            result.AppendLine("<td style=\"width:" + width + "%;border:0; background:none;padding:0;padding-left:1px;\">");
                        result.AppendLine("<div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\" style=\"margin-bottom: 1px;\"></div></div>");
                        result.AppendLine("</td>");
                        q++;
                    }
                    result.AppendLine("</tr>");
                }

            }


            styletable = "homepage2";
            outline = line2;
            return result.ToString();
        }
    }
}