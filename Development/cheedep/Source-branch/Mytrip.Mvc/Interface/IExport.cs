/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web;

namespace Mytrip.Mvc.Interface
{
    /// <summary>
    /// В А Ж Н О
    /// класс должен иметь имя "Export"!!!!
    /// Данный интерфейс не предназначен для наследования 
    /// т.к. класс Export должен быть статическим.
    /// Интерфейсом можно воспользоватся для передачи необходимых 
    /// названий методов и удалить наследование. 
    /// Класс "public static class Export" должен находится в
    /// корне библиотеки модуля, тоесть модуль имеет имя "Mytrip.Articles"
    /// путь к классу Export - "Mytrip.Articles.Export".
    /// </summary>
    public interface IExport
    {
        /// <summary>
        /// Передача таблицы с контентом для отображения
        /// на главной странице в основной колонке
        /// формируется через 
        /// GeneralMethods.StyleTable(int column, int style, int tr, int width, 
        /// string content, int count, int count2, int line, int line2, out int outtr, 
        /// out int outline, out string finaltr, out string start, out string end,
        /// out string styletable)
        /// </summary>
        /// <param name="categoryId">номер категории</param>
        /// <param name="line">количество строк</param>
        /// <param name="column">количество столбцов</param>
        /// <param name="content">длина контента</param>
        /// <param name="imgwidth">ширина картинки</param>
        /// <param name="style">номер стиля</param>
        /// <param name="viewtitle">отображение заголовка</param>
        /// <returns>возвращает HtmlString</returns>
        HtmlString HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle);
        
        #region ПРИМЕР РЕАЛИЗАЦИИ
        
        #endregion
        
        /// <summary>
        /// Передача ссылки на страницу по настройке 
        /// модуля в файле MytripConfiguration.xml.
        /// Должен вернуть [li][a/][/li] либо null
        /// </summary>
        /// <returns>возвращает HtmlString</returns>
        HtmlString Setting();

        #region ПРИМЕР РЕАЛИЗАЦИИ
        
        #endregion

        /// <summary>
        /// Передача ссылки на страницу по работе 
        /// с модулем для администратора сайта.
        /// Должен вернуть [li][a/][/li] либо null 
        /// </summary>
        /// <returns>возвращает HtmlString</returns>
        HtmlString Manager();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// Передача информации для пользователя 
        /// из данного модуля.
        /// Должен вернуть [li][a/][/li] либо string либо null
        /// </summary>
        /// <returns>возвращает HtmlString</returns>
        HtmlString Profile();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// Передача информации о деятельности пользователя 
        /// в данном модуле.
        /// Должен вернуть [li][a/][/li] либо string либо null 
        /// </summary>
        /// <returns>возвращает HtmlString</returns>
        HtmlString LastActivity(string userName);

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

    }
}
