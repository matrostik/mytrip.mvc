/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
namespace Mytrip.Mvc.Interface
{
    /// <summary>Интерфейс для создания и удаления таблиц
    /// в базе данных сайта модулем расширения
    /// </summary>
    public interface ICreateDataBase
    {
        /// <summary>
        /// Метод должен передать скрипты (string[]) для создания и удаления 
        /// таблиц в базе данных сайта для всех провайдеров SQL
        /// работающих с сайтом
        /// </summary>
        /// <remarks>
        /// **********************************************
        /// <para>СХЕМА РЕАЛИЗАЦИИ МЕТОДОВ КЛАССА</para>
        /// **********************************************
        /// <para>public void CreateAndDeleteDataBase(bool e)</para>
        ///  {
        ///  <para>**********************************************</para>
        ///  <para> a - скрипт (string[]) создания таблиц для MSSQL</para>
        ///  b - скрипт (string[]) удаления таблиц для MSSQL
        ///  <para>c - скрипт (string[]) создания таблиц для MySql</para>
        ///  d - скрипт (string[]) удаления таблиц для MySql
        ///  <para>**********************************************</para>
        ///  Mytrip.Mvc.Install.CreateDataBase.CreateDataBaseMSSQL(a, b, e); // e = true, выполняется скрипт a
        ///  <para>Mytrip.Mvc.Install.CreateDataBase.CreateDataBaseMYSQL(c, d, e); // e = true, выполняется скрипт c</para>
        /// }
        /// <para>**********************************************</para>
        /// скрипт (string[]) создания таблиц для MSSQL
        /// <para>**********************************************</para>
        /// private string[] a()
        /// <para>{</para>
        /// string[] result = {
        /// <para>"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlestag]') AND type in (N'U'))",</para>
        /// "BEGIN",
        /// <para>"CREATE TABLE [dbo].[mytrip_articlestag](",</para>
        /// ...
        /// <para>};</para>
        /// return result;
        /// <para>}</para>
        /// 
        /// 
        /// </remarks>
        /// <param name="e">true - выполняются скрипты "a", "c"</param>
        void CreateAndDeleteDataBase(bool e);
    }
}
