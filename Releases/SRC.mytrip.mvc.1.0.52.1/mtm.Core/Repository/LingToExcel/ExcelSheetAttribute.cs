using System;

namespace mtm.Core.Repository.LingToExcel
{
    public class ExcelSheetAttribute : Attribute
    {
        public string Name
        {
            get;
            set;
        }
    }
}