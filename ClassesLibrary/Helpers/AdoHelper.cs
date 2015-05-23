using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NLog;

namespace Organizations.Helpers
{
    public static class AdoHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
                using (var adapter = new SqlDataAdapter(queryString, connection))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }

                    try
                    {
                        adapter.Fill(table);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
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
                    logger.Error("Error Opening Connection");
                    Console.WriteLine(ex.Message);
                }

                SqlTransaction transaction = connection.BeginTransaction("Transaction");
                using (var command = new SqlCommand(queryString, connection) { Transaction = transaction })
                {
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
                    logger.Trace("Transaction {0} successfully completed", queryString);
                }
            }
        }

    }
}


