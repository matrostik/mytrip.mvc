using System;
using System.Reflection;

namespace mtm.Core.Repository.LingToExcel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ExcelColumnAttribute : Attribute
    {
        private PropertyInfo propertyInfo;

        private string storage;

        public ExcelColumnAttribute()
        {
            this.Name = string.Empty;
            this.storage = string.Empty;
        }

        public string Name
        {
            get;
            set;
        }

        public string Storage
        {
            get
            {
                if (this.storage == string.Empty)
                {
                    this.Storage = this.propertyInfo.Name;
                }

                return this.storage;
            }
            set { this.storage = value; }
        }

        internal PropertyInfo GetPropertyInfo()
        {
            return this.propertyInfo;
        }

        internal string GetSelectColumn()
        {
            if (this.Name == string.Empty)
            {
                return this.propertyInfo.Name;
            }

            return this.Name;
        }

        internal string GetStorageName()
        {
            if (this.Storage == string.Empty)
            {
                return this.propertyInfo.Name;
            }

            return this.storage;
        }

        internal bool IsFieldStorage()
        {
            return string.IsNullOrEmpty(this.storage) == false;
        }

        internal void SetProperty(PropertyInfo propInfo)
        {
            this.propertyInfo = propInfo;
        }
    }
}