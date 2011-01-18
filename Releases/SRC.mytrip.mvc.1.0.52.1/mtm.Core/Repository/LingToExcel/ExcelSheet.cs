using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using System.Text;

namespace mtm.Core.Repository.LingToExcel
{
    public class ExcelSheet<T> : IEnumerable<T>
    {
        private readonly ExcelProvider provider;

        private readonly List<T> rows;

        internal ExcelSheet(ExcelProvider provider)
        {
            this.provider = provider;
            this.rows = new List<T>();
        }

        public void DeleteOnSubmit(T entity)
        {
            this.provider.ChangeSet.DeleteObject(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.Load();
            return this.rows.GetEnumerator();
        }

        public void InsertOnSubmit(T entity)
        {
            //Add to tracking
            this.provider.ChangeSet.InsertObject(entity);
        }

        private void AddToTracking(Object obj, List<PropertyManager> props)
        {
            this.provider.ChangeSet.AddObject(new ObjectState(obj, props));
        }

        private static string BuildSelect()
        {
            string sheet = ExcelMapReader.GetSheetName(typeof(T));
            var builder = new StringBuilder();

            foreach (ExcelColumnAttribute columnAttribute in ExcelMapReader.GetColumnList(typeof(T)))
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }

                builder.AppendFormat("[{0}]", columnAttribute.GetSelectColumn());
            }

            builder.Append(" FROM [");
            builder.Append(sheet);
            builder.Append("$]");

            return "SELECT " + builder;
        }

        private static T CreateInstance()
        {
            return Activator.CreateInstance<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.Load();

            return this.rows.GetEnumerator();
        }

        private void Load()
        {
            string connectionString = ExcelConnectionString.GetConnectionString(this.provider.Filepath);
            List<ExcelColumnAttribute> columns = ExcelMapReader.GetColumnList(typeof(T));

            this.rows.Clear();

            using (var oleDbConnection = new OleDbConnection(connectionString))
            {
                oleDbConnection.Open();

                using (OleDbCommand oleDbCommand = oleDbConnection.CreateCommand())
                {
                    oleDbCommand.CommandText = BuildSelect();

                    using (OleDbDataReader reader = oleDbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T item = CreateInstance();
                            var propertyManagers = new List<PropertyManager>();

                            foreach (ExcelColumnAttribute column in columns)
                            {
                                object value = reader[column.GetSelectColumn()];

                                if (value is DBNull)
                                {
                                    value = null;
                                }

                                if (column.IsFieldStorage())
                                {
                                    const BindingFlags flags = BindingFlags.GetField |
                                                               BindingFlags.NonPublic |
                                                               BindingFlags.Instance |
                                                               BindingFlags.SetField |
                                                               BindingFlags.FlattenHierarchy;

                                    FieldInfo fieldInfo = typeof(T).GetField(column.GetStorageName(), flags);


                                    if (fieldInfo != null)
                                    {
                                        fieldInfo.SetValue(item, Convert.ChangeType(value, fieldInfo.FieldType));
                                    }
                                    else
                                    {
                                        if (value == null)
                                        {
                                            Type propertyType = typeof(T).GetProperty(column.GetStorageName()).PropertyType;

                                            if (propertyType == typeof(string))
                                            {
                                                value = string.Empty;
                                            }
                                        }

                                        typeof(T).GetProperty(column.GetStorageName()).SetValue(item, value, null);
                                    }
                                }
                                else
                                {
                                    typeof(T).GetProperty(column.GetStorageName()).SetValue(item, Convert.ChangeType(value, typeof(T).GetProperty(column.GetStorageName()).PropertyType), null);
                                }

                                propertyManagers.Add(new PropertyManager(column.GetPropertyInfo().Name, value));
                            }

                            this.rows.Add(item);
                            this.AddToTracking(item, propertyManagers);
                        }
                    }
                }
            }
        }
    }
}