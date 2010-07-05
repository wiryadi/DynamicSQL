using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Dynamic;

namespace DynamicSQL
{
    public class ExpandoSql: IDynamicSQL
    {
        private string connectionString;

        public ExpandoSql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void EachRow(string queryString, Action<dynamic> act)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(queryString, connection);
                connection.Open();

                var sqlReader = sqlCommand.ExecuteReader();

                while(sqlReader.Read())
                {
                    dynamic dynaRow = sqlReader.ToExpandoRow();
                    act(dynaRow);
                }

                sqlReader.Close();
            }
        }

        public void FirstRow(string queryString, Action<dynamic> act)
        {
             using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(queryString, connection);
                connection.Open();

                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    if (sqlReader.Read())
                    {
                        dynamic dynaRow = sqlReader.ToExpandoRow();
                        act(dynaRow);
                    }
                }
            }
        }

    }


}
