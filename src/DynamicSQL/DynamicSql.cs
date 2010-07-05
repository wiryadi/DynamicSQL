using System;
using System.Data.SqlClient;

namespace DynamicSQL
{
    public class DynamicSql : IDynamicSQL
    {
        string connectionString;

        public DynamicSql (string connectionString)
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
                    DynamicRow dynamicRow = sqlReader.ToDynamicRow();
                    act(dynamicRow);
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
                        DynamicRow dynaRow = sqlReader.ToDynamicRow();
                        act(dynaRow);
                    }
                }
            }
        }

       
    }
}
