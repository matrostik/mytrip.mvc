
    using System;
    using System.Collections.Generic;
    using System.Reflection;

namespace mtm.Core.Repository.LingToExcel
{
    public class ExcelMapReader
    {
        public static List<ExcelColumnAttribute> GetColumnList(Type t)
        {
            List<ExcelColumnAttribute> columnList = new List<ExcelColumnAttribute>();
            
            foreach (PropertyInfo propInfo in t.GetProperties())
            {
                object[] excelColumnAttributes = propInfo.GetCustomAttributes(typeof(ExcelColumnAttribute), true);

                if (excelColumnAttributes.Length > 0)
                {
                    ExcelColumnAttribute columnAttribute = (ExcelColumnAttribute)excelColumnAttributes[0];
                    columnAttribute.SetProperty(propInfo);

                    columnList.Add(columnAttribute);
                }
            }

            return columnList;
        }

        public static string GetSheetName(Type t)
        {
            object[] sheetList = t.GetCustomAttributes(typeof(ExcelSheetAttribute), true);
            
            if (sheetList.Length == 0)
            {
                throw new InvalidOperationException("ExcelSheetAttribute not found on type " + t.FullName);
            }
           
            ExcelSheetAttribute sheet = (ExcelSheetAttribute)sheetList[0];
            
            if (sheet.Name == string.Empty)
            {
                return t.Name;
            }

            return sheet.Name;
        }
    }
}