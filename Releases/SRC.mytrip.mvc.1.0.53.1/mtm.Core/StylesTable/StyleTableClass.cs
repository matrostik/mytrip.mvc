/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Text;

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
                    "<tr><td style=\"{0}\">{1}</td></tr>", 
                    "width:"+width+"%;", content));
                tr = tr + 2;
            }
            else if (column == 1 && tr > 0)
            {
                result.AppendLine(
                    string.Format(
                    "<tr><td style=\"{0}\">{1}</td></tr>",
                    "width:" + width + "%;", content));
                tr = tr + 2;
            }
            else if ((count == 1 || count % column == 1) && tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    "<tr><td style=\"{0}\">{1}</td>",
                    "width:" + width + "%;", content));
                tr++;
            }
            else if ((count == 1 || count % column == 1) && tr > 1)
            {
                result.AppendLine(
                    string.Format(
                     "<tr><td style=\"{0}\">{1}</td>",
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (count % column == 0 && tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    "<td style=\"{0}\">{1}</td></tr>",
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (count % column == 0 && tr > 1)
            {
                result.AppendLine(
                    string.Format(
                    "<td style=\"{0}\">{1}</td></tr>",
                    "width:" + width + "%;", content));
                tr++;
            }
            else if (tr <= 1)
            {
                result.AppendLine(
                    string.Format(
                    "<td style=\"{0}\">{1}</td>",
                    "width:" + width + "%;", content));
            }
            else if (tr > 1)
            {
                result.AppendLine(
                    string.Format(
                    "<td style=\"{0}\">{1}</td>",
                    "width:" + width + "%;", content));
            }
            styletable = "content"; outtr = tr; outline = line2;
            finaltr = "</td>"; start = ""; end = "";
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
                result.AppendLine(string.Format("<tr><td  style=\"{0}\">{1}</td></tr>", "width:" + width + "%;", content));
                
            }
            else if (count == 1 || count % column == 1)
            {
                line2++;
                result.AppendLine(string.Format("<tr><td  style=\"{0}\">{1}</td>", "width:" + width + "%;", content));
                if (count2 == 1 && line2 == line)
                {
                    result.AppendLine("</tr>");
                }
            }
            else if (count % column == 0)
            {

                result.AppendLine(string.Format("<td  style=\"{0}\">{1}</td></tr>", "width:" + width + "%;", content));
                
                
            }
            else
            {
                result.AppendLine(string.Format("<td style=\"{0}\">{1}</td>", "width:" + width + "%;", content));
                if (count2 != 0 && line2 == line && (count - ((line2 - 1) * column)) == count2)
                {
                    result.AppendLine("</tr>");
                }

            }
            styletable = "separate";
            outline = line2;
            return result.ToString();
        }

        //****************** E N D **********************
        #endregion

    }
}