using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Organizations.Helpers
{
    public static class AdoHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.
              ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public static DataTable GetDataTable(string queryString)
        {
            var table = new DataTable();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var adapter = new SqlDataAdapter(queryString, connection);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return table;
        }

        public static DataTableReader GetDataTableReader(DataTable table)
        {
            return table.CreateDataReader();
        }

        public static void ExecCommand(string queryString)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                SqlTransaction transaction = connection.BeginTransaction("Transaction");
                var command = new SqlCommand(queryString, connection) { Transaction = transaction };
                try
                {
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.Message);
                    }
                }
            }
        }

    }
}


