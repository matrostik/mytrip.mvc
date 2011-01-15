/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;
using mtm.Core.Wrap;

namespace mtm.Core.StylesTable
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
                    string.Format(
                    WrapRes.StyleTable1_1, 
                    "width:"+width+"%;", content));
                tr = tr + 2;
            }
            else if (column == 1 && tr > 0)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_2,
                    "width:" + width + "%;", content));
                tr = tr + 2;
            }
            else if ((count == 1 || count % column == 1) && tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_3,
                    "width:" + width + "%;", content));
                tr++;
            }
            else if ((count == 1 || count % column == 1) && tr > 1)
            {
                result.AppendLine(
                    string.Format(
                     WrapRes.StyleTable1_4,
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (count % column == 0 && tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_5,
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (count % column == 0 && tr > 1)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_6,
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_7,
                    "width:" + width + "%;", content));
            }
            else if (tr > 1)
            {
                result.AppendLine(
                    string.Format(
                    WrapRes.StyleTable1_8,
                    "width:" + width + "%;", content));
            }
            styletable = "content"; outtr = tr; outline = line2;
            finaltr = "</td>"; start = WrapRes.StyleTable1_0; end = WrapRes.StyleTable1_9;
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
                result.AppendLine(string.Format(WrapRes.StyleTable2_1, "width:" + width + "%;"));
                result.AppendLine(string.Format(WrapRes.StyleTable2_2, "width:" + width + "%;", content));
                result.AppendLine(string.Format(WrapRes.StyleTable2_3, "width:" + width + "%;"));
            }
            else if (count == 1 || count % column == 1)
            {
                line2++;
                if (count2 != 0 && line2 == line && !WrapRes.StyleTable2_4.Contains("null"))
                {
                    result.AppendLine("<tr>");
                    int q = 1;
                    while (q <= count2)
                    {
                        string a=(q == 1)?
                            string.Format(WrapRes.StyleTable2_4, "width:" + width + "%;")
                            : string.Format(WrapRes.StyleTable2_5, "width:" + width + "%;");
                        result.AppendLine(string.Concat(a,"</td>"));
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                else if ((count2 == 0 && !WrapRes.StyleTable2_4.Contains("null")) || (line2 < line && !WrapRes.StyleTable2_4.Contains("null")))
                {
                    result.AppendLine("<tr>");
                    int q = 1;
                    while (q <= column)
                    {
                        string a=(q == 1)?
                            string.Format(WrapRes.StyleTable2_4, "width:" + width + "%;")
                            : string.Format(WrapRes.StyleTable2_5, "width:" + width + "%;");
                        result.AppendLine(string.Concat(a,"</td>"));
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                result.AppendLine(string.Format(WrapRes.StyleTable2_6, "width:" + width + "%;", content));
                if (count2 == 1 && line2 == line)
                {
                    result.AppendLine(string.Format(WrapRes.StyleTable2_7, "width:" + width + "%;"));
                }
            }
            else if (count % column == 0)
            {

                result.AppendLine(string.Format(WrapRes.StyleTable2_8, "width:" + width + "%;", content));
                if (!WrapRes.StyleTable2_4.Contains("null"))
                {
                    int q = 1;
                    while (q <= column)
                    {
                        string a = (q == 1) ?
                                string.Format(WrapRes.StyleTable2_9, "width:" + width + "%;")
                                : string.Format(WrapRes.StyleTable2_10, "width:" + width + "%;");
                        result.AppendLine(string.Concat(a, "</td>"));
                        q++;
                    }
                    result.AppendLine("</tr>");
                }
                
            }
            else
            {
                result.AppendLine(string.Format(WrapRes.StyleTable2_11, "width:" + width + "%;", content));
                if (count2 != 0 && line2 == line && (count - ((line2 - 1) * column)) == count2)
                {

                    if (!WrapRes.StyleTable2_4.Contains("null"))
                    {
                        result.AppendLine("</tr><tr>");
                        int q = 1;
                        while (q <= count2)
                        {
                            string a = (q == 1) ?
                                string.Format(WrapRes.StyleTable2_9, "width:" + width + "%;")
                                : string.Format(WrapRes.StyleTable2_10, "width:" + width + "%;");
                            result.AppendLine(string.Concat(a, "</td>"));
                            q++;
                        }
                    }
                    result.AppendLine("</tr>");
                }

            }
            styletable = WrapRes.StyleTable2_12;
            outline = line2;
            return result.ToString();
        }

        //****************** E N D **********************
        #endregion

    }
}