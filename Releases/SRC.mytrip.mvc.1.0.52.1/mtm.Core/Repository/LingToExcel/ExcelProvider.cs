using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace mtm.Core.Repository.LingToExcel
{
    public class ExcelProvider
    {
        private readonly ChangeSet changes;

        private string filePath;

        private ExcelProvider()
        {
            this.changes = new ChangeSet();
        }

        internal ChangeSet ChangeSet
        {
            get { return this.changes; }
        }

        internal string Filepath
        {
            get { return this.filePath; }
        }

        public static ExcelProvider Create(string filePath)
        {
            var provider = new ExcelProvider { filePath = filePath };

            return provider;
        }

        private static void BuildDeleteClause(OleDbCommand cmd, ObjectState objState)
        {
            StringBuilder builder = new StringBuilder();

            string sheet = ExcelMapReader.GetSheetName(objState.Entity.GetType());

            builder.Append("DELETE FROM [");
            builder.Append(sheet);
            builder.Append("$]");

            cmd.CommandText = builder.ToString();

            BuildWhereClause(cmd, objState);
        }

        private static void BuildInsertClause(OleDbCommand cmd, ObjectState objState)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            string sheet = ExcelMapReader.GetSheetName(objState.Entity.GetType());

            builder.Append("INSERT INTO [");
            builder.Append(sheet);
            builder.Append("$]");

            foreach (ExcelColumnAttribute columnAttribute in ExcelMapReader.GetColumnList(objState.Entity.GetType()))
            {
                if (columns.Length > 0)
                {
                    columns.Append(", ");
                    values.Append(", ");
                }

                columns.AppendFormat("[{0}]", columnAttribute.GetSelectColumn());

                string paraNum = "@x" + cmd.Parameters.Count;

                values.Append(paraNum);

                object value = columnAttribute.GetPropertyInfo().GetValue(objState.Entity, null);

                OleDbParameter para = new OleDbParameter(paraNum, value);

                cmd.Parameters.Add(para);
            }

            cmd.CommandText = builder + "(" + columns + ") VALUES (" + values + ")";
        }

        private static void BuildUpdateClause(OleDbCommand cmd, ObjectState objState)
        {
            StringBuilder builder = new StringBuilder();

            string sheet = ExcelMapReader.GetSheetName(objState.Entity.GetType());

            builder.Append("UPDATE [");
            builder.Append(sheet);
            builder.Append("$] SET ");

            StringBuilder changeBuilder = new StringBuilder();

            List<ExcelColumnAttribute> columnAttributes = ExcelMapReader.GetColumnList(objState.Entity.GetType());
            List<ExcelColumnAttribute> changedColumns = (from c in columnAttributes
                                                         join p in objState.ChangedProperties on c.GetPropertyInfo().Name
                                                                 equals p.PropertyName
                                                         where p.HasChanged
                                                         select c).ToList();

            foreach (ExcelColumnAttribute changedColumn in changedColumns)
            {
                if (changeBuilder.Length > 0)
                {
                    changeBuilder.Append(", ");
                }

                string paraNum = "@x" + cmd.Parameters.Count;

                changeBuilder.AppendFormat("[{0}]", changedColumn.GetSelectColumn());
                changeBuilder.Append(" = ");
                changeBuilder.Append(paraNum);

                object val = changedColumn.GetPropertyInfo().GetValue(objState.Entity, null);

                OleDbParameter para = new OleDbParameter(paraNum, val);

                cmd.Parameters.Add(para);
            }

            builder.Append(changeBuilder.ToString());

            cmd.CommandText = builder.ToString();

            BuildWhereClause(cmd, objState);
        }

        private static void BuildWhereClause(OleDbCommand cmd, ObjectState objState)
        {
            StringBuilder builder = new StringBuilder();

            List<ExcelColumnAttribute> columnAttributes = ExcelMapReader.GetColumnList(objState.Entity.GetType());

            foreach (ExcelColumnAttribute column in columnAttributes)
            {
                PropertyManager propertyManager = objState.GetProperty(column.GetPropertyInfo().Name);

                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }

                builder.AppendFormat("[{0}]", column.GetSelectColumn());

                if (propertyManager.OrginalValue == DBNull.Value)
                {
                    builder.Append(" IS NULL");
                }
                else
                {
                    builder.Append(" = ");

                    string paraNum = "@x" + cmd.Parameters.Count;

                    builder.Append(paraNum);

                    OleDbParameter para = new OleDbParameter(paraNum, propertyManager.OrginalValue);

                    cmd.Parameters.Add(para);
                }
            }
            cmd.CommandText = cmd.CommandText + " WHERE " + builder;
        }

        public ExcelSheet<T> GetSheet<T>()
        {
            return new ExcelSheet<T>(this);
        }

        public void SubmitChanges()
        {
            string connectionString = ExcelConnectionString.GetConnectionString(this.Filepath);

            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                oleDbConnection.Open();

                foreach (ObjectState objectState in this.ChangeSet.ChangedObjects)
                {
                    using (OleDbCommand oleDbCommand = oleDbConnection.CreateCommand())
                    {
                        if (objectState.ChangeState == ChangeState.Deleted)
                        {
                            BuildDeleteClause(oleDbCommand, objectState);
                        }
                        if (objectState.ChangeState == ChangeState.Updated)
                        {
                            BuildUpdateClause(oleDbCommand, objectState);
                        }
                        if (objectState.ChangeState == ChangeState.Inserted)
                        {
                            BuildInsertClause(oleDbCommand, objectState);
                        }
                        try
                        {
                            oleDbCommand.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }
            }
        }
    }
}