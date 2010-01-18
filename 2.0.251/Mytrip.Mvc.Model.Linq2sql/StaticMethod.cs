using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Mvc.Model.Linq2sql
{
    public static class StaticMethod
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
            a = a.Replace("!", "");
            a = a.Replace(",", "");
            a = a.Replace(".", "");
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
        #region Преобразование краткого содержания
        /// <summary>
        /// Преобразование краткого содержания
        /// </summary>
        /// <param name="a">строка</param>
        /// <returns></returns>
        public static string DecodingDescription(string a)
        {
            a = a.Trim();
            a = a.Replace("<p>", "");
            a = a.Replace("</p>", "<br/>");
            return a;

        }
        #endregion
        #endregion
    }
}
