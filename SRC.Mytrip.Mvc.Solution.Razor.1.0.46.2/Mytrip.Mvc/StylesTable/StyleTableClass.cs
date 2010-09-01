/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;

namespace Mytrip.Mvc.StylesTable
{
    /// <summary>
    /// Стили таблиц
    /// </summary>
    internal static class StyleTableClass
    {
        #region Стиль для таблицы Единый фон с разделением внутренних границ ячеек бордером
        // **********************************************
        // Стиль для таблицы
        // Единый фон с разделением внутренних границ ячеек бордером
        // **********************************************

        /// <summary>
        /// Стиль для таблицы 
        /// Единый фон с разделением внутренних границ ячеек бордером
        /// </summary>
        /// <param name="column"></param>
        /// <param name="tr"></param>
        /// <param name="width"></param>
        /// <param name="content"></param>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <param name="line"></param>
        /// <param name="line2"></param>
        /// <param name="outtr"></param>
        /// <param name="outline"></param>
        /// <param name="finaltr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="styletable"></param>
        /// <returns></returns>
        internal static string StyleTable1(int column, int tr, int width, string content,int count, int count2, int line, int line2, out int outtr, out int outline, out string finaltr,out string start, out string end, out string styletable)
        {
            StringBuilder result = new StringBuilder();
            if (column == 1 && tr == 0)
            {
                result.AppendLine(
                    string.Format("<tr><td style=\"width:{0}%;border:0;\">{1}</td></tr>", width, content));
                tr = tr + 2;
            }
            else if (column == 1 && tr > 0)
            {
                result.AppendLine(
                    string.Format("<tr><td style=\"width:{0}%; border-right:0;border-left:0;border-bottom:0;\">{1}</td></tr>", width, content));
                tr = tr + 2;
            }
            else if ((count == 1 || count % column == 1) && tr <= 1)
            {
                result.AppendLine(
                    string.Format("<tr><td style=\"width:{0}%;border-top:0;border-left:0;border-bottom:0;\">{1}</td>", width, content));
                tr++;
            }
            else if ((count == 1 || count % column == 1) && tr > 1)
            {
                result.AppendLine(
                    string.Format("<tr><td style=\"width:{0}%;border-left:0;border-bottom:0;\">{1}</td>", width, content));
                tr++;
            }
            else if (count % column == 0 && tr <= 1)
            {
                result.AppendLine(
                    string.Format("<td style=\"width:{0}%;border:0;\">{1}</td></tr>", width, content));
                tr++;
            }
            else if (count % column == 0 && tr > 1)
            {
                result.AppendLine(
                    string.Format("<td style=\"width:{0}%;border-right:0;border-left:0;border-bottom:0;\">{1}</td></tr>", width, content));
                tr++;
            }
            else if (tr <= 1)
            {
                result.AppendLine(
                    string.Format("<td style=\"width:{0}%;border-top:0;border-left:0;border-bottom:0;\">{1}</td>", width, content));
            }
            else if (tr > 1)
            {
                result.AppendLine(
                    string.Format("<td style=\"width:{0}%;border-left:0;border-bottom:0;\">{1}</td>", width, content));
            }
            styletable = "content"; outtr = tr; outline = line2;
            finaltr = "</td>"; start = "<div class=\"content\">"; end = "</div>";
            return result.ToString();
        }

        //****************** E N D **********************
        #endregion

        #region Стиль для таблицы Каждая ячейка лежит в контейнере с округлыми краями
        // **********************************************
        // Стиль для таблицы
        // Каждая ячейка лежит в контейнере с округлыми краями
        // **********************************************

        /// <summary>
        /// Стиль для таблицы
        /// Каждая ячейка лежит в контейнере с округлыми краями
        /// </summary>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="content"></param>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <param name="line"></param>
        /// <param name="line2"></param>
        /// <param name="outline"></param>
        /// <param name="styletable"></param>
        /// <returns></returns>
        internal static string StyleTable2(int column, int width, string content,int count, int count2, int line, int line2, out int outline, out string styletable)
        {
            StringBuilder result = new StringBuilder();
            if (column == 1)
            {
                result.AppendLine(string.Format("<tr><td class=\"first1\" style=\"width:{0}%;\"></td></tr>",width));
                result.AppendLine(string.Format("<tr><td class=\"first2\" style=\"width:{0}%;\">{1}</td></tr>",width,content));
                result.AppendLine(string.Format("<tr><td class=\"first3\" style=\"width:{0}%;\"></td></tr>",width));
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
                        string a=(q == 1)?
                            string.Format("<td class=\"first1\" style=\"width:{0}%;\">",width)
                            : string.Format("<td class=\"last1\" style=\"width:{0}%;\">", width);
                        result.AppendLine(string.Concat(a,"</td>"));
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
                        string a=(q == 1)?
                            string.Format("<td class=\"first1\" style=\"width:{0}%;\">",width)
                            : string.Format("<td class=\"last1\" style=\"width:{0}%;\">", width);
                        result.AppendLine(string.Concat(a,"</td>"));
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                result.AppendLine(string.Format("<tr><td class=\"first2\" style=\"width:{0}%;\">{1}</td>", width, content));
                if (count2 == 1 && line2 == line)
                {
                    result.AppendLine(string.Format("</tr><tr><td class=\"first3\" style=\"width:{0}%;\"></td></tr>",width));
                }
            }
            else if (count % column == 0)
            {

                result.AppendLine(string.Format("<td class=\"last2\" style=\"width:{0}%;\">{1}</td></tr><tr>",width, content));
                int q = 1;
                while (q <= column)
                {
                    string a = (q == 1) ?
                            string.Format("<td class=\"first3\" style=\"width:{0}%;\">", width)
                            : string.Format("<td class=\"last3\" style=\"width:{0}%;\">", width);
                    result.AppendLine(string.Concat(a, "</td>"));
                    q++;
                }
                result.AppendLine("</tr>");
            }
            else
            {
                result.AppendLine(string.Format("<td class=\"last2\" style=\"width:{0}%;\">{1}</td>", width, content));
                if (count2 != 0 && line2 == line && (count - ((line2 - 1) * column)) == count2)
                {
                    result.AppendLine("</tr><tr>");
                    int q = 1;
                    while (q <= count2)
                    {
                        string a = (q == 1) ?
                            string.Format("<td class=\"first1\" style=\"width:{0}%;\">", width)
                            : string.Format("<td class=\"last1\" style=\"width:{0}%;\">", width);
                        result.AppendLine(string.Concat(a, "</td>"));
                        q++;
                    }
                    result.AppendLine("</tr>");
                }

            }
            styletable = "content";
            outline = line2;
            return result.ToString();
        }

        //****************** E N D **********************
        #endregion

    }
}