/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Mvc;

namespace Mytrip.Mvc.Interface
{
    /// <summary>
    /// <para>ВАЖНО</para>
    /// Контроллер должен иметь имя соответствующее
    /// имени сборки в которой он находится и слово Export.
    /// Если имя сборки имеет точку ее следует заменить на 
    /// нижнее подчеркивание
    /// <para>ПРИМЕР</para>
    /// Имя сборки Mytrip.Articles
    /// Имя контроллера Mytrip_ArticlesExportController
    /// </summary>
    public interface IControllerExport
    {
        /// <summary>Используется только если реализован в классе Export
        /// метод по формированию основного контента в табличной форме
        /// <para>HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)</para>
        /// для основной колонки главной страницы.
        /// </summary>
        /// <remarks>
        /// Отдает string следующего вида:
        /// <para>ПРИМЕР</para>
        /// <para>категория имеет id = 3, культуру ru-ru, название "Новости разработки"</para>
        /// <para>категория имеет id = 5, культуру ru-ru, название "Мои события"</para>
        /// <para>ФОРМИРОВАНИЕ СТРОКИ</para>
        /// <para>3_ru-ru_Новости разработки|5_ru-ru_Мои события</para>
        /// <para>*************************************************</para>
        /// Имеется в базе данных таблица категорий и статей. 
        /// Соответственно одна категория имеет много статей.
        /// <para>*************************************************</para>
        /// <para>Таблица категорий состоит из</para>
        /// <para>CategoryId - int     (первичный ключ)</para>
        /// <para>Title      - string  (название категории)</para>
        /// <para>Culture    - string  (принадлежность к культуре "ru-ru" или "en-us"...)</para>
        /// <para>AllCulture - bool    (true - разрешен показ во всех культурах)</para>
        /// <para>...</para>
        /// <para>*************************************************</para>
        /// <para>Соответственно репозиторий к таблице категорий</para>
        /// <para>Entities _entities;</para>
        /// <para>public Entities entities</para> 
        /// <para>{</para>
        /// <para>get {</para>
        /// <para>if (_entities == null)</para>
        /// <para>_entities = new Entities("строка подключения к базе данных");</para>
        /// <para>return _entities;</para> 
        /// <para>}</para>
        /// <para>}</para>
        /// <para>public IQueriable &lt; таблица_категорий &gt; GetCategories()</para> 
        /// <para>{</para>
        /// <para>return entities.таблица_категорий.OrderBy(x=>x.CategoryId);</para>
        /// <para>}</para>
        /// <para>*************************************************</para>
        /// РЕАЛИЗАЦИЯ МЕТОДА КОНТРОЛЛЕРА
        /// <para>*************************************************</para>
        /// <para>public string HomePage()</para>
        /// <para>{</para>
        /// <para>IQueriable &lt; таблица_категорий &gt; сategories = репозиторий.GetCategories();</para> 
        /// <para>bool start = true;</para>
        /// <para>string result = string.Empty;</para>
        /// <para>foreach (таблица_категорий item in сategories)</para>
        /// <para>{</para>
        /// <para>string culture = (item.AllCulture) ? "globe_" : string.Concat(item.Culture.ToLower(), "_");</para>
        /// <para>string _start = (start) ? string.Empty : "|";</para>
        /// <para>result += string.Concat(_start, item.CategoryId, "_", culture, item.Title);</para>
        /// <para>start = false;</para>
        /// <para>}</para>
        /// <para>return result;</para>
        /// <para>}</para>
        ///</remarks>
        /// <returns>возвращает string</returns>
        string HomePage();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string MenuPage();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ProfilePage();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string SideBarPage();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ActionResult CreateDataBaseAndXml();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ActionResult UninstallModule();

        #region ПРИМЕР РЕАЛИЗАЦИИ

        #endregion
    }
}
