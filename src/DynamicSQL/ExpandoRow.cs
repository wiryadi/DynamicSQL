using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Data.SqlClient;

namespace DynamicSQL
{
    public static class ExpandoRow
    {
        public static dynamic ToExpandoRow(this SqlDataReader sqlReader)
        {
            var expandoRow = new ExpandoObject();

            object[] rowValues = GetRowValues(sqlReader);

            for (int i = 0; i < sqlReader.FieldCount; i++)
            {
                string columnName = sqlReader.GetName(i);
                expandoRow.AddDynamicProperty(columnName, rowValues[i]);
            }

            return expandoRow;
        }

        private static object[] GetRowValues(SqlDataReader sqlReader)
        {
            object[] rowValues = new object[sqlReader.FieldCount];
            sqlReader.GetValues(rowValues);
            return rowValues;
        }

        private static void AddDynamicProperty(this ExpandoObject target, string propertyName, object value)
        {
            IDictionary<string, object> expandoAsDictionary = target;
            expandoAsDictionary[propertyName] = value;
        }
    }
}
