using System;
using System.IO;

namespace mtm.Core.Repository.LingToExcel
{
    public static class ExcelConnectionString
    {
        internal static string GetConnectionString(string filePath)
        {
            string connectionString = string.Empty;
            string excelExtension = Path.GetExtension(filePath);

            if (excelExtension == ".xls")
            {
                connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties= ""Excel 8.0;HDR=YES;""";

            }
            else if (excelExtension == ".xlsx" || excelExtension == ".xlsm")
            {
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES""";
            }
            else
            {
                throw new ArgumentOutOfRangeException("Excel file extension is not known.");
            }

            return string.Format(connectionString, filePath);
        }
    }
}